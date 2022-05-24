using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CreateCategory
    {
        
         

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