using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ProfileInfoRequestsAndResponses
{
    public class UpdateProfileInfoCommandRequest:IRequest<Results>
    {
        
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public string ?Email { get; set; }
        public string ?PhoneNumber { get; set; }
    }
}
