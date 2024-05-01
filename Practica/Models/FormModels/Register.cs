﻿using System.ComponentModel.DataAnnotations;

namespace Practica.Models.FormModels
{
    public class Register
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get;  set; }
        [Required]
        public bool AdminRole { get; set; }
    }
}