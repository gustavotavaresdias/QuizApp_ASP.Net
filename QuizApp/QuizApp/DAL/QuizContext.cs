using MySql.Data.Entity;
using QuizApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QuizApp.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class QuizContext : DbContext
    {
        public QuizContext() : base("QuizContext")
        {
        }        

        public DbSet<Student> Students { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }
        
        public DbSet<Unit> Units { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<OptionAnswer> OptionAnswers { get; set; }

        public DbSet<TypeQuestion> TypeQuestions { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<Unit>()
             .HasMany(c => c.Lecturers).WithMany(i => i.Units)
             .Map(t => t.MapLeftKey("UnitID")
                 .MapRightKey("LecturerID")
                 .ToTable("UnitLecturer"));

            modelBuilder.Entity<Question>()
            .HasMany(c => c.Questionnaires).WithMany(i => i.Questions)
            .Map(t => t.MapLeftKey("QuestionID")
                .MapRightKey("QuestionnaireID")
                .ToTable("QuestionnaireQuestion"));

            

        }
    }
}