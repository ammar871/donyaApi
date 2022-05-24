using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(250)]
        public string NameArbice { get; set; }


        [Required]
        public string NameEnglish { get; set; }


        [Required]
        public string NameFrance { get; set; }


        public string Image { get; set; }

        public DateTime date { get; set; }
    }
}






//  http -host-header=rewrite localhost:5000