using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CvHantering.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string School { get; set; }
        [Required]
        [MaxLength(100)]
        public string Degree { get; set; }
        [Required]
        []
        public DateTime StartDate { get; set; }
        [Required]
        
        public DateTime EndDate { get; set; }

        public Person Person { get; set; }
    }
}
