using CoursePractice.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace CoursePractice.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();
            var roles = db.Тип_учётной_записи.Select(r => new SelectListItem
            {
                Value = r.код_типа_пользователя.ToString(),
                Text = r.тип_пользователя
            }).ToList();

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            MonitoringSystemDBEntities db1 = new MonitoringSystemDBEntities();
            var roles = db1.Тип_учётной_записи.Select(r => new SelectListItem
            {
                Value = r.код_типа_пользователя.ToString(),
                Text = r.тип_пользователя
            }).ToList();

            ViewBag.Roles = roles;
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Учётные_записи user = null;
                using (MonitoringSystemDBEntities db = new MonitoringSystemDBEntities())
                {
                    user = db.Учётные_записи.FirstOrDefault(u => u.логин == model.Login && u.пароль== model.Password);
                }
                if (user != null && user.тип_учётной_записи == model.SelectedRole)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    // После успешной аутентификации пользователя
                    int userId = user.код_пользователя;
                    Session["UserId"] = userId;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с такими данными не найден!");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();
            MonitoringSystemDBEntities db = new MonitoringSystemDBEntities();
            // Здесь получите список ролей из таблицы Тип_учётной_записи и преобразуйте его в List<SelectListItem>
            var roles = db.Тип_учётной_записи.Select(r => new SelectListItem
            {
                Value = r.код_типа_пользователя.ToString(),
                Text = r.тип_пользователя
            }).ToList();
            roles.Remove(roles[0]);
            model.Roles = roles;


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Учётные_записи user = null;
                using (MonitoringSystemDBEntities db = new MonitoringSystemDBEntities())
                {                   
                    user = db.Учётные_записи.FirstOrDefault(u => u.логин == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (MonitoringSystemDBEntities db = new MonitoringSystemDBEntities())
                    {
                        db.Учётные_записи.Add(new Учётные_записи { логин = model.Login, пароль = model.Password, тип_учётной_записи= model.SelectedRole });
                        db.SaveChanges();

                        user = db.Учётные_записи.Where(u => u.логин == model.Login && u.пароль == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        
                        FormsAuthentication.SetAuthCookie(model.Login, true);

                        // Сохранение информации о текущем пользователе в сеансе
                        Session["UserId"] = user.код_пользователя;

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            Session.Clear();
            Session.Abandon();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            foreach (string cookie in Request.Cookies.AllKeys)
            {
                Request.Cookies.Remove(cookie);
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }


            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}