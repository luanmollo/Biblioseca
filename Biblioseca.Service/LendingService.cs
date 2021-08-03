using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Members;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class LendingService
    {
        private readonly LendingDao lendingDao;
        private readonly BookDao bookDao;
        private readonly MemberDao memberDao;

        public LendingService(LendingDao lendingDao, BookDao bookDao, MemberDao memberDao)
        {
            this.lendingDao = lendingDao;
            this.bookDao = bookDao;
            this.memberDao = memberDao;
        }

        public Lending LendABook(int bookId, int memberId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            Member member = memberDao.Get(memberId);
            Ensure.NotNull(member, "Socio no existe. ");
            Ensure.IsTrue(member.Lendings.Count < 2, "No puede pedir prestado más libros. ");

            IEnumerable<Lending> lendings = lendingDao.GetLendingsByBookId(bookId);
            Ensure.IsTrue(!lendings.Any(), "El libro ya fue prestado. ");

            Lending lending = new Lending
            {
                Book = book,
                Member = member,
                LendDate = DateTime.Now
            };

            lendingDao.Save(lending);

            return lending;
        }

        
    }
}