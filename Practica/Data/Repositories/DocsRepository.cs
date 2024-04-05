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
        public async Task<Doc> RegisterDoc(Doc doc )
        {
            try
            {
                db.Doc.AddOrUpdate(doc);
                db.SaveChanges();
                return await db.Doc.Take<Doc>(1).Where(x => x.Id == doc.Id).FirstOrDefaultAsync<Doc>();
            }
            catch (Exception e)
            {
                Doc nullDoc = new Doc { Id = "-1", Name = e.ToString() };
                return nullDoc;
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
    }
}