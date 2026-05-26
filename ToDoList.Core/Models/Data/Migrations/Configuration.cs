namespace ToDoList.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.Data.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"Models/Data/Migrations";
        }

        protected override void Seed(Models.Data.MainDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
