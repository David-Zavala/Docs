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
            // ************* Comentar para trabajar en Home *************
            int userResult = await UserIsAdmin();
            if (userResult == -1) return RedirectToAction("Login", "Login");
            if (userResult == 1) return RedirectToAction("HomeAdmin");
            return View();

            //return RedirectToAction("HomeAdmin");
        }

        public async Task<ActionResult> HomeAdmin(int page = 1, string filter = "None")
        {
            // ************* Comentar para trabajar en HomeAdmin *************
            int userResult = await UserIsAdmin();
            if (userResult == -1) return RedirectToAction("Login", "Login");
            if (userResult == 0) return RedirectToAction("Home");

            List<Doc> Docs = await docsR.GetDocsList();
            return View(Docs);
        }
        private async Task<int> GetDocsCount()
        {
            return await docsR.GetItemsCount();
        }
        private async Task<List<Doc>> GetDocsWithConditions(int page, int itemsPerPage, string filter)
        {
            List<Doc> docs;
            if (filter == "None") docs = await GetDocsWithPagination(page, itemsPerPage);
            else docs = await GetDocsWithPaginationAndFilter(page, itemsPerPage, filter);
            return docs;
        }
        private async Task<List<Doc>> GetDocsWithPagination(int page,int itemsPerPage)
        {
            return await docsR.GetDocsListWithPagination(page,itemsPerPage);
        }
        private async Task<List<Doc>> GetDocsWithPaginationAndFilter(int page, int itemsPerPage, string filter)
        {
            return await docsR.GetDocsListWithPaginationAndFilter(page,itemsPerPage,filter);
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