namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AdminRole", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Users", "AdminRole");
        }
    }
}
