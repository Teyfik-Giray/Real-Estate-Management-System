using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class KullaniciController : Controller
    {
        //Emlak1Entities1 db = new Emlak1Entities1();
        //Emlak2Entities db = new Emlak2Entities();
        //EmlakEntities db = new EmlakEntities();
        // EmlakEntities1 db = new EmlakEntities1();
        //EmlakEntities2 db = new EmlakEntities2();
        ProfilicEmlakEntities1 db = new ProfilicEmlakEntities1();


        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Kullanici()
        {
            var sozlesmeList = db.TBL_sozlesme.ToList(); // TBL_sozlesme verilerini al
            var kasaList = db.TBL_kasa.ToList(); // TBL_kasa verilerini al
           

            // MyViewModel örneği oluştur
            var viewModel = new MyViewModel
            {
                TBL_Sozlesme = sozlesmeList,
                TBL_Kasa = kasaList,
               
            };

            // Razor sayfasına MyViewModel örneğini geçir
            return View(viewModel);

        }
    }
}