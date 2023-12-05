using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public class KWeeklyTotalSqlDao : IKWeeklyTotalDao
    {


        // TODO: Implement KWeeklyTotalSqlDao methods
        // TODO: Add async to methods
        // TODO: Add Mapper
        public Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }
    }
}