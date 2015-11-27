namespace Task3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskInfoTaskBoxID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskInfoes", "TaskBoxID", c => c.String());
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskInfoes", "TaskBoxID");
        }
    }
}
