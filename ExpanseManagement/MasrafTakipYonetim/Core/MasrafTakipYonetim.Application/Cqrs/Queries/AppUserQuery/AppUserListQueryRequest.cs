using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class AppUserListQueryRequest : BasePagination<Results>
    {
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public string ?Email { get; set; }
        public string ?PhoneNumber { get; set; }
       
        public string? RoleName { get; set; }


        //public ExpenseCategoryType ExpenseCategoryType { get; set; }
       
    }
}

