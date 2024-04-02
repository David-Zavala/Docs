namespace Practica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Docs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 128),
                        RegisteredEmail = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                        Age = c.Int(nullable: false),
                        Education = c.String(nullable: false),
                        DocPath = c.String(nullable: false),
                        LastUpdate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Email, cascadeDelete: true)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Docs", "Email", "dbo.Users");
            DropIndex("dbo.Docs", new[] { "Email" });
            DropTable("dbo.Users");
            DropTable("dbo.Docs");
        }
    }
}
