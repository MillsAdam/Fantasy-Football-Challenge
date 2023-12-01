using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO
{
    public class ConfigurationSqlDao : IConfigurationDao
    {
        private readonly string _connectionString;

        public ConfigurationSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }


        public async Task UpdateConfiguration(Configuration configuration)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "UPDATE configuration SET config_value = @config_value WHERE config_key = @config_key;", connection);
                {
                    command.Parameters.AddWithValue("@config_value", configuration.ConfigValue);
                    command.Parameters.AddWithValue("@config_key", configuration.ConfigKey);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Configuration>> GetConfigurations()
        {
            List<Configuration> configurations = new List<Configuration>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT config_key, config_value FROM configuration;", connection);
                {
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    {
                        while (await reader.ReadAsync())
                        {
                            configurations.Add(new Configuration
                            {
                                ConfigKey = Convert.ToString(reader["config_key"]),
                                ConfigValue = Convert.ToInt32(reader["config_value"])
                            });
                        }
                    }              
                }
            }
            return configurations;
        }

        public async Task<int> GetConfigurationValue(string configKey)
        {
            int configValue = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT config_value FROM configuration WHERE config_key = @config_key;", connection);
                {
                    command.Parameters.AddWithValue("@config_key", configKey);
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    {
                        while (await reader.ReadAsync())
                        {
                            configValue = Convert.ToInt32(reader["config_value"]);
                        }
                    }
                }
            }
            return configValue;
        }
    }
}