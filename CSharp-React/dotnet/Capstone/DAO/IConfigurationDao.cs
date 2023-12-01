using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IConfigurationDao
    {
        Task UpdateConfiguration(Configuration configuration);
        Task<List<Configuration>> GetConfigurations();
        Task<int> GetConfigurationValue(string configKey);
    }
}