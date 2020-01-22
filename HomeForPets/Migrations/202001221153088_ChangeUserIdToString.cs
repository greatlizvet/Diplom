namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forms", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Forms", new[] { "AppUser_Id" });
            DropColumn("dbo.Forms", "AppUserId");
            RenameColumn(table: "dbo.Forms", name: "AppUser_Id", newName: "AppUserId");
            AlterColumn("dbo.Forms", "AppUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Forms", "AppUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Forms", "AppUserId");
            AddForeignKey("dbo.Forms", "AppUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forms", "AppUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Forms", new[] { "AppUserId" });
            AlterColumn("dbo.Forms", "AppUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Forms", "AppUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Forms", name: "AppUserId", newName: "AppUser_Id");
            AddColumn("dbo.Forms", "AppUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Forms", "AppUser_Id");
            AddForeignKey("dbo.Forms", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
