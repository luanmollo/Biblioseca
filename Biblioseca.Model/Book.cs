using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual int ISBN { get; set; }

    }
}
