using System.Collections.Generic;

namespace Practica.Models
{
    public class Pagination<T> where T : class
    {
        public int ActualPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalePages { get; set; }
        public string Filter { get; set; }
        public List<T> Result { get; set; }
    }
}