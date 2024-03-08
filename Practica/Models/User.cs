using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public List<Doc> Docs { get; set; }
    }
}