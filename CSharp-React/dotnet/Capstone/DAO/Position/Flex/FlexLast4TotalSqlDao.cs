using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public class FlexLast4TotalSqlDao : IFlexLast4TotalDao
    {

        
        // TODO: Implement FlexLast4TotalSqlDao methods
        // TODO: Add async to methods
        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAsync(string pos)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndConfAsync(string pos, string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndTeamAsync(string pos, string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndNameAsync(string pos, string name)
        {
            throw new NotImplementedException();
        }

    }
}