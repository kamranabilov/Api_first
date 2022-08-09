using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs
{
    public class CarGetDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool? Display { get; set; }
        public EngineInCarGetDto Engine { get; set; }
    }

    public class EngineInCarGetDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int CarsCount { get; set; }
    }
}
