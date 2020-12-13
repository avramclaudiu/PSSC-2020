using Access.Primitives.EFCore;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Access.Primitives.Extensions.ObjectExtensions;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuest;
using StackUnderflow.Domain.Core.Contexts.Question.SendConfirm;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using Orleans;
using Microsoft.AspNetCore.Http;
using GrainInterfaces;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("question")]
    public class QuestController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;

        }


        [HttpPost("post")]
        public async Task<IActionResult> CreateAndConfirmationQuestion([FromBody] CreateQuestCmd createQuestCmd)
        {
            QuestWrite ctx = new QuestWrite(
               new EFList<Post>(_dbContext.Post),
               new EFList<User>(_dbContext.User));

            var dependencies = new QuestDependencies();
            dependencies.GenerateConfirmationToken = () => Guid.NewGuid().ToString();
            dependencies.SendConfirmationEmail = SendEmail;

            var expr = from createQuestResult in QuestDomain.CreateQuestion(createQuestCmd)
                       let user = createQuestResult.SafeCast<CreateQuestResult.QuestCreated>().Select(p => p.Author)
                       let confirmQuestCmd = new ConfirmQuestCmd(user)
                       from ConfirmQuestResult in QuestDomain.ConfirmQuestion(confirmQuestCmd)
                       select new { createQuestResult, ConfirmQuestResult };
            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.createQuestResult.Match(
                created => (IActionResult)Ok(created.Question.PostId),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));

        }
        private TryAsync<ConfirmAcknowledgement> SendEmail(ConfirmLetter letter)
       => async () =>
       {
           var emialSender = _client.GetGrain<IEmailSender>(0);
           await emialSender.SendEmailAsync(letter.Letter);
           return new ConfirmAcknowledgement(Guid.NewGuid().ToString());
       };
    }
}