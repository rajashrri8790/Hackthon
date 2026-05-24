using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        string ProcessPayment(PaymentDto dto);
    }
}
