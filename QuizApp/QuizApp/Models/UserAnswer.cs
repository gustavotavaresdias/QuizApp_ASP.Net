using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class UserAnswer
    {
        [Key, ForeignKey("Student"), Column(Order = 0)]
        public int StudentID { get; set; }
        [Key, ForeignKey("Question"), Column(Order = 1)]
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public virtual Student Student { get; set; }
        public virtual Question Question { get; set; }

    }
}