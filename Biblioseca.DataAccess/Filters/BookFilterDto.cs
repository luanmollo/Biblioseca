using Biblioseca.Model;

namespace Biblioseca.DataAccess.Filters
{
    public class BookFilterDto : Filter
    {
        public string Title { get; set; }
        public int? Stock { get; set; }
    }
}