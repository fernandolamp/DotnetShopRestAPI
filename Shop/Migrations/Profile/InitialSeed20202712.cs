using FluentMigrator;
using Shop.Models;

namespace Migrator.Profile
{
    [Migration(202027123, "Seed Inicial")]
    public class InitialSeed20202712 : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Insert.IntoTable("User").Row(new { Password = "123", Role = "manager", Username = "adm" });
            Insert.IntoTable("Categories")
                .Row(new { Title = "Petiscos" })
                .Row(new { Title = "Limpeza" });

            Insert.IntoTable("Products").Row(new {Price = 1.5M, Title = "Torcida 200g", CategoryId = 1 });
            Insert.IntoTable("Products").Row(new { Price = 1, Title = "Detergente", CategoryId = 2 });
        }
    }
}
