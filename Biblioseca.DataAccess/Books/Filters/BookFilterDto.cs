using Biblioseca.Model;

namespace Biblioseca.DataAccess.Books.Filters
{
    public class BookFilterDto
    {
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
    }
}