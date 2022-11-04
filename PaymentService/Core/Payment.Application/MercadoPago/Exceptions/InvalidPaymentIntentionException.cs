namespace Payment.Application.MercadoPago.Exceptions;

public class InvalidPaymentIntentionException : Exception
{
    public InvalidPaymentIntentionException(string message) : base(message)
    { }
}
