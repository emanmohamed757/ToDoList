using System.Data.Entity;
using ToDoList.Core.Models.Data.Entities;

namespace ToDoList.Core.Models.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("name=MainDbContext")
        {
        }

         public virtual DbSet<ToDo> ToDos { get; set; }

         public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Notebook> Notebooks { get; set; }
    }
}