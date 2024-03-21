using System;
using System.Configuration;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class FormValidationsController : Controller
    {
        // GET: FormValidations/CheckEmail
        [HttpGet]
        public bool CheckEmail(string email)
        {
            string emailRegexString = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]+$";
            RegexStringValidator emailRegex = new RegexStringValidator(emailRegexString);
            try { emailRegex.Validate(email); }
            catch (ArgumentException) { return false; }

            string localPart = email.Split('@')[0];
            if (localPart.Length > 64 || localPart[0] == '.' || localPart[localPart.Length - 1] == '.') return false;

            if (email.Length > 254) return false;

            return true;
        }
        [HttpGet]
        public bool CheckNumber(NumberObj numberObj)
        {
            if (numberObj == null) return false;
            if (numberObj.Number < numberObj.Min) return false;
            if (numberObj.Number > numberObj.Max) return false;
            return true;
        }
        public class NumberObj
        {
            public int Number { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}