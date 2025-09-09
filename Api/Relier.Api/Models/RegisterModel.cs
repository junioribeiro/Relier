using System.ComponentModel.DataAnnotations;

namespace Relier.Api.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Tamanho maximo para senha de 20 caracteres")]
        [Display(Name = "Senha")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Senha")]
        [Compare("Password", ErrorMessage = "Senha não combina")]
        public required string confirmPassword { get; set; }
    }
}
