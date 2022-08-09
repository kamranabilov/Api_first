using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs.Engine
{
    public class EngineGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short HP { get; set; }
        public string Value { get; set; }
        public short Torque { get; set; }
    }

    public class EnginePostDtoValidation : AbstractValidator<EnginePostDto>
    {
        public EnginePostDtoValidation()
        {
            RuleFor(e => e.Name).MaximumLength(40).NotEmpty();
            RuleFor(e=>e.HP).GreaterThanOrEqualTo((short)50).WithMessage("min 50 character")
                .NotEmpty().WithMessage("empty be nnot");
            RuleFor(e=>e.Value).MaximumLength(5).NotEmpty();
            RuleFor(e=>e.Torque).GreaterThanOrEqualTo((short)40).NotEmpty();
        }
    }
}
