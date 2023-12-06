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
                builder
                .UseInMemoryDatabase("roverdb")
                .UseLazyLoadingProxies();
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rover>(rover => rover
                .HasOne(rover => rover.RoverBuilder)
                .WithMany(roverBuilder => roverBuilder.Rovers)
                .OnDelete(DeleteBehavior.Cascade)
            );

            modelBuilder.Entity<RoverBuilder>(builder => builder
                .HasMany(builder => builder.Rovers)
                .WithOne(rover => rover.RoverBuilder)
                .HasForeignKey(builder => builder.RoverId)
                .OnDelete(DeleteBehavior.Cascade)
            );
            
            modelBuilder.Entity<VisitedPlaces>(VisitedPlanet => VisitedPlanet
                .HasMany(visitedplanet => visitedplanet.RoverBuilders)
                .WithOne(roverbuilder => roverbuilder.VisitedPlaces)
                .HasForeignKey(VisitedPlanet => VisitedPlanet.BuilderId)
                .OnDelete(DeleteBehavior.Cascade)
            );
            
            modelBuilder.Entity<Rover>().HasData(new Rover[]
            {
                new Rover("1#Curiosity#2011.11.26#2021.11.22#1"),
                new Rover("2#Perseverance#2020.07.30#2023.11.18#1"),
                new Rover("3#Sojourner#1997.07.01#1997.09.27#1"),
                new Rover("4#IDEFIX#2025.01.01#2025.01.02#3"),
                new Rover("5#Dragonfly#2034.01.01#2034.03.01#1"),
                new Rover("6#MASCOT#2014.12.03#2018.10.3#3"),
                new Rover("7#MINERVA-II#2005.11.12#2019.10.26#6"),
                new Rover("8#Opportunity#2004.01.25#2018.07.15#1"),
                new Rover("9#Ispace Rover#2024.01.01#2024.01.07#4"),
                new Rover("10#Zhurong#2021.05.14#2022.05.14#2"),
                new Rover("11#Pragyan#2019.01.06#2019.01.15#5")
                
            });
            
            modelBuilder.Entity<RoverBuilder>().HasData(new RoverBuilder[] {
                new RoverBuilder("1#NAS#2"),
                new RoverBuilder("2#CNSA#2"),
                new RoverBuilder("3#DLR#3"),
                new RoverBuilder("4#ispace Europe#1"),
                new RoverBuilder("5#ISRO#1"),
                new RoverBuilder("6#JAXA#3")
            });
            
            modelBuilder.Entity<VisitedPlaces>().HasData(new VisitedPlaces[] {
                new VisitedPlaces("1#Moon#Moon#384000#"),
                new VisitedPlaces("2#Mars#Planet#225000000#"),
                new VisitedPlaces("3#Asteroids#Asteroids#1#"),
                new VisitedPlaces("4#Titan#Moon#1200000000#")
            });
            
        }

    }
}
