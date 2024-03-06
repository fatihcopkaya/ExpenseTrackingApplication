using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Dtos.Payment;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;

using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryQueries
{
    public class GetExpenseCategoryListQueryHandler : IRequestHandler<GetExpenseCategoryListQueryRequest,Results>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly ICacheService _redisService;
        public GetExpenseCategoryListQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, ICacheService redis)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _redisService = redis;   
        }

        public async Task<Results> Handle(GetExpenseCategoryListQueryRequest request, CancellationToken cancellationToken)
        {

            var cacheKey = $"ExpenseCategoryList";//Bir önbellek anahtarı oluşturuyorum. Bu anahtar, belirli bir önbellek girişini tanımlar. Bu durumda, "ExpenseCategoryList" adlı bir anahtar kullanılıyor.
            // Redis cache'ten veri okuma
            var cachedData = await _redisService.GetAsync<List<ListExpenseCategoryRequestDto>>(cacheKey);
            if (cachedData != null)
            {
                return Results<List<ListExpenseCategoryRequestDto>>.Success(cachedData);
            }

            var expneseCategoryList = await _expenseCategoryRepository.GetListAsync(x=>!x.IsDeleted);
            var expenseCategoryDto= expneseCategoryList.Select(p=> new ListExpenseCategoryRequestDto()
            {
                Id = p.Id,
                Name = p.Name,
                //UserExpenseCategory=p.UserExpenseCategory,
                CreatedDate=p.CreatedDate,
                UpdatedDate=p.UpdatedDate,
                IsDeleted=p.IsDeleted




            }).AsQueryable();

            if (expenseCategoryDto.ToList().Count == 0) {

                throw new MasrafTakipCustomException($"{nameof(ExpenseCategory)} List Not Found", 404);

            }


            //Results results;


            //if (!string.IsNullOrEmpty(request.SortField))
            //{
            //    //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            //    expenseCategoryDto = expenseCategoryDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            //}
            //else
            //{
            //    results = await Task.FromResult(expenseCategoryDto.OrderBy(x => x.Id)
            //        .QueryResourceNotMapped<ListExpenseCategoryRequestDto>(request.PageNumber, request.PageSize));
            //}


            await _redisService.SetAsync(cacheKey, expenseCategoryDto.ToList(), config =>
            {
                // Cache ayarlarını burada belirleyebilirsiniz.
                config.AbsoluteExpiration = 60; // 60 dakika
                config.SlidingExpiration = 30; // 30 dakika
            });

            return Results<List<ListExpenseCategoryRequestDto>>.Success(expenseCategoryDto.ToList());

            //results = await Task.FromResult(expenseCategoryDto.QueryResourceNotMapped<ListExpenseCategoryRequestDto>(request.PageNumber, request.PageSize));
            //return results;
        }
    }
}
