using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.DataAccess.Lendings.Filters
{
    public class LendingFilterDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public bool WasReturned { get; set; }
    }
}
