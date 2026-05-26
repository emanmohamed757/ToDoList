namespace ToDoList.Core.Models.Data.Entities
{
    public class Status : IDeletable
    {
        public StatusOption Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}