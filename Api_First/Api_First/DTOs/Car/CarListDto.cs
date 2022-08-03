using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs.Car
{
    public class CarListDto
    {
        public List<CarListItemDto> CarListItemDtos { get; set; }
        public int TotalCount { get; set; }
    }
}
