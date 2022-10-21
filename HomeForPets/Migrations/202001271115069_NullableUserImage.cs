namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableUserImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ImageID", "dbo.Images");
            DropIndex("dbo.AspNetUsers", new[] { "ImageID" });
            AlterColumn("dbo.AspNetUsers", "ImageID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ImageID");
            AddForeignKey("dbo.AspNetUsers", "ImageID", "dbo.Images", "ImageID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ImageID", "dbo.Images");
            DropIndex("dbo.AspNetUsers", new[] { "ImageID" });
            AlterColumn("dbo.AspNetUsers", "ImageID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "ImageID");
            AddForeignKey("dbo.AspNetUsers", "ImageID", "dbo.Images", "ImageID", cascadeDelete: true);
        }
    }
}
