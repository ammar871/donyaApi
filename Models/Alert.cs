using System;
namespace Commander.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Alert()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
