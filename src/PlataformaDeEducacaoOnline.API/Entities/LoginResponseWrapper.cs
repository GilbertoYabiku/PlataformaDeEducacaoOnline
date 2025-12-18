namespace PlataformaDeEducacaoOnline.API.Entities;

public class LoginResponseWrapper
{
    public bool Sucesso { get; set; }
    public LoginResponse Data { get; set; } = new();
}