using Api_First.DAL;
using Api_First.DTOs;
using Api_First.DTOs.Engine;
using Api_First.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(int page = 1, string search=null)
        {
            var query = _context.Engines.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.Name.Contains(search));
            }
            ListDto<EnginListItemDto> ListDto = new ListDto<EnginListItemDto>
            {
                ListItemDtos = query.Select(e => new EnginListItemDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    HP = e.HP,
                    Torque = e.Torque,
                    Value = e.Value
                }).Skip((page - 1) * 4).Take(4).ToList(), TotalCount = query.Count()
            };
            return Ok(ListDto);

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(EnginePostDto engineDto)
        {
            if (engineDto == null) return BadRequest(402);
            if (_context.Engines.Any(e => e.Name == engineDto.Name)) return BadRequest(400);
            Engine engine = new Engine
            {
                Name = engineDto.Name,
                HP = engineDto.HP,
                Value = engineDto.Value,
                Torque = engineDto.Torque
            };
            await _context.Engines.AddAsync(engine);
            await _context.SaveChangesAsync();
            return StatusCode(202, new { id = engine.Id, engine=engineDto});
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, EnginePostDto dto)
        {
            if (id == 0) return BadRequest(400);
            if (_context.Engines.Any(e => e.Name == dto.Name)) return BadRequest(401);
            Engine existed =await _context.Engines.FirstOrDefaultAsync(e => e.Id == id);
            if (existed == null) return NotFound();
            _context.Entry(existed).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
            return StatusCode(202);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(402);
            Engine existed = _context.Engines.FirstOrDefault(e => e.Id == id);
            if (existed == null) return BadRequest(401);
            _context.Engines.Remove(existed);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
