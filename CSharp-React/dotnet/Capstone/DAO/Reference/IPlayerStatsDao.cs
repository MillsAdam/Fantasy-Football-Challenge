using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.Models.Data;

namespace Capstone.DAO.Reference
{
    public interface IPlayerStatsDao
    {
        Task AddPlayerStatsDtoAsync(PlayerStatsDto playerStatsDto);
        Task AddDefenseStatsDtoAsync(PlayerStatsDto playerStatsDto);
        Task AddPlayerProjectionsDtoAsync(PlayerStatsDto playerStatsDto);
        Task AddDefenseProjectionsDtoAsync(PlayerStatsDto playerStatsDto);

        Task UpsertPlayerStatsDtoAsync(PlayerStatsDto playerStatsDto);
        Task UpsertDefenseStatsDtoAsync(PlayerStatsDto playerStatsDto);
        Task UpsertPlayerProjectionsDtoAsync(PlayerStatsDto playerStatsDto);
        Task UpsertDefenseProjectionsDtoAsync(PlayerStatsDto playerStatsDto);

        Task AddPlayerStatsExtAsync(PlayerStatsExt playerStatsExt);
        Task AddDefenseStatsExtAsync(PlayerStatsExt playerStatsExt);
        Task AddPlayerProjectionsExtAsync(PlayerStatsExt playerStatsExt);
        Task AddDefenseProjectionsExtAsync(PlayerStatsExt playerStatsExt);

        Task UpsertPlayerStatsExtAsync(PlayerStatsExt playerStatsExt);
        Task UpsertDefenseStatsExtAsync(PlayerStatsExt playerStatsExt);
        Task UpsertPlayerProjectionsExtAsync(PlayerStatsExt playerStatsExt);
        Task UpsertDefenseProjectionsExtAsync(PlayerStatsExt playerStatsExt);

    }
}