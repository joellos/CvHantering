using System.ComponentModel.DataAnnotations;

namespace CvHantering.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(11)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public List<Education> Educations { get; set; }
    }
}
