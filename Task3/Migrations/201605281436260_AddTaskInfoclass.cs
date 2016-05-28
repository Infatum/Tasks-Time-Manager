namespace Task3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskInfoclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskInfoes", "HourRate", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskInfoes", "HourRate");
        }
    }
}
