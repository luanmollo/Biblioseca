using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Web.Authors
{
    public partial class SureToDelete : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private int authorId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.authorId = Convert.ToInt32(Request.QueryString.Get("id"));
        }

        protected void LinkDeleteAuthor_OnClick(object sender, EventArgs e)
        {
            Author author = this.authorDao.Get(authorId);
            Ensure.NotNull(author, "El autor no existe. ");

            author.MarkAsDeleted();
            this.authorDao.Save(author);

            Response.Redirect("~/Authors/Index.aspx");
        }


        protected void LinkDoNotDeleteAuthor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Authors/Index.aspx");
        }

    }
}