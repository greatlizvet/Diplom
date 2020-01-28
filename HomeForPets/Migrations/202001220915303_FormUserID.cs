namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "UserID");
        }
    }
}
