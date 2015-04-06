using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;


namespace MedCardWeb.Areas._Public.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /_Public/Data/

        public JsonResult GetLastContact()
        {
            User c = null;
            using (DBMedCardEntities4 edb = new DBMedCardEntities4())
            {
                c = edb.Users.OrderByDescending(a => a.Id).Take(2).FirstOrDefault();

            }
            return new JsonResult { Data = c, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Getlogin()
        {
            User c = null;
            List<User> Userl = new List<User>();
            using (DBMedCardEntities4 edb = new DBMedCardEntities4())
            {
                Userl = edb.Users.OrderBy(a => a.login).ToList();

            }
            return new JsonResult { Data = Userl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
