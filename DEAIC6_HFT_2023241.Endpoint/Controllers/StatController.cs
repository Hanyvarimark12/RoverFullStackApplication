using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEAIC6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IRoverLogic roverlogic;
        IRoverBuilderLogic builderlogic;
        IVisitedPlacesLogic planetlogic;

        public StatController(IRoverLogic roverlogic, IRoverBuilderLogic builderlogic, IVisitedPlacesLogic planetlogic)
        {
            this.roverlogic = roverlogic;
            this.builderlogic = builderlogic;
            this.planetlogic = planetlogic;
        }

        [HttpGet]
        public IEnumerable<Logic.RoverNumber> BuilderRovers()
        {
            return roverlogic.BuilderRovers();
        }

        [HttpGet]
        public IEnumerable<Logic.RoverBuilded> BuilderDistance()
        {
            return builderlogic.BuilderDistance();
        }

        [HttpGet]
        public Logic.MaxBuilderNumber BuilderWithMostVisitedPlaces()
        {
            return builderlogic.BuilderWithMostVisitedPlaces();
        }

        [HttpGet]
        public IEnumerable<Logic.RoverTraveled> VisitedByLaunchDate()
        {
            return planetlogic.VisitedByLaunchDate();
        }

        [HttpGet]
        public Logic.RoverNumberByPlanet MostRoverNumber()
        {
            return planetlogic.MostRoverNumber();
        }

    }
}
