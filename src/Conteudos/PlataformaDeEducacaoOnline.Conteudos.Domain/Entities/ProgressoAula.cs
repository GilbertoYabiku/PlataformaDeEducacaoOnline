using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

public class ProgressoAula : BaseEntity
{
    public Guid AlunoId { get; private set; }
    public Guid AulaId { get; private set; }
    public EnumAulaStatus Status { get; private set; }
    public Aula Aula { get; set; }

    protected ProgressoAula() { }

    public ProgressoAula(Guid alunoId, Guid aulaId)
    {
        AlunoId = alunoId;
        AulaId = aulaId;
        Status = EnumAulaStatus.NaoIniciada;
        Validar();
    }
    public void EmAndamento() => Status = EnumAulaStatus.EmAndamento;
    public void ConcluirAula() => Status = EnumAulaStatus.Concluida;

    public void Validar()
    {
        if (AlunoId == Guid.Empty)
            throw new DomainException("O ID do aluno não pode ser vazio.");
        if (AulaId == Guid.Empty)
            throw new DomainException("O ID da aula não pode ser vazio.");
    }
}