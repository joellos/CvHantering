using System.ComponentModel.DataAnnotations;

namespace CvHantering.DTOs.PersonDTOs
{
    public class PersonDTOCreate
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
    }
}
