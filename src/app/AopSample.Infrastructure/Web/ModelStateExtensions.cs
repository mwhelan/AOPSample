using System.Web.Mvc;
using AopSample.Core.Commands;
using Shared.Core.Extensions;

namespace AopSample.Infrastructure.Web
{
    public static class ModelStateExtensions
    {
        public static void AddErrorsToModelState(this ModelStateDictionary modelState, ValidatingException validationException)
        {
            validationException.Errors.Each(x => modelState.AddModelError((string) x.PropertyName, (string) x.ErrorMessage));
        }
    }
}
