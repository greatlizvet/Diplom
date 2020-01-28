namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpansionForm_SexColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "Sex", c => c.String(nullable: false));
            AddColumn("dbo.Forms", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "Color");
            DropColumn("dbo.Forms", "Sex");
        }
    }
}
