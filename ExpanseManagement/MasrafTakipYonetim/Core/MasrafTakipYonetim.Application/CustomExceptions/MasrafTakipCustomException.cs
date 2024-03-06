using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.CustomExceptions
{
    public class MasrafTakipCustomException : Exception
    {
        public int StatusCode { get; } // StatusCode özelliği eklendi

        public MasrafTakipCustomException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
