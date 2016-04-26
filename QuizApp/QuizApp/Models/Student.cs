
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuizApp.Models
{

    public class Student
    {
        public int StudentID { get; set; }        
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<UserAnswer> UserAnswer { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

    }

}