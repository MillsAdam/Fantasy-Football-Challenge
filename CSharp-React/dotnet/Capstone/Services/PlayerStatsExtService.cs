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
        private KService _kService;
        private DefService _defService;
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
            FlexPosService flexPosService, 
            KService kService, 
            DefService defService)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbService = qbService;
            _flexService = flexService;
            _flexPosService = flexPosService;
            _kService = kService;
            _defService = defService;
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
                    return await _kService.searchKStatsAsync(interval, category, filter, week);
                case DEF:
                    return await _defService.searchDefStatsAsync(interval, category, filter, week);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}