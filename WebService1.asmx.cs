using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MedCardWeb
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description="Удалить запись медкарту")]
        public string WSaddRow(int id)
        {

            DBMedCardEntities4 DB = new DBMedCardEntities4();
            string @yt = "не удалена";
            var value = DB.MedCards.FirstOrDefault(c => c.Id == id);
            if (value != null)
            {
                DB.Set<MedCard>().Remove(value); //удаляет через коллекцию
                DB.SaveChanges();
                @yt = "удалена";
            }

            return "Запись " + @yt;
        }
    }
}

