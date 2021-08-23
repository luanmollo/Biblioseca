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

            lending.Lend();

            lendingDao.Save(lending);

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
                ReturnDate = null 
            };

            IEnumerable<Lending> lendings = lendingDao.GetByFilter(lendingFilterDto);

            //en linea 80 busco el primer elemento de lendings y busco su id (si hice bien las cosas nunca deberia haber más de un elemento en lendings)
            //cuando no existe el prestamo la app corta en la linea 89: System.NullReferenceException
            Ensure.NotNull(lendings.FirstOrDefault<Lending>(), "Préstamo no existe o ya fue devuelto. ");

            //doble validación (si hice bien los pasos anteriores las lineas 93 y 94 no harian falta)
            Ensure.IsTrue(lendings.FirstOrDefault<Lending>().Book.Id == book.Id, "El libro no corresponde al préstamo");
            Ensure.IsTrue(lendings.FirstOrDefault<Lending>().Member.Id == member.Id, "El socio no corresponde al préstamo");

            Lending lending = lendings.FirstOrDefault<Lending>();

            lending.Return();

            lendingDao.Save(lending);

            //no se graba en la base de datos el cambio en el stock. PREGUNTAR. también probe con bookDao.Save(book). se graba solo cuando hago Session.Flush()

            return lending;
        }

        public IEnumerable<Lending> List()
        {
            IEnumerable<Lending> lendings = this.lendingDao.GetAll();
            Ensure.IsTrue(lendings.Any(), "No hay préstamos para listar");
                        
            return lendings;
        }

        public IEnumerable<Lending> ListActualLendings()
        {
            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                ReturnDate = null
            };

            IEnumerable<Lending> lendings = this.lendingDao.GetByFilter(lendingFilterDto);
            Ensure.IsTrue(lendings.Any(), "No hay préstamos para listar");

            return lendings;

        }

        public Lending Get(int lendingId)
        {
            return this.lendingDao.Get(lendingId);
        }

        public LendingError ThereAreLendings()
        {
            IEnumerable<Lending> lendings = this.lendingDao.GetAll();

            LendingError lendingError = new LendingError
            {
                HasError = !lendings.Any()
            };

            return lendingError;
        }

        public LendingError CanGetLending(int memberId)
        {
            LendingFilterDto lendingFilterDto = new LendingFilterDto
            {
                MemberId = memberId,
                ReturnDate = null
            };

            IEnumerable<Lending> lendings = this.lendingDao.GetByFilter(lendingFilterDto);

            LendingError lendingError = new LendingError
            {
                HasError = lendings.Any()
            };

            return lendingError;
        }



    }
}