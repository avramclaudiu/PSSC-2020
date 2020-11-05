using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.SendQuestion
{
    public class SendQuestionCMD
    {
        public SendQuestionCMD(int questionID,int questionOwnerID)
        {
            QuestionID = questionID;
            QuestionOwnerID = questionOwnerID;
        }
        public int QuestionOwnerID { get; }
        public int QuestionID { get; }
    }
}
