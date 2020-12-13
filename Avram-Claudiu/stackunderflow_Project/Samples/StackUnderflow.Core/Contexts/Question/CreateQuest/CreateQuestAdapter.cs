using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuest;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.Domain.Schema.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuest.CreateQuestResult;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public partial class CreateQuestAdapter : Adapter<CreateQuestCmd, ICreateQuestResult, QuestWrite, QuestDependencies>
    {
        private readonly IExecutionContext _ex;

        public CreateQuestAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<ICreateQuestResult> Work(CreateQuestCmd command, QuestWrite state, QuestDependencies dependencies)
        {
            var workflow = from valid in command.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestFromCommand(command))
                           select t;
            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidQuest(ex.ToString()));
            return result;
        }

        public ICreateQuestResult AddQuestionIfMissing(QuestWrite state, QuestBody question)
        {
            if (state.Questions.Any(p => p.Title.Equals(question.Title)))
                return new QuestNotCreated();
            if (state.Questions.All(p => p.QuestionId != question.QuestionId))
                state.Questions.Add(question);
            return new QuestCreated(question);
        }

        private QuestBody CreateQuestFromCommand(CreateQuestCmd cmd)
        {
            var question = new QuestBody()
            {
                Title = cmd.Title,
                Body = cmd.Body,
                Tags = cmd.Tags
            };
            return question;
        }

        public override Task PostConditions(CreateQuestCmd op, CreateQuestResult.ICreateQuestResult result, QuestWrite state)
        {
            return Task.CompletedTask;
        }

    }
}
