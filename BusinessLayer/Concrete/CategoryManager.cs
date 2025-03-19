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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly CategoryValidator _categoryValidator;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
            _categoryValidator = new CategoryValidator();
        }

        public void TDelete(Category entity)
        {
            var validator = new CategoryValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _categoryDal.Delete(entity);
        }

        public List<Category> TGetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public void TInsert(Category entity)
        {
            var validator = new CategoryValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Insert"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _categoryDal.Insert(entity);
        }

        public void TUpdate(Category entity)
        {
            var validator = new CategoryValidator();
            var result = validator.Validate(entity, options => options.IncludeRuleSets("Update"));

            if (!result.IsValid)
            {
                // Hataları fırlat
                throw new ValidationException(result.Errors);
            }

            _categoryDal.Update(entity);
        }
    }
}
