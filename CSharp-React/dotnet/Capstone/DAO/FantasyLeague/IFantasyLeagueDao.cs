using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IFantasyLeagueDao
    {
        Task<List<FantasyLeagueModel>> GetFantasyLeagues();
        FantasyLeagueModel GetFantasyLeagueByLeagueId(int fantasyLeagueId);
        FantasyLeagueModel GetFantasyLeagueByLeagueName(string leagueName);
        Task<FantasyLeagueModel> CreateFantasyLeague(User user, string leagueName, string leaguePassword);
        Task<List<FantasyLeagueModel>> GetListOfFantasyLeaguesByLeagueName(string leagueName);
        Task SetCurrentLeagueAsync(User user, int fantasyLeagueId);
        Task<int> GetCurrentFantasyLeagueIdByUser(User user);
    }
}