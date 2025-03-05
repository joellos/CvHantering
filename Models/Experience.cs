using System.ComponentModel.DataAnnotations;

namespace CvHantering.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Company { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(0, 100)]
        public int Years { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public Person Person { get; set; }
    }
}
