using FluentValidation;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses
{
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommandRequest>
    {
        public CreateExpenseCommandValidator()
        {
            RuleFor(p => p.ExpenseCategoryId)
             .NotNull()
             .NotEqual(Guid.Empty)
             .WithMessage("Lütfen  Boş Geçmeyiniz!")
             .WithMessage("Lütfen 'Id' alanını Boş Geçmeyiniz!");

            RuleFor(p => p.Description)
              .NotNull()
              .WithMessage("Lütfen 'Açıklama' yı Boş Geçmeyiniz!")
              .MaximumLength(80)
              .MinimumLength(1)
              .WithMessage("'Açıklama' değeri 1 ile 80 karakter arasında olmalıdır.");

            RuleFor(p => p.Amount)
               .NotNull()
               .WithMessage("Amount alanı boş olamaz.")
               .InclusiveBetween(0, 100000)
               .WithMessage("Amount alanı 0 ile 100000 arasında olmalıdır.");

            RuleFor(p => p.ExpenseTypeId)
              .NotNull()
              .NotEqual(Guid.Empty)
              .WithMessage("Lütfen  Boş Geçmeyiniz!")
              .WithMessage("Lütfen 'Id' alanını Boş Geçmeyiniz!");

        }
    }
}
