using System;
using DIP.Sensors.Domain.Repositorys;
using DIP.Sensors.Infra.Postgres.Repositorys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DIP.Sensors.Infra.Postgres
{
    public class UoWApplication : IUoWApplication
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IStringLocalizer _stringLocalizer;

        private ISensorRepository sensorRepository;

        public UoWApplication(IConfiguration configuration,
           ILogger logger,
           IStringLocalizer stringLocalizer,
           DBContext dBContext)
        {
            _configuration = configuration;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _context = dBContext;

        }

        public ISensorRepository SensorRepository
        {
            get => sensorRepository ??= new SensorRepository(_configuration, _logger, _stringLocalizer, _context);
        }

        public void BeginTransaction()
        {
            _context.Transaction.Connection = _context.Connection;
        }

        public void Commit()
        {
            _context.Transaction.ExecuteNonQuery();
            Dispose();
        }

        public void Dispose()
        {
            _context.Transaction?.Dispose();
        }

        public void Rollback()
        {
            _context.Transaction.Transaction.Rollback();
            Dispose();
        }
    }
}
