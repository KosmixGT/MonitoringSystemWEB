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
    public class МКДController : Controller
    {
        private MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();

        // GET: МКД
        public ActionResult Index()
        {
            if (Session["UserId"] != null && User.IsInRole("Менеджер УК"))
            {
                int userId = (int)Session["UserId"];
                var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
                var мКД = db.МКД.Include(м => м.Состояние_дома).Include(м => м.Способ_управления_МКД).Include(м => м.Управляющие_компании).Where(м => м.УК == ukID);
                return View(мКД.ToList());
            }
            else
            {
                var мКД = db.МКД.Include(м => м.Состояние_дома).Include(м => м.Способ_управления_МКД).Include(м => м.Управляющие_компании);
                return View(мКД.ToList());
            }
            
        }

        // GET: МКД/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МКД мКД = db.МКД.Find(id);
            if (мКД == null)
            {
                return HttpNotFound();
            }
            return View(мКД);
        }

        // GET: МКД/Create
        public ActionResult Create()
        {
            ViewBag.состояние_МКД = new SelectList(db.Состояние_дома, "код_состояния_объекта", "состояние_объекта_МКД");
            ViewBag.тип_управления = new SelectList(db.Способ_управления_МКД, "код_типа_управления", "способ_управления");
            ViewBag.УК = new SelectList(db.Управляющие_компании, "код_ук", "название");
            return View();
        }

        // POST: МКД/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "код_МКД,адрес,кол_жил_пом,состояние_МКД,детская_площадка,спортивная_площадка,тип_управления,ОКТМО,УК,кадастровый_номер,год_постройки,год_ВДВУ,тип_дома,кол_этажей,кол_подъездов,кол_лифтов,площадь_жил_пом,площадь_нежил_пом,площадь_ПВСОИ,площадь_зем_участка,фундамент,тип_перекрытий,мат_несущих_стен,площадь_подвала,тип_мусоропровода,вентиляция,водоотведение,система_водостоков,газоснабжение,горячее_водоснаб,холодное_водоснаб,теплоснабжение,электроснабжение,мат_отделки_фасада,форма_крыши,утепляющие_слои_чердачных_перекрытий,мат_окон,мат_сети_отопления,мат_теплоизоляции_сети")] МКД мКД)
        {
            if (ModelState.IsValid)
            {
                db.МКД.Add(мКД);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.состояние_МКД = new SelectList(db.Состояние_дома, "код_состояния_объекта", "состояние_объекта_МКД", мКД.состояние_МКД);
            ViewBag.тип_управления = new SelectList(db.Способ_управления_МКД, "код_типа_управления", "способ_управления", мКД.тип_управления);
            ViewBag.УК = new SelectList(db.Управляющие_компании, "код_ук", "название", мКД.УК);
            return View(мКД);
        }

        // GET: МКД/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МКД мКД = db.МКД.Find(id);
            if (мКД == null)
            {
                return HttpNotFound();
            }
            ViewBag.состояние_МКД = new SelectList(db.Состояние_дома, "код_состояния_объекта", "состояние_объекта_МКД", мКД.состояние_МКД);
            ViewBag.тип_управления = new SelectList(db.Способ_управления_МКД, "код_типа_управления", "способ_управления", мКД.тип_управления);
            ViewBag.УК = new SelectList(db.Управляющие_компании, "код_ук", "название", мКД.УК);
            return View(мКД);
        }

        // POST: МКД/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "код_МКД,адрес,кол_жил_пом,состояние_МКД,детская_площадка,спортивная_площадка,тип_управления,ОКТМО,УК,кадастровый_номер,год_постройки,год_ВДВУ,тип_дома,кол_этажей,кол_подъездов,кол_лифтов,площадь_жил_пом,площадь_нежил_пом,площадь_ПВСОИ,площадь_зем_участка,фундамент,тип_перекрытий,мат_несущих_стен,площадь_подвала,тип_мусоропровода,вентиляция,водоотведение,система_водостоков,газоснабжение,горячее_водоснаб,холодное_водоснаб,теплоснабжение,электроснабжение,мат_отделки_фасада,форма_крыши,утепляющие_слои_чердачных_перекрытий,мат_окон,мат_сети_отопления,мат_теплоизоляции_сети")] МКД мКД)
        {
            if (ModelState.IsValid)
            {
                db.Entry(мКД).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.состояние_МКД = new SelectList(db.Состояние_дома, "код_состояния_объекта", "состояние_объекта_МКД", мКД.состояние_МКД);
            ViewBag.тип_управления = new SelectList(db.Способ_управления_МКД, "код_типа_управления", "способ_управления", мКД.тип_управления);
            ViewBag.УК = new SelectList(db.Управляющие_компании, "код_ук", "название", мКД.УК);
            return View(мКД);
        }

        // GET: МКД/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МКД мКД = db.МКД.Find(id);
            if (мКД == null)
            {
                return HttpNotFound();
            }
            return View(мКД);
        }

        // POST: МКД/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            МКД мКД = db.МКД.Find(id);
            db.МКД.Remove(мКД);
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
