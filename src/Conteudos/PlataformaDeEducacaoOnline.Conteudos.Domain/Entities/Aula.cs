using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

public class Aula : BaseEntity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Conteudo { get; private set; }
    public Guid CursoId { get; private set; }

    private readonly List<Material> _materiais = [];
    public IReadOnlyCollection<Material> Materiais => _materiais;
    public Curso? Curso { get; private set; }
    protected Aula() {}

    public Aula(string nome, string conteudo)
    {
        Nome = nome;
        Conteudo = conteudo;
        Validar();
    }

    public void AssociarCurso(Guid cursoId)
    {
        CursoId = cursoId;
    }

    public void AdicionarMaterial(Material material)
    {
        if (MaterialExistente(material))
            throw new DomainException("Material já associado a esta aula.");

        material.AssociarAula(Id);
        _materiais.Add(material);
    }

    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new DomainException("O nome da aula é obrigatório.");

        if (string.IsNullOrWhiteSpace(Conteudo))
            throw new DomainException("O conteúdo da aula é obrigatório.");
    }

    private bool MaterialExistente(Material material)
    {
        return _materiais.Any(m => m.Id == material.Id);
    }
}