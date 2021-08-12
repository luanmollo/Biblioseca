using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Book : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ISBN { get; set; }
        public virtual double Price { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual int Stock { get; set; }
        public virtual bool ISBNVerified { get; set; }



    }
}
