using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEAIC6_HFT_2023241.Models;
using System.IO;

namespace DEAIC6_HFT_2023241.Repository
{
    public class RoverDbContext : DbContext
    {
        public DbSet<Rover> Rovers { get; set; }
        public DbSet<RoverBuilder> roverBuilders { get; set; }
        public DbSet<VisitedPlaces> visitedPlaces { get; set; }

        public RoverDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hanyv\source\repos\DEAIC6_HFT_2023241\DEAIC6_HFT_2023241.Repository\Rovers.mdf;Integrated Security=True
                string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:|DataDirectory|Rovers.mdf;Integrated Security=True";
                builder
                .UseInMemoryDatabase(conn)
                .UseLazyLoadingProxies();
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rover>(Rover => Rover.HasOne<RoverBuilder>()
            .WithMany()
            .HasForeignKey(Rover => Rover.BuilderId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<RoverBuilder>(RoverBuilder => RoverBuilder.HasMany<Rover>()
                .WithOne()
            );

            modelBuilder.Entity<VisitedPlaces>(VisitedPlanet => VisitedPlanet.HasMany<Rover>()
            .WithOne()
            .HasForeignKey(VisitedPlanet => VisitedPlanet.RoverId)
            .OnDelete(DeleteBehavior.SetNull)
            );
            //RoverId#RoverName#LaunchDate#LandDate#RoverBuilder#VisitedPlaces#BuilderId#RoverStatus
            modelBuilder.Entity<Rover>().HasData(new Rover[]
            {
                new Rover("1#Curiosity#2011.11.26#2012.08.06#NAS#1#1#1"),
                new Rover("2#Perseverance#2020.07.30#2021.02.18#NAS#1#1#1"),
                new Rover("3#Sojourner##1997.07.01#NAS#1#1#1"),
                new Rover("4#MMX#2025.01.01#2025.01.01#DLR#0#3#-1"),
                new Rover("5#Dragonfly#2034.01.01#2034.01.01#NAS#0#1#-1")
            });
            
        }

    }
}
