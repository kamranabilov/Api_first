using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs
{
    public class CarPostDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool Display { get; set; }
    }

    public class CarPostDtoValidation: AbstractValidator<CarPostDto>
    {
        public CarPostDtoValidation()
        {
            RuleFor(c => c.Brand).NotNull().WithMessage("value enter").MaximumLength(40).WithMessage("length 40 charecter");
            RuleFor(c => c.Model).NotNull().WithMessage("value enter").MaximumLength(40).WithMessage("length 40 charecter");
            RuleFor(c => c.Color).NotNull().WithMessage("value enter").MaximumLength(20).WithMessage("length 40 charecter");
            RuleFor(c => c.Price).NotNull().WithMessage("value enter").GreaterThanOrEqualTo(2000).WithMessage("min 2000")
                .LessThanOrEqualTo(999999.99m).WithMessage("max 999999.99");

        }
    }
}
