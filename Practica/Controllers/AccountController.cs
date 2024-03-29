﻿using Practica.Data.Respositories;
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

                UserToReturn existingUser = await usersRepository.TryLogin(viewModel);
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
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login","Login");
        }
    }
}