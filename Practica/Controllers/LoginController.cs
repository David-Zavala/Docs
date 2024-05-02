using Practica.Data.Models;
using Practica.Data.Respositories;
using Practica.Models;
using Practica.Models.FormModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsersRepository usersR = new UsersRepository();
        public ActionResult Login(Login LoginModel = null)
        {
            var openSession = Session["Name"]?.ToString();
            if (openSession == null)
            {
                LoginModel = LoginModel ?? new Login { Email = "", Password = "" };
                return View(LoginModel);
            }
            else return RedirectToAction("Home", "Home");
        }
        public async Task<ActionResult> Register()
        {
            int userResult = await UserIsAdmin();
            if (userResult == -1) return RedirectToAction("Login", "Login");
            if (userResult == 0) return RedirectToAction("Home", "Home");
            return View();
        }
        private async Task<int> UserIsAdmin()
        {
            string activeUserName = Session["Name"]?.ToString();
            string activeUserEmail = Session["Email"]?.ToString();
            if (activeUserName != null && activeUserEmail != null)
            {
                UserToReturn activeUser = await usersR.GetUserByEmail(activeUserEmail);
                bool activeUserRole = activeUser.AdminRole;

                if (activeUserRole == true)
                {
                    return 1;
                }
                else return 0;
            }
            else return -1;
        }
    }
}