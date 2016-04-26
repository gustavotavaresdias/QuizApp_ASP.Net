
using System.Collections.Generic;
namespace QuizApp.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string CorrectAnswer { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
        public int TypeQuestionID { get; set; }
        public virtual TypeQuestion TypeQuestion { get; set; }
        public virtual ICollection<OptionAnswer> OptionsAnswer { get; set; }
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }

    }
}