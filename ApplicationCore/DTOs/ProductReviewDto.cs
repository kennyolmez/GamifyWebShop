using Domain.Entities;

namespace ApplicationCore.DTOs
{
    public class ProductReviewDto
    {
        public ProductReviewDto(ProductReview review)
        {
            Comment = review.Comment;
            Rating = review.Rating;
            ProductId = review.ProductId;
            UserEmail = review.UserEmail;
            UserId = review.UserId;
            DatePosted = review.DatePosted;
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public string? UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime DatePosted { get; set; }
    }
}