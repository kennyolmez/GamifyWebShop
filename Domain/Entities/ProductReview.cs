using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductReview
    {
        public ProductReview(int rating, string comment, string userId, string userEmail)
        {
            Rating = rating;
            Comment = comment;
            UserId = userId;
            UserEmail = userEmail;
        }
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string? UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
