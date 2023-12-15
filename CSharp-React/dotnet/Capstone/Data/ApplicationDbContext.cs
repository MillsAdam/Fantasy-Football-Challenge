using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.Models.Data;
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
        public DbSet<FantasyLineup> FantasyLineups { get; set; }
        public DbSet<LineupPlayer> LineupPlayers { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<PlayerStatsDto> PlayerStatsDtos { get; set; }
        public DbSet<DefenseStats> DefenseStats { get; set; }
        public DbSet<SearchPlayerDto> SearchPlayerDtos { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<PlayerStatsExt> PlayerStatsExt { get; set; }
        public DbSet<PlayerStatsExtDto> PlayerStatsExtDtos { get; set; }
        public DbSet<FantasyLeagueModel> FantasyLeagueModels { get; set; }
        public DbSet<FantasyMember> FantasyMembers { get; set; }
        public DbSet<ReturnLeague> ReturnLeagues { get; set; }
        public DbSet<FantasyLeagueRegistration> FantasyLeagueRegistrations { get; set; }
        
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}