using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    [Index(nameof(Admno), IsUnique = true)]
    public class Student
    {
        [Key]
        public int id { get; set; }
        public int Admno { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string course { get; set; }
        public int mark { get; set; }
        public string ImageUrl { get; set; }
    }
}
