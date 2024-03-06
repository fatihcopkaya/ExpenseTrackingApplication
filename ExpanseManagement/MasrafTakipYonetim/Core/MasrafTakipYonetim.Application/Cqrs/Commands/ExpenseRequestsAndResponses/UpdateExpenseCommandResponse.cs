using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses
{
    public class UpdateExpenseCommandResponse : IRequest<UpdateExpenseCommandRequest>
    {
        public string Message { get; set; }
    }
}
