namespace TasksTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropProjectDescriptionText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectDescriptions", "ProjectDescriptionText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectDescriptions", "ProjectDescriptionText");
        }
    }
}
