using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SDM.Main.Helpers.Attributes
{
    /// <summary>
    /// Implements custom error handler for MVC controllers.
    /// </summary>
    public class CustomErrorHandle : FilterAttribute, IExceptionFilter
    {
        //#region Overrides of HandleErrorAttribute

        ///// <summary>
        ///// Called when an exception occurs.
        ///// </summary>
        ///// <param name="filterContext">The action-filter context.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        //public override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;
        //    //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, filterContext.Exception.ToString());
        //    filterContext.HttpContext.Response.StatusCode = 500;
        //    filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
        //    filterContext.HttpContext.Response.Flush();
        //}

        //#endregion

        #region Implementation of IExceptionFilter

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, filterContext.Exception.ToString());
            //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Hi");
            //filterContext.Result = new HttpStatusCodeResult(123, filterContext.Exception.Message);            
            //filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
            //filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message.Replace(Environment.NewLine, "<<<new-line>>>");
            //filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.ToString().Replace(Environment.NewLine, "<<<new-line>>>");
            //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            // because we cant write NewLine into status description, we must write one-line description and restore the NewLine in client
            // (at loading.js file)
            // and it shouldnt be more than 512 characters
            string encodedDescription = filterContext.Exception.ToString().Replace(Environment.NewLine, "~~NL~~");
            if (encodedDescription.Length > 512)
            {
                encodedDescription = encodedDescription.Substring(0, 490) + " (...truncated)";
            }

            //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, encodedDescription);            
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //filterContext.HttpContext.Response.StatusDescription = encodedDescription;
            filterContext.HttpContext.Response.Write(filterContext.Exception);
            //filterContext.HttpContext.Response.Flush();
        }

        #endregion
    }
}