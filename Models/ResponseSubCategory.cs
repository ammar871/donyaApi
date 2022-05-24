using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class ResponseSubCategory
    {
        public SubCategory SubCategory { get; set; }

        public int Conter { get; set; }
    }
}






//  http -host-header=rewrite localhost:5000