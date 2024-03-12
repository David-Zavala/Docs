namespace Practica.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Practica.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Practica.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Practica.Data.DataContext context)
        {
            User user1 = new User { Email = "d@d.com", Password = "ddd" , Name = "David Zavala", BirthDate = DateTime.Parse("2002-03-06") };
            User user2 = new User { Email = "o@o.com", Password = "ooo" , Name = "Oscar Leija", BirthDate = DateTime.Parse("2001-03-06") };
            Doc doc1 = new Doc { User = user1, Name = "ArchivoEjem1", Email = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Education = "Carrera en progreso", DocPath = "../SaveDocs/archivo1.txt" };
            Doc doc2 = new Doc { User = user1, Name = "ArchivoEjem2", Email = "ejem2@e.com", BirthDate = DateTime.Parse("2002-03-06"), Education = "Carrera en progreso", DocPath = "../SaveDocs/archivo2.txt" };
            Doc doc3 = new Doc { User = user2, Name = "ArchivoEjem3", Email = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Education = "Carrera en progreso", DocPath = "../SaveDocs/archivo3.txt" };
            user1.Docs.Add(doc1);
            user1.Docs.Add(doc2);
            user2.Docs.Add(doc3);
            
            context.User.AddOrUpdate(x => x.Email,
                user1,
                user2
            );
            context.Doc.AddOrUpdate(x => x.Id,
                doc1,
                doc2,
                doc3
            );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
