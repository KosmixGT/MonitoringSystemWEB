using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoursePractice;

namespace CoursePractice.Controllers
{
    public class Учётные_записиController : Controller
    {
        private MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();

        // GET: Учётные_записи
        public ActionResult Index()
        {
            var учётные_записи = db.Учётные_записи.Include(у => у.Тип_учётной_записи1);
            return View(учётные_записи.ToList());
        }

        // GET: Учётные_записи/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Учётные_записи учётные_записи = db.Учётные_записи.Find(id);
            if (учётные_записи == null)
            {
                return HttpNotFound();
            }
            return View(учётные_записи);
        }

        // GET: Учётные_записи/Create
        public ActionResult Create()
        {
            ViewBag.тип_учётной_записи = new SelectList(db.Тип_учётной_записи, "код_типа_пользователя", "тип_пользователя");
            return View();
        }

        // POST: Учётные_записи/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "код_пользователя,логин,пароль,тип_учётной_записи")] Учётные_записи учётные_записи)
        {
            if (ModelState.IsValid)
            {
                db.Учётные_записи.Add(учётные_записи);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.тип_учётной_записи = new SelectList(db.Тип_учётной_записи, "код_типа_пользователя", "тип_пользователя", учётные_записи.тип_учётной_записи);
            return View(учётные_записи);
        }

        // GET: Учётные_записи/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Учётные_записи учётные_записи = db.Учётные_записи.Find(id);
            if (учётные_записи == null)
            {
                return HttpNotFound();
            }
            ViewBag.тип_учётной_записи = new SelectList(db.Тип_учётной_записи, "код_типа_пользователя", "тип_пользователя", учётные_записи.тип_учётной_записи);
            return View(учётные_записи);
        }

        // POST: Учётные_записи/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "код_пользователя,логин,пароль,тип_учётной_записи")] Учётные_записи учётные_записи)
        {
            if (ModelState.IsValid)
            {
                db.Entry(учётные_записи).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.тип_учётной_записи = new SelectList(db.Тип_учётной_записи, "код_типа_пользователя", "тип_пользователя", учётные_записи.тип_учётной_записи);
            return View(учётные_записи);
        }

        // GET: Учётные_записи/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Учётные_записи учётные_записи = db.Учётные_записи.Find(id);
            if (учётные_записи == null)
            {
                return HttpNotFound();
            }
            return View(учётные_записи);
        }

        // POST: Учётные_записи/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Учётные_записи учётные_записи = db.Учётные_записи.Find(id);
            db.Учётные_записи.Remove(учётные_записи);
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
