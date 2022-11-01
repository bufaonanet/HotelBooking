namespace Application.Guest.DTOs;
using Guest = Domain.Entities.Guest;

public class GuestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string IdNumber { get; set; }
    public int IdTypeCode { get; set; }

    public Guest DtoToEntity()
    {
        return new Guest
        {
            Id = this.Id,
            Name = this.Name,
            Surname = this.Surname,
            Email = this.Email,
            DocumentId = new Domain.ValueObjects.PersonId
            {
                IdNumber = this.IdNumber,
                DocumentType = (Domain.Enums.DocumentType)this.IdTypeCode
            }
        };
    }
}
