namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderForReg_ChangeCityDataType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderForRegistrations", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderForRegistrations", "CityID");
            AddForeignKey("dbo.OrderForRegistrations", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
            DropColumn("dbo.OrderForRegistrations", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderForRegistrations", "City", c => c.String(nullable: false));
            DropForeignKey("dbo.OrderForRegistrations", "CityID", "dbo.Cities");
            DropIndex("dbo.OrderForRegistrations", new[] { "CityID" });
            DropColumn("dbo.OrderForRegistrations", "CityID");
        }
    }
}
