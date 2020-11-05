using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.SendQuestion
{
    [AsChoice]
    public static partial class SendQuestionRESULT
    {
        public interface ISendQuestionResult {}
        public class AcknowledgementSend : ISendQuestionResult 
        {
            public AcknowledgementSend(int questionID,int questionOwnerID)
            {
                QuestionID = questionID;
                QuestionOwnerID = questionOwnerID;
            }
            public int QuestionOwnerID { get; }
            public int QuestionID { get; }
        }
        public class AcknowledgementNotSend : ISendQuestionResult
        {
            public AcknowledgementNotSend(string message)
            {
                Message = message;
            }
            public string Message { get; }
        }
    }
}
