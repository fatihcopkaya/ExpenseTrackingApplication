using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ChangePasswordRequestAndResponses
{
    public class ChangePasswordResponse : IRequest<ChangePasswordRequest>
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
