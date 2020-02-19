using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageAPI.DTO
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {

            RuleFor(a => a.FullName).NotEmpty().MaximumLength(50).WithMessage("Ad maximum 50 xarakter ola bilər");
            RuleFor(a => a.Password).NotEmpty().MinimumLength(3).WithMessage("sifre minimum 3 simvol ola biler").MaximumLength(100).WithMessage("sifre maximum 30 xarakter ola bilər");
            RuleFor(a => a.Email).NotEmpty().EmailAddress().WithMessage("Duzgun bir email yazin");

        }
    }
}
