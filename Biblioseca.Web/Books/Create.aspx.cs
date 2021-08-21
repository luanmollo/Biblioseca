using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Service;

namespace Biblioseca.Web.Books
{
    public partial class Create : BasePage
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        private void BindData()
        {
            BindAuthors();
            BindCategories();
        }

        private void BindCategories()
        {
            CategoryService categoryService = new CategoryService(this.categoryDao);
            IEnumerable<Category> categories = categoryService.List();

            this.categoryList.DataTextField = "value";
            this.categoryList.DataValueField = "key";
            this.categoryList.DataSource = categories
                .ToDictionary(category => category.Id, category => $"{category.Name}");
            this.categoryList.DataBind();
        }

        private void BindAuthors()
        {
            AuthorService authorService = new AuthorService(this.authorDao);
            IEnumerable<Author> authors = authorService.List();

            this.authorList.DataTextField = "value";
            this.authorList.DataValueField = "key";
            this.authorList.DataSource = authors
                .ToDictionary(author => author.Id, author => $"{author.FirstName} {author.LastName}");
            this.authorList.DataBind();
        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
            BookService bookService = new BookService(this.bookDao);

            CategoryService categoryService = new CategoryService(this.categoryDao);
            Category category = categoryService.Get(Convert.ToInt32(this.categoryList.SelectedValue));

            AuthorService authorService = new AuthorService(this.authorDao);
            Author author = authorService.Get(Convert.ToInt32(this.authorList.SelectedValue));

            Book book = Book.Create
                (
                    this.textBoxTitle.Text,
                    this.textBoxDescription.Text,
                    this.textBoxISBN.Text,
                    Convert.ToDouble(this.textBoxPrice.Text),
                    category,
                    author,
                    Convert.ToInt32(this.textBoxStock.Text)
                );

            bookService.Create(book);

            Response.Redirect("~/Books/Index.aspx");
        }
    }
}