using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        //Emlak1Entities1 db = new Emlak1Entities1();
        // Emlak2Entities db = new Emlak2Entities();
        //EmlakEntities db = new EmlakEntities();
        //EmlakEntities1 db = new EmlakEntities1();
        //EmlakEntities2 db = new EmlakEntities2();
        ProfilicEmlakEntities1 db = new ProfilicEmlakEntities1();

        public ActionResult Index()
        {
            return View();
        }

    
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBL_admin user)
        {
            var user_ID = db.TBL_admin.FirstOrDefault(x => x.Admin_ad == user.Admin_ad && x.Admin_sifre == user.Admin_sifre);
            if(user_ID != null )
            {
                FormsAuthentication.SetAuthCookie(user.Admin_ad, false);

                if ((bool)user_ID.Rol) // Rol sütunu true ise
                {
                    return RedirectToAction("Anasayfa", "Anasayfa"); // Yönetici paneline yönlendirme
                }
                else
                {
                    return RedirectToAction("Kullanici", "Kullanici"); // Kullanıcı paneline yönlendirme
                }

                //return RedirectToAction("Anasayfa", "Anasayfa");  // AnasayfaController'daki Index aksiyonuna yönlendirme
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Admin_ad, string Admin_sifre)
        {
            if (string.IsNullOrEmpty(Admin_ad) || string.IsNullOrEmpty(Admin_sifre))
            {
                ViewBag.Message = "Lütfen tüm alanları doldurun.";
                ViewBag.Success = false;
                return View();
            }

            var admin = db.TBL_admin.FirstOrDefault(a => a.Admin_ad == Admin_ad);
            if (admin != null)
            {
                admin.Admin_sifre = Admin_sifre;
                db.SaveChanges();
                ViewBag.Message = "Şifreniz başarıyla güncellendi. Tekrar giriş yapmalısınız !";
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı bulunamadı.";
                ViewBag.Success = false;
            }

            return View();
        }
    }
}