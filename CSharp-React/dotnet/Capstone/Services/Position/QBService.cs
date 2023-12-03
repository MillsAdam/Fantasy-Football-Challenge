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
        private IQBSeasonAverageDao _qbSeasonAverageDao;
        private IQBLast4TotalDao _qbLast4TotalDao;
        private IQBLast4AverageDao _qbLast4AverageDao;
        private IQBWeeklyTotalDao _qbWeeklyTotalDao;
        private IQBWeeklyProjectedDao _qbWeeklyProjectedDao;
        private const string SEASON_TOTAL = "season total";
        private const string SEASON_AVERAGE = "season average";
        private const string LAST_4_TOTAL = "last 4 total";
        private const string LAST_4_AVERAGE = "last 4 average";
        private const string WEEKLY_TOTAL = "weekly total";
        private const string WEEKLY_PROJECTED = "weekly projected";
        private const string ALL = "all";
        private const string CONF = "conf";
        private const string TEAM = "team";
        private const string NAME = "name";

        public QBService(IConfiguration configuration, 
            IQBSeasonTotalDao qbSeasonTotalDao, 
            IQBSeasonAverageDao qbSeasonAverageDao, 
            IQBLast4TotalDao qbLast4TotalDao, 
            IQBLast4AverageDao qbLast4AverageDao, 
            IQBWeeklyTotalDao qbWeeklyTotalDao, 
            IQBWeeklyProjectedDao qbWeeklyProjectedDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _qbSeasonTotalDao = qbSeasonTotalDao;
            _qbSeasonAverageDao = qbSeasonAverageDao;
            _qbLast4TotalDao = qbLast4TotalDao;
            _qbLast4AverageDao = qbLast4AverageDao;
            _qbWeeklyTotalDao = qbWeeklyTotalDao;
            _qbWeeklyProjectedDao = qbWeeklyProjectedDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchQBStatsAsync(string interval, string category, string filter, int? week)
        {
            switch(interval)
            {
                case SEASON_TOTAL:
                    return await handleSeasonTotal(category, filter);
                case SEASON_AVERAGE:
                    return await handleSeasonAverage(category, filter);
                case LAST_4_TOTAL:
                    return await handleLast4Total(category, filter);
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

        private async Task<List<PlayerStatsExtDto>> handleSeasonAverage(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _qbSeasonAverageDao.getQBSeasonAverageStatsAsync();
                case CONF:
                    return await _qbSeasonAverageDao.getQBSeasonAverageStatsByConfAsync(filter);
                case TEAM:
                    return await _qbSeasonAverageDao.getQBSeasonAverageStatsByTeamAsync(filter);
                case NAME:
                    return await _qbSeasonAverageDao.getQBSeasonAverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Total(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _qbLast4TotalDao.getQBLast4TotalStatsAsync();
                case CONF:
                    return await _qbLast4TotalDao.getQBLast4TotalStatsByConfAsync(filter);
                case TEAM:
                    return await _qbLast4TotalDao.getQBLast4TotalStatsByTeamAsync(filter);
                case NAME:
                    return await _qbLast4TotalDao.getQBLast4TotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

    }
}