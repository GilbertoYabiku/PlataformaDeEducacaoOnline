using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities
{
    public class Matricula : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public DateTime? DataMatricula { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public Guid StatusId { get; private set; }
        public Aluno Aluno { get; set; }
        public StatusMatricula Status { get; private set; }
        protected Matricula() { }
        public Matricula(Guid alunoId, Guid cursoId, StatusMatricula status)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            Validar();
            Iniciar(status);
        }
        public void Iniciar(StatusMatricula status)
        {
            if (status?.Codigo != ((int)EnumMatricula.Iniciada))
                throw new DomainException("A matrícula deve estar com o status 'Iniciada'.");

            AssociarStatus(status);
        }

        public void Ativar(StatusMatricula status)
        {
            if (status?.Codigo != (int)EnumMatricula.Ativa)
                throw new DomainException("A matrícula deve estar com o status 'Ativa'.");

            AssociarStatus(status);
            DataMatricula = DateTime.Now;
        }
        public void Concluir(StatusMatricula status)
        {
            if (status?.Codigo != (int)EnumMatricula.Concluida)
                throw new DomainException("A matrícula deve estar com o status 'Concluída'.");

            AssociarStatus(status);
            DataConclusao = DateTime.Now;
        }
        public void AguardandoPagamento(StatusMatricula status)
        {
            if (status?.Codigo != (int)EnumMatricula.AguardandoPagamento)
                throw new DomainException("A matrícula deve estar com o status 'Aguardando Pagamento'.");

            AssociarStatus(status);
        }

        private void AssociarStatus(StatusMatricula status)
        {
            if (status == null)
                throw new DomainException("O status da matrícula não pode ser nulo.");
            Status = status;
        }

        private void Validar()
        {
            if (AlunoId == Guid.Empty)
                throw new DomainException("O campo AlunoId é obrigatório.");
            if (CursoId == Guid.Empty)
                throw new DomainException("O campo CursoId é obrigatório.");
        }
    }
}
