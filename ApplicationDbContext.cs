using Microsoft.EntityFrameworkCore;
using webapicurso.Models;
public class ApplicationDbContext : DbContext 
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}