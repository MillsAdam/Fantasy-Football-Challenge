using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public class FlexLast4AverageSqlDao : IFlexLast4AverageDao
    {


        // TODO: Implement FlexLast4AverageSqlDao methods
        // TODO: Add async to methods
        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAsync(string pos)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndConfAsync(string pos, string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndTeamAsync(string pos, string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndNameAsync(string pos, string name)
        {
            throw new NotImplementedException();
        }

    }
}