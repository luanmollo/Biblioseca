using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Lendings.Filters;
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

            BookService bookService = new BookService(bookDao, lendingDao);
            Ensure.IsTrue(bookService.IsAvailable(bookId), "El libro no está disponible");

            Ensure.IsTrue(!lendingDao.GetNotReturnedLendingsByMemberId(memberId).Any(), "El socio no puede pedir prestado más libros");

            Console.WriteLine(book.Stock);

            book.WasLended();
            bookDao.Save(book);

            Lending lending = new Lending
            {
                Book = book,
                Member = member,
                LendDate = DateTime.Now,
                WasReturned = false
            };

            Console.WriteLine($"Préstamo creado exitosamente. Libro: {lending.Book.Title}. Socio: {lending.Member.FirstName} {lending.Member.LastName}");

            //no se graba en la base de datos el cambio en el stock. PREGUNTAR. también probe con bookDao.Save(book)

            lendingDao.Save(lending);

            Console.WriteLine(book.Stock);

            return lending;
        }

        public Lending ReturnABook(int bookId, int memberId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            Member member = memberDao.Get(memberId);
            Ensure.NotNull(member, "Socio no existe. ");

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                BookId = bookId,
                MemberId = memberId,
                WasReturned = false                
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            //en linea 80 busco el primer elemento de lendings y busco su id (si hice bien las cosas nunca deberia haber más de un elemento en lendings)
            Lending lending = lendingDao.Get(lendings.ElementAt(0).Id);
            Ensure.NotNull(lending, "Préstamo no existe. ");

            Ensure.IsTrue(lending.Book == book, "El libro no corresponde al préstamo");
            Ensure.IsTrue(lending.Member == member, "El socio no corresponde al préstamo");


            Console.WriteLine(book.Stock);

            book.WasReturned();
            lending.WasReturned = true;

            Console.WriteLine($"Préstamo devuelto exitosamente. Libro: {lending.Book.Title}. Socio: {lending.Member.FirstName} {lending.Member.LastName}");

            bookDao.Save(book);
            lendingDao.Save(lending);

            //no se graba en la base de datos el cambio en el stock. PREGUNTAR. también probe con bookDao.Save(book)

            Console.WriteLine(book.Stock);


            return lending;
        }

        public IEnumerable<Lending> List()
        {
            IEnumerable<Lending> lendings = this.lendingDao.GetAll();
            Ensure.IsTrue(lendings.Any(), "No hay préstamos para listar");

            Console.WriteLine("Lista de préstamos:");
            foreach(Lending lending in lendings)
            {
                Console.WriteLine($"\t{lending.Book.Title}, prestado a {lending.Member.FirstName} {lending.Member.LastName} el {lending.LendDate}");
            }
            return lendings;
        }


    }
}