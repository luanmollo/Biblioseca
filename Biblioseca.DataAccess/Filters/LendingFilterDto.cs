using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.DataAccess.Filters
{
    public class LendingFilterDto : Filter
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Returned { get; set; }
    }
}
