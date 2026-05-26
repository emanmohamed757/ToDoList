namespace ToDoList.Core.Models.Data.Entities
{
    internal interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
