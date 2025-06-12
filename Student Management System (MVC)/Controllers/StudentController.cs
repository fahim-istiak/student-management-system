using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_Management_System__MVC_.Data;
using Student_Management_System__MVC_.Models;
using System.IO;

namespace Student_Management_System__MVC_.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBContext db = new StudentDBContext();

        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Age")] Student student, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    string path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                    imageFile.SaveAs(path);
                    student.ImagePath = "~/Images/" + uniqueFileName;
                }

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,ImagePath")] Student student, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(student.ImagePath))
                    {
                        string oldPath = Server.MapPath(student.ImagePath);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    string fileName = Path.GetFileName(imageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    string path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                    imageFile.SaveAs(path);
                    student.ImagePath = "~/Images/" + uniqueFileName;
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
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