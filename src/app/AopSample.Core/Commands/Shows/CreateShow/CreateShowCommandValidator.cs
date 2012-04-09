using FluentValidation;

namespace AopSample.Core.Commands.Shows.CreateShow
{
    public class CreateShowCommandValidator : AbstractValidator<CreateShowCommand>
    {
        public CreateShowCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Overview).NotEmpty();
        }
    }
}