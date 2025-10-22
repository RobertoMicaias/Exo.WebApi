using Microsoft.EntityFrameworkCore;
using Exo.WebApi.Models;
using Microsoft.Extensions.Configuration;

namespace Exo.WebApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext() { }

        public ExoContext(DbContextOptions<ExoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Lê a connection string direto do appsettings.json
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure() // adiciona resiliência a falhas de conexão
                );
            }
        }

        public DbSet<Projeto> Projetos { get; set; }
    }
}
