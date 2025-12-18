using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities
{
    public class Aluno : Usuario
    {
        public string Nome { get; private set; }

        private readonly List<Matricula> _matriculas = [];

        private readonly List<Certificado> _certificados = [];
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        public IReadOnlyCollection<Certificado> Certificados => _certificados;

        // Ef Constructor
        protected Aluno() : base(Guid.NewGuid()) { }

        public Aluno(Guid id, string nome) : base(id)
        {
            Nome = nome;
        }

        public void AdicionarMatricula(Matricula matricula)
        {
            if (VerificarMatricula(matricula))
                throw new Exception("Não foi possível realizar a matrícula, pois ela já existe.");

            _matriculas.Add(matricula);
        }

        public void AdicionarCertificado(Certificado certificado)
        {
            if (VerificarCertificado(certificado))
                throw new Exception("Não foi possível adicionar o certificado, pois ele já existe.");

            _certificados.Add(certificado);
        }

        private bool VerificarCertificado(Certificado certificado)
        {
            return _certificados.Any(c => c.Id == certificado.Id);
        }

        private bool VerificarMatricula(Matricula matricula)
        {
            return _matriculas.Any(m => m.Id == matricula.Id);
        }
    }
}
