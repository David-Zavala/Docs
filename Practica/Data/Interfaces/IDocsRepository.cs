using Practica.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practica.Data.Interfaces
{
    internal interface IDocsRepository
    {
        Task<ICollection<Doc>> GetUserDocs(string email);
        Task<Doc> GetDoc(string docId);
        Task<List<Doc>> GetDocsList();
        Task<Doc> RegisterDoc(Doc doc);
        Task<Doc> UpdateDoc(Doc doc);
        Task<Doc> DeleteDoc(Doc doc);
    }
}
