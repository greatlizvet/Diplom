namespace HomeForPets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpansionForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "Desieses", c => c.String(nullable: false));
            AddColumn("dbo.Forms", "Temperament", c => c.String(nullable: false));
            AddColumn("dbo.Forms", "Difficulties", c => c.String(nullable: false));
            AddColumn("dbo.Forms", "Specificity", c => c.String(nullable: false));
            AlterColumn("dbo.Forms", "Age", c => c.String(nullable: false));
            DropColumn("dbo.Forms", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Forms", "Age", c => c.String());
            DropColumn("dbo.Forms", "Specificity");
            DropColumn("dbo.Forms", "Difficulties");
            DropColumn("dbo.Forms", "Temperament");
            DropColumn("dbo.Forms", "Desieses");
        }
    }
}
