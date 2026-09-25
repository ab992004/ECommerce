namespace ECommerce521.Models
{
    public class ApplicationUserOTP
    {
        public int Id { get; set; }
        public string OTP { get; set; }
        public bool IsValid { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ValidTo { get; set; } = DateTime.UtcNow.AddDays(1);
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
