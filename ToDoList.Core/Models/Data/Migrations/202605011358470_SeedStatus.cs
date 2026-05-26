namespace ToDoList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStatus : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Status VALUES (1, 'Pending', 0)");
            Sql("INSERT INTO Status VALUES (2, 'Ongoing', 0)");
            Sql("INSERT INTO Status VALUES (3, 'Completed', 0)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Status");
        }
    }
}
