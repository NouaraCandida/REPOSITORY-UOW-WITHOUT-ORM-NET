
using System;
using System.Collections.Generic;
using DIP.Core.Repository;
using DIP.Sensors.Domain.Models;
using DIP.Sensors.Domain.Repositorys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Linq;

namespace DIP.Sensors.Infra.Postgres.Repositorys
{
    public class SensorRepository: ISensorRepository
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IStringLocalizer _stringLocalizer;

        public SensorRepository(IConfiguration configuration,
           ILogger logger,
           IStringLocalizer stringLocalizer,
           DBContext dBContext)
        {
            _configuration = configuration;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _context = dBContext;
        }

        public Sensor Get(Guid id)
        {
            _context.Transaction.CommandText = "select id, nome from sensores where id=@id";
            _context.Transaction.Parameters.AddWithValue("id", id);
            var rdr = _context.Transaction.ExecuteReader();

            Sensor sensor = new Sensor();
            while (rdr.Read())
            {
                sensor.Id = rdr.GetGuid(0);
                sensor.Name = rdr.GetString(1);
            }
            return sensor;
        }

        public void Insert(Sensor sensor)
        {
            //with dapper
            _context.Transaction.CommandText = "insert into sensores (nome, nome_regiao, nome_pais) values (@nome, @nome_regiao, @nome_pais)";
            _context.Transaction.Parameters.AddWithValue("nome", sensor.Name);
            _context.Transaction.Parameters.AddWithValue("nome_regiao", ((int)sensor.NameRegion));
            _context.Transaction.Parameters.AddWithValue("nome_pais", (int)sensor.NameCountry);
        }

        public void Remove(Sensor sensor)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sensor> Search(OptionsSearch optionsSearch)
        {
            throw new NotImplementedException();
        }

        public void Update(Sensor sensor)
        {
            throw new NotImplementedException();
        }
    }
}
