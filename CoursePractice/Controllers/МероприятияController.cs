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
    public class МероприятияController : Controller
    {
        private MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();

        // GET: Мероприятия
        public ActionResult Index()
        {
            if (Session["UserId"] != null && User.IsInRole("Менеджер УК"))
            {
                int userId = (int)Session["UserId"];
                var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
                var мероприятия = db.Мероприятия.Include(м => м.Категория_мероприятия).Include(м => м.МКД).Include(м => м.Статус_мероприятия).Where(o => o.МКД.УК == ukID);
                return View(мероприятия.ToList());
            }
            else
            {
                var мероприятия = db.Мероприятия.Include(м => м.Категория_мероприятия).Include(м => м.МКД).Include(м => м.Статус_мероприятия);
                return View(мероприятия.ToList());
            }
        }

        // GET: Мероприятия/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Мероприятия мероприятия = db.Мероприятия.Find(id);
            if (мероприятия == null)
            {
                return HttpNotFound();
            }
            return View(мероприятия);
        }

        // GET: Мероприятия/Create
        public ActionResult Create()
        {
            ViewBag.кат_меропр = new SelectList(db.Категория_мероприятия, "код_категории_меропр", "категория_меропр");

            // Фильтрация объектов МКД по управляющей компании с кодом 5
            int userId = (int)Session["UserId"];
            var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
            var мкдList = db.МКД.Where(м => м.УК == ukID).ToList();
            ViewBag.объект_МКД = new SelectList(мкдList, "код_МКД", "адрес");

            //ViewBag.объект_МКД = new SelectList(db.МКД, "код_МКД", "адрес");

            ViewBag.статус_меропр = new SelectList(db.Статус_мероприятия, "код_тек_статуса_меропр", "статус_меропр");
            return View();
        }

        // POST: Мероприятия/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "код_мероприятия,кат_меропр,тема_меропр,объект_МКД,описание,дата_время_проведения,исполнитель,статус_меропр")] Мероприятия мероприятия)
        {
            if (ModelState.IsValid)
            {
                db.Мероприятия.Add(мероприятия);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.кат_меропр = new SelectList(db.Категория_мероприятия, "код_категории_меропр", "категория_меропр", мероприятия.кат_меропр);
            // Фильтрация объектов МКД по управляющей компании с кодом 5
            int userId = (int)Session["UserId"];
            var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
            var мкдList = db.МКД.Where(м => м.УК == ukID).ToList();
            ViewBag.объект_МКД = new SelectList(мкдList, "код_МКД", "адрес");
            ViewBag.статус_меропр = new SelectList(db.Статус_мероприятия, "код_тек_статуса_меропр", "статус_меропр", мероприятия.статус_меропр);
            return View(мероприятия);
        }

        // GET: Мероприятия/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Мероприятия мероприятия = db.Мероприятия.Find(id);
            if (мероприятия == null)
            {
                return HttpNotFound();
            }
            ViewBag.кат_меропр = new SelectList(db.Категория_мероприятия, "код_категории_меропр", "категория_меропр", мероприятия.кат_меропр);
            // Фильтрация объектов МКД по управляющей компании с кодом 5
            int userId = (int)Session["UserId"];
            var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
            var мкдList = db.МКД.Where(м => м.УК == ukID).ToList();
            ViewBag.объект_МКД = new SelectList(мкдList, "код_МКД", "адрес");
            ViewBag.статус_меропр = new SelectList(db.Статус_мероприятия, "код_тек_статуса_меропр", "статус_меропр", мероприятия.статус_меропр);
            return View(мероприятия);
        }

        // POST: Мероприятия/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "код_мероприятия,кат_меропр,тема_меропр,объект_МКД,описание,дата_время_проведения,исполнитель,статус_меропр")] Мероприятия мероприятия)
        {
            if (ModelState.IsValid)
            {
                db.Entry(мероприятия).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.кат_меропр = new SelectList(db.Категория_мероприятия, "код_категории_меропр", "категория_меропр", мероприятия.кат_меропр);
            // Фильтрация объектов МКД по управляющей компании с кодом 5
            int userId = (int)Session["UserId"];
            var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
            var мкдList = db.МКД.Where(м => м.УК == ukID).ToList();
            ViewBag.объект_МКД = new SelectList(мкдList, "код_МКД", "адрес");
            ViewBag.статус_меропр = new SelectList(db.Статус_мероприятия, "код_тек_статуса_меропр", "статус_меропр", мероприятия.статус_меропр);
            return View(мероприятия);
        }

        // GET: Мероприятия/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Мероприятия мероприятия = db.Мероприятия.Find(id);
            if (мероприятия == null)
            {
                return HttpNotFound();
            }
            return View(мероприятия);
        }

        // POST: Мероприятия/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Мероприятия мероприятия = db.Мероприятия.Find(id);
            db.Мероприятия.Remove(мероприятия);
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
