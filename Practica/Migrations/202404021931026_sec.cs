namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Docs");
            AlterColumn("dbo.Docs", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Docs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Docs");
            AlterColumn("dbo.Docs", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Docs", "Id");
        }
    }
}
