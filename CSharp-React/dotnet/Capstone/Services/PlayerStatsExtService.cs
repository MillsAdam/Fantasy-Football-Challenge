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
        private const string RB = "rb";
        private const string WR = "wr";
        private const string TE = "te";
        private const string FLEX = "flex";
        private const string K = "k";
        private const string DEF = "def";

        public PlayerStatsExtService(IConfiguration configuration, QBService qbService)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbService = qbService;
        }

        public async Task<List<PlayerStatsExtDto>> searchPlayerStatsAsync(string position, string interval, string category, string filter, int? week)
        {
            switch(position)
            {
                case QB:
                    return await _qbService.searchQBStatsAsync(interval, category, filter, week);
                case FLEX:
                    return new List<PlayerStatsExtDto>();
                case RB:
                case WR:
                case TE:
                    return new List<PlayerStatsExtDto>();
                case K:
                    return new List<PlayerStatsExtDto>();
                case DEF:
                    return new List<PlayerStatsExtDto>();
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}