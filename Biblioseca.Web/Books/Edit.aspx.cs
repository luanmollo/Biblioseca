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
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;

namespace Biblioseca.Web.Books
{
    public partial class Edit : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private int bookId;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bookId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }
        private void BindData()
        {
            BindAuthors();
            BindCategories();
            BindBook();
        }

        private void BindCategories()
        {
            CategoryService categoryService = new CategoryService(this.categoryDao);
            this.categoryList.DataValueField = nameof(Category.Id);
            this.categoryList.DataTextField = nameof(Category.Name);
            this.categoryList.DataSource = categoryService.List();
            this.categoryList.DataBind();
        }

        private void BindAuthors()
        {
            AuthorService authorService = new AuthorService(this.authorDao);
            this.authorList.DataValueField = nameof(Author.Id);
            this.authorList.DataTextField = nameof(Author.FullName);
            this.authorList.DataSource = authorService.List();
            this.authorList.DataBind();
        }

        private void BindBook()
        {
            BookService bookService = new BookService(this.bookDao);
            Book book = bookService.Get(this.bookId);
            Ensure.NotNull(book, "El libro no existe. ");

            this.textBoxTitle.Text = book.Title;
            this.textBoxDescription.Text = book.Description;
            this.textBoxISBN.Text = book.ISBN;
            this.textBoxPrice.Text = book.Price.ToString();
            this.categoryList.SelectedValue = book.Category.Id.ToString();
            this.authorList.SelectedValue = book.Author.Id.ToString();
            this.textBoxStock.Text = book.Stock.ToString();

        }

        protected void ButtonEditBook_Click(object sender, EventArgs e)
        {
            BookService bookService = new BookService(this.bookDao);
            Book book = bookService.Get(this.bookId);
            Ensure.NotNull(book, "El libro no existe. ");

            CategoryService categoryService = new CategoryService(this.categoryDao);
            Category category = categoryService.Get(Convert.ToInt32(this.categoryList.SelectedValue));
            Ensure.NotNull(category, "La categoría no existe. ");

            AuthorService authorService = new AuthorService(this.authorDao);
            Author author = authorService.Get(Convert.ToInt32(this.authorList.SelectedValue));
            Ensure.NotNull(author, "El autor no existe. ");

            book.Title = this.textBoxTitle.Text;
            book.Description = this.textBoxDescription.Text;
            book.ISBN = this.textBoxISBN.Text;
            book.Price = Convert.ToDouble(this.textBoxPrice.Text);
            book.Category = category;
            book.Author = author;
            book.Stock = Convert.ToInt32(this.textBoxStock.Text);

            bookDao.Save(book);

            Response.Redirect("~/Books/Index.aspx");
        }



    }
}