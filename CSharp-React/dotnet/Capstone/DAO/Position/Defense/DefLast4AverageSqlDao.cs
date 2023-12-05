using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public class DefLast4AverageSqlDao : IDefLast4AverageDao
    {


        // TODO: Implement methods
        // TODO: Add async to methods
        // TODO: Add mapper
        public Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}