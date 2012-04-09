using FluentValidation;

namespace AopSample.Core.Commands.Shows.EditShow
{
    public class EditShowCommandValidator : AbstractValidator<EditShowCommand>
    {
        public EditShowCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Overview).NotEmpty();
        }
    }
}