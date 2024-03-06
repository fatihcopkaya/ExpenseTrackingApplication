using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Wallet
{
    public class GetWalletInfoDto
    {
        public float TotalPayments { get; set; }
        public float TotalExpenses { get; set; }
    }
}
