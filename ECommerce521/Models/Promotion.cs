namespace ECommerce521.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ValidTo { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool isValid { get; set; } = true;
        public int MaxUsage { get; set; } = 100;
    }
}
