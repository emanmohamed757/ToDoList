namespace ToDoList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.ToDoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterializedPath = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        CompletedOn = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Notebook_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Notebooks", t => t.Notebook_Id)
                .Index(t => t.StatusId)
                .Index(t => t.Notebook_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
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
            
            CreateTable(
                "dbo.TagToDoes",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        ToDo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.ToDo_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.ToDoes", t => t.ToDo_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.ToDo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoes", "Notebook_Id", "dbo.Notebooks");
            DropForeignKey("dbo.TagToDoes", "ToDo_Id", "dbo.ToDoes");
            DropForeignKey("dbo.TagToDoes", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ToDoes", "StatusId", "dbo.Status");
            DropIndex("dbo.TagToDoes", new[] { "ToDo_Id" });
            DropIndex("dbo.TagToDoes", new[] { "Tag_Id" });
            DropIndex("dbo.ToDoes", new[] { "Notebook_Id" });
            DropIndex("dbo.ToDoes", new[] { "StatusId" });
            DropTable("dbo.TagToDoes");
            DropTable("dbo.Tags");
            DropTable("dbo.Status");
            DropTable("dbo.ToDoes");
            DropTable("dbo.Notebooks");
        }
    }
}
