using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5EF6Demo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string strEmail = fc["inputMail3"];
            string strPWD = fc["inputPWD3"];
            ViewBag.LoginState = strEmail+"登录后";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}