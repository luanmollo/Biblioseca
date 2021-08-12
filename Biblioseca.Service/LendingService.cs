using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Lendings;
using Biblioseca.DataAccess.Filters;
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

            BookService bookService = new BookService(bookDao);
            Ensure.IsTrue(bookService.IsAvailable(bookId), "El libro no está disponible");

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                MemberId = memberId,
                ReturnDate = null
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            Ensure.IsTrue(!lendings.Any(), "El socio no puede pedir prestado más libros");

            Lending lending = new Lending
            {
                Book = book,
                Member = member,
                LendDate = DateTime.Now,
            };

            Console.WriteLine(book.Stock);

            lending.Lend();

            Console.WriteLine($"Préstamo creado exitosamente. Libro: {lending.Book.Title}. Socio: {lending.Member.FirstName} {lending.Member.LastName}");

            //no se graba en la base de datos el cambio en el stock. PREGUNTAR. también probe con bookDao.Save(book)

            lendingDao.Save(lending);
            

            Console.WriteLine(book.Stock);

            return lending;
        }

        public bool ReturnABook(int bookId, int memberId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            Member member = memberDao.Get(memberId);
            Ensure.NotNull(member, "Socio no existe. ");

            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                BookId = bookId,
                MemberId = memberId,
                ReturnDate = null 
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            //en linea 80 busco el primer elemento de lendings y busco su id (si hice bien las cosas nunca deberia haber más de un elemento en lendings)
            //cuando no existe el prestamo la app corta en la linea 89: System.NullReferenceException
            Lending lending = lendingDao.Get(lendings.ElementAtOrDefault<Lending>(0).Id);
            Ensure.NotNull(lending, "Préstamo no existe. ");

            //doble validación (si hice bien los pasos anteriores las lineas 93 y 94 no harian falta)
            Ensure.IsTrue(lending.Book == book, "El libro no corresponde al préstamo");
            Ensure.IsTrue(lending.Member == member, "El socio no corresponde al préstamo");

            lending.Return();

            Console.WriteLine($"Préstamo devuelto exitosamente. Libro: {lending.Book.Title}. Socio: {lending.Member.FirstName} {lending.Member.LastName}");

            lendingDao.Save(lending);

            //no se graba en la base de datos el cambio en el stock. PREGUNTAR. también probe con bookDao.Save(book). se graba solo cuando hago Session.Flush()

            return true;
        }

        public IEnumerable<Lending> List()
        {
            IEnumerable<Lending> lendings = this.lendingDao.GetAll();
            Ensure.IsTrue(lendings.Any(), "No hay préstamos para listar");

            return lendings;
        }


    }
}