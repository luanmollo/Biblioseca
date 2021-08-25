using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Authors
{
    public partial class Index : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(this.authorDao);
            AuthorError authorError = authorService.ThereAreAuthors();

            if (authorError.HasError)
            {
                Response.Redirect("~/Authors/Errors/ThereAreNotAuthorsError.aspx");
            }

            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            AuthorService authorService = new AuthorService(authorDao);

            this.GridViewAuthors.DataSource = authorService.List();
            this.GridViewAuthors.DataBind();
        }

        protected void LinkCreateNewAuthor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Authors/Create.aspx");
        }

        protected void GridViewAuthors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.NewEditIndex]?.Values?[0]);
            Response.Redirect(string.Format("~/Authors/Edit.aspx?id={0}", authorId));
        }

        protected void GridViewAuthors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.RowIndex]?.Values?[0]);
            Response.Redirect(string.Format("~/Authors/SureToDelete.aspx?id={0}", authorId));
        }
    }
}