using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;


namespace BusinessLayer.ValidationRules
{
    public class AuthorValidator  :AbstractValidator<Author>
    {
        public AuthorValidator() 
        {

            RuleSet("Insert", () =>
            {
                RuleFor(x => x.AuthorName)
                    .NotEmpty().WithMessage("Yazar ismi boş olamaz.")
                    .MinimumLength(2).WithMessage("Yazar ismi en az 2 karakter olmalıdır.")
                    .MaximumLength(20).WithMessage("Yazar ismi en fazla 20 karakter olmalıdır.");

                RuleFor(x => x.AuthorLastname)
                    .NotEmpty().WithMessage("Yazar ismi boş olamaz.")
                    .MinimumLength(2).WithMessage("Yazar ismi en az 2 karakter olmalıdır.")
                    .MaximumLength(20).WithMessage("Yazar ismi en fazla 20 karakter olmalıdır.");
            });

            RuleSet("Delete", () =>
            {
                RuleFor(x => x.AuthorId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });

            RuleSet("Update", () => 
            {
                RuleFor(x => x.AuthorId)
                    .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");
            });


        }
    }
}
