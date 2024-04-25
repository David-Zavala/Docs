namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Practica.Data.Models;
    using Practica.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            User user1 = new User { Email = "d@d.com", Password = "ddd", Name = "David Zavala", LastUpdate = GetDateTimeNow(), AdminRole = true };
            User user2 = new User { Email = "a@a.com", Password = "aaa", Name = "Alberto Trevino", LastUpdate = GetDateTimeNow(), AdminRole = false };
            User user3 = new User { Email = "b@b.com", Password = "bbb", Name = "Beto", LastUpdate = GetDateTimeNow(), AdminRole = false };
            
            Doc doc1 = new Doc { Id = "prueba1", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc2 = new Doc { Id = "prueba2", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc3 = new Doc { Id = "prueba3", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc4 = new Doc { Id = "prueba4", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc5 = new Doc { Id = "prueba5", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc6 = new Doc { Id = "prueba6", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc7 = new Doc { Id = "prueba7", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc8 = new Doc { Id = "prueba8", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc9 = new Doc { Id = "prueba9", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc10 = new Doc { Id = "prueba10", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc11 = new Doc { Id = "prueba11", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc12 = new Doc { Id = "prueba12", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc13 = new Doc { Id = "prueba13", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc14 = new Doc { Id = "prueba14", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc15 = new Doc { Id = "prueba15", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc16 = new Doc { Id = "prueba16", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc17 = new Doc { Id = "prueba17", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc18 = new Doc { Id = "prueba18", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc19 = new Doc { Id = "prueba19", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc20 = new Doc { Id = "prueba20", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc21 = new Doc { Id = "prueba21", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
            Doc doc22 = new Doc { Id = "prueba22", User = user3, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc23 = new Doc { Id = "prueba23", User = user3, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc24 = new Doc { Id = "prueba24", User = user3, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };

            user3.Docs.Add(doc1);
            user3.Docs.Add(doc2);
            user3.Docs.Add(doc3);
            user3.Docs.Add(doc4);
            user3.Docs.Add(doc5); 
            user3.Docs.Add(doc6);
            user3.Docs.Add(doc7); 
            user3.Docs.Add(doc8);
            user3.Docs.Add(doc9);
            user3.Docs.Add(doc10);
            user3.Docs.Add(doc11);
            user3.Docs.Add(doc12);
            user3.Docs.Add(doc13);
            user3.Docs.Add(doc14);
            user3.Docs.Add(doc15);
            user3.Docs.Add(doc16);
            user3.Docs.Add(doc17);
            user3.Docs.Add(doc18);
            user3.Docs.Add(doc19);
            user3.Docs.Add(doc20);
            user3.Docs.Add(doc21);
            user3.Docs.Add(doc22);
            user3.Docs.Add(doc23);
            user3.Docs.Add(doc24);

            context.User.AddOrUpdate(x => x.Email,
                user1,
                user2,
                user3
            );

            context.Doc.AddOrUpdate(x => x.Id,
                doc1,
                doc2,
                doc3,
                doc4,
                doc5,
                doc6,
                doc7,
                doc8,
                doc9,
                doc10,
                doc11,
                doc12,
                doc13,
                doc14,
                doc15,
                doc16,
                doc17,
                doc18,
                doc19,
                doc20,
                doc21,
                doc22,
                doc23,
                doc24
            );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        private int GetAge(string birthDate)
        {
            string[] yearMonthDay = birthDate.Split('-');
            int year = Int32.TryParse(yearMonthDay[0], out _)? Int32.Parse(yearMonthDay[0]) : -1;
            int month = Int32.TryParse(yearMonthDay[1], out _) ? Int32.Parse(yearMonthDay[1]) : -1;
            int day = Int32.TryParse(yearMonthDay[2], out _) ? Int32.Parse(yearMonthDay[2]) : -1; ;
            
            string[] yearMonthDayNow = DateTime.Now.ToString().Substring(0,10).Split('/'); //DateTime.Now format: DD/MM/YYYY
            int yearNow = Int32.TryParse(yearMonthDayNow[2], out _) ? Int32.Parse(yearMonthDayNow[2]) : -1;
            int monthNow = Int32.TryParse(yearMonthDayNow[1], out _) ? Int32.Parse(yearMonthDayNow[1]) : -1;
            int dayNow = Int32.TryParse(yearMonthDayNow[0], out _) ? Int32.Parse(yearMonthDayNow[0]) : -1;

            int age = yearNow - year;
            if (monthNow <= month)
                if (dayNow < day)
                    age--;

            return age;
        }

        private DateTime GetDateTimeNow()
        {
            string[] now = DateTime.Now.ToString().Substring(0,10).Split('/');
            string newFormat = now[2] + '-' + now[1] + '-' + now[0];
            return DateTime.Parse(newFormat);
        }
    }
}
