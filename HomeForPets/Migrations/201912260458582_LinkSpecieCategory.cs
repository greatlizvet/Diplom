namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Npgsql;
    
    public partial class LinkSpecieCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Species", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Species", "SpecieName", c => c.String(nullable: false));
            CreateIndex("dbo.Species", "CategoryID");
            AddForeignKey("dbo.Species", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Species", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Species", new[] { "CategoryID" });
            AlterColumn("dbo.Species", "SpecieName", c => c.String());
            DropColumn("dbo.Species", "CategoryID");
        }
    }
}
