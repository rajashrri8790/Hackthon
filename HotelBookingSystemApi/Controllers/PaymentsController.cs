using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace HotelBookingSystemApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult ProcessPayment(PaymentDto dto)
        {
            return Ok(_service.ProcessPayment(dto));
        }
    }
}
