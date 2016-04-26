using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}