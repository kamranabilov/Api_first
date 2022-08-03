using Api_First.DAL;
using Api_First.DTOs.Engine;
using Api_First.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnginesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnginesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Engine engine = _context.Engines.FirstOrDefault(e => e.Id == id);
            if (engine == null) return NotFound();
            EngineGetDto dto = new EngineGetDto
            {
                Id = engine.Id,
                Name = engine.Name,
                HP = engine.HP,
                Value = engine.Value,
                Torque = engine.Torque
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(EnginePostDto engineDto)
        {
            if (engineDto == null) return BadRequest(402);
            Engine engine = new Engine
            {
                Name = engineDto.Name,
                HP = engineDto.HP,
                Value = engineDto.Value,
                Torque = engineDto.Torque
            };
            await _context.AddAsync(engine);
            await _context.SaveChangesAsync();
            return StatusCode(202, new { id = engine.Id, engine=engineDto});
        }
    }
}
