using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities;

public class Usuario : BaseEntity, IAggregateRoot
{
    public Usuario(Guid Id) : base(Id) { }
}

