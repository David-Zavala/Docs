using System.Web.Mvc;

namespace Practica.Controllers
{
    public class LoginController : Controller
    {
        // GET: Index
        public ActionResult Login()
        {
            var openSession = Session["LoggedIn"]?.ToString();
            if (openSession == null) return View();
            else return RedirectToAction("Index", "Home");
        }
    }
}