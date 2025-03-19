using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BookValidator:AbstractValidator<Book>
    {
        public BookValidator() 
        {

            RuleSet("Insert", () =>
            {
                RuleFor(x => x.BookName)
                    .NotEmpty().WithMessage("Kitap ismi boş olamaz.")
                    .MinimumLength(2).WithMessage("Kitap ismi en az 2 karakter olmalıdır.")
                    .MaximumLength(10).WithMessage("Kitap ismi en fazla 10 karakter olmalıdır.");

                RuleFor(x => x.BookDescription)
                    .NotEmpty().WithMessage("Kitap açıklaması boş olamaz.")
                    .MinimumLength(10).WithMessage("Kitap açıklaması en az 10 karakter olmalıdır.");
            });
            
            RuleSet("Delete", () =>
            {
                RuleFor(x => x.BookId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });
            
            RuleSet("Update", () =>
            {
                RuleFor(x => x.BookId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });



        }
    }
}
