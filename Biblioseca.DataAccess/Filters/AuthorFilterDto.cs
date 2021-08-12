using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.DataAccess.Filters
{
    public class AuthorFilterDto : Filter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
