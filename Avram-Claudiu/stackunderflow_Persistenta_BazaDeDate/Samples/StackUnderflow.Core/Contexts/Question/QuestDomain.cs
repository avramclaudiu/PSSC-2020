using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuest;
using StackUnderflow.Domain.Core.Contexts.Question.SendConfirm;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuest.CreateQuestResult;
using static StackUnderflow.Domain.Core.Contexts.Question.SendConfirm.ConfirmQuestResult;
namespace StackUnderflow.Domain.Core.Contexts.Question
{
   public static class QuestDomain
    {
        public static Port<ICreateQuestResult> CreateQuestion(CreateQuestCmd command) => NewPort<CreateQuestCmd, ICreateQuestResult>(command);
        public static Port<IConfirmQuestResult> ConfirmQuestion(ConfirmQuestCmd command) => NewPort<ConfirmQuestCmd, IConfirmQuestResult>(command);
    }
}
