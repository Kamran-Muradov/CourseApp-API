﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistWithNameAsync(string name)
        {
            return await _context.Rooms.AnyAsync(m => m.Name == name);
        }

        public async Task<bool> ExistWithNameExceptIdAsync(int id, string name)
        {
            return await _context.Rooms.AnyAsync(m => m.Name == name && m.Id != id);
        }
    }
}
