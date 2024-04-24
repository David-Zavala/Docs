using Practica.Data.Models;
using Practica.Data.Repositories;
using Practica.Data.Respositories;
using Practica.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

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

        private readonly int _ItemsPerPage = 10;
        private List<Doc> _Docs;
        private Pagination<Doc> _DocPagination;

        public async Task<ActionResult> HomeAdmin(int page = 1)
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

            int _TotalItems = await GetDocsCount();

            _Docs = await GetDocsWithPagination(page, _ItemsPerPage);

            var _TotalPages = (int)Math.Ceiling((double)_TotalItems / _ItemsPerPage);

            _DocPagination = new Pagination<Doc>()
            {
                ItemsPerPage = _ItemsPerPage,
                TotalItems = _TotalItems,
                TotalePages = _TotalPages,
                ActualPage = page,
                Result = _Docs
            };
            // Enviamos a la Vista la 'Clase de paginación'
            return View(_DocPagination);
        }
        private async Task<int> GetDocsCount()
        {
            return await docsR.GetItemsCount();
        }
        private async Task<List<Doc>> GetDocsWithPagination(int page,int itemsPerPage)
        {
            return await docsR.GetDocsListWithPagination(page,itemsPerPage);
        }
    }
}