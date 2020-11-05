using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateAnsOP
{
    public static partial class CreateReplyRESULT
    {
        [AsChoise]
        public interface ICreateReplyResult { }
        public class ReplyCreated : ICreateReplyResult
        {
            public ReplyCreated(int replyID,int questionID,string body)
            {
                ReplyID = replyID;
                QuestionID = questionID;
                Body = body;
            }
            public int ReplyID { get; }
            public int QuestionID { get; }
            public string Body { get; }

        }
        public class ReplyNotCreated : ICreateReplyResult
        {
            public ReplyCreated(string message)
            {
                Message = message;
            }
            public string Message { get; }
        }

    }
}
