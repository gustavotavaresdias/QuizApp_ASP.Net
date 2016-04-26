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
    public class OptionAnswersController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: OptionAnswers
        public ActionResult Index()
        {
            var optionAnswers = db.OptionAnswers.Include(o => o.Question);
            return View(optionAnswers.ToList());
        }

        // GET: OptionAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionAnswer optionAnswer = db.OptionAnswers.Find(id);
            if (optionAnswer == null)
            {
                return HttpNotFound();
            }
            return View(optionAnswer);
        }

        // GET: OptionAnswers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title");
            return View();
        }

        // POST: OptionAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Option,QuestionID")] OptionAnswer optionAnswer)
        {
            if (ModelState.IsValid)
            {
                db.OptionAnswers.Add(optionAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", optionAnswer.QuestionID);
            return View(optionAnswer);
        }

        // GET: OptionAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionAnswer optionAnswer = db.OptionAnswers.Find(id);
            if (optionAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", optionAnswer.QuestionID);
            return View(optionAnswer);
        }

        // POST: OptionAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Option,QuestionID")] OptionAnswer optionAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(optionAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", optionAnswer.QuestionID);
            return View(optionAnswer);
        }

        // GET: OptionAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionAnswer optionAnswer = db.OptionAnswers.Find(id);
            if (optionAnswer == null)
            {
                return HttpNotFound();
            }
            return View(optionAnswer);
        }

        // POST: OptionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OptionAnswer optionAnswer = db.OptionAnswers.Find(id);
            db.OptionAnswers.Remove(optionAnswer);
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
