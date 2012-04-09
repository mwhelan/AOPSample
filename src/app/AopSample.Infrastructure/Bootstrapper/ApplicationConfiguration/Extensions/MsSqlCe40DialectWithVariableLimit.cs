using NHibernate.Dialect;

namespace AopSample.Infrastructure.Bootstrapper.ApplicationConfiguration.Extensions
{
    public class MsSqlCe40DialectWithVariableLimit : MsSqlCe40Dialect
    {
        public override bool SupportsVariableLimit
        {
            get
            {
                return true;
            }
        }
    }
}