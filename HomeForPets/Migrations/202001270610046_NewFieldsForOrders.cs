namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldsForOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderForRegistrations", "Comment", c => c.String());
            AddColumn("dbo.OrderForRegistrations", "Denied", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderForRegistrations", "Denied");
            DropColumn("dbo.OrderForRegistrations", "Comment");
        }
    }
}
