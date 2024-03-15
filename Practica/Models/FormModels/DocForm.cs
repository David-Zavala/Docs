using Practica.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practica.Models.FormModels
{
    public class DocForm
    {
        [Required]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        // BirthDate inputs
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Day { get; set; }
        // BirthDate inputs
        
        public string Education { get; set; }
        [Required]
        public string DocPath { get; set; }
    }
}