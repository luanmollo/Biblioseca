using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Members;
using Biblioseca.Service;

namespace Biblioseca.Web
{
    public partial class Lendings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LendingDao lendingDao = new LendingDao(Global.SessionFactory);
            BookDao bookDao = new BookDao(Global.SessionFactory);
            MemberDao memberDao = new MemberDao(Global.SessionFactory);
            LendingService lendingService = new LendingService(lendingDao, bookDao, memberDao);

            this.GridViewLendings.DataSource = lendingService.List();
            this.GridViewLendings.DataBind();
        }
    }
}