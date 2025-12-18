using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Alunos.Data.Context;
using PlataformaDeEducacaoOnline.API.Data;
using PlataformaDeEducacaoOnline.Conteudos.Data.Context;
using PlataformaDeEducacaoOnline.Financeiro.Data.Context;

namespace PlataformaDeEducacaoOnline.API.Configurations;

public static class DbContextConfiguration
{
    public static WebApplicationBuilder AddDbContextConfiguration(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            builder.Services.AddDbContext<ConteudosContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AlunosContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);
            builder.Services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<FinanceiroContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
        else
        {
            builder.Services.AddDbContext<ConteudosContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging();
            });
            builder.Services.AddDbContext<AlunosContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging();
            });
            builder.Services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging();
            });
            builder.Services.AddDbContext<FinanceiroContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging();
            });
        }
       
        return builder;
    }
}