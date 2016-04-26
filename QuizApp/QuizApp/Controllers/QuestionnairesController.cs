using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizApp.DAL;
using QuizApp.Models;
using QuizApp.ViewModels;
using System.Data.Entity.Infrastructure;

namespace QuizApp.Controllers
{
    public class QuestionnairesController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: Questionnaires
        public ActionResult Index()
        {
            var questionnaires = db.Questionnaires.Include(q => q.Unit);
            return View(questionnaires.ToList());
        }

        // GET: Questionnaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        // GET: Questionnaires/Create
        public ActionResult Create()
        {
            var questionnaire = new Questionnaire();
            questionnaire.Questions = new List<Question>();
            PopulateAssignedQuestionData(questionnaire);

            ViewBag.UnitID = new SelectList(db.Units, "ID", "Name");
            return View();
        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,EndDate,UnitID")] Questionnaire questionnaire, string[] selectedQuestions)
        {
            if (selectedQuestions != null)
            {
                questionnaire.Questions = new List<Question>();
                foreach (var question in selectedQuestions)
                {
                    var questionToAdd = db.Questions.Find(int.Parse(question));
                    questionnaire.Questions.Add(questionToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitID = new SelectList(db.Units, "ID", "Name", questionnaire.UnitID);
            PopulateAssignedQuestionData(questionnaire);
            return View(questionnaire);
        }

        // GET: Questionnaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();

            PopulateAssignedQuestionData(questionnaire);
            
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            ViewBag.UnitID = new SelectList(db.Units, "ID", "Name", questionnaire.UnitID);
            return View(questionnaire);
        }

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Edit(int? id, string[] selectedQuestions)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionnarieToUpdate = db.Questionnaires
               .Include(i => i.Questions)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(questionnarieToUpdate, "",
               new string[] { "ID", "Name", "EndDate", "UnitID" }))
            {
                try
                {
                    UpdateQuestionnaireQuestion(selectedQuestions, questionnarieToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            
            ViewBag.UnitID = new SelectList(db.Units, "ID", "Name", questionnarieToUpdate.UnitID);
            PopulateAssignedQuestionData(questionnarieToUpdate);
            return View(questionnarieToUpdate);
        }
        /*
        public ActionResult Edit([Bind(Include = "ID,Name,EndDate,UnitID")] Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitID = new SelectList(db.Units, "ID", "Name", questionnaire.UnitID);
            return View(questionnaire);
        }
        */

        // GET: Questionnaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            db.Questionnaires.Remove(questionnaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedQuestionData(Questionnaire questionnaire)
        {
            var allQuestions = db.Questions;
            var instructorCourses = new HashSet<int>(questionnaire.Questions.Select(c => c.QuestionID));
            var viewModel = new List<AssignedQuestionData>();
            foreach (var question in allQuestions)
            {
                viewModel.Add(new AssignedQuestionData
                {
                    QuestionID = question.QuestionID,
                    Title = question.Title,
                    Assigned = instructorCourses.Contains(question.QuestionID)
                });
            }
            ViewBag.Questions = viewModel;
        }
        
        private void UpdateQuestionnaireQuestion(string[] selectedQuestions, Questionnaire questionnarieToUpdate)
        {
            if (selectedQuestions == null)
            {
                questionnarieToUpdate.Questions = new List<Question>();
                return;
            }

            var selectedQuestionsHS = new HashSet<string>(selectedQuestions);
            var questionnaireQuestions = new HashSet<int>
                (questionnarieToUpdate.Questions.Select(c => c.QuestionID));
            
            foreach (var question in db.Questions)
            {
                if (selectedQuestionsHS.Contains(question.QuestionID.ToString()))
                {
                    if (!questionnaireQuestions.Contains(question.QuestionID))
                    {
                        questionnarieToUpdate.Questions.Add(question);
                    }
                }
                else
                {
                    if (questionnaireQuestions.Contains(question.QuestionID))
                    {
                        questionnarieToUpdate.Questions.Remove(question);
                    }
                }
            }
        }
    }
}
