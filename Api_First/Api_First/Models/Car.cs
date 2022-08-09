using Api_First.Models.Base;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.Models
{
    public class Car:BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        //[Column(TypeName = "decimal(6.2)")]
        public decimal Price { get; set; }       
        public string Color { get; set; }
        public bool? Display { get; set; }
        public Engine Engine { get; set; }
        public int? EngineId { get; set; }
    }
}
