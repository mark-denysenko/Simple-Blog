using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace WebUI.Validation.Filters
{
    public class ValidatePageParameterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int? numPage = filterContext.ActionParameters["page"] as int?;
            if (numPage != null && numPage.HasValue)
            {
                if (numPage.Value < 1)
                {
                    LogManager.GetCurrentClassLogger().Error("Invalid page! - " + numPage.Value);
                    throw new ArgumentException("Exception from ValidatePageParameterAttribute, page is lower than 1!");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}