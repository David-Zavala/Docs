using Practica.Data.Models;
using Practica.Data.Interfaces;
using Practica.Models.FormModels;
using Practica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Web;

namespace Practica.Data.Respositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext db = new DataContext();

        async Task<ICollection<UserToReturn>> IUsersRepository.GetUsers()
        {
            List<User> users = await db.User.ToListAsync<User>();
            ICollection<UserToReturn> usersToReturn = null;
            
            for (int i=0; i<users.Count(); i++)
            {
                usersToReturn.Append(new UserToReturn
                {
                    Email = users[i].Email,
                    Name = users[i].Name,
                    Docs = users[i].Docs,
                    AdminRole = users[i].AdminRole
                });
            }

            return usersToReturn;
        }

        public async Task<UserToReturn> GetUserByEmail(string email)
        {
            User user = await db.User.FindAsync(email);

            if (user == null) return null;

            return new UserToReturn {
                Email = user.Email,
                Name = user.Name,
                Docs = user.Docs,
                AdminRole = user.AdminRole
            };
        }

        public async Task<User> GetFullUserByEmail(string email)
        {
            User user = await db.User.FindAsync(email);
            return user;
        }

        public async Task<UserToReturn> TryLogin(Login loginUser)
        {
            User registeredUser = await GetFullUserByEmail(loginUser.Email);
            
            if (registeredUser == null) return null;
            if (registeredUser.Password != loginUser.Password) return null;

            return new UserToReturn {
                Email = registeredUser.Email,
                Name = registeredUser.Name,
                Docs = registeredUser.Docs,
                AdminRole = registeredUser.AdminRole
            };
        }

        public async Task<UserToReturn> RegisterUser(User user)
        {
            try
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                
                UserToReturn registeredUser = await GetUserByEmail(user.Email);
                return registeredUser;
            }
            catch (Exception e)
            {
                throw new Exception("Problema con la base de datos, no se pudo realizar el registro.", e);
            }
        }

        public async Task<UserToReturn> UpdateUser(User user)
        {
            try
            {
                db.User.AddOrUpdate(user);
                await db.SaveChangesAsync();

                UserToReturn registeredUser = await GetUserByEmail(user.Email);
                return registeredUser;
            }
            catch (Exception e)
            {
                throw new Exception("Problema con la base de datos, no se pudo realizar el registro.", e);
            }
        }

        public Task<ActionResult> DeleteUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
