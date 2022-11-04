using Application.Payment.DTOs;

namespace Application.Payment.Ports;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProvider);
}
