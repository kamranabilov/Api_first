using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs.Engine
{
    public class EnginePostDto
    {
        public string Name { get; set; }
        public short HP { get; set; }
        public string Value { get; set; }
        public short Torque { get; set; }
    }
}
