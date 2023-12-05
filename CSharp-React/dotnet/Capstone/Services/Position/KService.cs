using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO.Position.Kicker;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;

namespace Capstone.Services.Position
{
    public class KService
    {
        private readonly string _connectionString;
        private IKSeasonTotalDao _kSeasonTotalDao;
        private IKSeasonAverageDao _kSeasonAverageDao;
        private IKLast4TotalDao _kLast4TotalDao;
        private IKLast4AverageDao _kLast4AverageDao;
        private IKWeeklyTotalDao _kWeeklyTotalDao;
        private IKWeeklyProjectedDao _kWeeklyProjectedDao;
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

        public KService(IConfiguration configuration, 
            IKSeasonTotalDao kSeasonTotalDao, 
            IKSeasonAverageDao kSeasonAverageDao, 
            IKLast4TotalDao kLast4TotalDao, 
            IKLast4AverageDao kLast4AverageDao, 
            IKWeeklyTotalDao kWeeklyTotalDao, 
            IKWeeklyProjectedDao kWeeklyProjectedDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _kSeasonTotalDao = kSeasonTotalDao;
            _kSeasonAverageDao = kSeasonAverageDao;
            _kLast4TotalDao = kLast4TotalDao;
            _kLast4AverageDao = kLast4AverageDao;
            _kWeeklyTotalDao = kWeeklyTotalDao;
            _kWeeklyProjectedDao = kWeeklyProjectedDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchKStatsAsync(string interval, string category, string filter, int? week)
        {
            switch(interval)
            {
                case SEASON_TOTAL:
                    return await handleSeasonTotal(category, filter);
                case SEASON_AVERAGE:
                    return await handleSeasonAverage(category, filter);
                case LAST_4_TOTAL:
                    return await handleLast4Total(category, filter);
                case LAST_4_AVERAGE:
                    return await handleLast4Average(category, filter);
                case WEEKLY_TOTAL:
                    return await handleWeeklyTotal(category, filter, week);
                case WEEKLY_PROJECTED:
                    return await handleWeeklyProjected(category, filter, week);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonTotal(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _kSeasonTotalDao.getKSeasonTotalStatsAsync();
                case CONF:
                    return await _kSeasonTotalDao.getKSeasonTotalStatsByConfAsync(filter);
                case TEAM:
                    return await _kSeasonTotalDao.getKSeasonTotalStatsByTeamAsync(filter);
                case NAME:
                    return await _kSeasonTotalDao.getKSeasonTotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonAverage(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _kSeasonAverageDao.getKSeasonAverageStatsAsync();
                case CONF:
                    return await _kSeasonAverageDao.getKSeasonAverageStatsByConfAsync(filter);
                case TEAM:
                    return await _kSeasonAverageDao.getKSeasonAverageStatsByTeamAsync(filter);
                case NAME:
                    return await _kSeasonAverageDao.getKSeasonAverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Total(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _kLast4TotalDao.getKLast4TotalStatsAsync();
                case CONF:
                    return await _kLast4TotalDao.getKLast4TotalStatsByConfAsync(filter);
                case TEAM:
                    return await _kLast4TotalDao.getKLast4TotalStatsByTeamAsync(filter);
                case NAME:
                    return await _kLast4TotalDao.getKLast4TotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Average(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _kLast4AverageDao.getKLast4AverageStatsAsync();
                case CONF:
                    return await _kLast4AverageDao.getKLast4AverageStatsByConfAsync(filter);
                case TEAM:
                    return await _kLast4AverageDao.getKLast4AverageStatsByTeamAsync(filter);
                case NAME:
                    return await _kLast4AverageDao.getKLast4AverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyTotal(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _kWeeklyTotalDao.getKWeeklyTotalStatsAsync(week.Value);
                case CONF:
                    return await _kWeeklyTotalDao.getKWeeklyTotalStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _kWeeklyTotalDao.getKWeeklyTotalStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _kWeeklyTotalDao.getKWeeklyTotalStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyProjected(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _kWeeklyProjectedDao.getKWeeklyProjectedStatsAsync(week.Value);
                case CONF:
                    return await _kWeeklyProjectedDao.getKWeeklyProjectedStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _kWeeklyProjectedDao.getKWeeklyProjectedStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _kWeeklyProjectedDao.getKWeeklyProjectedStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}