using Api_First.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.Models
{
    public class Engine:BaseEntity
    {
        public string Name { get; set; }
        public short HP { get; set; }
        public string Value { get; set; }
        public short Torque { get; set; }
        public List<Car> Cars { get; set; }
    }
}
