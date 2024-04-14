using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using CoursePractice;

namespace CoursePractice.Controllers
{
    public class ОбращенияController : Controller
    {
        private MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();

        // GET: Обращения
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserId"] != null && User.IsInRole("Житель"))
            {
                int userId = (int)Session["UserId"];
                var обращения = db.Обращения.Include(о => о.МКД).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи).Where(y => y.Учётные_записи.код_пользователя == userId);
                //обращения = обращения.OrderBy(о => о.Статус_обращения1.статус_обр);
                return View(обращения.ToList());
            }
            else if (Session["UserId"] != null && User.IsInRole("Менеджер УК"))
            {
                int userId = (int)Session["UserId"];
                var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
                var обращения = db.Обращения.Include(о => о.МКД).Where(y => y.МКД.УК == ukID).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи);
                //обращения = обращения.OrderBy(о => о.Статус_обращения1.статус_обр);
                return View(обращения.ToList());
            }
            else
            {
                var обращения = db.Обращения.Include(о => о.МКД).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи);
                //обращения = обращения.OrderBy(о => о.Статус_обращения1.статус_обр);
                return View(обращения.ToList());
            }

            
        }

        [HttpGet]
        public ActionResult IndexWithSortOrder(string sortOrder)
        {

            ViewBag.SortOrder = sortOrder;
            IQueryable<Обращения> обращения;

            if (Session["UserId"] != null && User.IsInRole("Менеджер УК"))
            {
                int userId = (int)Session["UserId"];
                var ukID = db.Управляющие_компании.FirstOrDefault(u => u.учётная_запись == userId).код_ук;
                обращения = db.Обращения.Include(о => о.МКД).Where(y => y.МКД.УК == ukID).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи);
            }
            else if (Session["UserId"] != null && User.IsInRole("Житель"))
            {
                int userId = (int)Session["UserId"];
                обращения = db.Обращения.Include(о => о.МКД).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи).Where(y => y.Учётные_записи.код_пользователя == userId);
            }
            else
            {
                обращения = db.Обращения.Include(о => о.МКД).Include(о => о.Сезон_обращения).Include(о => о.Статус_обращения1).Include(о => о.Тема_обращения).Include(о => о.Учётные_записи);
            }

            // Примените сортировку по дате в соответствии с выбранным sortOrder
            if (sortOrder == "desc")
            {
                обращения = обращения.OrderByDescending(m => m.дата_обращения);
            }
            else
            {
                обращения = обращения.OrderBy(m => m.дата_обращения);
            }

            return View("Index", обращения.ToList());
        }



        // GET: Обращения/Mark/5
        public ActionResult Mark(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обращения обращения = db.Обращения.Find(id);
            if (обращения == null)
            {
                return HttpNotFound();
            }
            return View(обращения);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkPost(FormCollection form)
        {
            int id;
            if (int.TryParse(form["код_обращения"], out id))
            {
                Обращения обращение = db.Обращения.Find(id);
                if (обращение != null)
                {
                    обращение.оценка = form["оценка"];

                    if (ModelState.IsValid)
                    {
                        // Обновление оценки в базе данных
                        db.Entry(обращение).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index"); // Перенаправление на другую страницу после успешного обновления
                    }
                }
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обращения обращения = db.Обращения.Find(id);
            if (обращения == null)
            {
                return HttpNotFound();
            }
            return View(обращения);
        }

        // GET: Обращения/Create
        [Authorize(Roles = "Житель")]
        public ActionResult Create()
        {
            ViewBag.объект_МКД = new SelectList(db.МКД, "код_МКД", "адрес");
            ViewBag.сезонность = new SelectList(db.Сезон_обращения, "код_сезонности_обр", "сезонность_обр");
            ViewBag.статус_обращения = new SelectList(db.Статус_обращения, "код_статуса_обращения", "статус_обр");
            ViewBag.тема = new SelectList(db.Тема_обращения, "код_темы_обращения", "тема_проблемы");
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин");
            return View();
        }

        // POST: Обращения/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "код_обращения,тема,сезонность,срок_подг_ответа,срок_подтв_ответа,описание,статус_обращения,дата_обращения,объект_МКД,обслужив_лицо,оценка,учётная_запись")] Обращения обращения, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                обращения.дата_обращения = DateTime.Now;
                if (User.IsInRole("Житель"))
                {
                    // код для присваивания полю учётная_запись новой записи значение 1
                    обращения.учётная_запись = (int)Session["UserId"];
                    обращения.статус_обращения = 5;
                    обращения.дата_обращения = DateTime.Now;
                }
                обращения.дата_обращения = DateTime.Now;

                // Save the files if they were uploaded
                if (files != null && files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            byte[] imageData = null;
                            using (var binaryReader = new BinaryReader(file.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(file.ContentLength);
                            }

                            var фото = new Фото
                            {
                                номер_обращения = обращения.код_обращения,
                                данные_фото = imageData
                            };

                            db.Фото.Add(фото);
                        }
                    }
                }

                db.Обращения.Add(обращения);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.объект_МКД = new SelectList(db.МКД, "код_МКД", "адрес", обращения.объект_МКД);
            ViewBag.сезонность = new SelectList(db.Сезон_обращения, "код_сезонности_обр", "сезонность_обр", обращения.сезонность);
            ViewBag.статус_обращения = new SelectList(db.Статус_обращения, "код_статуса_обращения", "статус_обр", обращения.статус_обращения);
            ViewBag.тема = new SelectList(db.Тема_обращения, "код_темы_обращения", "тема_проблемы", обращения.тема);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", обращения.учётная_запись);
            return View(обращения);
        }

        // GET: Обращения/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обращения обращения = db.Обращения.Find(id);
            if (обращения == null)
            {
                return HttpNotFound();
            }
            ViewBag.объект_МКД = new SelectList(db.МКД, "код_МКД", "адрес", обращения.объект_МКД);
            ViewBag.сезонность = new SelectList(db.Сезон_обращения, "код_сезонности_обр", "сезонность_обр", обращения.сезонность);
            ViewBag.статус_обращения = new SelectList(db.Статус_обращения, "код_статуса_обращения", "статус_обр", обращения.статус_обращения);
            ViewBag.тема = new SelectList(db.Тема_обращения, "код_темы_обращения", "тема_проблемы", обращения.тема);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", обращения.учётная_запись);
            return View(обращения);
        }

        // POST: Обращения/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "код_обращения,тема,сезонность,срок_подг_ответа,срок_подтв_ответа,описание,статус_обращения,дата_обращения,объект_МКД,обслужив_лицо,оценка,учётная_запись")] Обращения обращения)
        {            
            if (ModelState.IsValid)
            {
                db.Entry(обращения).State = EntityState.Modified;
                db.SaveChanges();             
                return RedirectToAction("Index");
            }
            ViewBag.объект_МКД = new SelectList(db.МКД, "код_МКД", "адрес", обращения.объект_МКД);
            ViewBag.сезонность = new SelectList(db.Сезон_обращения, "код_сезонности_обр", "сезонность_обр", обращения.сезонность);
            ViewBag.статус_обращения = new SelectList(db.Статус_обращения, "код_статуса_обращения", "статус_обр", обращения.статус_обращения);
            ViewBag.тема = new SelectList(db.Тема_обращения, "код_темы_обращения", "тема_проблемы", обращения.тема);
            ViewBag.учётная_запись = new SelectList(db.Учётные_записи, "код_пользователя", "логин", обращения.учётная_запись);
            return View(обращения);
        }

        // GET: Обращения/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обращения обращения = db.Обращения.Find(id);
            if (обращения == null)
            {
                return HttpNotFound();
            }
            return View(обращения);
        }

        // POST: Обращения/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Обращения обращения = db.Обращения.Find(id);
            db.Обращения.Remove(обращения);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadPhoto(int код_обращения, HttpPostedFileBase uploadedPhoto)
        {
            if (uploadedPhoto != null && uploadedPhoto.ContentLength > 0)
            {
                // Проверка расширения файла
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(uploadedPhoto.FileName).ToLower();

                if (allowedExtensions.Contains(fileExtension))
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(uploadedPhoto.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadedPhoto.ContentLength);
                    }

                    var новоеФото = new Фото
                    {
                        номер_обращения = код_обращения,
                        данные_фото = imageData
                    };

                    db.Фото.Add(новоеФото);
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = код_обращения });
                }
                else
                {
                    // Ошибка: выбран файл, который не является изображением
                    ModelState.AddModelError("uploadedPhoto", "Пожалуйста, выберите файл изображения.");
                }
            }

            // Перенаправление на действие "Edit" без изменений
            return RedirectToAction("Edit", new { id = код_обращения });
        }


        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            Фото фото = db.Фото.Find(id);
            if (фото != null)
            {
                db.Фото.Remove(фото);
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = фото.номер_обращения });
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
