using Practica.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("index", viewModel);


                string encryptedPwd = viewModel.Password;
                var userPassword = Convert.ToString(ConfigurationManager.AppSettings["config:Password"]);
                var userName = Convert.ToString(ConfigurationManager.AppSettings["config:Username"]);
                if (encryptedPwd.Equals(userPassword) && viewModel.Email.Equals(userName))
                {
                    var roles = new string[] { "SuperAdmin", "Admin" };
                    var jwtSecurityToken = Authentication.GenerateJwtToken(userName, roles.ToList());
                    Session["LoginedIn"] = userName;
                    var validUserName = Authentication.ValidateToken(jwtSecurityToken);
                    return RedirectToAction("index", "Home", new { token = jwtSecurityToken });

                }

                ModelState.AddModelError("", "Invalid username or password.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View("Index", viewModel);
        }

    }
}