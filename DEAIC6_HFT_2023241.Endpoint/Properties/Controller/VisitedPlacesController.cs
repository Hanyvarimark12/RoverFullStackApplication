using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEAIC6_HFT_2023241.Endpoint.Properties.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class VisitedPlacesController : ControllerBase
    {
        IVisitedPlacesLogic logic;

        public VisitedPlacesController(IVisitedPlacesLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<VisitedPlaces> ReadAll()
        {
            return this.logic.ReadAll();
        }

        
        [HttpGet("{id}")]
        public VisitedPlaces Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] VisitedPlaces value)
        {
            this.logic.Create(value);
        }
        
        [HttpPut]
        public void Update([FromBody] VisitedPlaces value)
        {
            this.logic.Update(value);
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
