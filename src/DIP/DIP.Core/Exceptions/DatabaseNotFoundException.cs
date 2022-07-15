using System;
namespace DIP.Core.Exceptions
{
    public class DatabaseNotFoundException: Exception
    {
        public DatabaseNotFoundException()
        {

        }
        public DatabaseNotFoundException(string message)
            : base(message)
        {
        }

        public DatabaseNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
