using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentPortal.Extensions
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool yesno)
        {
            var text = yesno ? "Yes" : "No";
            return new MvcHtmlString(text);
        }
    }
}