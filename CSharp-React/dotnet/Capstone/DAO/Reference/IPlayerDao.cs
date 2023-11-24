using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPlayerDao
    {
        Task AddPlayerAsync(PlayerDto playerDto);
        Task<string> GetPlayerPositionByPlayerIdAsync(int playerId);
        Task<List<SearchPlayerDto>> GetPlayerIdByNameAsync(string playerName);
    }
}