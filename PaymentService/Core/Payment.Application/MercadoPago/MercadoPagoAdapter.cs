﻿using Application;
using Application.Payment.DTOs;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Payment.Application.MercadoPago.Exceptions;

namespace Payment.Application.MercadoPago;

public class MercadoPagoAdapter : IPaymentProcessor
{

    public Task<PaymentResponse> CapturePayment(string paymentIntention)
    {
        try
        {
            if (string.IsNullOrEmpty(paymentIntention))
            {
                throw new InvalidPaymentIntentionException(paymentIntention);
            }

            paymentIntention += "/success";

            var dto = new PaymentStateDto
            {
                CreatedDate = DateTime.Now,
                Message = $"Successfully paid {paymentIntention}",
                PaymentId = "123",
                Status = Status.Success
            };

            var response = new PaymentResponse
            {
                Data = dto,
                Success = true,
                Message = "Payment successfully processed"
            };

            return Task.FromResult(response);
        }
        catch (InvalidPaymentIntentionException)
        {
            var resp = new PaymentResponse()
            {
                Success = false,
                ErrorCode = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                Message = "The selected payment intention is invalid"
            };
            return Task.FromResult(resp);
        }
    }
}