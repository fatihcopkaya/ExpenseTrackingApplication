using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery
{
    public class GetAllWalletByCategoryByExpenseCategoryIdResponse: IRequest<GetAllWalletByCategoryByExpenseCategoryIdRequest>
    {

        public IEnumerable<String> ExpenseCategoryName { get; set; }
        public IEnumerable<float>TotalPaments  { get; set; }
        public IEnumerable<float> TotalExpense { get; set; }
    }
}
