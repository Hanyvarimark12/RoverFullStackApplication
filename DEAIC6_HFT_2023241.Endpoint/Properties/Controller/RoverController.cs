﻿using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEAIC6_HFT_2023241.Endpoint.Properties.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        IRoverLogic logic;

        public RoverController(IRoverLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Rover> ReadAll()
        {
            return this.logic.ReadAll();
        }

        
        [HttpGet("{id}")]
        public Rover Read(int id)
        {
            return this.logic.Read(id);
        }

        
        [HttpPost]
        public void Create([FromBody] Rover value)
        {
            this.logic.Create(value); 
        }

       
        [HttpPut]
        public void Update([FromBody] Rover value)
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
