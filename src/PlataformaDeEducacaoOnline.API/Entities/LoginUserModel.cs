using System.ComponentModel.DataAnnotations;

namespace PlataformaDeEducacaoOnline.API.Entities;

public class LoginUserModel
{
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "Campo {0} está em formato inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public string? Senha { get; set; }
}