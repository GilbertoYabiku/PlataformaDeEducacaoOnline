using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

public class ProgressoCurso : BaseEntity
{
    public Guid CursoId { get; private set; }
    public Guid AlunoId { get; private set; }
    public int TotalAulas { get; private set; }
    public int AulasConcluidas { get; private set; }
    public decimal PercentualConcluido { get; private set; }
    public bool CursoConcluido => PercentualConcluido == 100m;
    public Curso Curso { get; set; }

    protected ProgressoCurso() { }

    public ProgressoCurso(Guid cursoId, Guid alunoId, int totalAulas)
    {
        CursoId = cursoId;
        AlunoId = alunoId;
        TotalAulas = totalAulas;
        AulasConcluidas = 0;
        PercentualConcluido = 0m;
    }

    public void IncrementarProgresso()
    {
        if (AulasConcluidas < TotalAulas)
            AulasConcluidas++;

        AtualizarPercentual();
    }

    private void AtualizarPercentual()
    {
        PercentualConcluido = TotalAulas == 0 ? 0m : Math.Round((decimal)AulasConcluidas / TotalAulas * 100, 2);
    }
}