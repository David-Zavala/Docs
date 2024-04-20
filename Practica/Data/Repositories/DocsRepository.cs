using Practica.Data.Interfaces;
using Practica.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace Practica.Data.Repositories
{
    public class DocsRepository : IDocsRepository
    {
        private readonly DataContext db = new DataContext();
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
        public Task<Doc> DeleteDoc(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Doc> GetDoc(string email, int docId)
        {
            throw new NotImplementedException();
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