namespace PlataformaDeEducacaoOnline.API.Entities;

public class Curso
{   
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Conteudo { get; set; }
    public decimal Preco { get; set; }
}

public class CursoNovoDto
{
    public string Nome { get; set; }
    public string Conteudo { get; set; }
    public decimal Preco { get; set; }
}