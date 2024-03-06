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
    public class UpdateExpenseCommandRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
        //public UpdateExpenseRequestDto updateExpenseDto { get; set; }
    }
}
