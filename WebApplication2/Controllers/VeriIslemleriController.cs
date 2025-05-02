using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class VeriIslemleriController : Controller
    {
        //Emlak1Entities1 db = new Emlak1Entities1();
        // Emlak2Entities db = new Emlak2Entities();
        //EmlakEntities db = new EmlakEntities();
        // EmlakEntities1 db = new EmlakEntities1();
        //EmlakEntities2 db = new EmlakEntities2();
        ProfilicEmlakEntities1 db = new ProfilicEmlakEntities1();


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(TBL_konutturu p1)
        {
            TBL_konutturu yeniUrun = new TBL_konutturu();
            yeniUrun.konuttur_ad = p1.konuttur_ad;
            yeniUrun.oda_sayısı = p1.oda_sayısı;
            yeniUrun.metre_kare = p1.metre_kare;
            yeniUrun.adres = p1.adres;
            yeniUrun.ısınma_turu = p1.ısınma_turu;
            yeniUrun.esyalimi = p1.esyalimi;
            yeniUrun.gayrimenkul_durum = p1.gayrimenkul_durum;


            db.TBL_konutturu.Add(yeniUrun);
            db.SaveChanges();

            return RedirectToAction("GayrimenkulIslemler", "Anasayfa");
        }


        public ActionResult Sil(int id)
        {
            var urun = db.TBL_konutturu.Find(id);
            db.TBL_konutturu.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("GayrimenkulIslemler" , "Anasayfa");
            


        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_konutturu.Find(id);
            return View("UrunGetir", urun);
        }

        [HttpPost]
        public ActionResult Guncelle(TBL_konutturu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Güncelleme işlemi
                    var existingKonut = db.TBL_konutturu.FirstOrDefault(k => k.tur_id == model.tur_id);
                    if (existingKonut != null)
                    {
                        existingKonut.konuttur_ad = model.konuttur_ad;
                        existingKonut.oda_sayısı = model.oda_sayısı;
                        existingKonut.metre_kare = model.metre_kare;
                        existingKonut.adres = model.adres;
                        existingKonut.ısınma_turu = model.ısınma_turu;
                        existingKonut.esyalimi = model.esyalimi;
                        existingKonut.gayrimenkul_durum = model.gayrimenkul_durum;
                        db.SaveChanges();
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Konut bulunamadı." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Model durumu geçersiz. Lütfen tüm alanları doldurun." });
        }


      
        public ActionResult Ekle1()
        {
            return View();
        }
 
     
       public ActionResult Sill(int id)
        {
            var urun = db.TBL_konut.Find(id);
            db.TBL_konut.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("GayrimenkulDurumBilgileri", "Anasayfa");
        }


        public ActionResult UrunGetir2(int id)
        {
            var urun = db.TBL_konut.Find(id);
            return View("UrunGetir2", urun);
        }

        [HttpPost]
        public ActionResult Guncelle2(TBL_konut model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingKonut = db.TBL_konut.FirstOrDefault(k => k.konut_id == model.konut_id);
                    if (existingKonut != null)
                    {
                        existingKonut.TBL_evsahibi.konuts_ad = model.TBL_evsahibi.konuts_ad;
                        existingKonut.TBL_kiraci.kiraci_ad = model.TBL_kiraci.kiraci_ad;
                        existingKonut.konut_adres = model.konut_adres;
                        existingKonut.kira_tutar = model.kira_tutar;

                        // Kira_Durum alanını değer olarak ayarla
                        existingKonut.Kira_Durum = Convert.ToBoolean(model.Kira_Durum);

                        db.SaveChanges();
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Konut bulunamadı." });
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    // Entity Framework validation errors
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    return Json(new { success = false, message = exceptionMessage });
                }
                catch (DbUpdateException ex)
                {
                    var message = "A database error occurred while updating the entries.";
                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }
                    return Json(new { success = false, message = message });
                }

               
                catch (Exception ex)
                {
                    // General exception
                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += " Inner Exception: " + ex.InnerException.Message;
                    }

                    return Json(new { success = false, message = message });
                }
            }

            return Json(new { success = false, message = "Model durumu geçersiz. Lütfen tüm alanları doldurun." });
        }


        public ActionResult Sil3(int id)
        {
            var urun = db.TBL_sozlesme.Find(id);
            db.TBL_sozlesme.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("KiraKontratlari", "Anasayfa");


        }

        public ActionResult UrunGetir3(int id)
        {
            // Kiracılar ve Ev Sahiplerini çek
            var kiracilar = db.TBL_kiraci.ToList();
            var evSahipleri = db.TBL_evsahibi.ToList();
            var konutlar = db.TBL_konut.ToList(); // Örnek olarak konutları çekelim

            // ViewBag için SelectList oluştur
            ViewBag.Kiracilar = new SelectList(kiracilar, "kiraci_id", "kiraci_ad");
            ViewBag.EvSahipleri = new SelectList(evSahipleri, "konuts_id", "konuts_ad");
            ViewBag.Konutlar = new SelectList(konutlar, "konut_id", "konut_adres");

            // Sözleşme bilgilerini getir
            var sozlesme = db.TBL_sozlesme.Find(id);

            return View(sozlesme);
        }

        public ActionResult Guncelle3(int id)
        {
            var sozlesme = db.TBL_sozlesme.Find(id);

            if (sozlesme == null)
            {
                return HttpNotFound();
            }

            // Kiracı ve Ev Sahibi listelerini ViewBag'e ekleyin
            ViewBag.Kiracilar = new SelectList(db.TBL_kiraci.Select(k => new
            {
                kiraci_id = k.kiraci_id,
                FullName = k.kiraci_ad + "   " + k.kiraci_soyad // Combine first name and last name
            }), "kiraci_id", "FullName");

            ViewBag.EvSahipleri = new SelectList(db.TBL_evsahibi.Select(e => new
            {
                konuts_id = e.konuts_id,
                FullName = e.konuts_ad + "  " + e.konuts_soyad // Combine first name and last name
            }), "konuts_id", "FullName");

            ViewBag.Konutlar = new SelectList(db.TBL_konut.Select(c => new
            {
                konut_id = c.konut_id,
                FullName = c.konut_adres
            }), "konut_id", "FullName");

            return View(sozlesme);
        }


        [HttpPost]
        public ActionResult Guncelle3(TBL_sozlesme p1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p1).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Güncelleme başarısız oldu." });
        }




        public ActionResult Ekle3()
        {
            // View'a Ev Sahipleri ve Kiracıları aktar
            ViewBag.EvSahipleri = new SelectList(db.TBL_evsahibi.Select(e => new
            {
                konuts_id = e.konuts_id,
                FullName = e.konuts_ad + " " + e.konuts_soyad // Combine first name and last name
            }), "konuts_id", "FullName");

            ViewBag.Kiracilar = new SelectList(db.TBL_kiraci.Select(k => new
            {
                kiraci_id = k.kiraci_id,
                FullName = k.kiraci_ad + " " + k.kiraci_soyad // Combine first name and last name
            }), "kiraci_id", "FullName");

            ViewBag.Konutlar = new SelectList(db.TBL_konut, "konut_id", "konut_adres");

            return View();
        }

        [HttpPost]
        public ActionResult Ekle3(TBL_sozlesme p1, string depozito_durum)
        {
            if (ModelState.IsValid)
            {
                // Kiracı ve Ev Sahibi nesnelerini ilgili tablolardan bul
                p1.TBL_kiraci = db.TBL_kiraci.FirstOrDefault(k => k.kiraci_id == p1.kiraci_id);
                p1.TBL_evsahibi = db.TBL_evsahibi.FirstOrDefault(e => e.konuts_id == p1.konutsahibi_id);
                p1.TBL_konut = db.TBL_konut.FirstOrDefault(c => c.konut_id == p1.konut_id);

                // Depozito durumu string'ten bool'a dönüştürme
                bool depozitoDurumu = (depozito_durum == "True");
                p1.depozito_durum = depozitoDurumu;

                // Yeni sözleşmeyi veritabanına ekle
                db.TBL_sozlesme.Add(p1);
                db.SaveChanges();

                // Başarılı bir şekilde eklendikten sonra kullanıcıyı yönlendir
                return RedirectToAction("KiraKontratlari", "Anasayfa");
            }

            // Geçersiz model durumunda, View'a Ev Sahipleri ve Kiracıları aktar ve formu tekrar göster
            ViewBag.EvSahipleri = new SelectList(db.TBL_evsahibi.Select(e => new
            {
                konuts_id = e.konuts_id,
                FullName = e.konuts_ad + " " + e.konuts_soyad // Combine first name and last name
            }), "konuts_id", "FullName");

            ViewBag.Kiracilar = new SelectList(db.TBL_kiraci.Select(k => new
            {
                kiraci_id = k.kiraci_id,
                FullName = k.kiraci_ad + " " + k.kiraci_soyad // Combine first name and last name
            }), "kiraci_id", "FullName");

            ViewBag.Konutlar = new SelectList(db.TBL_konut.Select(c => new
            {
                konut_id=c.konut_id,
                FullName = c.konut_adres
            }),"konut_id","FullName");

            return View(p1);
        }




        public ActionResult SilKiraci(int id)
        {
            var urun = db.TBL_kiraci.Find(id);
            db.TBL_kiraci.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("MusteriIslemleri", "Anasayfa");


        }
        public ActionResult UrunGetirKiraci(int id)
        {
            var urun = db.TBL_kiraci.Find(id);
            return View("UrunGetirKiraci", urun);
        }
        public ActionResult EkleKiraci()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EkleKiraci(TBL_kiraci p1)
        {
            TBL_kiraci yeniUrun = new TBL_kiraci();
            yeniUrun.kiraci_tc = p1.kiraci_tc;
            yeniUrun.kiraci_ad = p1.kiraci_ad;
            yeniUrun.kiraci_soyad = p1.kiraci_soyad;
            yeniUrun.apartman_blok = p1.apartman_blok;
            yeniUrun.kiraci_daire_no = p1.kiraci_daire_no;
            yeniUrun.kira_gun = p1.kira_gun;

            db.TBL_kiraci.Add(yeniUrun);
            db.SaveChanges();

            return RedirectToAction("MusteriIslemleri", "Anasayfa");
        }
        [HttpPost]
        public ActionResult GuncelleKiraci(TBL_kiraci model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingKiraci = db.TBL_kiraci.FirstOrDefault(k => k.kiraci_id == model.kiraci_id);
                    if (existingKiraci != null)
                    {
                        existingKiraci.kiraci_tc = model.kiraci_tc;
                        existingKiraci.kiraci_ad = model.kiraci_ad;
                        existingKiraci.kiraci_soyad = model.kiraci_soyad;
                        existingKiraci.apartman_blok = model.apartman_blok;
                        existingKiraci.kiraci_daire_no = model.kiraci_daire_no;
                        existingKiraci.kira_gun = model.kira_gun;

                        db.SaveChanges();
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Kiracı bulunamadı." });
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    // Entity Framework validation errors
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    return Json(new { success = false, message = exceptionMessage });
                }
                catch (DbUpdateException ex)
                {
                    var message = "Veritabanında güncelleme sırasında bir hata oluştu.";
                    if (ex.InnerException != null)
                    {
                        message += " İç Hata: " + ex.InnerException.Message;
                    }
                    return Json(new { success = false, message = message });
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += " İç Hata: " + ex.InnerException.Message;
                    }

                    return Json(new { success = false, message = message });
                }
            }

            return Json(new { success = false, message = "Model geçersiz. Lütfen tüm alanları doldurun." });
        }

        public ActionResult Eklee()
        {
            ViewBag.EvSahipleri = db.TBL_evsahibi.ToList();
            ViewBag.Kiracilar = db.TBL_kiraci.ToList();
            return View();
        }


        [HttpPost]
       
        public ActionResult Eklee(TBL_konut p1, int konuts_id, int kiraci_id, string Kira_Durum)
        {
            if (ModelState.IsValid)
            {
                p1.TBL_evsahibi = db.TBL_evsahibi.Find(konuts_id);
                p1.TBL_kiraci = db.TBL_kiraci.Find(kiraci_id);

                // Formdan gelen string değeri bool türüne dönüştürme
                bool kiraDurumu;
                if (bool.TryParse(Kira_Durum, out kiraDurumu))
                {
                    p1.Kira_Durum = kiraDurumu;

                    db.TBL_konut.Add(p1);
                    db.SaveChanges();
                    return RedirectToAction("GayrimenkulDurumBilgileri", "Anasayfa"); //Ödendi seçilince hata oluyor
                }
            }

            // View'a Ev Sahipleri ve Kiracıları aktar
            ViewBag.EvSahipleri = db.TBL_evsahibi.ToList();
            ViewBag.Kiracilar = db.TBL_kiraci.ToList();
            return View();
        }


    }
}