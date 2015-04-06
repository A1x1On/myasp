using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedCardWeb.Areas._Private.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /_Private/Admin/

        public ActionResult Index()
        {

            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "_Public", mess = 0 });
            }

        }

        public ActionResult EditProfile()
        {

            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "_Public", mess = 0 });
            }

        }


        public ActionResult ChangeMk(string t, string d)
        {
          
            ViewBag.t = t;
            ViewBag.d = d;

            return View();
        }

        [HttpPost]
        public ActionResult ChangeMk(MedCard medC)
        {
            DBMedCardEntities4 db = new DBMedCardEntities4();

            var value = db.MedCards.FirstOrDefault(c => c.Id == medC.Id);

            if (value != null)
            {
                value.title = medC.title;
                value.discription = medC.discription;
                db.SaveChanges();
            }

            return RedirectToAction("ListMk", new { medTitle = medC.title });
        
        }


        public ActionResult AddMk()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AddMk(MedCard medC)
        {

            DBMedCardEntities4 db = new DBMedCardEntities4();

            MedCard cards = new MedCard
            {
                title = medC.title, discription = medC.discription

            };
            db.MedCards.Add(cards);
            db.SaveChanges();



            return RedirectToAction("ListMk", new { medTitle = medC.title });
        }
  



        public ActionResult ListMk()
        {
  
            var db = new DBMedCardEntities4(); //подцепляемся к бд


            var query = from cards in db.MedCards
                        where cards.Id != 0
                        orderby cards.Id
                        select cards;


            return View(query.ToArray()); //передаем массив query выборки в View
        }


    }
}
