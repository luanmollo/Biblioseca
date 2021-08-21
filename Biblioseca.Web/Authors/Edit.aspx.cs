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
    public partial class Edit : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private int authorId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.authorId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!this.IsPostBack)
            {
                this.BindAuthor();
            }
        }
        private void BindAuthor()
        {
            AuthorService authorService = new AuthorService(this.authorDao);
            Author author = authorService.Get(this.authorId);
            Ensure.NotNull(author, "Author no existe. ");

            this.textBoxFirstName.Text = author.FirstName;
            this.textBoxLastName.Text = author.LastName;
        }

        protected void ButtonEditAuthor_Click(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(authorDao);
            Author author = authorService.Get(this.authorId);
            Ensure.NotNull(author, "Author no existe. ");

            author.SetFirstName(this.textBoxFirstName.Text);
            author.SetLastName(this.textBoxLastName.Text);

            authorService.Update(author);

            Response.Redirect("~/Authors/Index.aspx");
        }
    }
}