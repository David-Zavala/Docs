 using Microsoft.Ajax.Utilities;
using Practica.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class AccountController : Controller
    {
        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Login", "Login", viewModel);


                string encryptedPwd = viewModel.Password;
                var userPassword = Convert.ToString(ConfigurationManager.AppSettings["config:Password"]);
                var userName = Convert.ToString(ConfigurationManager.AppSettings["config:Username"]);
                if (encryptedPwd.Equals(userPassword) && viewModel.Email.Equals(userName))
                {
                    var roles = new string[] { "User" };
                    var jwtSecurityToken = Authentication.GenerateJwtToken(userName, roles.ToList());
                    Session["LoginedIn"] = userName;
                    var validUserName = Authentication.ValidateToken(jwtSecurityToken);
                    Session["JWTToken"] = jwtSecurityToken;
                    return RedirectToAction("Index", "Home", new { token = jwtSecurityToken });

                }

                ModelState.AddModelError("", "Invalid username or password.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View("Login", "Login",viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            Session.Abandon();
            Session.Clear();
            return View("Login","Login");
        }

        [HttpGet]
        public JsonResult CheckEmail(string email)
        {
            JsonResult error = Json(new { isValid = false, errorMessage = $"El correo electrónico no es válido" }, JsonRequestBehavior.AllowGet);
            
            string emailRegexString = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]+$";
            RegexStringValidator emailRegex = new RegexStringValidator(emailRegexString);
            try { emailRegex.Validate(email); }
            catch (ArgumentException) { return error; }

            string localPart = email.Split('@')[0];
            if (localPart.Length > 64 || localPart[0] == '.' || localPart[localPart.Length-1] == '.') return error;

            if (email.Length > 254) return error;

            return Json(new { isValid = true }, JsonRequestBehavior.AllowGet);
        }
    }
}