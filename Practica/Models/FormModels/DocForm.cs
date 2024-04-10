using Practica.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Practica.Models.FormModels
{
    public class DocForm
    {
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
        
        public string EducationLevel { get; set; }
        public string EducationLevelOtherOption { get; set; }
        public string EducationProgress { get; set; }
        //[Required]
        public HttpPostedFileBase Doc { get; set; }

        public string FileExtension { get; set; }
    }
}