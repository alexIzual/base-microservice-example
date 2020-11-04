using System;
using System.Data;

namespace Migrator
{
    public abstract class Migration : FluentMigrator.Migration
    {
        protected void ExecuteNonQuery(string query)
        {
            Execute.Sql(query);
        }
    }
}
