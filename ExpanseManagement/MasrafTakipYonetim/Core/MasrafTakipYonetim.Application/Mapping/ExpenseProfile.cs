using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.Expense;
using MasrafTakipYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Mapping
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<CreateExpenseRequestDto, Expense>().ReverseMap();
            CreateMap<UpdateExpenseRequestDto, Expense>().ReverseMap();
            CreateMap<DeleteExpenseRequestDto, Expense>().ReverseMap();
        }
    }
}
