using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Practica.Data.Models;

namespace Practica.Models
{
    public class UserToReturn
    {
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public List<Doc> Docs { get; set; } = new List<Doc>();
    }
}