namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormAppUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "AppUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Forms", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Forms", "AppUser_Id");
            AddForeignKey("dbo.Forms", "AppUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Forms", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Forms", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Forms", new[] { "AppUser_Id" });
            DropColumn("dbo.Forms", "AppUser_Id");
            DropColumn("dbo.Forms", "AppUserId");
        }
    }
}
