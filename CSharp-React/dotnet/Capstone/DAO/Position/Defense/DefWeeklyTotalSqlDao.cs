using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public class DefWeeklyTotalSqlDao : IDefWeeklyTotalDao
    {


        // TODO: Implement methods
        // TODO: Add async to methods
        // TODO: Add mapper
        public Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsAsync(int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByNameAsync(string name, int week)
        {
            throw new NotImplementedException();
        }

        
    }
}