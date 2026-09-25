using System.ComponentModel.DataAnnotations;

namespace ECommerce521.ViewModels
{
    public class ForgetPasswordVM
    {
        public int Id { get; set; }

        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;
    }
}
