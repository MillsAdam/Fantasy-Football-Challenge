using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public class FlexWeeklyProjectedSqlDao : IFlexWeeklyProjectedDao
    {


        // TODO: Implement methods for FlexWeeklyProjectedSqlDao
        // TODO: Add async to methods
        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAsync(string pos, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndConfAsync(string pos, string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndTeamAsync(string pos, string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndNameAsync(string pos, string name, int week)
        {
            throw new NotImplementedException();
        }
        
    }
}