using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Expense;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses
{
    public class DeleteExpenseCommandRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
    }
      //public DeleteExpenseRequestDto deleteExpenseDto { get; set; }
}
