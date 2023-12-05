using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public class KLast4AverageSqlDao : IKLast4AverageDao
    {


        // TODO: Implement methods for KLast4AverageSqlDao
        // TODO: Add async to methods
        // TODO: Add Mapper
        public Task<List<PlayerStatsExtDto>> getKLast4AverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}