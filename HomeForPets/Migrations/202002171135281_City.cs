namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class City : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            AddColumn("dbo.AspNetUsers", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityID");
            AddForeignKey("dbo.AspNetUsers", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CityID", "dbo.Cities");
            DropIndex("dbo.AspNetUsers", new[] { "CityID" });
            DropColumn("dbo.AspNetUsers", "CityID");
            DropTable("dbo.Cities");
        }
    }
}
