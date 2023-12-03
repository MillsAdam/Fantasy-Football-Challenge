using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Capstone.DAO.Position.Quarterback;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;

namespace Capstone.Services.Position
{
    public class QBService
    {
        private readonly string _connectionString;
        private IQBSeasonTotalDao _qbSeasonTotalDao;
        private const string SEASON = "season";
        private const string TOTAL = "total";
        private const string ALL = "all";
        private const string CONF = "conf";
        private const string TEAM = "team";
        private const string NAME = "name";

        public QBService(IConfiguration configuration, IQBSeasonTotalDao qbSeasonTotalDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbSeasonTotalDao = qbSeasonTotalDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchQBStatsAsync(string interval, string points, string category, string filter, int? week)
        {
            switch(interval)
            {
                case SEASON:
                    return await handleSeasonInterval(points, category, filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonInterval(string points, string category, string filter)
        {
            switch(points)
            {
                case TOTAL:
                    return await handleSeasonTotal(category, filter);
                default: 
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonTotal(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _qbSeasonTotalDao.getQBSeasonTotalStatsAsync();
                case CONF:
                    return await _qbSeasonTotalDao.getQBSeasonTotalStatsByConfAsync(filter);
                case TEAM:
                    return await _qbSeasonTotalDao.getQBSeasonTotalStatsByTeamAsync(filter);
                case NAME:
                    return await _qbSeasonTotalDao.getQBSeasonTotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

    }
}