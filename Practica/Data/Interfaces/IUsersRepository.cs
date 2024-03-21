using Practica.Data.Models;
using Practica.Models;
using Practica.Models.FormModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<ICollection<UserToReturn>> GetUsers();
        Task<UserToReturn> GetUserByEmail(string email);
        Task<UserToReturn> RegisterUser(User user);
        Task<UserToReturn> UpdateUser(string email);
        Task<ActionResult> DeleteUser(string email);
        Task<UserToReturn> TryLogin(Login loginUser);
    }
}