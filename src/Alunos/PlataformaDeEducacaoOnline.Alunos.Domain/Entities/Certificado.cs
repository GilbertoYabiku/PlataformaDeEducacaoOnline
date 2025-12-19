using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities
{
    public class Certificado : BaseEntity
    {
        public string NomeAluno { get; private set; }
        public string NomeCurso { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public string Descricao { get; private set; }
        public string Arquivo { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Aluno Aluno { get; set; }
        public Matricula Matricula { get; set; }
        protected Certificado() { }
        public Certificado(string nomeAluno, string nomeCurso, Guid matriculaId, Guid alunoId, DateTime? dataConclusao)
        {
            NomeAluno = nomeAluno;
            NomeCurso = nomeCurso;
            MatriculaId = matriculaId;
            AlunoId = alunoId;
            DataEmissao = DateTime.Now;
            DataConclusao = dataConclusao;
            GerarDescricao();
            Validar();
        }

        private void GerarDescricao()
        {
            Descricao = $"Certificamos que o(a) aluno(a) {NomeAluno} concluiu o curso {NomeCurso} com sucesso no dia {DataConclusao:dd/MM/yyyy}.";
        }

        public void AdicionarArquivo(string arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                throw new Exception("Arquivo do certificado inválido.");

            Arquivo = arquivo;
        }

        public void Validar()
        {
            if (AlunoId == Guid.Empty)
                throw new Exception("O campo AlunoId é obrigatório.");
            if (MatriculaId == Guid.Empty)
                throw new Exception("O campo MatriculaId é obrigatório.");
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new Exception("O campo Descrição é obrigatório.");
            if (string.IsNullOrWhiteSpace(NomeAluno))
                throw new Exception("O campo Nome Aluno é obrigatório.");
            if (string.IsNullOrWhiteSpace(NomeCurso))
                throw new Exception("O campo Nome Curso é obrigatório.");
            if (!DataConclusao.HasValue)
                throw new Exception("O campo Data Conclusão é obrigatório.");
        }
    }
}
