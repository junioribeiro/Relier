using System.ComponentModel.DataAnnotations;

namespace Relier.Api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email deve estar no formato valido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatorio")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Tamanho maximo para senha de 20 caracteres")]
        public required string Password { get; set; }
    }
}
