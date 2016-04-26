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
    public class TypeQuestionsController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: TypeQuestions
        public ActionResult Index()
        {
            return View(db.TypeQuestions.ToList());
        }

        // GET: TypeQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestions.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // GET: TypeQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] TypeQuestion typeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.TypeQuestions.Add(typeQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeQuestion);
        }

        // GET: TypeQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestions.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // POST: TypeQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] TypeQuestion typeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeQuestion);
        }

        // GET: TypeQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestions.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // POST: TypeQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeQuestion typeQuestion = db.TypeQuestions.Find(id);
            db.TypeQuestions.Remove(typeQuestion);
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
