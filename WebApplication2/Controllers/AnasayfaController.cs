using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;
using WebApplication2.Models;
using System.Web.Helpers;


namespace WebApplication2.Controllers
{
    public class AnasayfaController : Controller
    {
        //Emlak1Entities1 db = new Emlak1Entities1();
        // Emlak2Entities db = new Emlak2Entities();
        //EmlakEntities db = new EmlakEntities();
        //EmlakEntities1 db =new EmlakEntities1();
        //EmlakEntities2 db = new EmlakEntities2();
        ProfilicEmlakEntities1 db = new ProfilicEmlakEntities1();
        
        // GET: Anasayfa
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Anasayfa()
        {
        
        var sozlesmeList = db.TBL_sozlesme.ToList(); // TBL_sozlesme verilerini al
        var kasaList = db.TBL_kasa.ToList(); // TBL_kasa verilerini al
        var getaylikgelir = new GetAylıkGelir(); // GetAylıkGelir sınıfından bir örnek oluştur,
         var aylikList = db.TBL_aylık_gelir.ToList();
        

            // MyViewModel örneği oluştur
            var viewModel = new MyViewModel
        {
            TBL_Sozlesme = sozlesmeList,
            TBL_Kasa = kasaList,
            TBL_Aylikgelir=aylikList
            
        };

    // Razor sayfasına MyViewModel örneğini geçir
    return View(viewModel);
    }

    public ActionResult Grafik()
        {
            var degerler = db.TBL_kasa.ToList();
            return View(degerler);
        }
        public ActionResult Ayarlar()
        {
            return View();
        }
        public ActionResult Kullanicilar()
        {
            return View();
        }

        public ActionResult UpTimeMonitor()
        {
            return View();
        }
        public ActionResult Datatables()
        {
            return View();
        }

        public ActionResult GayrimenkulIslemler()
        {

            var degerler = db.TBL_konutturu.ToList();
            return View(degerler);
        }
        public ActionResult Musteriler_YeniMusteri()
        {
            return View();
        }
     
        public ActionResult SifreDegistir()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreDegistir(string Username, string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            if (NewPassword != ConfirmPassword)
            {
                ViewBag.Message = "Yeni şifre ve onay şifresi eşleşmiyor.";
                return View();
            }

            var admin = db.TBL_admin.SingleOrDefault(a => a.Admin_ad == Username);
            if (admin == null)
            {
                ViewBag.Message = "Kullanıcı adı bulunamadı.";
                return View();
            }

            // Kullanıcı adı sistemde var ise ve eski şifre doğru ise
            if (admin.Admin_sifre == CurrentPassword)
            {
                // Yeni şifreyi kaydet
                admin.Admin_sifre = NewPassword;
                db.SaveChanges();

                ViewBag.Message = "Şifre başarıyla değiştirildi.";
                return View();
            }
            else
            {
                ViewBag.Message = "Mevcut şifre yanlış.";
                return View();
            }
        }

      
       

        public ActionResult GayrimenkulDetaylari()
        {
            var degerler = db.TBL_konutturu.ToList();
            return View(degerler);
        }
        public ActionResult GayrimenkulDurumBilgileri()
        {
            var degerler = db.TBL_konut.ToList();
            return View(degerler);
        }

        public ActionResult MusteriIslemleri()
        {
            var degerler = db.TBL_kiraci.ToList();
            return View(degerler);

        }
        public ActionResult KiraciAbonelikBilgileri()
        {
            return View();
        }
        public ActionResult MusteriDetaylari()
        {
            return View();

        }
        public ActionResult KiraciBilgileri()
        {
            var degerler = db.TBL_kiraci.ToList();
            return View(degerler);
        }
        public ActionResult KiraKontratlari()
        {
            var degerler = db.TBL_sozlesme.ToList();
            var kiracilar = db.TBL_kiraci.ToList();
            var evsahibi = db.TBL_evsahibi.ToList();
            return View(degerler);
        }


        public ActionResult BelgeYukle()
        {
            return View();
        }
        public ActionResult MailGonder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MailGonder(Email model)
        {
            MailMessage mailim = new MailMessage();
            mailim.To.Add("sukriyekrmn.26@gmail.com");
            mailim.From = new MailAddress("kubilayermicik9@gmail.com");
            mailim.Subject = "Kubilay ermicikden mesajınız var" + model.Baslik;
            mailim.Body = "Sayın yetkili, " + model.AdSoyad + "Kişisinden gelen mesajın içeriği aşağıdaki gibidir. <br>" + model.İcerik;
            mailim.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("kubilayermicik65@gmail.com", "kuduagdfiijkqehd");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mailim);
                TempData["Message"] = "Mesajınız iletilmiştir.En kısa zamanda size geri dönüş sağlanacaktir";
            }

            catch (SmtpException smtpEx)
            {
                // Log or display smtpEx.Message for more detailed information
                TempData["Message"] = "Mesaj Gönderilemedi.Hata nedeni:" + smtpEx.Message;
            }
            catch (Exception ex)
            {
                // Log or display ex.Message for more detailed information
                TempData["Message"] = "Mesaj Gönderilemedi.Hata nedeni:" + ex.Message;
            }

            return View();
        }
        public ActionResult Bildirimler()
        {
            return View();
        }
        public ActionResult GelirDetay()
        {
            var degerler = db.TBL_kasa.ToList();
            return View(degerler);
          
        }
        public ActionResult MusteriBilgisiDuzenle()
        {
            return View();
        }
        public ActionResult KonutBilgisiDuzenle()
        {
            return View();
        }
        public ActionResult EkraniKilitle()
        {
            return View();
        }
    }
}