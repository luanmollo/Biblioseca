using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Service;

namespace Biblioseca.Web.Authors
{
    public partial class Create : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(authorDao);

            Author author = new Author
            {
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text
            };

            authorDao.Save(author);
            
            Response.Redirect("~/Authors/Index.aspx");
        }

    }
}