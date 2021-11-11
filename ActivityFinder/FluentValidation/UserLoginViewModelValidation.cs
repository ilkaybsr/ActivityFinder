using Business.DTO;
using FluentValidation;

namespace API.FluentValidation
{
    public class UserLoginViewModelValidation : AbstractValidator<UserLoginViewModel>
    {
        public UserLoginViewModelValidation()
        {
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
}
