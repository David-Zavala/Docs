using Practica.Data.Interfaces;
using Practica.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI;

namespace Practica.Data.Repositories
{
    public class DocsRepository : IDocsRepository
    {
        private readonly DataContext db = new DataContext();
        public async Task<List<Doc>> GetDocsListWithPagination(int page, int itemsPerPage)
        {
            List<Doc> docs = await db.Doc.OrderBy(x => x.LastUpdate)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return docs;
        }
        public async Task<List<Doc>> GetDocsListWithPaginationAndFilter(int page, int itemsPerPage, string filter)
        {
            filter = filter.ToLower();
            List<Doc> docs = await db.Doc
                .Where(
                    x => x.Id.ToLower().Contains(filter) || 
                    x.Name.ToLower().Contains(filter) || 
                    x.Email.ToLower().Contains(filter) ||
                    x.RegisteredEmail.ToLower().Contains(filter) ||
                    x.BirthDate.ToString().ToLower().Contains(filter) ||
                    x.Age.ToString().ToLower().Contains(filter) ||
                    x.Education.ToLower().Contains(filter) ||
                    x.DocPath.ToLower().Contains(filter) ||
                    x.LastUpdate.ToString().ToLower().Contains(filter) ||
                    x.Count.ToString().ToLower().Contains(filter)
                )
                .OrderBy(x => x.LastUpdate)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return docs;
        }
        public async Task<int> GetItemsCount()
        {
            return await db.Doc.CountAsync();
        }
        public async Task<Doc> RegisterDoc(Doc doc)
        {
            try
            {
                var user = await db.User.FirstOrDefaultAsync(u => u.Email == doc.Email);

                if (user != null)
                {
                    doc.User = user;
                    db.Doc.AddOrUpdate(doc);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return new Doc { Id = "-3312", Name = "Parece haber un problema con el usuario actual" };
                }
                return await db.Doc.FindAsync(doc.Id);
            }
            catch (Exception e)
            {
                return new Doc { Id = "-1", Name = e.ToString() };
            }
        }
        public Task<Doc> DeleteDoc(string docId)
        {
            throw new NotImplementedException();
        }

        public async Task<Doc> GetDoc(string docId)
        {
            Doc doc = await db.Doc.FindAsync(docId);
            return doc;
        }

        public Task<ICollection<Doc>> GetUserDocs(string email)
        {
            throw new NotImplementedException();
        }


        public Task<Doc> UpdateDoc(Doc doc)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Doc>> GetDocsList()
        {
            List<Doc> docs = await db.Doc.ToListAsync();
            return docs;
        }
    }
}