using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class CreateAppUserCommandValidator:AbstractValidator<CreateAppUserCommandRequest>
    {
        public CreateAppUserCommandValidator() 
        {
            RuleFor(p => p.FirstName)
              .NotNull()
              .WithMessage("Lütfen 'Adı' Boş Geçmeyiniz!")
              .MaximumLength(50)
              .MinimumLength(3)
              .WithMessage("'Adı' değeri 3 ile 50 karakter arasında olmalıdır.");

            RuleFor(p => p.LastName)
              .NotNull()
              .WithMessage("Lütfen 'Soyadı' nı Boş Geçmeyiniz!")
              .MaximumLength(50)
              .MinimumLength(2)
              .WithMessage("'Soyadı' değeri 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(p => p.Email)
              .NotNull()
              .Must(email => email.Contains("@"))
              .WithMessage("'Email' alanı geçerli bir email adresi içermelidir.")
              .MaximumLength(50)
              .MinimumLength(3)
              .WithMessage("'Email' alanı geçerli bir email adresi içermelidir.");

            RuleFor(p => p.PhoneNumber)
              .NotNull()
              .WithMessage("Lütfen 'Telefon Numarası'nı Boş Geçmeyiniz!")
              .MaximumLength(11)
              .MinimumLength(10)
              .WithMessage("'Telefon Numarası' değeri 10 ile 11 karakter arasında olmalıdır.");

            RuleFor(p => p.Password)
              .NotNull()
              .WithMessage("Lütfen 'Şifre'yi Boş Geçmeyiniz!")
              .MaximumLength(50)
              .MinimumLength(3)
              .WithMessage("'FirstName' değeri 3 ile 20 karakter arasında olmalıdır.");



        }
    }
}
