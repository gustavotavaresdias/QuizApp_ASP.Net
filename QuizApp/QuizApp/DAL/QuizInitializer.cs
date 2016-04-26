using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizApp.Models;

namespace QuizApp.DAL
{
    public class QuizInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<QuizContext>
    {
        protected override void Seed(QuizContext context)
        {
            //Initialize Course table
            var listCourse = new List<Course>{
                new Course{Name = "Course Test",}
            };
            listCourse.ForEach(obj => context.Courses.Add(obj));
            context.SaveChanges();
            
            //Initialize Lecturer table
            var listLecturer = new List<Lecturer>{
                new Lecturer{FirstName = "Administrator",LastName = "Master",Email = "admin@admin.com",Password = "123",IsAdm = true,LecturerNumber=0909090909,},
                new Lecturer{FirstName = "Teste",LastName = "Lecturer",Email = "admin@admin.com",Password = "123",IsAdm = false,LecturerNumber=123456789,}
            };
            listLecturer.ForEach(obj => context.Lecturers.Add(obj));
            context.SaveChanges();

            //Initialize Student table
            var listStudent = new List<Student>{
                new Student{FirstName = "Student",LastName = "Test",Email = "admin@admin.com",Password = "123",StudentNumber=0101010101,
                    CourseID = listCourse.Single(i => i.Name == "Course Test").ID}
            };
            listStudent.ForEach(obj => context.Students.Add(obj));
            context.SaveChanges();

            //Initialize Unit table
            var listUnit = new List<Unit>{
                new Unit{Name = "Unit Test", CourseID = listCourse.Single(i => i.Name == "Course Test").ID}
            };
            listUnit.ForEach(obj => context.Units.Add(obj));
            context.SaveChanges();

            //Initialize Type Question table
            var listTypeQuestion = new List<TypeQuestion>{
                new TypeQuestion{Type = "True/False"},
                new TypeQuestion{Type = "Text"},
                new TypeQuestion{Type = "Multiple Choice - Many correct answer"},
                new TypeQuestion{Type = "Multiple Choice - Single correct answer"},

            };
            listTypeQuestion.ForEach(obj => context.TypeQuestions.Add(obj));
            context.SaveChanges();

        }
    }
}