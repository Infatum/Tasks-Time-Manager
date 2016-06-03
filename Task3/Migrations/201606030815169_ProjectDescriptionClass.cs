namespace TasksTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectDescriptionClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectDescriptions", "FreelanceMode", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectDescriptions", "FreelanceMode");
        }
    }
}
