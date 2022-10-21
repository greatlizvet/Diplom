namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderForRegistration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderForRegistrations",
                c => new
                    {
                        OrderForRegistrationID = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        City = c.String(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderForRegistrationID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .Index(t => t.ImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderForRegistrations", "ImageID", "dbo.Images");
            DropIndex("dbo.OrderForRegistrations", new[] { "ImageID" });
            DropTable("dbo.OrderForRegistrations");
        }
    }
}
