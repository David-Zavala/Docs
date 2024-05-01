using Practica.Data.Models;
using Practica.Data.Respositories;
using Practica.Models;
using Practica.Models.FormModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class AccountController : Controller
    {
        UsersRepository usersR = new UsersRepository();
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

                UserToReturn existingUser = await usersR.TryLogin(viewModel);
                if (existingUser != null)
                {
                    //var roles = new string[] { "User" , "Admin" };
                    //var jwtSecurityToken = Authentication.GenerateJwtToken(existingUser.Name, roles.ToList());
                    //Session["LoggedIn"] = existingUser.Name;
                    //var validUserName = Authentication.ValidateToken(jwtSecurityToken);
                    //Session["JWTToken"] = jwtSecurityToken;
                    //return RedirectToAction("Index", "Home", new { token = jwtSecurityToken });

                    Session["Name"] = existingUser.Name;
                    Session["Email"] = existingUser.Email;
                    return RedirectToAction("Home", "Home");

                }

                return Json(new { success = false, message = "Los datos de usuario no son válidos. Si esto es un problema contactar a un administrador" });

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Los datos de usuario no son válidos. Si esto es un problema contactar a un administrador" });
            }
        }
        FormValidationsController formValidations = new FormValidationsController();

        [HttpPost]
        public async Task<ActionResult> Register(Register newUser)
        {
            if (formValidations.CheckAlphabeticInput(newUser.Name) == 0 ||
                formValidations.CheckEmail(newUser.Email) == 0 ||
                formValidations.CheckPassword(newUser.Password) == 0 ||
                newUser.Password != newUser.ConfirmPassword)
                return Json(new { success = false, message = "Error en las validaciones de los datos."});

            User newUserMapped = MapRegisterToUser(newUser);

            try
            {
                int actualUserResult = await UserIsAdmin();
                if (actualUserResult == -1) RedirectToAction("Login", "Login");
                if (actualUserResult == 0) RedirectToAction("Home", "Home");

                UserToReturn registeredUser = await usersR.RegisterUser(newUserMapped);
                return Json(new { success = true, message = "Nuevo usuario registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al intentar registrar usuario en la base de datos: " + ex.Message });
            }

        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login","Login");
        }

        private User MapRegisterToUser(Register toRegister)
        {
            User mapped = new User { 
                Name = toRegister.Name,
                Email = toRegister.Email,
                Password = toRegister.Password,
                AdminRole = toRegister.AdminRole,
                LastUpdate = GetDateTimeNow()
            };
            return mapped;
        }

        private DateTime GetDateTimeNow()
        {
            string[] now = DateTime.Now.ToString().Substring(0, 10).Split('/');
            string newFormat = now[2] + '-' + now[1] + '-' + now[0];
            return DateTime.Parse(newFormat);
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