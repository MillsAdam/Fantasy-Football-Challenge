using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Capstone.Services.Position;
using Microsoft.Extensions.Configuration;

namespace Capstone.Services
{
    public class PlayerStatsExtService
    {
        private readonly string _connectionString;
        private QBService _qbService;
        private const string QB = "qb";

        public PlayerStatsExtService(IConfiguration configuration, QBService qbService)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbService = qbService;
        }

        public async Task<List<PlayerStatsExtDto>> searchPlayerStatsAsync(string position, string interval, string category, string filter, int? week)
        {
            List<PlayerStatsExtDto> playerStatsExtDtos = new List<PlayerStatsExtDto>();
            switch(position)
            {
                case QB:
                    playerStatsExtDtos = await _qbService.searchQBStatsAsync(interval, category, filter, week);
                    break;
            }

            return playerStatsExtDtos;
        }
    }
}