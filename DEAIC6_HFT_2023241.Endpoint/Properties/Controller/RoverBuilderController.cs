using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DEAIC6_HFT_2023241.Endpoint.Properties.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class RoverBuilderController : ControllerBase
    {
        IRoverBuilderLogic logic;

        public RoverBuilderController(IRoverBuilderLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<RoverBuilder> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public RoverBuilder Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] RoverBuilder value)
        {
            this.logic.Create(value);
        }

        
        [HttpPut]
        public void Updat([FromBody] RoverBuilder value)
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
