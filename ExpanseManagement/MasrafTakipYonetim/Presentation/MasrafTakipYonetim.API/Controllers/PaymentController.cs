using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery;
using MasrafTakipYonetim.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentService _paymentService;

        public PaymentController(IMediator mediator, IPaymentService paymentService)
        {
            _mediator = mediator;
            _paymentService = paymentService;
        }


        //[HttpPost("list")]

        //public async Task<IActionResult> GetPaymentListAsync([FromBody] GetPaymentListQueryRequest queryRequest)
        //{
        //    return Ok(await _mediator.Send(queryRequest));      // GET isteği ile alınan sorguyu belirtilen mediator (arama) aracılığıyla çalıştırıp ve sonucu aldım.

        //}
        [HttpPost("list")]
        [Authorize(Roles = "Ödeme Listele")]

        public async Task<IActionResult> GetPaymentsByExpenseCategoryTypeAsync(GetPaymentListsIdQueryRequest queryRequest)
        {
            return Ok(await _mediator.Send(queryRequest));      // GET isteği ile alınan sorguyu belirtilen mediator (arama) aracılığıyla çalıştırıp ve sonucu aldım.

        }


        [HttpPost("Reportlist")]
        [Authorize(Roles = "Ödeme Listele")]

        public async Task<IActionResult> GetPaymentListForReportAsync(GetPaymentListForReportRequest queryRequest)
        {
            return Ok(await _mediator.Send(queryRequest));      // GET isteği ile alınan sorguyu belirtilen mediator (arama) aracılığıyla çalıştırıp ve sonucu aldım.

        }


        [HttpPost("CreatePayment")]
        [Authorize(Roles = "Ödeme Oluştur")]
        public async Task<IActionResult> CreatePayment(CreatePaymentCommandRequest createPaymentCommandRequest)
        {


            return Ok(await _mediator.Send(createPaymentCommandRequest));


        }

        [HttpPut("UpdatePayment")]
        [Authorize(Roles = "Ödeme Güncelle")]

        public async Task<IActionResult> UpdatePayment(UpdatePaymentRequest updatePaymentRequest)
        {
            var result = await _mediator.Send(updatePaymentRequest);

            return Ok(result);


        }


        [HttpDelete("DeletePayment")]
        [Authorize(Roles = "Ödeme Sil")]

        public async Task<IActionResult> DeletePayment([FromQuery] DeletePaymentRequest deletePaymentRequest)
        {
            DeletePaymentResponse deletePaymentResponse = await _mediator.Send(deletePaymentRequest);
            return Ok(deletePaymentResponse);

        }
        [HttpGet("getTotalAmount")]
        //[Authorize(Roles = "Süper Admin,Admin,Kullanıcı")]

        public async Task<IActionResult> GetTotalAmountByExpenseCategoryType([FromQuery] GetPaymentAmountForHomeQueryRequest queryRequest)

        {
            return Ok(await _mediator.Send(queryRequest));      // GET isteği ile alınan sorguyu belirtilen mediator (arama) aracılığıyla çalıştırıp ve sonucu aldım.

        }

        [HttpPost("create-new-period")]
        public async Task<IActionResult> CreateNewPeriod()
        {
            await _paymentService.CreatePaymentsofMountJob(); // veya _paymentService.CreatePaymentsofMountJob();

            return Ok("Yeni dönem başarıyla oluşturuldu.");
        }
    }
}
