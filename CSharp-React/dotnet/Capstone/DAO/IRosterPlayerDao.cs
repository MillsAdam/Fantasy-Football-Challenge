using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IRosterPlayerDao
    {
        Task<List<RosterPlayer>> GetRosterPlayers();
        Task<List<RosterPlayer>> GetRosterPlayersByUser(User user);
        Task CreateRosterPlayer(User user, int playerId);
        // Task UpdateRosterPlayer(User user, int playerId);
        // Task DeleteRosterPlayer(User user, int playerId);
    }
}