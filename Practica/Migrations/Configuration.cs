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
            Doc doc1 = new Doc { Id = "prueba1", User = user1, Name = "ArchivoEjem1", Email = "aaa", RegisteredEmail = "ejem1@e.com", BirthDate = DateTime.Parse("2002-03-06"), Age = GetAge("2002-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo1.txt", LastUpdate = GetDateTimeNow() };
            Doc doc2 = new Doc { Id = "prueba2", User = user1, Name = "ArchivoEjem2", Email = "aaa", RegisteredEmail = "ejem2@e.com", BirthDate = DateTime.Parse("2000-12-06"), Age = GetAge("2000-12-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo2.txt", LastUpdate = GetDateTimeNow() };
            Doc doc3 = new Doc { Id = "prueba3", User = user2, Name = "ArchivoEjem3", Email = "aaa", RegisteredEmail = "ejem3@e.com", BirthDate = DateTime.Parse("2001-03-06"), Age = GetAge("2001-03-06"), Education = "Carrera en progreso", DocPath = "Data/SavedFiles/archivo3.txt", LastUpdate = GetDateTimeNow() };
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
