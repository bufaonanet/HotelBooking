namespace Application.Guest.DTOs;

using Domain.Guest.Enums;
using Domain.Guest.ValueObjects;
using Guest = Domain.Guest.Entities.Guest;

public class GuestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string IdNumber { get; set; }
    public int IdTypeCode { get; set; }

    public Guest MapToEntity()
    {
        return new Guest
        {
            Id = this.Id,
            Name = this.Name,
            Surname = this.Surname,
            Email = this.Email,
            DocumentId = new PersonId
            {
                IdNumber = this.IdNumber,
                DocumentType = (DocumentType)this.IdTypeCode
            }
        };
    }

    public  static GuestDTO MapToDto(Guest guest)
    {
        return new GuestDTO
        {
            Id = guest.Id,
            Email = guest.Email,
            IdNumber = guest.DocumentId.IdNumber,
            IdTypeCode = (int)guest.DocumentId.DocumentType,
            Name = guest.Name,
            Surname = guest.Surname,
        };
    }
}
