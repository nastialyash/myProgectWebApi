using Microsoft.EntityFrameworkCore;
using myProgectWebApi.DAL.Entities;
using System.Collections.Generic;

namespace myProgectWebApi.DAL
{
    public class GameDbContext:DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
    }
}
