using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRVTextToImage;
using System.Net.Mail;
using MedCardWeb.Areas._Public.Models;
using System.Threading.Tasks;
using System.IdentityModel;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Text;
using System.Web.Security;


namespace MedCardWeb.Areas._Public.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /_Public/Home/

        public ActionResult Index(string mess)
        {

            ViewBag.Mess = mess != null ? "<font style='font-size: 25px;'>Вы не можете зайти в этот раздел так как вы не зарегистрированны</font><br><br>" : "";
            ViewBag.rrr = "sssss";

            return View();
        }


       // [ChildActionOnly]
        public ActionResult OModel()
        {
            var edb = new DBMedCardEntities4();

            var query = from Usersl in edb.Users
                        where Usersl.Id != -1
                        orderby Usersl.login
                        select Usersl;

            return PartialView("~/Areas/_Public/Views/Shared/_otherModel.cshtml", query.ToArray());
        }


        //[NonAction]  
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User u, string CaptchaText)
        {
            var  @jsLogin ="1";
            var  @jsMail = "1";


            


            ViewBag.Attempt = "Попытка";
            if (ModelState.IsValid && this.Session["CaptchaImageText"].ToString() == CaptchaText)
            {
                ViewBag.Message2 = "Catcha Validation Success!";
                ViewBag.Attempt = "валидация валидна";
                using (DBMedCardEntities4 edb = new DBMedCardEntities4())
                {

                    


                    string @Encrypt = Crypt.Encrypt(u.login + u.pass + DateTime.Now.ToString(), u.pass); // закодировать 1 параметр
                    string @password = "p1s:cyjdsvujljv";
                    u.ConfirMail = @Encrypt + @password;
                    

                   //string @Decrypt = "?code=" + Crypt.Decrypt("Fe7bbECCPnu4iklNM0CtBquvjNW+JJwG4rFH8GscvruUjGMVAu4ngWSirNogr9LK", "Jhgdsj&78^ccc"); // Разодировать 1 параметр
                    
                    edb.Users.Add(u);
                    ViewBag.Attempt = "валидация валидна + добавлен пользователь";
                    
                    
                    ViewBag.Message = "Пользователь успешно зарегистрирован";

                    // Отправка подтверждения
                    SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 25);
                    Smtp.Credentials = new NetworkCredential("A1x1On@yandex.ru", "2engine2");
                    Smtp.EnableSsl = true; //Формирование письма Mail


                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("A1x1On@yandex.ru");
                    message.To.Add(new MailAddress(u.email.ToString())); // Тому кто регистрируется указал свой mail

                    
                    //message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;

                    message.Subject = "Заголовок";
                    message.Body = "<html><body><br><img src=\"http://www.cyberforum.ru/images/cyberforum_logo.jpg\" alt=\"Super Game!\">" +@" 
                    <br>Здравствуйте уважаемый(я) " + u.firsеName +" "+ u.lastName + @" !
                    <br>Вы получили это письмо, потому что вы зарегистрировались на http://www.supergame.ru или сменили e-mail в профиле.
                    <br>Высылаем Вам секретный код для активации вашего профиля.
                    <br>                                                                                              
                    <br>Код активации:       <b>" + @Encrypt + @password + @"</b>
                    <br>Ваш логин: " + u.login.ToString() + "<br>Ваш пароль" + u.pass.ToString() + "<br> Пройдите по ссылке для подтверждения: <a href='http://localhost:62901/_Public/Home/ConfirMail/?code=" + @Encrypt + @password +"'>Клик</a><br><br>Мы будем рады видеть Вас на нашем сайте и желаем Вам приятой игры!</body></html>";
                    
                    
             
                    
                    Smtp.Send(message);//отправка

                    edb.SaveChanges();
                    ModelState.Clear();
                    u = null;
                    ViewBag.m1 = "Успешно отправил";
                }

            }
            else
            {
                ViewBag.Message2 = "Catcha Validation Failed!";
            }
            return View(u);
        }

        public ActionResult ConfirMail(string code)
        {
            //var @bytes = Crypt.GetBytes(code);
            //string @Strs = Crypt.GetString(@bytes);
     
            @code = code.Replace(" ", "+");

            var DB = new DBMedCardEntities4();
            var value = DB.Users.FirstOrDefault(c => c.ConfirMail.Contains(code));
            if (value != null)
            {
                value.ConfirmPass = value.pass;
                value.ConfirMail = "confirmed";
                ViewBag.Code1 = "Поздравляем! Ваш профиль подтвержден";
                DB.SaveChanges();
            }
            
            else
            {
                ViewBag.Code1 = "Ваш профиль уже подтвержден";
                //string @Decr = Crypt.Decrypt(code, "0987654321");
                //ViewBag.Code = @Decr;
            }

            return View();
        }












        public ActionResult LogIn()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(MedCardWeb.Areas._Public.Models.Us i)
        {


 
            if (ModelState.IsValid)
            {

         




                using (DBMedCardEntities4 edb = new DBMedCardEntities4())
                {
               
                    var v = edb.Users.Where(a => a.login.Equals(i.login) && a.pass.Equals(i.pass) && a.ConfirMail.Equals("confirmed") ).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.Id.ToString();
                        Session["LogedUserName"] = v.firsеName.ToString(); 
                         
                    }
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/_Public/Views/Shared/_PartialViewLogInAjax.cshtml", i);
            }
            else
            {
                return RedirectToAction("Index");
            }

            
        }

        public ActionResult LogOut(MedCardWeb.Areas._Public.Models.Us o)
        {
            Session["LogedUserID"] = null;
            Session["LogedUserName"] = null;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/_Public/Views/Shared/_PartialViewLogInAjax.cshtml", o);
            }
            else
            {
                return RedirectToAction("Index", o);
            }
        }





        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "none")] //отрубаем кэш
        public FileResult GetCapthcaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage(); // создание объекта того самого .dll
            this.Session["CaptchaImageText"] = CI.GetRandomString(5); // получаем рандомную строку из 5 символов
            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75, Color.DarkGreen, Color.White); // генерируем изображение исходя из полученой рандомной строки
            MemoryStream stream = new MemoryStream(); // создаем облость памяти для изображений и др фйлов
            CI.Image.Save(stream, ImageFormat.Png); // сохроняем туда изображение формата PNG
            stream.Seek(0, SeekOrigin.Begin); // хз искать чегото
            return new FileStreamResult(stream, "image/png"); // вернуть ссылку из памяти
        }





    }
}
