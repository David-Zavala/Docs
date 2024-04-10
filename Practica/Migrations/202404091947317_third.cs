namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Docs", "Count", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Docs", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Docs", "LastUpdate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Users", "LastUpdate");
            DropColumn("dbo.Docs", "Count");
        }
    }
}
