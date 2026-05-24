using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace HotelBookingSystemApi.Controllers
{
    [ApiController]
    [Route("api/v1/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(_service.GetAllRooms());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _service.GetRoomById(id);

            if (room == null)
                return NotFound("Room not found");

            return Ok(room);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddRoom(RoomDto dto)
        {
            return Ok(_service.AddRoom(dto));
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, RoomDto dto)
        {
            return Ok(_service.UpdateRoom(id, dto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            return Ok(_service.DeleteRoom(id));
        }
    }
}