using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;

namespace Fivet.Dao
{

    public class FivetContext : DbContext
    {

        public DbSet<Persona> Personas {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=fivet.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Persona>( p => 
            {

                // Primary 
                p.HasKey(p => p.uid);

                // Index in rut
                p.Property(p => p.rut).IsRequired();
                p.HasIndex(p => p.rut).IsUnique();
                // Index in Email
                p.Property(p => p.email).IsRequired();
                p.HasIndex(p => p.email).IsUnique();
            });

            // Insert the data
            modelBuilder.Entity<Persona>().HasData(
                new Persona(){
                    uid = 1,
                    rut = "193982336",
                    nombre = "Ignacio",
                    apellido = "Fuenzalida",
                    direccion = "Linares 2656",
                    email = "fuenzalida.veas@gmail.com"
                }

            );


        }


    }


}

