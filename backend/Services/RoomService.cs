using Data;
using DTOs;
using Models;
using Services.Interfaces;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room? GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public string AddRoom(RoomDto dto)
        {
            var exists = _context.Rooms.Any(x => x.RoomNumber == dto.RoomNumber);

            if (exists)
                return "Room number already exists";

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                PricePerNight = dto.PricePerNight,
                Capacity = dto.Capacity,
                Amenities = dto.Amenities,
                IsAvailable = true
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return "Room added successfully";
        }

        public string UpdateRoom(int id, RoomDto dto)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.Id == id);

            if (room == null)
                return "Room not found";

            room.RoomNumber = dto.RoomNumber;
            room.RoomType = dto.RoomType;
            room.PricePerNight = dto.PricePerNight;
            room.Capacity = dto.Capacity;
            room.Amenities = dto.Amenities;

            _context.SaveChanges();

            return "Room updated successfully";
        }

        public string DeleteRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.Id == id);

            if (room == null)
                return "Room not found";

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return "Room deleted successfully";
        }
    }
}