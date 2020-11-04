using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Migrator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MigrationAttribute : FluentMigrator.MigrationAttribute
    {
        public MigrationAttribute(long version, string description) 
            : base(version, description)
        {
        }

        public MigrationAttribute(long version, TransactionBehavior transactionBehavior = TransactionBehavior.Default, string description = null) 
            : base(version, transactionBehavior, description)
        {
        }
    }
}
