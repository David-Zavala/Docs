using Practica.Data.Models;
using Practica.Data.Repositories;
using Practica.Data.Respositories;
using Practica.Models.FormModels;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class DocController : Controller
    {
        private readonly DocsRepository docsR = new DocsRepository();
        private readonly UsersRepository usersR = new UsersRepository();

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(DocForm docData)
        {
            string actualEmail = "d@d.com"; /*Session["Email"]?.ToString();*/
            string birthDate = docData.Year.ToString() + "-" + docData.Month.ToString() + "-" + docData.Day.ToString();
            string createdId = actualEmail + "_" + docData.Name + "_" + GetDateTimeNowAsString();
            string createdDocPath = "~/Data/SavedFiles/" + createdId + "." + docData.FileExtension;

            Doc mappedDoc = new Doc
            {
                Id = createdId,
                User = await usersR.GetUserForDoc(actualEmail),
                Name = docData.Name,
                Email = actualEmail,
                RegisteredEmail = docData.Email,
                BirthDate = DateTime.Parse(birthDate),
                Age = GetAge(birthDate),
                Education = docData.EducationLevel + " " + docData.EducationProgress,
                DocPath = createdDocPath,
                LastUpdate = GetDateTimeNow(),
            };

            /* Intentar guardar lainformación en la base de datos */
            try
            {
                Doc registeredDoc = await docsR.RegisterDoc(mappedDoc);
                //if (registeredDoc.Id == "-1") throw new Exception("Hubo un error en el repositorio");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al registrar en la base de datos: " + ex.Message });
            }

            /* Intentar guardar el archivo en el directorio correspondiente */
            HttpPostedFileBase file = docData.Doc;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string rutaArchivo = Server.MapPath(createdDocPath);
                    file.SaveAs(rutaArchivo);
                }
                else
                {
                    return Json(new { success = false, message = "No se ha enviado ningún archivo o llego nulo." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar el archivo: " + ex.Message });
            }

            /* Si todo lo demas sale bien se llega a este punto */
            return Json(new { success = true, message = "Registro realizado con éxito." });
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
        private string GetDateTimeNowAsString()
        {
            DateTime now = DateTime.Now;
            string fecha = now.ToString("dd-MM-yyyy-HH-mm-ss");
            return fecha;
        }
        private DateTime GetDateTimeNow()
        {
            string[] now = DateTime.Now.ToString().Substring(0, 10).Split('/');
            string newFormat = now[2] + '-' + now[1] + '-' + now[0];
            return DateTime.Parse(newFormat);
        }
    }
}
