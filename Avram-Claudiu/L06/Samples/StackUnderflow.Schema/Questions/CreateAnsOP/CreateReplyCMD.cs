using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateAnsOP
{
    public class CreateReplyCMD
    {
        public CreateReplyCMD(int questionID,int authorUserID,string body)
        {
            QuestionID = questionID;
            AuthorUserID = authorUserID;
            Body = body;
        }
        [Required]
        public int QuestionID { get; }
        [Required]
        public int AuthorUserID { get; }
        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string Body { get; }
    }
}
