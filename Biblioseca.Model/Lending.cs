﻿using System;
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
        public virtual DateTime? LendDate { get; set; }
        public virtual DateTime? ReturnDate { get; set; }
        public virtual bool Returned { get; set; }

        public virtual void Lend()
        {
            this.Book.Stock--;
            this.Returned = false;
        }
        public virtual void Return()
        {
            this.Book.Stock++;
            this.ReturnDate = DateTime.Now;
            this.Returned = true;
        } 

    }
}
