using System;
using System.Web.Mvc;

namespace AopSample.Infrastructure.Web.ActionResults
{
    public static class FormActionResultExtensions
    {
        /// <summary>
        /// The result that should be executed if action succeeds. Usually a RedirectToAction.
        /// </summary>
        public static FormActionResult<TForm> WithSuccessResult<TForm>(this FormActionResult<TForm> result, ActionResult successResult)
            where TForm : class
        {
            result.Success = successResult;
            return result;
        }

        /// <summary>
        /// The result that should be executed if action fails. Should only be called for partials.
        /// </summary>
        public static FormActionResult<TForm> WithFailureResult<TForm>(this FormActionResult<TForm> result, ActionResult failureResult)
            where TForm : class
        {
            result.Failure = failureResult;
            return result;
        }

        /// <summary>
        /// Adds data to the ViewBag that is used by the View. Is called if the action fails.
        /// </summary>
        /// <typeparam name="T">The Command that is being handled</typeparam>
        /// <param name="key">The ViewBag/ViewData dictionary key.</param>
        /// <param name="action">The method to be called to populate the data in the dictionary</param>
        /// <returns></returns>
        public static FormActionResult<TForm> WithViewBagData<TForm>(this FormActionResult<TForm> result, string key, Func<object> action)
            where TForm : class
        {
            result.FailureActions.Add(key, action);
            return result;
        }
    }
}