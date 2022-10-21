namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTitleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TitleName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Title", c => c.String());
            DropColumn("dbo.AspNetUsers", "TitleName");
        }
    }
}
