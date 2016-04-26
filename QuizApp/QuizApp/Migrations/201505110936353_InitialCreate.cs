namespace QuizApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        StudentNumber = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.UserAnswer",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.StudentID, t.QuestionID })
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CorrectAnswer = c.String(),
                        TypeQuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.TypeQuestion", t => t.TypeQuestionID, cascadeDelete: true)
                .Index(t => t.TypeQuestionID);
            
            CreateTable(
                "dbo.OptionAnswer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Option = c.String(),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Questionnaire",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EndDate = c.DateTime(nullable: false),
                        UnitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Unit", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LecturerNumber = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsAdm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TypeQuestion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UnitLecturer",
                c => new
                    {
                        UnitID = c.Int(nullable: false),
                        LecturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UnitID, t.LecturerID })
                .ForeignKey("dbo.Unit", t => t.UnitID, cascadeDelete: true)
                .ForeignKey("dbo.Lecturer", t => t.LecturerID, cascadeDelete: true)
                .Index(t => t.UnitID)
                .Index(t => t.LecturerID);
            
            CreateTable(
                "dbo.QuestionnaireQuestion",
                c => new
                    {
                        QuestionID = c.Int(nullable: false),
                        QuestionnaireID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionID, t.QuestionnaireID })
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Questionnaire", t => t.QuestionnaireID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.QuestionnaireID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswer", "StudentID", "dbo.Student");
            DropForeignKey("dbo.UserAnswer", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Question", "TypeQuestionID", "dbo.TypeQuestion");
            DropForeignKey("dbo.QuestionnaireQuestion", "QuestionnaireID", "dbo.Questionnaire");
            DropForeignKey("dbo.QuestionnaireQuestion", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Questionnaire", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.UnitLecturer", "LecturerID", "dbo.Lecturer");
            DropForeignKey("dbo.UnitLecturer", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.Unit", "CourseID", "dbo.Course");
            DropForeignKey("dbo.OptionAnswer", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Student", "CourseID", "dbo.Course");
            DropIndex("dbo.QuestionnaireQuestion", new[] { "QuestionnaireID" });
            DropIndex("dbo.QuestionnaireQuestion", new[] { "QuestionID" });
            DropIndex("dbo.UnitLecturer", new[] { "LecturerID" });
            DropIndex("dbo.UnitLecturer", new[] { "UnitID" });
            DropIndex("dbo.Unit", new[] { "CourseID" });
            DropIndex("dbo.Questionnaire", new[] { "UnitID" });
            DropIndex("dbo.OptionAnswer", new[] { "QuestionID" });
            DropIndex("dbo.Question", new[] { "TypeQuestionID" });
            DropIndex("dbo.UserAnswer", new[] { "QuestionID" });
            DropIndex("dbo.UserAnswer", new[] { "StudentID" });
            DropIndex("dbo.Student", new[] { "CourseID" });
            DropTable("dbo.QuestionnaireQuestion");
            DropTable("dbo.UnitLecturer");
            DropTable("dbo.TypeQuestion");
            DropTable("dbo.Lecturer");
            DropTable("dbo.Unit");
            DropTable("dbo.Questionnaire");
            DropTable("dbo.OptionAnswer");
            DropTable("dbo.Question");
            DropTable("dbo.UserAnswer");
            DropTable("dbo.Student");
            DropTable("dbo.Course");
        }
    }
}
