namespace TasksTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectDescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectDescriptions",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            AddColumn("dbo.TaskInfoes", "Task_Id", c => c.Int(nullable: false));
            AddColumn("dbo.TaskInfoes", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.TaskInfoes", "Project_ProjectId");
            AddForeignKey("dbo.TaskInfoes", "Project_ProjectId", "dbo.ProjectDescriptions", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskInfoes", "Project_ProjectId", "dbo.ProjectDescriptions");
            DropIndex("dbo.TaskInfoes", new[] { "Project_ProjectId" });
            DropColumn("dbo.TaskInfoes", "Project_ProjectId");
            DropColumn("dbo.TaskInfoes", "Task_Id");
            DropTable("dbo.ProjectDescriptions");
        }
    }
}
