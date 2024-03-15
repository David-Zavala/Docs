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
        private DataContext db = new DataContext();

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
                    BirthDate = users[i].BirthDate,
                    Docs = users[i].Docs,
                });
            }

            return usersToReturn;
        }

        public async Task<UserToReturn> GetUserByEmail(string email)
        {
            User user = await db.User.Take(1).Where(x => x.Email == email).FirstOrDefaultAsync();
            return new UserToReturn {
                Email = user.Email,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Docs = user.Docs,
            };
        }

        public async Task<UserToReturn> CheckPassword(Login loginUser)
        {
            User registeredUser = await GetFullUserByEmail(loginUser.Email);
            
            if (registeredUser == null) return null;
            if (registeredUser.Password != loginUser.Password) return null;

            return new UserToReturn {
                Email = registeredUser.Email,
                Name = registeredUser.Name,
                BirthDate= registeredUser.BirthDate,
                Docs = registeredUser.Docs,
            };
        }

        private async Task<User> GetFullUserByEmail(string email)
        {
            return await db.User.Take(1).Where(x => x.Email == email).FirstOrDefaultAsync();
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
