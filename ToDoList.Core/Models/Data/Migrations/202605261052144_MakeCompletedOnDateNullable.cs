namespace ToDoList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCompletedOnDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "CompletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "CompletedOn", c => c.DateTime(nullable: false));
        }
    }
}
