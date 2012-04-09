using FluentValidation;

namespace AopSample.Infrastructure.Web.ViewModels.Shows
{
    public class EditShowFormValidator : AbstractValidator<EditShowForm>
    {
        public EditShowFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Overview).NotEmpty();
        }
    }
}