using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Project.Contracts.V1.Requests;

namespace Project.Models.Validation
{
    public class RegistrationModelValidator : AbstractValidator<UserModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
