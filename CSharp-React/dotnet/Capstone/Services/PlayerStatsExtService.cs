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
        private FlexService _flexService;
        private FlexPosService _flexPosService;
        private const string QB = "qb";
        private const string RB = "rb";
        private const string WR = "wr";
        private const string TE = "te";
        private const string FLEX = "flex";
        private const string K = "k";
        private const string DEF = "def";

        public PlayerStatsExtService(IConfiguration configuration, 
            QBService qbService, 
            FlexService flexService, 
            FlexPosService flexPosService)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbService = qbService;
            _flexService = flexService;
            _flexPosService = flexPosService;
        }

        public async Task<List<PlayerStatsExtDto>> searchPlayerStatsAsync(string position, string interval, string category, string filter, int? week)
        {
            switch(position)
            {
                case QB:
                    return await _qbService.searchQBStatsAsync(interval, category, filter, week);
                case FLEX:
                    return await _flexService.searchFlexStatsAsync(interval, category, filter, week);
                case RB:
                case WR:
                case TE:
                    return await _flexPosService.searchFlexPosStatsAsync(position, interval, category, filter, week);
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