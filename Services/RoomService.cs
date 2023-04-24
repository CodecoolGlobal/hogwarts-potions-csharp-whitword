using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class RoomService : IRoomService
    {
        public const int MaxIngredientsForPotions = 5;
        private readonly HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context = context;
        }

        

        public async Task AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(long roomId)
        {
            var room = await _context.Rooms
                .FindAsync(roomId);
            return room;
        }

        public Task<List<Room>> GetAllRooms()
        {
            var rooms = _context.Rooms.ToListAsync();
            return rooms;
        }

        public Task UpdateRoom(Room room)
        {
            var roomToUpdate = _context.Rooms
                .Find(room.ID);
            if (roomToUpdate != null)
            {
                _context.Rooms.Remove(roomToUpdate);
                _context.Rooms.Add(room);
                _context.SaveChangesAsync();
            }
            return GetRoom(room.ID);
        }

        public Task DeleteRoom(long id)
        {
            var roomToUpdate = _context.Rooms
                .Find(id);
            if (roomToUpdate != null) _context.Remove((object)roomToUpdate);
            return GetAllRooms();
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            var rooms =  _context.Rooms.ToList()
                .Where(room => room.Residents
                                   .FirstOrDefault(student =>
                                       student.PetType == PetType.Cat || student.PetType == PetType.Owl) ==
                               null).ToList();
            return Task.FromResult<List<Room>>(rooms);
        }
    }
}
