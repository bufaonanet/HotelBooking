using Application.Room.Requests;
using Application.Room.Responses;

namespace Application.Room.Ports;

public interface IRoomManager
{
    Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request);
    Task<RoomResponse> GetRoomAsync(int roomId);
}
