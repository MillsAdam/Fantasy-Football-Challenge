using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public class KSeasonAverageSqlDao : IKSeasonAverageDao
    {

        
        // TODO: Implement methods for KSeasonAverageSqlDao
        // TODO: Add async to methods
        // TODO: Add Mapper
        public Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}