using Domain.Room.Enums;

namespace Domain.Room.ValueObjects;

public class Price
{
    public decimal Value { get; set; }
    public AccepedCurrencies Currency { get; set; }
}