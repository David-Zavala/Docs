using Practica.Data.Models;
using Practica.Data.Repositories;
using Practica.Data.Respositories;
using Practica.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersRepository usersR = new UsersRepository();
        private readonly DocsRepository docsR = new DocsRepository();
        public async Task<ActionResult> Home()
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

            // ************* Comentar para trabajar en Home *************
            //var activeUserName = Session["Name"]?.ToString();
            //var activeUserEmail = Session["Email"]?.ToString();
            //if (activeUserName != null && activeUserEmail != null)
            //{
            //    UserToReturn activeUser = await usersR.GetUserByEmail(activeUserEmail);
            //    bool activeUserRole = activeUser.AdminRole;
            //    if (activeUserRole == true) return RedirectToAction("HomeAdmin");
            //    else return View();
            //}
            //else return RedirectToAction("Login", "Login");

            return RedirectToAction("HomeAdmin");
        }
        public async Task<ActionResult> HomeAdmin()
        {
            // ************* Comentar para trabajar en Home *************
            //var activeUserName = Session["Name"]?.ToString();
            //var activeUserEmail = Session["Email"]?.ToString();
            //if (activeUserName != null && activeUserEmail != null)
            //{
            //    UserToReturn activeUser = await usersR.GetUserByEmail(activeUserEmail);
            //    bool activeUserRole = activeUser.AdminRole;

            //    if (activeUserRole == true)
            //    {
            //        List<Doc> docs = await GetDocs();
            //        return View(docs);
            //    }
            //    else return RedirectToAction("Home");
            //}
            //else return RedirectToAction("Login", "Login");

            List<Doc> docs = await GetDocs();
            return View(docs);
        }
        private async Task<List<Doc>> GetDocs()
        {
            return await docsR.GetDocsList();
        }
    }
}