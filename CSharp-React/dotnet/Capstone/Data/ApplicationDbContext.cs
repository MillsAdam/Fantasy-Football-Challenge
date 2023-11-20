using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamDto> TeamDtos { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerDto> PlayerDtos { get; set; }
        public DbSet<FantasyRoster> FantasyRosters { get; set; }
        public DbSet<RosterPlayer> RosterPlayers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}