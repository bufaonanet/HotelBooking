using Application.Payment.DTOs;

namespace Application.Payment.Responses;

public class PaymentResponse : Response
{
    public PaymentStateDto Data { get; set; }
}
