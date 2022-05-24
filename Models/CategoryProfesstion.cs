using System.ComponentModel.DataAnnotations;

public class CategoryProfission
{



    [Key]
    public int Id { get; set; }


    public int Status { get; set; }

    [Required]
    [MaxLength(250)]
    public string NameArbice { get; set; }


    [Required]
    public string NameEnglish { get; set; }


    [Required]
    public string NameFrance { get; set; }


    public string Image { get; set; }
}