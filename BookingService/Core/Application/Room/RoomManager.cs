using Application.Room.DTOs;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Room;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request)
    {
        try
        {
            var room = RoomDto.MapToEntity(request.Data);

            await room.Save(_roomRepository);
            request.Data.Id = room.Id;

            return new RoomResponse
            {
                Success = true,
                Data = request.Data,
            };

        }
        catch (InvalidRoomPriceException)
        {

            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_INVALID_PRICE,
                Message = "Price invalid"
            };
        }
        catch (InvalidRoomDataException)
        {

            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information passed"
            };
        }
        catch (Exception)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }

    }

    public async Task<RoomResponse> GetRoomAsync(int roomId)
    {
        var room = await _roomRepository.GetAsync(roomId);

        if (room is null)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                Message = "No Room record was found with the given Id"
            };
        }

        return new RoomResponse
        {
            Data = RoomDto.MapToDto(room),
            Success = true
        };
    }
}
