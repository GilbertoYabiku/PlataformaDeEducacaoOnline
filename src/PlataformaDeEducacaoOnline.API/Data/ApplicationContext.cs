using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlataformaDeEducacaoOnline.API.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext(options)
{
}