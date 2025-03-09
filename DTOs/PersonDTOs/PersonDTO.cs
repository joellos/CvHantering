using CvHantering.DTOs.EducationDTO;
using CvHantering.DTOs.ExperienceDTO;
using CvHantering.Models;
using System.ComponentModel.DataAnnotations;

namespace CvHantering.DTOs.PersonDTOs
{
    public class PersonDTO
    {

        [Required]
        [MaxLength(50)]
        public string? PersonName { get; set; }
        [Required]
        [EmailAddress]
        public string? PersonEmail { get; set; }
        [Required]
        [MaxLength(11)]
        public string? PersonPhone { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? PersonDescription { get; set; }

        public List<EducationDto>? Educations { get; set; }
        public List<ExperienceDto>? Experiences { get; set; }
    }
}
