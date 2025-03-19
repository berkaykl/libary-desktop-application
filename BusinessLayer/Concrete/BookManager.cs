using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly BookValidator _bookValidator;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
            _bookValidator = new BookValidator();
        }

        public void TDelete(Book entity)
        {
            var validator = new BookValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }
            _bookDal.Delete(entity);
        }

        public List<Book> TGetAll()
        {
            return _bookDal.GetAll();
        }

        public List<object> TGetBooksWithCategory()
        {
            return _bookDal.GetBooksWithCategory();
        }

        public Book TGetById(int id)
        {
            return _bookDal.GetById(id);
        }

        public void TInsert(Book entity)
        {
            var validator = new BookValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Insert"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _bookDal.Insert(entity);
        }

        public void TUpdate(Book entity)
        {
            var validator = new BookValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Update"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }
            _bookDal.Update(entity);
        }
    }
}
