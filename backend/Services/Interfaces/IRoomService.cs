using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();
        Room? GetRoomById(int id);
        string AddRoom(RoomDto dto);
        string UpdateRoom(int id, RoomDto dto);
        string DeleteRoom(int id);
    }
}
