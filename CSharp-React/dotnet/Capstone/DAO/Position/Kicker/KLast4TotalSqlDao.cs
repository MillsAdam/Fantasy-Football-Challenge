using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public class KLast4TotalSqlDao : IKLast4TotalDao
    {


        // TODO: Implement methods for KLast4TotalSqlDao
        // TODO: Add async to methods
        // TODO: Add Mapper
        public Task<List<PlayerStatsExtDto>> getKLast4TotalStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}