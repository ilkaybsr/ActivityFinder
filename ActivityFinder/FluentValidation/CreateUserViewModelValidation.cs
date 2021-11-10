using Business.DTO;
using FluentValidation;

namespace API.FluentValidation
{
    public class CreateUserViewModelValidation : AbstractValidator<CreateUserViewModel>
    {
        public CreateUserViewModelValidation()
        {

            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
