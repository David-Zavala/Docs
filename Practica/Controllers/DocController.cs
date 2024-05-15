using Practica.Data.Models;
using Practica.Data.Repositories;
using Practica.Data.Respositories;
using Practica.Models;
using Practica.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            string actualEmail = Session["Email"]?.ToString();
            string birthDate = docData.Year.ToString() + "-" + docData.Month.ToString() + "-" + docData.Day.ToString();
            string createdId = actualEmail + "_" + docData.Name + "_" + GetDateTimeNowAsString();
            string createdDocPath = "~/Data/SavedFiles/" + createdId + "." + docData.FileExtension;

            Doc mappedDoc = new Doc
            {
                Id = createdId,
                User = await usersR.GetFullUserByEmail(actualEmail),
                Name = docData.Name,
                Email = actualEmail,
                RegisteredEmail = docData.Email,
                BirthDate = DateTime.Parse(birthDate),
                Age = GetAge(birthDate),
                Education = docData.EducationLevel + " " + docData.EducationProgress,
                DocPath = createdDocPath,
                LastUpdate = GetDateTimeNow(),
            };

            HttpPostedFileBase file = docData.Doc;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    /* Intentar guardar la información en la base de datos */
                    Doc registeredDoc = await docsR.RegisterDoc(mappedDoc);
                    /* Intentar guardar el archivo en el directorio correspondiente */
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
                return Json(new { success = false, message = "Error al registrar en la base de datos: " + ex.Message });
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
        [HttpPost]
        public async Task<ActionResult> Delete(string fileId)
        {
            //string actualEmail = Session["Email"]?.ToString();
            //string[] fileIdParts = fileId.Split('_');
            //string fileEmail = fileIdParts.Take(fileIdParts.Length - 2).ToString();

            //UserToReturn actualUser = await usersR.GetUserByEmail(actualEmail);

            //if (actualUser == null || (actualUser.AdminRole != true && actualEmail != fileEmail)) return Json(new { success = false, message = "Usuario no valido para realizar esta acción" });

            Doc actualDoc = await docsR.GetDoc(fileId);
            if (actualDoc == null) return Json(new { success = false, message = "El documento que se busca eliminar ya no existe, contacte a un administrador." });
            try
            {
                string fileFullPath = Request.MapPath(actualDoc.DocPath);
                if (System.IO.File.Exists(fileFullPath))
                {
                    System.IO.File.Delete(fileFullPath);
                    Doc ans = await docsR.DeleteDoc(actualDoc);
                    if (ans.Id == "-1") return Json(new { success = false, message = "Hubo un error en la base de datos, contacte a un adminsitrador" });
                }
                else return Json(new { success = false, message = "El archivo no se encuentra debido a un error, contacte a un adminsitrador" });
            }
            catch
            {
                return Json(new { success = false, message = "Ocurrio un error durante el proceso de eliminación del documento, contacte a un adminsitrador" });
            }

            /* Si todo lo demas sale bien se llega a este punto */
            return Json(new { success = true, message = "Registro eliminado con éxito." });
        }
        [HttpPost]
        public async Task<ActionResult> MultipleDelete(string[] fileIds)
        {
            //string actualEmail = Session["Email"]?.ToString();
            //string[] fileIdParts = fileId.Split('_');
            //string fileEmail = fileIdParts.Take(fileIdParts.Length - 2).ToString();

            //UserToReturn actualUser = await usersR.GetUserByEmail(actualEmail);

            //if (actualUser == null || (actualUser.AdminRole != true && actualEmail != fileEmail)) return Json(new { success = false, message = "Usuario no valido para realizar esta acción" });
            
            List<string> errors = new List<string>();
            foreach (string fileId in fileIds)
            {
                Doc actualDoc = await docsR.GetDoc(fileId);
                if (actualDoc == null)
                {
                    errors.Add(fileId + ": " + "El documento que se busca eliminar ya no existe, contacte a un administrador.");
                    continue;
                }
                try
                {
                    string fileFullPath = Request.MapPath(actualDoc.DocPath);
                    if (System.IO.File.Exists(fileFullPath))
                    {
                        System.IO.File.Delete(fileFullPath);
                        Doc ans = await docsR.DeleteDoc(actualDoc);
                        if (ans.Id == "-1")
                        {
                            errors.Add(fileId + ": " + "Hubo un error en la base de datos, contacte a un adminsitrador.");
                            continue;
                        }
                    }
                    else 
                    {
                        errors.Add(fileId + ": " + "El archivo no se encuentra debido a un error, contacte a un adminsitrador.");
                        continue;
                    }
                }
                catch
                {
                    errors.Add(fileId + ": " + "Ocurrio un error durante el proceso de eliminación del documento, contacte a un adminsitrador.");
                    continue;
                }
            }
            /* Si todo lo demas sale bien se llega a este punto */
            return Json(new { success = true, message = "Registros eliminados con éxito.", notDeletedFiles = errors });
        }
        [HttpGet]
        public ActionResult DownloadImage(string filePath)
        {
            var imagePath = Server.MapPath(filePath);

            if (System.IO.File.Exists(imagePath))
            {
                Response.AddHeader("Content-Disposition", "attachment;filename=DealerAdTemplate.png");
                Response.WriteFile(imagePath);
                Response.End();
                return null;
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
