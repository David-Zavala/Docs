using Practica.Data.Respositories;
using Practica.Models;
using Practica.Models.FormModels;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class AccountController : Controller
    {
        UsersRepository usersRepository = new UsersRepository();
        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Login", "Login", viewModel);

                UserToReturn existingUser = await usersRepository.CheckPassword(viewModel);
                if (existingUser != null)
                {
                    //var roles = new string[] { "User" , "Admin" };
                    //var jwtSecurityToken = Authentication.GenerateJwtToken(existingUser.Name, roles.ToList());
                    //Session["LoggedIn"] = existingUser.Name;
                    //var validUserName = Authentication.ValidateToken(jwtSecurityToken);
                    //Session["JWTToken"] = jwtSecurityToken;
                    //return RedirectToAction("Index", "Home", new { token = jwtSecurityToken });

                    Session["LoggedIn"] = existingUser.Name;
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("", "Invalid username or password.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return RedirectToAction("Login", "Login",viewModel);
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