using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO.Position.Flex;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;

namespace Capstone.Services.Position
{
    public class FlexService
    {
        private readonly string _connectionString;
        private IFlexSeasonTotalDao _flexSeasonTotalDao;
        private IFlexSeasonAverageDao _flexSeasonAverageDao;
        private IFlexLast4TotalDao _flexLast4TotalDao;
        private IFlexLast4AverageDao _flexLast4AverageDao;
        private IFlexWeeklyTotalDao _flexWeeklyTotalDao;
        private IFlexWeeklyProjectedDao _flexWeeklyProjectedDao;
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

        public FlexService(IConfiguration configuration, 
            IFlexSeasonTotalDao flexSeasonTotalDao, 
            IFlexSeasonAverageDao flexSeasonAverageDao, 
            IFlexLast4TotalDao flexLast4TotalDao, 
            IFlexLast4AverageDao flexLast4AverageDao, 
            IFlexWeeklyTotalDao flexWeeklyTotalDao, 
            IFlexWeeklyProjectedDao flexWeeklyProjectedDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _flexSeasonTotalDao = flexSeasonTotalDao;
            _flexSeasonAverageDao = flexSeasonAverageDao;
            _flexLast4TotalDao = flexLast4TotalDao;
            _flexLast4AverageDao = flexLast4AverageDao;
            _flexWeeklyTotalDao = flexWeeklyTotalDao;
            _flexWeeklyProjectedDao = flexWeeklyProjectedDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchFlexStatsAsync(string interval, string category, string filter, int? week)
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
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsAsync();
                case CONF:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByConfAsync(filter);
                case TEAM:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByTeamAsync(filter);
                case NAME:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonAverage(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsAsync();
                case CONF:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByConfAsync(filter);
                case TEAM:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByTeamAsync(filter);
                case NAME:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Total(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsAsync();
                case CONF:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByConfAsync(filter);
                case TEAM:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByTeamAsync(filter);
                case NAME:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Average(string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsAsync();
                case CONF:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByConfAsync(filter);
                case TEAM:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByTeamAsync(filter);
                case NAME:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByNameAsync(filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyTotal(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsAsync(week.Value);
                case CONF:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyProjected(string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsAsync(week.Value);
                case CONF:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByConfAsync(filter, week.Value);
                case TEAM:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByTeamAsync(filter, week.Value);
                case NAME:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByNameAsync(filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}