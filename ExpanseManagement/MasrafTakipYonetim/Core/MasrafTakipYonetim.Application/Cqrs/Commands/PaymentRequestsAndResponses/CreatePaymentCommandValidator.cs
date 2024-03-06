using FluentValidation;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommandRequest>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(p => p.AppUserId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("Lütfen  Boş Geçmeyiniz!")
            .WithMessage("Lütfen 'Id' alanını Boş Geçmeyiniz!");
           


            RuleFor(p => p.ExpenseCategoryId)
              .NotNull()
              .NotEqual(Guid.Empty)
              .WithMessage("Lütfen  Boş Geçmeyiniz!")
              .WithMessage("Lütfen 'Id' alanını Boş Geçmeyiniz!");
             

            RuleFor(p => p.PaymentAmount)
               .NotNull()
               .WithMessage("Ödeme alanı boş olamaz.")
               .InclusiveBetween(0, 100000)
               .WithMessage("Ödeme alanı 0 ile 100000 arasında olmalıdır.");

        }
    }
}
