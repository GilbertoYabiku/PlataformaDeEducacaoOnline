namespace PlataformaDeEducacaoOnline.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool? Deletado { get; set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}