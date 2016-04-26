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

namespace QuizApp.Controllers
{
    public class UserAnswersController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: UserAnswers
        public ActionResult Index()
        {
            var userAnswers = db.UserAnswers.Include(u => u.Question).Include(u => u.Student);
            return View(userAnswers.ToList());
        }

        // GET: UserAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            return View(userAnswer);
        }

        // GET: UserAnswers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: UserAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,QuestionID,Answer")] UserAnswer userAnswer)
        {
            if (ModelState.IsValid)
            {
                db.UserAnswers.Add(userAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", userAnswer.QuestionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", userAnswer.StudentID);
            return View(userAnswer);
        }

        // GET: UserAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", userAnswer.QuestionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", userAnswer.StudentID);
            return View(userAnswer);
        }

        // POST: UserAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,QuestionID,Answer")] UserAnswer userAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", userAnswer.QuestionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", userAnswer.StudentID);
            return View(userAnswer);
        }

        // GET: UserAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            return View(userAnswer);
        }

        // POST: UserAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            db.UserAnswers.Remove(userAnswer);
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
    }
}
