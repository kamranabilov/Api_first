using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DTOs.Account
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegiisterPostDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegiisterPostDtoValidation()
        {
            RuleFor(r => r.Name).MaximumLength(20).NotEmpty();
            RuleFor(r => r.Surname).MaximumLength(20).NotEmpty();
            RuleFor(r => r.Username).MaximumLength(20).NotEmpty();
            RuleFor(r => r.Email).MaximumLength(20).NotEmpty().EmailAddress(mode: FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r).Custom((r, context) =>
            {
                if (r.Password != r.ConfirmPassword) context.AddFailure(new ValidationFailure("Password","Password and confirmpassword not match"));
            });
        }
    }
}
