using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses
{
    public class CreateExpenseCommandResponse : IRequest<CreateExpenseCommandRequest>
    {
        public string Message { get; set; }
    }
}
