using StackUnderflow.EF.Models;
using StackUnderflow.DatabaseModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestRead
    {
        public QuestRead(IEnumerable<QuestBody> questions, IEnumerable<User> users)
        {
            Questions = questions;
            Users = users;
        }

        public IEnumerable<QuestBody> Questions { get; }

        public IEnumerable<User> Users { get; }
    }
}
