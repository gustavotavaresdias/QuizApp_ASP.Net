using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class TypeQuestion
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}