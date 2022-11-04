using Application.Payment.DTOs;
using Application.Payment.Ports;
using Payment.Application.MercadoPago;

namespace Payment.Application;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProvider)
    {
        switch (selectedPaymentProvider)
        {
            case SupportedPaymentProviders.MercadoPago:
                return new MercadoPagoAdapter();

            default: return new NotImplementedPaymentProvider();
        }
    }
}