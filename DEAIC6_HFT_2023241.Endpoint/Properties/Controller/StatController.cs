using DEAIC6_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEAIC6_HFT_2023241.Endpoint.Properties.Controller
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

        // GET: api/<StatController>
        [HttpGet]
        public IEnumerable<RoverBuilded> RoverBuild()
        {
            return this.builderlogic.RoverBuild();
        }

        [HttpGet]
        public MaxRoverNumber MoreVisitedPlaces()
        {
            return this.builderlogic.MoreVisitedPlaces();
        }

        [HttpGet]
        public IEnumerable<RoverNumber> BuilderRovers()
        {
            return this.roverlogic.BuilderRovers();
        }

        [HttpGet]
        public IEnumerable<RoverTraveled> VisitedByBuilder()
        {
            return this.planetlogic.VisitedByBuilder();
        }

        [HttpGet]
        public RoverNumberByPlanet MostRoverNumber()
        {
            return this.planetlogic.MostRoverNumber();
        }
    }
}
