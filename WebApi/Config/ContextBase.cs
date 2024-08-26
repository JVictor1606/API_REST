using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Config
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) 
        {
        }

        public DbSet<ProductModel> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string GetConectionString()
        {
            return "Server=DESKTOP-BFQ0RL6\\SQLEXPRESS;Database=ApiIdentityJwT;User Id=sa;Password=jvictor10;TrustServerCertificate=True;";
        }


    }
}
