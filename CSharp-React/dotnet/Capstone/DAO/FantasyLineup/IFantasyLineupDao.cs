using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IFantasyLineupDao
    {
        Task CreateFantasyLineup(int fantasyRosterId);
        Task<FantasyLineup> GetFantasyLineupByUser(User user);
        Task<double> GetWeeklyScoreByUserAndWeek(User user, int gameWeek);
    }
}