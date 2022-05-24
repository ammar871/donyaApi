using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class AddsUpdateDto
    {




        public int CategoryId { get; set; }
        public string Title { get; set; }



        public string Address { get; set; }


        public string HomeCategory { get; set; }



        public string SubCategory { get; set; }



        public string Desc { get; set; }


        public string AdvertiserName { get; set; }


        public string Phone { get; set; }



        public string PhoneWhats { get; set; }


        public bool IsImage { get; set; }


        public int Status { get; set; }



        public string UserId { get; set; }
        public string Country { get; set; }

        public string lat { get; set; }

        public string lang { get; set; }


        public string Images { get; set; }

        public DateTime date { get; set; }

    }
}