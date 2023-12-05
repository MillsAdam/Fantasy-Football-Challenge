using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public class KWeeklyProjectedSqlDao : IKWeeklyProjectedDao
    {


        // TODO: Implement KWeeklyProjectedSqlDao methods
        // TODO: Add async to methods
        // TODO: Add Mapper
        public Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }
    }
}