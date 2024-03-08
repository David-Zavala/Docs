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
        public int Id { get; set; } = -1;
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string BirthDate { get; set; } = "";
        [Required]
        public string Education { get; set; } = "";
        [Required]
        public string DocPath { get; set; } = "";
    }
}