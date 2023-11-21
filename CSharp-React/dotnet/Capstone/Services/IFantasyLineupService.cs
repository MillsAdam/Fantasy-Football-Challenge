using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.Services
{
    public interface IFantasyLineupService
    {
        Task CreateLineupPlayerAsync(User user, int playerId);
    }
}