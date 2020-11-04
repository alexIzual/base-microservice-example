using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.First.Migrations
{
    [Tags("bookdb")]
    [Migration(2, "Докинули данных в books")]
    public class M00002 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            var publisherId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7");

            Insert.IntoTable("books").Row(new
            {
                id = Guid.NewGuid(),
                name = "Book_3",
                publishdate = DateTime.UtcNow,
                publisherid = publisherId,
                price = 395m,
            });

            Insert.IntoTable("books").Row(new
            {
                id = Guid.NewGuid(),
                name = "Book_4",
                publishdate = DateTime.UtcNow,
                publisherid = publisherId,
                price = 3990.97m,
            });
        }
    }
}
