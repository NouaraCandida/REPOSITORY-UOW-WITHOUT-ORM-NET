using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DIP.Sensors.Infra.Postgres
{

    public sealed class DBContext : IDisposable
    {
        public NpgsqlConnection Connection { get; }
        public NpgsqlCommand Transaction { get; set; }

        public DBContext(IConfiguration configuration, ILogger logger, IStringLocalizer stringLocalizer)
        {
            var cs = configuration.GetConnectionString("Sensor");
            if (string.IsNullOrWhiteSpace(cs))
            {
                logger.LogError("ConnectionString not configured.");
                throw new ApplicationException(stringLocalizer["ErroConfigurate"]);
            }

            Connection = new NpgsqlConnection(cs);
            Connection.Open();

            Transaction = new NpgsqlCommand();
        }

        public void Dispose() => Connection?.Dispose();
    }
    
}
