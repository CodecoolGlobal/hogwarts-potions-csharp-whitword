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
            var rooms = await _context.Rooms
                .Include(room => room.Residents)
                .ToListAsync();
                
                return rooms.FirstOrDefault(room => room.ID == roomId);
        }

        public Task<List<Room>> GetAllRooms()
        {
            var rooms = _context.Rooms
                .Include(room => room.Residents)
                .ToListAsync();
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
            var rooms = _context.Rooms
                .Where(room => room.Residents
                                   .FirstOrDefault(student =>
                                       student.PetType == PetType.Cat || student.PetType == PetType.Owl) ==
                               null)
                .Include(room => room.Residents)
                .ToList();
            return Task.FromResult<List<Room>>(rooms);
        }

        public Task<List<Room>> AvailableRooms()
        {
            var rooms = _context.Rooms
                .Include(room => room.Residents)
                .Where(room => !room.Residents
                    .Any()).ToList();
            return Task.FromResult<List<Room>>(rooms);
        }

        public async Task<Student> AddStudentToRoom(Student student)
        {
            Student studentToHandle = await _context.Students.FindAsync(student.ID);

            if (studentToHandle == null) return null;
            
            var rooms = GetAllRooms().Result;
            Room roomToAddStudent = AvailableRooms().Result.FirstOrDefault() ??
                                    rooms.FirstOrDefault(r => r.Residents.Count < r.Capacity);
            if (roomToAddStudent == null) return null;

            Room roomToRemoveStudent = rooms.FirstOrDefault(room => room.Residents.Contains(studentToHandle));
            if (roomToRemoveStudent != null)
            {
                Room oldRoomInDb = GetRoom(roomToRemoveStudent.ID).Result;
                oldRoomInDb!.Residents.Remove(studentToHandle);
            }

            Room newRoomInDb = GetRoom(roomToAddStudent.ID).Result;
            newRoomInDb.Residents.Add(studentToHandle);
            studentToHandle.Room = roomToAddStudent;
            await _context.SaveChangesAsync();
            return await _context.Students.FindAsync(student.ID);;
        }
    }
}
