using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    public class Doc
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Education { get; set; }
        public string DocPath { get; set; }
    }
}