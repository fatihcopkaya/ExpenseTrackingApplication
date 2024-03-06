using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class ExpenseCategoryProfile:Profile
    {
       public  ExpenseCategoryProfile() 
        {
            CreateMap<CreateExpenseCategoryRequestDto, ExpenseCategory>().ReverseMap();
            CreateMap<DeleteExpenseCategoryRequestDto, ExpenseCategory>().ReverseMap();
            CreateMap<UpdateExpenseCategoryRequestDto, ExpenseCategory>().ReverseMap();
        
        
        }    
    }
}
