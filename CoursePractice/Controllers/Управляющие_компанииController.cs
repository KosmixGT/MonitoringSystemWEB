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
    public class Управляющие_компанииController : Controller
    {
        private MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();

        // GET: Управляющие_компании
        public ActionResult Index()
        {
            if (Session["UserId"] != null && User.IsInRole("Менеджер УК"))
            {
                int userId = (int)Session["UserId"];
                var управляющие_компании = db.Управляющие_компании.Include(у => у.ОПФ_ук).Include(у => у.Учётные_записи).Where(o => o.Учётные_записи.код_пользователя == userId);
                return View(управляющие_компании.ToList());
            }
            else
            {
                var управляющие_компании = db.Управляющие_компании.Include(у => у.ОПФ_ук).Include(у => у.Учётные_записи);
                return View(управляющие_компании.ToList());
            }
            
        }

        // GET: Управляющие_компании/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Управляющие_компании управляющие_компании = db.Управляющие_компании.Find(id);
            if (управляющие_компании == null)
            {
                return HttpNotFound();
            }
            return View(управляющие_компании);
        }

        // GET: Управляющие_компании/Create
        public ActionResult Create()
        {
            ViewBag.ОПФ = new SelectList(db.ОПФ_ук, "код_опф", "орг_прав_форма");
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин");
            return View();
        }

        // POST: Управляющие_компании/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "код_ук,название,фирм_наим,сокр_наим,ФИО_руков,дата_получения_лицензии,номер_лицензии,орган_выдавший_лицензию,ИНН,ОПФ,ОГРН,почтовый_адрес,адрес_ФМОУ,место_ГРЮЛ,адрес_дисп_службы,контактный_телефон,адрес_электронной_почты,учётная_запись,подтверждение")] Управляющие_компании управляющие_компании)
        {
            if (ModelState.IsValid)
            {
                db.Управляющие_компании.Add(управляющие_компании);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ОПФ = new SelectList(db.ОПФ_ук, "код_опф", "орг_прав_форма", управляющие_компании.ОПФ);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", управляющие_компании.учётная_запись);
            return View(управляющие_компании);
        }

        // GET: Управляющие_компании/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Управляющие_компании управляющие_компании = db.Управляющие_компании.Find(id);
            if (управляющие_компании == null)
            {
                return HttpNotFound();
            }
            ViewBag.ОПФ = new SelectList(db.ОПФ_ук, "код_опф", "орг_прав_форма", управляющие_компании.ОПФ);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", управляющие_компании.учётная_запись);
            return View(управляющие_компании);
        }

        // POST: Управляющие_компании/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "код_ук,название,фирм_наим,сокр_наим,ФИО_руков,дата_получения_лицензии,номер_лицензии,орган_выдавший_лицензию,ИНН,ОПФ,ОГРН,почтовый_адрес,адрес_ФМОУ,место_ГРЮЛ,адрес_дисп_службы,контактный_телефон,адрес_электронной_почты,учётная_запись,подтверждение")] Управляющие_компании управляющие_компании)
        {
            if (ModelState.IsValid)
            {
                db.Entry(управляющие_компании).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ОПФ = new SelectList(db.ОПФ_ук, "код_опф", "орг_прав_форма", управляющие_компании.ОПФ);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", управляющие_компании.учётная_запись);
            return View(управляющие_компании);
        }


        // GET: Управляющие_компании/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Управляющие_компании управляющие_компании = db.Управляющие_компании.Find(id);
            if (управляющие_компании == null)
            {
                return HttpNotFound();
            }
            return View(управляющие_компании);
        }

        // POST: Управляющие_компании/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Управляющие_компании управляющие_компании = db.Управляющие_компании.Find(id);
            db.Управляющие_компании.Remove(управляющие_компании);
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
