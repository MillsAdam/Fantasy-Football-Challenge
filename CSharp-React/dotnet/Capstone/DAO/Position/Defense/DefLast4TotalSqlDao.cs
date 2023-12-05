using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public class DefLast4TotalSqlDao : IDefLast4TotalDao
    {


        // TODO: Implement methods
        // TODO: Add async to methods
        // TODO: Add mapper
        public Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}