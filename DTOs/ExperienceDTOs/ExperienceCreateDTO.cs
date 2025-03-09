using CvHantering.Models;
using System.ComponentModel.DataAnnotations;

namespace CvHantering.DTOs.ExperienceDTO
{
    public class ExperienceCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string? ExperienceCompany { get; set; }
        [Required]
        [MaxLength(50)]
        public string? ExperienceTitle { get; set; }
        [Required]
        [Range(0, 100)]
        public int ExperienceYears { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? ExperienceDescription { get; set; }

        public Person? Person { get; set; }


    }
}
