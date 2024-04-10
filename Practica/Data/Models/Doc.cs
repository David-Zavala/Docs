using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica.Data.Models
{
    public class Doc
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Count { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RegisteredEmail { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string DocPath { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
    }
}