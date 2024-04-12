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
        public List<Doc> Docs { get; set; } = new List<Doc> ();
        [Required]
        public DateTime LastUpdate { get; set; }
        [Required]
        public bool AdminRole { get; set; }
    }
}