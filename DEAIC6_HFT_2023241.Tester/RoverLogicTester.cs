using NUnit.Framework;
using System;
using DEAIC6_HFT_2023241.Logic;
using Moq;
using DEAIC6_HFT_2023241.Repository;
using DEAIC6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace DEAIC6_HFT_2023241.Tester
{
    [TestFixture]
    public class RoverLogicTester
    {
        RoverLogic logic;
        Mock<IRepository<Rover>> mockRoverRepo;

        [SetUp]
        public void Init()
        {
            mockRoverRepo = new Mock<IRepository<Rover>>();

            List<Rover> rovers = new List<Rover>()
            {
                new Rover("99#Rover1#2017.01.12#2017.01.27#6"),  //15 days
                new Rover("100#Rover2#2017.01.12#2017.01.26#6"), //14 days
                new Rover("101#Rover3#2017.01.12#2017.01.25#7"), //13 days
                new Rover("102#Rover4#2017.01.12#2017.01.28#6")
            };

            RoverBuilder rB = new RoverBuilder("6#Builder1#11");
            RoverBuilder rB1 = new RoverBuilder("7#Builder2#12");

            rovers[0].RoverBuilder = rB;
            rovers[1].RoverBuilder = rB;
            rovers[2].RoverBuilder = rB1;
            rovers[3].RoverBuilder = rB;

            mockRoverRepo.Setup(m => m.ReadAll()).Returns(rovers.AsQueryable());
            logic = new RoverLogic(mockRoverRepo.Object);
        }

        [Test]
        public void BuilderRoversTester()
        {
            var result = logic.BuilderRovers().ToList();
            var expected = new List<RoverNumber>()
            {
                new RoverNumber()
                {
                    BuilderId = 6,
                    Number = 3
                },
                new RoverNumber()
                {
                    BuilderId = 7,
                    Number = 1
                }
            };

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreateRoverTestCorrect()
        {
            Rover testRover = new Rover("232#Rover1#2016.01.01#2016.01.03#1");
            logic.Create(testRover);

            mockRoverRepo.Verify(m => m.Create(testRover), Times.Once);
        }

        [Test]
        public void CreateRoverTestInCorrect()
        {
            Rover testRover = new Rover("-1#Rover1#2016.01.01#2016.01.03#1");

            var exp = Assert.Throws<ArgumentException>(() => logic.Create(testRover));
            Assert.AreEqual("RoverId cannot be null!", exp.Message);
            mockRoverRepo.Verify(m => m.Create(testRover), Times.Never);
        }
    }
}
