using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs.Car
{
    public class CarListItemDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool? Display { get; set; }
        public EngineInCarGetDto Engine { get; set; }
    }
   
}
