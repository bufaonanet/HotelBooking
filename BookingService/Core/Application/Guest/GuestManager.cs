﻿using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Guest;

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
            await guest.Save(_guestRepository);

            request.Data.Id = guest.Id;

            return new GuestResponse
            {
                Data = request.Data,
                Success = true
            };

        }
        catch (InvalidPersonDocumentIdException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                Message = "The ID passed is not valid"
            };
        }
        catch (MissingRequiredInformation)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information passed"
            };
        }
        catch (InvalidEmailException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_EMAIL,
                Message = "The given email is not valid"
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

    public async Task<GuestResponse> GetGuest(int guestId)
    {
        var guest = await _guestRepository.GetAsync(guestId);

        if(guest is null)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                Message = "No Guest record was found with the given Id"
            };
        }

        return new GuestResponse
        {
            Data = GuestDTO.MapToDto(guest),
            Success = true,
        };
    }
}
