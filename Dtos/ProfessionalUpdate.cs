using System;

namespace Commander.Models{



    public class ProfessionalUpdate{


    
        // public int Rate { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }


         public string Phone { get; set; }


        public string ProfessionId { get; set; }
        public string FullName { get; set; }

         public string Details { get; set; }

        public string UserId { get; set; }
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ProfessionalUpdate()
        {
            CreatedAt = DateTime.Now;
        }
    }
}