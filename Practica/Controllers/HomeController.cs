using Practica.Data;
using Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var openSession = Session["JWTToken"]?.ToString();
            var tokenUsername = Session["LogedIn"]?.ToString();

            var username = "";
            if (openSession != null) username = Authentication.ValidateToken(openSession);

            if (username != "" && username == tokenUsername)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }
    }
}