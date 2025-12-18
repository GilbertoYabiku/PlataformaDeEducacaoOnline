namespace PlataformaDeEducacaoOnline.API.Entities;

public class LoginResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenDto UserToken { get; set; }
}
public class UserTokenDto
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public IEnumerable<ClaimDto> Claims { get; set; }
}
public class ClaimDto
{
    public string Type { get; set; }
    public string Value { get; set; }
}