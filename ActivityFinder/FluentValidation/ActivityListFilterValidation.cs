using API.Concrate;
using FluentValidation;

namespace API.FluentValidation
{
    public class ActivityListFilterValidation : AbstractValidator<ActivityListFilter>
    {
        public ActivityListFilterValidation()
        {
            RuleFor(x => x.ItemSize).NotNull();
            RuleFor(x => x.PageSize).NotNull();

            RuleFor(x => x.ItemSize).GreaterThan(0);
        }
    }
}
