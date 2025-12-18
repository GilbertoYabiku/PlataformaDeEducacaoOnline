using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Alunos.Data.Context;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.API.Data;
using PlataformaDeEducacaoOnline.API.Entities;
using PlataformaDeEducacaoOnline.Conteudos.Data.Context;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Application.Models;
using PlataformaDeEducacaoOnline.Financeiro.Data.Context;

namespace PlataformaDeEducacaoOnline.API.Configurations;

public static class DbMigrationHelpers
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        EnsureSeedData(app).Wait();
    }

    public static async Task EnsureSeedData(WebApplication application)
    {
        var service = application.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(service);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var contextConteudos = scope.ServiceProvider.GetRequiredService<ConteudosContext>();
        var contextAlunos = scope.ServiceProvider.GetRequiredService<AlunosContext>();
        var contextIdentity = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var contextFinanceiro = scope.ServiceProvider.GetRequiredService<FinanceiroContext>();
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

        if (env.IsDevelopment() || env.IsEnvironment("Testing"))
        {   
            await contextAlunos.Database.EnsureDeletedAsync();
            await contextConteudos.Database.EnsureDeletedAsync();
            await contextIdentity.Database.EnsureDeletedAsync();
            await contextFinanceiro.Database.EnsureDeletedAsync();

            await contextConteudos.Database.MigrateAsync();
            await contextAlunos.Database.MigrateAsync();
            await contextIdentity.Database.MigrateAsync();
            await contextFinanceiro.Database.MigrateAsync();

            await SeedUsersAndRoles(scope.ServiceProvider);
            await SeedDataInitial(contextAlunos, contextConteudos, contextIdentity, contextFinanceiro);
        }
    }

    private static async Task SeedUsersAndRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roles = new List<string>() { "ADMIN", "ALUNO" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var user = new IdentityUser
        {
            Email = "aluno@teste.com",
            EmailConfirmed = true,
            UserName = "aluno@teste.com",
        };

        var user2 = new IdentityUser
        {
            Email = "aluno2@teste.com",
            EmailConfirmed = true,
            UserName = "aluno2@teste.com",
        };

        var userAdmin = new IdentityUser
        {
            Email = "admin@teste.com",
            EmailConfirmed = true,
            UserName = "admin@teste.com",
        };

        await userManager.CreateAsync(user, "Teste@123");
        await userManager.CreateAsync(user2, "Teste@123");
        await userManager.CreateAsync(userAdmin, "Teste@123");

        await userManager.AddToRoleAsync(user, "ALUNO");
        await userManager.AddToRoleAsync(user2, "ALUNO");
        await userManager.AddToRoleAsync(userAdmin, "ADMIN");
    }

    private static async Task SeedDataInitial(
         AlunosContext dbAlunosContext,
         ConteudosContext dbConteudosContext,
         ApplicationContext dbApplicationContext,
         FinanceiroContext dbPagamentoContext)
    {
        if (dbAlunosContext.Set<Aluno>().Any() || dbAlunosContext.Set<Alunos.Domain.Entities.Matricula>().Any() || dbAlunosContext.Set<Usuario>().Any())
            return;
        if (dbConteudosContext.Set<Conteudos.Domain.Entities.Curso>().Any() || dbConteudosContext.Set<Conteudos.Domain.Entities.Aula>().Any())
            return;

        var user = await dbApplicationContext.Users.FirstOrDefaultAsync(x => x.Email == "aluno@teste.com");
        var user2 = await dbApplicationContext.Users.FirstOrDefaultAsync(x => x.Email == "aluno2@teste.com");
        var userAdmin = await dbApplicationContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@teste.com");

        var admin = new Usuario(Guid.Parse(userAdmin.Id));
        var aluno = new Aluno(Guid.Parse(user.Id), "fulano");
        var aluno2 = new Aluno(Guid.Parse(user2.Id), "fulano2");

        // CURSO 1 - C#
        var curso = new Conteudos.Domain.Entities.Curso("Curso C#", "Teste", admin.Id, 250);
        var aula = new Conteudos.Domain.Entities.Aula("Aula 1", "Teste");
        var aula2 = new Conteudos.Domain.Entities.Aula("Aula 2", "Teste");
        var aula3 = new Conteudos.Domain.Entities.Aula("Aula 3", "Teste");
        curso.AdicionarAula(aula);
        curso.AdicionarAula(aula2);
        curso.AdicionarAula(aula3);

        // CURSO 2 - Angular 
        var curso2 = new Conteudos.Domain.Entities.Curso("Angular", "Teste", admin.Id, 150);
        var aula4 = new Conteudos.Domain.Entities.Aula("Aula 1", "Teste");
        var aula5 = new Conteudos.Domain.Entities.Aula("Aula 2", "Teste");
        var aula6 = new Conteudos.Domain.Entities.Aula("Aula 3", "Teste");
        curso2.AdicionarAula(aula4);
        curso2.AdicionarAula(aula5);
        curso2.AdicionarAula(aula6);

        // StatusMatricula
        var statusIniciada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Iniciada,
            Descricao = "Iniciada"
        };

        var statusAguardandoPag = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.AguardandoPagamento,
            Descricao = "Aguardando Pagamento"
        };
        var statusAtiva = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Ativa,
            Descricao = "Ativa"
        };
        var statusConcluida = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Concluida,
            Descricao = "Concluída"
        };
        var statusCancelada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Cancelada,
            Descricao = "Cancelada"
        };

        // Matriculas
        var matriculaAtiva = new Alunos.Domain.Entities.Matricula(aluno2.Id, curso.Id, statusIniciada);
        matriculaAtiva.Ativar(statusAtiva);

        var matriculaAguardando = new Alunos.Domain.Entities.Matricula(aluno.Id, curso.Id, statusIniciada);
        matriculaAguardando.AguardandoPagamento(statusAguardandoPag);

        var matriculaConcluida = new Alunos.Domain.Entities.Matricula(aluno.Id, curso2.Id, statusIniciada);
        matriculaConcluida.Concluir(statusConcluida);

        // Progresso das aulas para a matrícula concluída
        var progressoAula1 = new ProgressoAula(aluno.Id, aula4.Id);
        progressoAula1.ConcluirAula();
        var progressoAula2 = new ProgressoAula(aluno.Id, aula5.Id);
        progressoAula2.ConcluirAula();
        var progressoAula3 = new ProgressoAula(aluno.Id, aula6.Id);
        progressoAula3.ConcluirAula();

        // Progresso das aulas para a matrícula ativa
        var progressoAulaAtiva1 = new ProgressoAula(aluno2.Id, aula.Id);
        progressoAulaAtiva1.EmAndamento();

        // Progresso do curso concluído
        var progressoCursoConcluido = new ProgressoCurso(curso2.Id, aluno.Id, curso2.Aulas.Count);
        progressoCursoConcluido.IncrementarProgresso(); // aula4
        progressoCursoConcluido.IncrementarProgresso(); // aula5
        progressoCursoConcluido.IncrementarProgresso(); // aula6

        // Certificado para o aluno
        var certificado = new Certificado(aluno.Nome, curso2.Nome, matriculaConcluida.Id, aluno.Id, matriculaConcluida.DataConclusao);

        // Pagamento
        var pagamento = new Pagamento
        {
            AlunoId = aluno2.Id,
            CursoId = curso.Id,
            NomeCartao = "Nome do Cartão",
            NumeroCartao = "5502093788528294",
            ExpiracaoCartao = "12/25",
            CvvCartao = "455",
            Valor = curso.Preco,
        };
        var transacao = new Transacao
        {
            PagamentoId = pagamento.Id,
            MatriculaId = matriculaAtiva.Id,
            StatusTransacao = StatusTransacao.Pago,
            Pagamento = pagamento,
            Total = pagamento.Valor,
        };

        await dbAlunosContext.Set<Aluno>().AddRangeAsync([aluno, aluno2]);
        await dbAlunosContext.Set<Usuario>().AddAsync(admin);
        await dbAlunosContext.Set<StatusMatricula>().AddRangeAsync([
            statusIniciada, statusAtiva, statusAguardandoPag, statusConcluida, statusCancelada
        ]);
        await dbAlunosContext.Set<Alunos.Domain.Entities.Matricula>().AddRangeAsync([matriculaAtiva, matriculaAguardando, matriculaConcluida]);
        await dbAlunosContext.Set<Certificado>().AddAsync(certificado);

        await dbConteudosContext.Set<Conteudos.Domain.Entities.Curso>().AddRangeAsync([curso, curso2]);
        await dbConteudosContext.Set<Conteudos.Domain.Entities.Aula>().AddRangeAsync([aula, aula2, aula3, aula4, aula5, aula6]);
        await dbConteudosContext.Set<ProgressoAula>().AddRangeAsync([progressoAula1, progressoAula2, progressoAula3, progressoAulaAtiva1]);
        await dbConteudosContext.Set<ProgressoCurso>().AddAsync(progressoCursoConcluido);

        await dbPagamentoContext.Set<Pagamento>().AddAsync(pagamento);
        await dbPagamentoContext.Set<Transacao>().AddAsync(transacao);

        await dbAlunosContext.SaveChangesAsync();
        await dbConteudosContext.SaveChangesAsync();
        await dbPagamentoContext.SaveChangesAsync();
    }
}