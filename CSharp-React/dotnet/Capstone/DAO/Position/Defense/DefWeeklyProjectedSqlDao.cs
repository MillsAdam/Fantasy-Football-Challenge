using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public class DefWeeklyProjectedSqlDao : IDefWeeklyProjectedDao
    {


        // TODO: Implement DefWeeklyProjectedSqlDao methods
        // TODO: Add async to methods
        // TODO: Add mapper
        public Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }

        
    }
}