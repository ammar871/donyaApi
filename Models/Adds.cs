using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Adds
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }



        [Required]
        public string Address { get; set; }


        [Required]
        public string HomeCategory { get; set; }


        [Required]
        public string SubCategory { get; set; }


        [Required]
        public string Desc { get; set; }


        [Required]
        public string AdvertiserName { get; set; }


        [Required]
        public string Phone { get; set; }


        [Required]
        public string PhoneWhats { get; set; }


        public bool IsImage { get; set; }


        public int Status { get; set; }


        [Required]
        public string UserId { get; set; }
        public string Country { get; set; }

         public string Token { get; set; }

        public string lat { get; set; }

        public string lang { get; set; }


        public string Images { get; set; }

        public DateTime date { get; set; }

        public Adds()
        {

            date = DateTime.Now;
        }

    }
}