using FluentMigrator.Runner.VersionTableInfo;
using System;

namespace Microservice.First.Migrations.Common
{
    public class VersionTable : IVersionTableMetaData
    {
        public object ApplicationContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool OwnsSchema => true;

        public string SchemaName => "public";

        public string TableName => "_migrations";

        public string ColumnName => "version";

        public string DescriptionColumnName => "description";

        public string UniqueIndexName => "version_pk";

        public string AppliedOnColumnName => "appliedon";
    }
}
