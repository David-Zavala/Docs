using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public List<Doc> Docs { get; set; } = new List<Doc> ();
    }
}