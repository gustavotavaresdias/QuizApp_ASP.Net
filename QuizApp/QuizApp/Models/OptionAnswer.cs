using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class OptionAnswer
    {
        public int ID { get; set; }
        public string Option { get; set; }
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }
}