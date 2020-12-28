using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMigrator.Migrations
{
    [Migration(202027122)]
    public class Setup20202712 : Migration
    {
        public override void Down()
        {
            Delete.Table("Setup");
        }

        public override void Up()
        {
            Create.Table("Setup")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("SeedExecuted").AsBoolean()
                .WithColumn("InstallationDate").AsDateTime();

        }
    }
}
