using System;
using System.Data;
using System.Data.SqlClient;

namespace DIP.Sensor.Infra.Repository
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection("");
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
