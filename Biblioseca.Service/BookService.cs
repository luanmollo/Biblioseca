using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;
        private readonly LendingDao lendingDao;

        public BookService(BookDao bookDao, LendingDao lendingDao)
        {
            this.bookDao = bookDao;
            this.lendingDao = lendingDao;
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            IEnumerable<Lending> lendings = this.lendingDao.GetLendingsByBookId(bookId);

            return lendings == null || !lendings.Any();
        }
    }
}