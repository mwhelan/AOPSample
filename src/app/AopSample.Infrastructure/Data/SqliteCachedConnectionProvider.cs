using System.Data;
using NHibernate.Connection;

namespace AopSample.Infrastructure.Data
{
    public class SqliteCachedConnectionProvider : DriverConnectionProvider
    {
        static IDbConnection _databaseConnection;

        public override void CloseConnection(IDbConnection conn)
        {
            //base.CloseConnection(conn);
        }

        public override IDbConnection GetConnection()
        {
            if (_databaseConnection == null)
            {
                _databaseConnection = base.GetConnection();
            }

            return _databaseConnection;
        }
    }
}