using Application;
using Application.Payment.DTOs;
using Payment.Application;
using Payment.Application.MercadoPago;

namespace Payments.UnitTests;

public class MercadoPagoTests
{
    [Fact]
    public void ShouldReturn_MercadoPagoAdapter_Provider()
    {
        var factory = new PaymentProcessorFactory();

        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

        Assert.Equal(typeof(MercadoPagoAdapter), provider.GetType());
    }

    [Fact]
    public async Task Should_FailWhenPaymentIntentionStringIsInvalid()
    {
        var factory = new PaymentProcessorFactory();

        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

        var res = await provider.CapturePayment("");

        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION, res.ErrorCode);
        Assert.Equal("The selected payment intention is invalid", res.Message);
    }

    [Fact]
    public async Task Should_SuccessfullyProcessPayment()
    {
        var factory = new PaymentProcessorFactory();

        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

        var res = await provider.CapturePayment("https://mercadopago.com.br/asdf");

        Assert.True(res.Success);
        Assert.Equal("Payment successfully processed", res.Message);
        Assert.NotNull(res.Data);        
        Assert.NotNull(res.Data.PaymentId);
    }

}