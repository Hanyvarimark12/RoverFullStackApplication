using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Test
{
    [TestFixture]
    public class VisitedPlaceTester
    { 
        VisitedPlacesLogic logic;
        Mock<IRepository<VisitedPlaces>> mockPlanetRepo;

        [SetUp]
        public void Init()
        {
            mockPlanetRepo = new Mock<IRepository<VisitedPlaces>>();

            List<VisitedPlaces> planets = new List<VisitedPlaces>()
            {
                new VisitedPlaces("33#Planet1#Type1#100"),
                new VisitedPlaces("35#Planet2#Type2#1000")
            };

            RoverBuilder rb1 = new RoverBuilder("11#Builder1#33");
            RoverBuilder rb2 = new RoverBuilder("12#Builder1#35");

            Rover r1 = new Rover("1#Rover1#2014.01.01#2014.02.02#11");
            Rover r2 = new Rover("2#Rover2#2015.01.01#2015.02.02#11");
            Rover r3 = new Rover("3#Rover3#2016.01.01#2016.02.02#11");
            Rover r4 = new Rover("4#Rover4#2017.01.01#2017.02.02#12");

            rb1.Rovers.Add(r1);
            rb1.Rovers.Add(r2);
            rb1.Rovers.Add(r3);
            rb2.Rovers.Add(r4);

            planets[0].RoverBuilders.Add(rb1);
            planets[1].RoverBuilders.Add(rb2);


            mockPlanetRepo.Setup(m => m.ReadAll()).Returns(planets.AsQueryable());
            logic = new VisitedPlacesLogic(mockPlanetRepo.Object);
        }

        [Test]
        public void MostRoverNumberTester()
        {
            var result = logic.MostRoverNumber();
            RoverNumberByPlanet expected = new RoverNumberByPlanet() { Id = 33, RoverNumber = 3 };

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void VisitedByBuilderTester()
        {
            //launch date
            var result = logic.VisitedByBuilder();
            var expected = new List<RoverTraveled>()
            {
                new RoverTraveled()
                {
                    VisitedPlaceId = 33,
                    Landing = new DateTime(2014,01,01),
                    Name = "Planet1"
                },
                new RoverTraveled()
                {
                    VisitedPlaceId = 35,
                    Landing = new DateTime(2017,01,01),
                    Name = "Planet2"
                }
            };

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreatePlanetTestCorrect()
        {
            VisitedPlaces testPlanet = new VisitedPlaces("1#Planet1#Type1#1");
            logic.Create(testPlanet);

            mockPlanetRepo.Verify(m => m.Create(testPlanet), Times.Once);
        }

        [Test]
        public void CreatePlanetTestInCorrect()
        {
            VisitedPlaces testPlanet = new VisitedPlaces("1#Planet1#Type1#-1");

            var exp = Assert.Throws<ArgumentException>(() => logic.Create(testPlanet));
            Assert.AreEqual(exp.Message, "PlaceId cannot be null!/Distance cannot be negative!");
        }
    }
}
