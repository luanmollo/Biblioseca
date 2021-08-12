using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class AuthorService
    {
        private readonly AuthorDao authorDao;

        public AuthorService(AuthorDao authorDao)
        {
            this.authorDao = authorDao;
        }


        public IEnumerable<Author> List()
        {
            IEnumerable<Author> authors = this.authorDao.GetAll();
            Ensure.IsTrue(authors.Any(), "No hay autores para listar. ");

            return authors;
        }


    }
}
