using Api_First.DAL;
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
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarsController(AppDbContext context)
        {
            _context = context;
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

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c=>c.Id==id);
            if (car is null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(car);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {

            return Ok(_context.Cars.ToList());
        }

        //[HttpGet]
        //[Route("create")]
        //public IActionResult CreateCars(Car car)
        //{
        //    _context.Cars.AddRange(_cars);
        //    _context.SaveChanges();
        //    return Ok();
        //}
    }
}
