namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unpublic_Form : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "UnPublished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "UnPublished");
        }
    }
}
