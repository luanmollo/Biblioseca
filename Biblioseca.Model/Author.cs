using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Model
{
    public class Author : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FullName => FirstName + " "+ LastName;

    }
}
