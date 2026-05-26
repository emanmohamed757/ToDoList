namespace ToDoList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelToToDo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Level");
        }
    }
}
