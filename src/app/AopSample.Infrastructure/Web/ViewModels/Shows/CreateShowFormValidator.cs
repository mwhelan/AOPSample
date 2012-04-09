using FluentValidation;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class CreateShowFormValidator : AbstractValidator<CreateShowForm>
    {
        public CreateShowFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Overview).NotEmpty();
        }
    }
}