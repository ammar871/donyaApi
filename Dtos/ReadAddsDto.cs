using System;
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Dtos
{
    public class ReadAddsDto
    {

        public int Id { get; set; }


        public int CategoryId { get; set; }


        public string Title { get; set; }



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