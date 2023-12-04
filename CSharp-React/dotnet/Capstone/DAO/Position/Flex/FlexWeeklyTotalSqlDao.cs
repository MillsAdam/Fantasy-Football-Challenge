using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public class FlexWeeklyTotalSqlDao : IFlexWeeklyTotalDao
    {


        // TODO: Implement methods for FlexWeeklyTotalSqlDao
        // TODO: Add async to methods
        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAsync(string pos, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndConfAsync(string pos, string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndTeamAsync(string pos, string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndNameAsync(string pos, string name, int week)
        {
            throw new NotImplementedException();
        }

    }
}