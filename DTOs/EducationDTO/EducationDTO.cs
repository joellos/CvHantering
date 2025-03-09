using CvHantering.Models;
using System.ComponentModel.DataAnnotations;

namespace CvHantering.DTOs.EducationDTO
{
    public class EducationDto
    {
        [Required]
        [MaxLength(50)]
        public string? EducationSchool { get; set; }
        [Required]
        [MaxLength(100)]
        public string? EducationDegree { get; set; }
        [Required]

        public DateTime EducationStartDate { get; set; }
        [Required]

        public DateTime EducationEndDate { get; set; }

       
    }
}
