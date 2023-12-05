using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public class DefSeasonAverageSqlDao : IDefSeasonAverageDao
    {


        // TODO: Implement methods
        // TODO: Add async to methods
        // TODO: Add mapper
        public Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}