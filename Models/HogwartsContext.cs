using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }
    }
}
