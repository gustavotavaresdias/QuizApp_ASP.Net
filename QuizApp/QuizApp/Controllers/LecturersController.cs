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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;

namespace QuizApp.Controllers
{
    public class LecturersController : Controller
    {
        private QuizContext db = new QuizContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Lecturers
        public ActionResult Index()
        {
            return View(db.Lecturers.ToList());
        }

        // GET: Lecturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // GET: Lecturers/Create
        public ActionResult Create()
        {
            var lecturer = new Lecturer();

            lecturer.Units = new List<Unit>();
            PopulateAssignedUnitData(lecturer);
            return View();
        }

        // POST: Lecturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LecturerNumber,FirstName,LastName,Email,Password,IsAdm")] Lecturer lecturer, string[] selectedUnits)
        {
            var user = new ApplicationUser() { UserName = lecturer.LecturerNumber.ToString(), Email = lecturer.Email };
            IdentityResult result = await UserManager.CreateAsync(user, lecturer.Password);

            ApplicationDbContext adc = new ApplicationDbContext();
            string q = "SELECT ID FROM AspNetUsers WHERE UserName = '" + user.UserName + "'";
            DbRawSqlQuery<string> id = adc.Database.SqlQuery<string>(q);

          


            lecturer.ID = id.ToList<string>()[0];
            if (selectedUnits != null)
            {
                lecturer.Units = new List<Unit>();
                foreach (var unit in selectedUnits)
                {
                    var unitToAdd = db.Units.Find(int.Parse(unit));
                    lecturer.Units.Add(unitToAdd);
                }
            }
            
            if (ModelState.IsValid)
            {
                db.Lecturers.Add(lecturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedUnitData(lecturer);
            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers
                .Include(i => i.Units)
                .Where(i => i.ID == id)
                .Single();
            
            PopulateAssignedUnitData(lecturer);
            
            //Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string[] selectedUnits)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lecturerToUpdate = db.Lecturers
               .Include(i => i.Units)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(lecturerToUpdate, "",
               new string[] { "LecturerNumber" , "FirstName" , "LastName" , "Email" , "Password" , "IsAdm" }))
            {
                try
                {
                    UpdateLecturerUnits(selectedUnits, lecturerToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedUnitData(lecturerToUpdate);
            return View(lecturerToUpdate);
        }

        private void UpdateLecturerUnits(string[] selectedUnits, Lecturer lecturerToUpdate)
        {
            if (selectedUnits == null)
            {
                lecturerToUpdate.Units = new List<Unit>();
                return;
            }

            var selectedUnitsHS = new HashSet<string>(selectedUnits);
            var instructorCourses = new HashSet<int>
                (lecturerToUpdate.Units.Select(c => c.ID));
            foreach (var unit in db.Units)
            {
                if (selectedUnitsHS.Contains(unit.ID.ToString()))
                {
                    if (!instructorCourses.Contains(unit.ID))
                    {
                        lecturerToUpdate.Units.Add(unit);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(unit.ID))
                    {
                        lecturerToUpdate.Units.Remove(unit);
                    }
                }
            }
        }
        //public ActionResult Edit([Bind(Include = "ID,LecturerNumber,FirstName,LastName,Email,Password,IsAdm")] Lecturer lecturer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lecturer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(lecturer);
        //}

        // GET: Lecturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lecturer lecturer = db.Lecturers.Find(id);
            db.Lecturers.Remove(lecturer);
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

        private void PopulateAssignedUnitData(Lecturer lecturer)
        {
            var allUnits = db.Units;
            var lecturerUnits = new HashSet<int>(lecturer.Units.Select(c => c.ID));
            var viewModel = new List<AssignedUnitData>();
            foreach (var unit in allUnits)
            {
                viewModel.Add(new AssignedUnitData
                {
                    UnitID = unit.ID,
                    Title = unit.Name,
                    Assigned = lecturerUnits.Contains(unit.ID)
                });
            }
            ViewBag.Units = viewModel;
        }

    }
}
