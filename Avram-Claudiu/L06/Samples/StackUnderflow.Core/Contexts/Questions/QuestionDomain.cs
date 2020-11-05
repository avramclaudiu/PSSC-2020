using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CreateAnsOP;
using static StackUnderflow.Domain.Schema.Questions.CreateAnsOP.CreateReplyRESULT;
using static PortExt;
using static StackUnderflow.Domain.Schema.Questions.LanguageOp.LanguageRESULT;
using StackUnderflow.Domain.Schema.Questions.LanguageOp;
using StackUnderflow.Domain.Schema.Questions.SendQuestion;
using static StackUnderflow.Domain.Schema.Questions.SendQuestion.SendQuestionRESULT;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionDomain
    {
        public static Port<ICreateReplyResult> CreateReply(int questionID, int authorUserID, string body) =>
            NewPort<CreateReplyCMD, ICreateReplyResult>(new CreateReplyCMD(questionID, authorUserID, body));
        public static Port<ILanguageResult> Language(string text) =>
          NewPort<LanguageCMD, ILanguageResult>(new LanguageCMD(text));

        public static Port<ISendQuestionResult> SendQuestion(int questionID, int questionOwnerID) =>
          NewPort<SendQuestionCMD, ISendQuestionResult>(new SendQuestionCMD(questionID,questionOwnerID));

    }
}
