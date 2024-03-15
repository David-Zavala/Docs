using System.Web.Mvc;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // ************* Comentado hasta implementar JWT *************
            //var openSession = Session["JWTToken"]?.ToString();
            //var tokenUsername = Session["LogedIn"]?.ToString();

            //var username = "";
            //if (openSession != null) username = Authentication.ValidateToken(openSession);

            //if (username != "" && username == tokenUsername)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login","Login");
            //}

            // ************* Comentado hasta acabar vista Home *************
            //var openSession = Session["LoggedIn"]?.ToString();
            //if (openSession != null) return View();
            //else return RedirectToAction("Login", "Login");

            return View();
        }
    }
}