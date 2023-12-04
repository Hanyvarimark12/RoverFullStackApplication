using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Test
{
    [TestFixture]
    public class RoverBuilderLogicTester
    {
        RoverBuilderLogic logic;
        VisitedPlacesLogic planetlogic;
        Mock<IRepository<RoverBuilder>> mockBuilderRepo;

        [SetUp]
        public void Init()
        {
            mockBuilderRepo = new Mock<IRepository<RoverBuilder>>();

            List<RoverBuilder> builders = new List<RoverBuilder>()
            {
                new RoverBuilder("11#Builder1#33"),
                new RoverBuilder("12#Builder2#35"),
                new RoverBuilder("13#Builder3#35")
            };

            VisitedPlaces planet = new VisitedPlaces("33#Planet1#Type1#100");
            VisitedPlaces planet1 = new VisitedPlaces("35#Planet2#Type2#1000");

            builders[0].VisitedPlaces = planet;
            builders[1].VisitedPlaces = planet1;
            builders[2].VisitedPlaces = planet1;

            mockBuilderRepo.Setup(m => m.ReadAll()).Returns(builders.AsQueryable());
            logic = new RoverBuilderLogic(mockBuilderRepo.Object);
        }

        [Test]
        public void MoreVisitedPlacesTest()
        {
            //planet with most builders
            var result = logic.BuilderWithMostVisitedPlaces();
            var expected = new Logic.MaxBuilderNumber() { Id =  35, BuilderNumber = 2};

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RoverBuildTest()
        {
            //order builder by distance
            var result = logic.BuilderDistance().ToList();
            var expected = new List<Logic.RoverBuilded>()
            {
                new Logic.RoverBuilded()
                {
                    Id = 13,
                    Distance = 1000
                },
                new Logic.RoverBuilded()
                {
                    Id = 11,
                    Distance = 100
                },
                new Logic.RoverBuilded()
                {
                    Id = 12,
                    Distance = 100
                }
            };

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreateBuilderTestCorrect()
        {
            RoverBuilder testBuilder = new RoverBuilder("1#Builder1#33");
            logic.Create(testBuilder);

            mockBuilderRepo.Verify(m => m.Create(testBuilder), Times.Once);
        }

        [Test]
        public void CreateBuilderTestInCorrect()
        {
            RoverBuilder testBuilder = new RoverBuilder("-1##33");

            var exp = Assert.Throws<ArgumentException>(() => logic.Create(testBuilder));
            Assert.AreEqual("Cannot create RoverBuilder without name!/" +
                "RoverBuilderId cannot be null!", exp.Message);
            mockBuilderRepo.Verify(m => m.Create(testBuilder), Times.Never);
        }
    }
}
