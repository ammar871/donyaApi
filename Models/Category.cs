using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{


    public class Category
    {



        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(250)]
        public string NameArbice { get; set; }


        [Required]
        public string NameEnglish { get; set; }


        [Required]
        public string NameFrance { get; set; }


        public string Image { get; set; }
    }
}