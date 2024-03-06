using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class ExpenseTypeProfile:Profile
    {
        public ExpenseTypeProfile()
        {
            CreateMap<CreateExpenseTypeRequestDto, ExpenseType>().ReverseMap();
            CreateMap<UpdateExpenseTypeRequestDto, ExpenseType>().ReverseMap();
            CreateMap<DeleteExpenseTypeRequestDto, ExpenseType>().ReverseMap();
        }
    }
}
