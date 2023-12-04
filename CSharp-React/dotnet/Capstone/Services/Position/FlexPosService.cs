using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO.Position.Flex;
using Capstone.Models.Data;

namespace Capstone.Services.Position
{
    public class FlexPosService
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

        public FlexPosService(IFlexSeasonTotalDao flexSeasonTotalDao, 
            IFlexSeasonAverageDao flexSeasonAverageDao, 
            IFlexLast4TotalDao flexLast4TotalDao, 
            IFlexLast4AverageDao flexLast4AverageDao, 
            IFlexWeeklyTotalDao flexWeeklyTotalDao, 
            IFlexWeeklyProjectedDao flexWeeklyProjectedDao)
        {
            _flexSeasonTotalDao = flexSeasonTotalDao;
            _flexSeasonAverageDao = flexSeasonAverageDao;
            _flexLast4TotalDao = flexLast4TotalDao;
            _flexLast4AverageDao = flexLast4AverageDao;
            _flexWeeklyTotalDao = flexWeeklyTotalDao;
            _flexWeeklyProjectedDao = flexWeeklyProjectedDao;
        }

        public async Task<List<PlayerStatsExtDto>> searchFlexPosStatsAsync(string position, string interval, string category, string filter, int? week)
        {
            switch(interval)
            {
                case SEASON_TOTAL:
                    return await handleSeasonTotal(position, category, filter);
                case SEASON_AVERAGE:
                    return await handleSeasonAverage(position, category, filter);
                case LAST_4_TOTAL:
                    return await handleLast4Total(position, category, filter);
                case LAST_4_AVERAGE:
                    return await handleLast4Average(position, category, filter);
                case WEEKLY_TOTAL:
                    return await handleWeeklyTotal(position, category, filter, week);
                case WEEKLY_PROJECTED:
                    return await handleWeeklyProjected(position, category, filter, week);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonTotal(string position, string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByPosAsync(position);
                case CONF:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByPosAndConfAsync(position, filter);
                case TEAM:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByPosAndTeamAsync(position, filter);
                case NAME:
                    return await _flexSeasonTotalDao.getFlexSeasonTotalStatsByPosAndNameAsync(position, filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleSeasonAverage(string position, string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByPosAsync(position);
                case CONF:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByPosAndConfAsync(position, filter);
                case TEAM:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByPosAndTeamAsync(position, filter);
                case NAME:
                    return await _flexSeasonAverageDao.getFlexSeasonAverageStatsByPosAndNameAsync(position, filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Total(string position, string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByPosAsync(position);
                case CONF:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByPosAndConfAsync(position, filter);
                case TEAM:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByPosAndTeamAsync(position, filter);
                case NAME:
                    return await _flexLast4TotalDao.getFlexLast4TotalStatsByPosAndNameAsync(position, filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleLast4Average(string position, string category, string filter)
        {
            switch(category)
            {
                case ALL:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByPosAsync(position);
                case CONF:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByPosAndConfAsync(position, filter);
                case TEAM:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByPosAndTeamAsync(position, filter);
                case NAME:
                    return await _flexLast4AverageDao.getFlexLast4AverageStatsByPosAndNameAsync(position, filter);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyTotal(string position, string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByPosAsync(position, week.Value);
                case CONF:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByPosAndConfAsync(position, filter, week.Value);
                case TEAM:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByPosAndTeamAsync(position, filter, week.Value);
                case NAME:
                    return await _flexWeeklyTotalDao.getFlexWeeklyTotalStatsByPosAndNameAsync(position, filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }

        private async Task<List<PlayerStatsExtDto>> handleWeeklyProjected(string position, string category, string filter, int? week)
        {
            switch(category)
            {
                case ALL:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByPosAsync(position, week.Value);
                case CONF:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByPosAndConfAsync(position, filter, week.Value);
                case TEAM:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByPosAndTeamAsync(position, filter, week.Value);
                case NAME:
                    return await _flexWeeklyProjectedDao.getFlexWeeklyProjectedStatsByPosAndNameAsync(position, filter, week.Value);
                default:
                    return new List<PlayerStatsExtDto>();
            }
        }
    }
}