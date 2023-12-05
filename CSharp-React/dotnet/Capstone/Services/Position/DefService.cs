using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO.Position.Defense;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;

namespace Capstone.Services.Position
{
    public class DefService
    {
        private readonly string _connectionString;
        private IDefSeasonTotalDao _defSeasonTotalDao;
        private IDefSeasonAverageDao _defSeasonAverageDao;
        private IDefLast4TotalDao _defLast4TotalDao;
        private IDefLast4AverageDao _defLast4AverageDao;
        private IDefWeeklyTotalDao _defWeeklyTotalDao;
        private IDefWeeklyProjectedDao _defWeeklyProjectedDao;
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

        public DefService(IConfiguration configuration, 
            IDefSeasonTotalDao defSeasonTotalDao, 
            IDefSeasonAverageDao defSeasonAverageDao, 
            IDefLast4TotalDao defLast4TotalDao, 
            IDefLast4AverageDao defLast4AverageDao, 
            IDefWeeklyTotalDao defWeeklyTotalDao, 
            IDefWeeklyProjectedDao defWeeklyProjectedDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _defSeasonTotalDao = defSeasonTotalDao;
            _defSeasonAverageDao = defSeasonAverageDao;
            _defLast4TotalDao = defLast4TotalDao;
            _defLast4AverageDao = defLast4AverageDao;
            _defWeeklyTotalDao = defWeeklyTotalDao;
            _defWeeklyProjectedDao = defWeeklyProjectedDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchDefStatsAsync(string interval, string category, string filter, int? week)
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
                    return await _defSeasonTotalDao.getDefSeasonTotalStatsAsync();
                case CONF:
                    return await _defSeasonTotalDao.getDefSeasonTotalStatsByConfAsync(filter);
                case TEAM:
                    return await _defSeasonTotalDao.getDefSeasonTotalStatsByTeamAsync(filter);
                case NAME:
                    return await _defSeasonTotalDao.getDefSeasonTotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonAverage(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _defSeasonAverageDao.getDefSeasonAverageStatsAsync();
                case CONF:
                    return await _defSeasonAverageDao.getDefSeasonAverageStatsByConfAsync(filter);
                case TEAM:
                    return await _defSeasonAverageDao.getDefSeasonAverageStatsByTeamAsync(filter);
                case NAME:
                    return await _defSeasonAverageDao.getDefSeasonAverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Total(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _defLast4TotalDao.getDefLast4TotalStatsAsync();
                case CONF:
                    return await _defLast4TotalDao.getDefLast4TotalStatsByConfAsync(filter);
                case TEAM:
                    return await _defLast4TotalDao.getDefLast4TotalStatsByTeamAsync(filter);
                case NAME:
                    return await _defLast4TotalDao.getDefLast4TotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Average(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _defLast4AverageDao.getDefLast4AverageStatsAsync();
                case CONF:
                    return await _defLast4AverageDao.getDefLast4AverageStatsByConfAsync(filter);
                case TEAM:
                    return await _defLast4AverageDao.getDefLast4AverageStatsByTeamAsync(filter);
                case NAME:
                    return await _defLast4AverageDao.getDefLast4AverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyTotal(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _defWeeklyTotalDao.getDefWeeklyTotalStatsAsync(week.Value);
                case CONF:
                    return await _defWeeklyTotalDao.getDefWeeklyTotalStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _defWeeklyTotalDao.getDefWeeklyTotalStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _defWeeklyTotalDao.getDefWeeklyTotalStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyProjected(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _defWeeklyProjectedDao.getDefWeeklyProjectedStatsAsync(week.Value);
                case CONF:
                    return await _defWeeklyProjectedDao.getDefWeeklyProjectedStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _defWeeklyProjectedDao.getDefWeeklyProjectedStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _defWeeklyProjectedDao.getDefWeeklyProjectedStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}