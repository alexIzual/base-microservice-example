using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace Microservice.First.Migrations
{
    [Tags("bookdb")]
    [Migration(1, "Тестовая БД с таблицами publishers и books")]
    public class M00001 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Schema.Schema("public");

            Create.Table("publishers")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("logo").AsString()
                .WithColumn("foundationdate").AsDateTime2().NotNullable()
                .WithColumn("firstpublishdate").AsCustom("timestamp(2)");

            var publisherId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7");
            Insert.IntoTable("publishers").Row(new {
                id = publisherId,
                name = "Publisher_1",
                logo = "xuy",
                foundationdate = DateTime.UtcNow,
                firstpublishdate = DateTime.UtcNow,
            });

            if (!Schema.Table("books").Exists())
            {
                Create.Table("books")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("publishdate").AsDateTime().NotNullable()
                .WithColumn("publisherid").AsGuid().ForeignKey("publishers", "id")
                .WithColumn("price").AsCurrency().NotNullable();
            }

            Insert.IntoTable("books").Row(new
            {
                id = Guid.NewGuid(),
                name = "Book_1",
                publishdate = DateTime.UtcNow,
                publisherid = publisherId,
                price = 10000m,
            });

            Insert.IntoTable("books").Row(new
            {
                id = Guid.NewGuid(),
                name = "Book_2",
                publishdate = DateTime.UtcNow,
                publisherid = publisherId,
                price = 3500.67m,
            });
        }
    }
}
