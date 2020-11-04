using FluentMigrator;

[Tags("bookdb")]
[Migration(1)]
public class M00003 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Alter.Table("books").AddColumn("description").AsString().Nullable();
    }
}
