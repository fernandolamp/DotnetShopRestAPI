using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMigrator.Migrations
{
    [Migration(20202712)]
    public class InitialMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("Categories");
            Delete.Table("Products");
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString(20)
                .WithColumn("Password").AsString(20)
                .WithColumn("Role").AsString(20);

            Create.Table("Categories")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(64);

            Create.Table("Products")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(60)
                .WithColumn("Description").AsString(1024).Nullable()
                .WithColumn("Price").AsDecimal()
                .WithColumn("CategoryId").AsInt32().ForeignKey("Categories", "ID");
        }
    }
}
