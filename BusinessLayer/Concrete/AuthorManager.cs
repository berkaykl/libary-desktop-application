using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorDal _authorDal;
        private readonly AuthorValidator _validator;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
            _validator = new AuthorValidator(); //nesne oluşturduk
        }

        public void TDelete(Author entity)
        {
            var validator = new AuthorValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _authorDal.Delete(entity);
        }

        public List<Author> TGetAll()
        {
            return _authorDal.GetAll();
        }

        public Author TGetById(int id)
        {
            return _authorDal.GetById(id);
        }

        public List<Object> TGetFullName()
        {
            return _authorDal.GetFullName();
        }

        public void TInsert(Author entity)
        {
            var validator = new AuthorValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Insert"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _authorDal.Insert(entity);
        }

        public void TUpdate(Author entity)
        {
            var validator = new AuthorValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Update"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _authorDal.Update(entity);
        }
    }
}
