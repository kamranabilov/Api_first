using Api_First.DAL;
using Api_First.DTOs;
using Api_First.DTOs.Car;
using Api_First.Models;
using AutoMapper;
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
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CarsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //private List<Car> _cars = new List<Car>()
        //{
        //   new Car
        //   {
        //       Brand = "Mersedes",
        //       Model = "S-550",
        //       Price = 120000,
        //       Color = "Black"
        //   },
        //    new Car
        //   {
        //       Brand = "BMW",
        //       Model = "F-90",
        //       Price = 110000,
        //       Color = "White"
        //   },
        //    new Car
        //   {             
        //       Brand = "TOYOTA",
        //       Model = "Camry",
        //       Price = 80000,
        //       Color = "Blue"
        //   },
        //    new Car
        //   {
        //       Brand = "Volvo",
        //       Model = "Urus",
        //       Price = 50000,
        //       Color = "Green"
        //   }
        //};

        //[HttpGet]
        //[Route("create")]
        //public IActionResult CreateCars(Car car)
        //{
        //    _context.Cars.AddRange(_cars);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {
            Car car = _context.Cars.Include(c=>c.Engine).ThenInclude(c=>c.Cars).FirstOrDefault(c=>c.Id==id);
            CarGetDto dto = _mapper.Map<CarGetDto>(car);
            //CarGetDto getDto = new CarGetDto
            //{
            //    Id = car.Id,
            //    Brand = car.Brand,
            //    Model = car.Model,
            //    Price = car.Price,
            //    Color = car.Color,
            //    Display = car.Display,
            //    Engine = new EngineInCarGetDto
            //    {
            //        Id = car.EngineId,
            //        Name = car.Engine.Name,
            //        CarsCount = car.Engine.Cars.Count()
            //    } 
            //};
            //if (car is null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(dto);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(q => q.Brand.Contains(search));
            }

            //List<Car> cars = _context.Cars.Where(c => c.Display == true).Skip((page - 1)*4).Take(4).ToList();
            List<Car> cars = _context.Cars.Include(c => c.Engine).ThenInclude(e=>e.Cars).Skip((page - 1)*4).Take(4).ToList();

            ListDto<CarListItemDto> Dto = new ListDto<CarListItemDto>
            {
                ListItemDtos = _mapper.Map<List<CarListItemDto>>(cars),
                //ListItemDtos = query.Include(c=>c.Engine).ThenInclude(e=>e.Cars).Select(c => 
                //new CarListItemDto 
                //{ Id = c.Id, 
                //    Brand = c.Brand, 
                //    Model = c.Model,
                //    Price = c.Price,
                //    Color = c.Color, 
                //    Display = c.Display,
                //    Engine = new EngineInCarGetDto
                //    {
                //        Id = c.EngineId,
                //        Name = c.Engine.Name,
                //        CarsCount = c.Engine.Cars.Count()
                //    }
                //})
                //.Skip((page - 1) * 4)
                //.Take(4).ToList(), 
                TotalCount = query.Where(c=>c.Display==true).Count()
            };
            return Ok(Dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CarPostDto carDto)
        {
            if (carDto is null) return StatusCode(400);
            if (!_context.Engines.Any(e => e.Id == carDto.EngineId)) return BadRequest();
            Car car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Price = carDto.Price,
                Color = carDto.Color,
                Display = carDto.Display,
                EngineId = carDto.EngineId 
            };
           await _context.Cars.AddAsync(car);
           await _context.SaveChangesAsync();
             return StatusCode(201, new { id = car.Id, car = carDto});
        }

        [HttpPut("update/{id}")]
        //[Route("{id}")]
        public IActionResult Update(int id, CarPostDto carDto)
        {
            if (id == 0) return BadRequest(401);
            Car existed = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (existed == null) return BadRequest(402);
            _context.Entry(existed).CurrentValues.SetValues(carDto);
            // dto mentiqinde asagdakilara ehtiyac yoxdur
            //existed.Brand = carDto.Brand;
            //existed.Model = carDto.Model;
            //existed.Price = carDto.Price;
            //existed.Color = carDto.Color;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(402);
            Car existed = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (existed == null) return BadRequest(401);
            _context.Cars.Remove(existed);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("change/{id}")]
        public IActionResult ChangeDisplay(int id, bool display)
        {
            if (id == 0) return BadRequest();
            Car existed = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (existed == null) return NotFound();
            existed.Display = display;
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
