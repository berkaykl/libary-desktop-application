using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    internal class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleSet("Insert", () =>
            {
                RuleFor(x => x.CategoryName)
                    .NotEmpty().WithMessage("Kategori ismi boş olamaz.")
                    .MinimumLength(2).WithMessage("Kategori ismi en az 2 karakter olmalıdır.")
                    .MaximumLength(20).WithMessage("Kategori ismi en fazla 20 karakter olmalıdır.");
            });

            RuleSet("Delete", () =>
            {
                RuleFor(x => x.CategoryId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });
            
            RuleSet("Update", () =>
            {
                RuleFor(x => x.CategoryId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });
        }
    }
}
