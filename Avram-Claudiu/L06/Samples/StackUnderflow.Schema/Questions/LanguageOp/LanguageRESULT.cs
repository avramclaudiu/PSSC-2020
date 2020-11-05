using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.LanguageOp
{
    [AsChoice]
    public static partial class LanguageRESULT
    {
        public interface ILanguageResult { }
        public class Validation : ILanguageResult
        {
            public Validation(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public class NotValidation : ILanguageResult
        {
            public  NotValidation(string message)
            {
                Message=message;
            }
            public string Message { get; }
        }
    }
}
