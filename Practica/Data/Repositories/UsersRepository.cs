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

        public Task<UserToReturn> RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<UserToReturn> UpdateUser(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
