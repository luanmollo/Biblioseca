using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Lending : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Member Member { get; set; }
        public virtual DateTime LendDate { get; set; }
        public virtual DateTime ReturnDate { get; set; }
    }
}
