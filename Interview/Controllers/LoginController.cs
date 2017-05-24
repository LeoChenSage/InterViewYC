using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Utility;

namespace Interview.Views.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string account, string password, string captchaToken)
        {
            if (!VerifyCaptcha(captchaToken))
            {
                TempData["message"] = "驗證碼錯誤";
                return View("Index");
            }

            var user = Utility.CurrentUser.SignIn(account, password);
            if (user != null)
            {
                Session.RemoveAll();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  user.Account,//你想要存放在 User.Identy.Name 的值，通常是使用者帳號
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  false,//將管理者登入的 Cookie 設定成 Session Cookie
                  user.Name,//userdata看你想存放啥
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "登入失敗";
            return View();
        }

        private bool VerifyCaptcha(string response)
        {
            try
            {
                var jsSerializer = new JavaScriptSerializer();
                var secret = "6LdEtyIUAAAAAHiRrR6t3KBfSLAEFkMLG5wZGjni";
                var client = new WebClient();
                var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
                var res = jsSerializer.Deserialize<Interview.ViewModel.ReCaptchaResultViewModel>(reply);
                return res.success;
            }
            catch
            {
                return false;
            }
        }

    }
}