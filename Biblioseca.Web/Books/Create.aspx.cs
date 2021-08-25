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

            this.categoryList.DataValueField = nameof(Category.Id);
            this.categoryList.DataTextField = nameof(Category.Name);
            this.categoryList.DataSource = categoryService.List();
            this.categoryList.DataBind();
        }

        private void BindAuthors()
        {
            AuthorService authorService = new AuthorService(this.authorDao);
            IEnumerable<Author> authors = authorService.List();

            this.authorList.DataValueField = nameof(Author.Id);
            this.authorList.DataTextField = nameof(Author.FullName);
            this.authorList.DataSource = authorService.List();
            this.authorList.DataBind();
        }

        protected void ButtonCreateBook_Click(object sender, EventArgs e)
        {
            BookService bookService = new BookService(this.bookDao);

            CategoryService categoryService = new CategoryService(this.categoryDao);
            Category category = categoryService.Get(Convert.ToInt32(this.categoryList.SelectedValue));

            AuthorService authorService = new AuthorService(this.authorDao);
            Author author = authorService.Get(Convert.ToInt32(this.authorList.SelectedValue));

            Book book = new Book
            {
                Title = this.textBoxTitle.Text,
                Description = this.textBoxDescription.Text,
                ISBN = this.textBoxISBN.Text,
                Price = Convert.ToDouble(this.textBoxPrice.Text),
                Category = category,
                Author = author,
                Stock = Convert.ToInt32(this.textBoxStock.Text)

            };

            bookDao.Save(book);

            Response.Redirect("~/Books/Index.aspx");
        }
    }
}