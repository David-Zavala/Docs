using Practica.Data.Models;
using Practica.Data.Repositories;
using Practica.Data.Respositories;
using Practica.Models.FormModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class DocController : Controller
    {
        private readonly DocsRepository docsR = new DocsRepository();
        private readonly UsersRepository usersR = new UsersRepository();
        public async Task<Doc> Register(DocForm doc)
        {
            string actualEmail = Session["Email"]?.ToString();
            string birthDate = doc.Year.ToString() + "-" + doc.Month.ToString() + "-" + doc.Day.ToString();
            string createdId = actualEmail.Substring(0,3) + doc.Name.Substring(0,5) + "_" + GetDateTimeNow();
            string createdDocPath = "Data/SavedFiles/" + doc.Name + "_" + createdId;
            Doc mappedDoc = new Doc {
                Id = createdId,
                User = await usersR.GetUserForDoc(actualEmail),
                Name = doc.Name,
                Email = actualEmail,
                RegisteredEmail = doc.Email,
                BirthDate = DateTime.Parse(birthDate),
                Age = GetAge(birthDate),
                Education = doc.EducationLevel + " " + doc.EducationProgress,
                DocPath = createdDocPath,
                LastUpdate = GetDateTimeNow(),
            };
            return await docsR.RegisterDoc(mappedDoc);
        }
        private int GetAge(string birthDate)
        {
            string[] yearMonthDay = birthDate.Split('-');
            int year = Int32.TryParse(yearMonthDay[0], out _) ? Int32.Parse(yearMonthDay[0]) : -1;
            int month = Int32.TryParse(yearMonthDay[1], out _) ? Int32.Parse(yearMonthDay[1]) : -1;
            int day = Int32.TryParse(yearMonthDay[2], out _) ? Int32.Parse(yearMonthDay[2]) : -1; ;

            string[] yearMonthDayNow = DateTime.Now.ToString().Substring(0, 10).Split('/'); //DateTime.Now format: DD/MM/YYYY
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
            string[] now = DateTime.Now.ToString().Substring(0, 10).Split('/');
            string newFormat = now[2] + '-' + now[1] + '-' + now[0];
            return DateTime.Parse(newFormat);
        }
    }
}
