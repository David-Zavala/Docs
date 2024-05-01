using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class FormValidationsController : Controller
    {
        // GET: FormValidations/CheckEmail
        [HttpGet]
        public int CheckEmail(string email)
        {
            if (email == null) return 0;
            email = email.Trim();
            if (email == "") return 0;
            string emailRegexString = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]+$";
            RegexStringValidator emailRegex = new RegexStringValidator(emailRegexString);
            try { emailRegex.Validate(email); }
            catch (ArgumentException) { return 0; }

            string localPart = email.Split('@')[0];
            if (localPart.Length > 64 || localPart[0] == '.' || localPart[localPart.Length - 1] == '.') return 0;

            if (email.Length > 254) return 0;

            return 1;
        }
        [HttpGet]
        public int CheckPassword(string password)
        {
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&#.]{8,}$";
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#.]{8,}$";
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#.\-+\[\]])[A-Za-z\d@$!%*?&#.\-+\[\]]{8,}$";
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#.\-+\[\]\(\)])[A-Za-z\d@$!%*?&#.\-+\[\]\(\)]{8,}$";



            if (Regex.IsMatch(password, pattern)) return 1;
            
            return 0;
        }
        [HttpGet]
        public bool CheckNumber(NumberObj numberObj)
        {
            if (numberObj == null) return false;
            if (numberObj.Number < numberObj.Min) return false;
            if (numberObj.Number > numberObj.Max) return false;
            return true;
        }
        [HttpGet]
        public int CheckDoc(string fileType)
        {
            if (fileType == null) return 0;
            if (!(fileType == "image/jpeg" || fileType == "application/pdf")) return 0;
            return 1;

        }
        [HttpGet]
        public int CheckAlphabeticInput(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            str = str.Trim();

            string alphabeticRegexString = @"^[A-Za-zÀ-ÿ\s]+$";
            Regex alphabeticRegex = new Regex(alphabeticRegexString);
            bool result = alphabeticRegex.IsMatch(str);
            if (!result) return 0;
            return 1;
        }
        public class NumberObj
        {
            public int Number { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}