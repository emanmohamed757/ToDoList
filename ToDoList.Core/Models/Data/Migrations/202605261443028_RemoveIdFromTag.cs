namespace ToDoList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdFromTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagToDoes", "Tag_Id", "dbo.Tags");
            RenameTable(name: "dbo.TagToDoes", newName: "ToDoTags");
            DropForeignKey("dbo.ToDoes", "Notebook_Id", "dbo.Notebooks");
            DropIndex("dbo.ToDoes", new[] { "Notebook_Id" });
            DropIndex("dbo.ToDoTags", new[] { "Tag_Id" });
            RenameColumn(table: "dbo.ToDoTags", name: "Tag_Id", newName: "Tag_Name");
            DropPrimaryKey("dbo.Tags");
            DropPrimaryKey("dbo.ToDoTags");
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ToDoTags", "Tag_Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Tags", "Name");
            AddPrimaryKey("dbo.ToDoTags", new[] { "ToDo_Id", "Tag_Name" });
            CreateIndex("dbo.ToDoTags", "Tag_Name");
            AddForeignKey("dbo.ToDoTags", "Tag_Name", "dbo.Tags", "Name", cascadeDelete: true);
            DropColumn("dbo.ToDoes", "Notebook_Id");
            DropColumn("dbo.Tags", "Id");
            DropTable("dbo.Notebooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notebooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tags", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ToDoes", "Notebook_Id", c => c.Int());
            DropForeignKey("dbo.ToDoTags", "Tag_Name", "dbo.Tags");
            DropIndex("dbo.ToDoTags", new[] { "Tag_Name" });
            DropPrimaryKey("dbo.ToDoTags");
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.ToDoTags", "Tag_Name", c => c.Int(nullable: false));
            AlterColumn("dbo.Tags", "Name", c => c.String());
            AddPrimaryKey("dbo.ToDoTags", new[] { "Tag_Id", "ToDo_Id" });
            AddPrimaryKey("dbo.Tags", "Id");
            RenameColumn(table: "dbo.ToDoTags", name: "Tag_Name", newName: "Tag_Id");
            CreateIndex("dbo.ToDoTags", "Tag_Id");
            CreateIndex("dbo.ToDoes", "Notebook_Id");
            AddForeignKey("dbo.ToDoes", "Notebook_Id", "dbo.Notebooks", "Id");
            RenameTable(name: "dbo.ToDoTags", newName: "TagToDoes");
            AddForeignKey("dbo.TagToDoes", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
