using System.Collections.Generic;
using System.Web.Mvc;

using AopSample.Core.Domain;
using AopSample.Core.Interfaces.Data;
using AopSample.Infrastructure.Web.ActionResults;
using AopSample.Infrastructure.Web.Controllers;
using AopSample.Infrastructure.Web.ViewModels.Shows;

using MvcContrib;

namespace AopSample.Web.Controllers
{
    public class ShowsController : BaseController
    {
        private readonly IQuery<Show> _repository;

        public ShowsController(IQuery<Show> repository)
        {
            _repository = repository;
        }


        public AutoMappedViewResult Index()
        {
            var list = _repository.PagedList(_repository.All(), 0, 10, "Name").PageOfResults;
            return AutoMappedView<IEnumerable<ListShowView>>(list);
        }

        public ActionResult Details(int id)
        {
            return AutoMappedView<DetailShowView>(_repository.Single(x => x.Id == id));
        }

        public ViewResult Create()
        {
            return View(new CreateShowForm());
        }

        [HttpPost]
        public ActionResult Create(CreateShowForm form)
        {
            return HandleForm<CreateShowForm>(form)
                .WithSuccessResult(this.RedirectToAction(x => x.Index()));
            return View();
        }

        public AutoMappedViewResult Edit(int id)
        {
            return AutoMappedView<EditShowForm>(_repository.Single(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(EditShowForm form)
        {
            return HandleForm<EditShowForm>(form)
                .WithSuccessResult(this.RedirectToAction(x => x.Index()));
            return View();
        }

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Delete(EditShowForm form)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}