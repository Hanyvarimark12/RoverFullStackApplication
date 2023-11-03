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
            //modelBuilder.Entity<Rover>()
            //.HasOne(Rover => Rover.RoverBuilders)
            //.WithMany(roverbuilder => roverbuilder.Rovers)
            

            //modelBuilder.Entity<Rover>()
            //   .HasOne(Rover => Rover.VisitedPlaces)
            //   .WithMany(VisitedPlaces => VisitedPlaces.Rovers);

            modelBuilder.Entity<RoverBuilder>(RoverBuilder => RoverBuilder.HasMany<VisitedPlaces>()
                .WithMany(VisitedPlaces => VisitedPlaces.RoverBuilders)
                .UsingEntity<Rover>(
                    x => x.HasOne(x => x.VisitedPlaces)
                        .WithMany().HasForeignKey(x => x.VisitedPlaceId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.RoverBuilders)
                        .WithMany().HasForeignKey(x => x.BuilderId).OnDelete(DeleteBehavior.Cascade))
            );
            //1 visited place with more roverb
            //1 roverbuilder to more places
            //rover connects VisitedPlace to RoverBuilder 
            modelBuilder.Entity<VisitedPlaces>(VisitedPlanet => VisitedPlanet.HasMany<RoverBuilder>()
            .WithMany(RoverBuilder => RoverBuilder.VisitedPlaces)
            .UsingEntity<Rover>(
                x => x.HasOne(x => x.RoverBuilders)
                    .WithMany().HasForeignKey(x => x.BuilderId).OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne(x => x.VisitedPlaces)
                    .WithMany().HasForeignKey(x => x.VisitedPlaceId).OnDelete(DeleteBehavior.Cascade)
                )
            );
            //RoverId#RoverName#LaunchDate#LandDate#VisitedPlaceId#BuilderId
            modelBuilder.Entity<Rover>().HasData(new Rover[]
            {
                new Rover("1#Curiosity#2011.11.26#2012.08.06#1#1"),
                new Rover("2#Perseverance#2020.07.30#2021.02.18#1#1"),
                new Rover("3#Sojourner##1997.07.01##1#1"),
                new Rover("4#MMX#2025.01.01#2025.01.01#3#-1"),
                new Rover("5#Dragonfly#2034.01.01#2034.01.01#1#-1")
            });
            //BuilderId#BuilderName
            modelBuilder.Entity<RoverBuilder>().HasData(new RoverBuilder[] {
                new RoverBuilder("1#NAS"),
                new RoverBuilder("2#CNSA"),
                new RoverBuilder("3#DLR"),
                new RoverBuilder("4#ispace Europe"),
                new RoverBuilder("5#ISRO"),
                new RoverBuilder("6#JAXA")
            });
            //rover roverBuilder MM, VisitedPlaces rover, OM, 
            //PlaceId#PlanetName#PlanetType#Distance#RoverId
            modelBuilder.Entity<VisitedPlaces>().HasData(new VisitedPlaces[] {
                new VisitedPlaces("1#Moon#Moon#384000#"),
                new VisitedPlaces("2#Mars#Planet#225000000#"),
                new VisitedPlaces("3#Asteroids#Asteroids#-1#"),
                new VisitedPlaces("4#Titan#Moon#1200000000#")
            });
            
        }

    }
}
