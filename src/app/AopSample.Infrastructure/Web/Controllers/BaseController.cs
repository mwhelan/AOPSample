using System.Web.Mvc;
using AopSample.Core.Interfaces.Forms;
using AopSample.Infrastructure.Web.ActionResults;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Web.Controllers
{
    public class BaseController : Controller
    {
        public virtual IFormProcessor FormProcessor { get; set; }

        protected FormActionResult<TForm> HandleForm<TForm>(TForm form)
            where TForm : class
        {
            return new FormActionResult<TForm>(form, FormProcessor)
                .WithFailureResult(View(form));
        }

        protected AutoMappedViewResult AutoMappedView<TModel>(object entity)
        {
            Guard.That(entity).IsNotNull();
            ViewData.Model = entity;

            return new AutoMappedViewResult(typeof (TModel))
                       {
                           ViewData = ViewData,
                           TempData = TempData
                       };
        }
    }
}