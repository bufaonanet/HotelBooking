namespace Domain.Booking.Enums;

public enum Action
{
    Pay = 0,
    Finish = 1, //somente depois de pago e finalizado
    Cancel = 2, // não pode ser cancelado depois de pago
    Refound = 3, // somente depois de pago 
    Reopen = 4, // somente depois de cancelado
}