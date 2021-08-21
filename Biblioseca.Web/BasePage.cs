using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Biblioseca.Web
{
    public class BasePage : Page
    {
        protected void PageReload()
        {
            Response.Redirect(HttpContext
                .Current
                .Request
                .AppRelativeCurrentExecutionFilePath ?? string.Empty);
        }
    }
}