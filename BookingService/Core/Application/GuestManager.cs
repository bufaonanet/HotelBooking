using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Ports;

namespace Application;

public class GuestManager : IGuestManager
{
    private readonly IGuestRepository _guestRepository;

    public GuestManager(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
    {
        try
        {
            var guest = request.Data.MapToEntity();

            request.Data.Id = await _guestRepository.CreateAsync(guest);

            return new GuestResponse
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception)
        {
            return new GuestResponse
            {                
                Success = false,
                ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }
    }

    public Task<GuestResponse> GetGuest(int guestId)
    {
        throw new NotImplementedException();
    }
}
