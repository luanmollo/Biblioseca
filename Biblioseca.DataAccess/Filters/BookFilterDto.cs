using Biblioseca.Model;

namespace Biblioseca.DataAccess.Filters
{
    public class BookFilterDto : Filter
    {
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string CategoryName { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
    }
}