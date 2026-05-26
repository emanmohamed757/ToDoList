namespace ToDoList.Core.Models.Dtos
{
    internal class ToDoCreationDto : ToDoDto
    {
        public string MaterializedPathOfParent { get; set; }
    }
}