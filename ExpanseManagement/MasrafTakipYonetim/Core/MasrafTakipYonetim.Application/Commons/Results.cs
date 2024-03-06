using MasrafTakipYonetim.Application.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Commons
{
    public class Results<T>:Results
    {
        private Results(T data, bool isSuccess, IEnumerable<string> errors) : base(isSuccess, errors)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public static Results<T> Success(T data)
        {
            return new Results<T>(data, true, new string[] { });
        }
    }

    public class Results
    {
        protected Results(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors.ToArray();
        }

        protected Results()
        {
            IsSuccess = true;
            Errors = new string[] { };
        }

        public bool IsSuccess { get; private set; }
        public string[] Errors { get; private set; }

        public static Results Failure(IEnumerable<string> errors)
        {
            return new Results(false, errors);
        }

        public static Results Success()
        {
            return new Results(true, new List<string>());
        }
    }
}
