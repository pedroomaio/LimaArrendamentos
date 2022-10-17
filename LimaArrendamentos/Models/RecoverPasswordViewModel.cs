using System.ComponentModel.DataAnnotations;

namespace LimaArrendamentos.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
