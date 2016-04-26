using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Unit
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; set; }
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}