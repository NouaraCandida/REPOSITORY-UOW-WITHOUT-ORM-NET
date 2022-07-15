using System;
using System.Threading.Tasks;
using DIP.Sensor.Domain.Repositorys;
using DIP.Sensor.Infra.Postgres.Repositorys;
using DIP.Sensor.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DIP.Sensor.Infra.Postgres
{
    public class UoWApplication: IUoWApplication
    {
        private readonly DbSession _session;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IStringLocalizer _stringLocalizer;

        private ISensorRepository sensorRepository;

        public UoWApplication(IConfiguration configuration,
           ILogger logger,
           IStringLocalizer stringLocalizer,
           DbSession dbSession)
        {
            _configuration = configuration;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _session = dbSession;

            var cs = configuration.GetConnectionString("Sensor");
            if (string.IsNullOrWhiteSpace(cs))
            {
                _logger.LogError("ConnectionString not configured.");
                throw new ApplicationException(stringLocalizer["ErroConfigurate"]);
            }
        }

        public ISensorRepository SensorRepository
        {
            get => sensorRepository ??= new SensorRepository(_configuration,_logger, _stringLocalizer,_session);
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            _session.Transaction?.Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }
    }
}
