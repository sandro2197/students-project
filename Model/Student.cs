using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
