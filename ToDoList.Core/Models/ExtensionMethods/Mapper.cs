using Serilog;
using ToDoList.Core.Models.Data.Entities;
using ToDoList.Core.Models.Dtos;

namespace ToDoList.Core.Models.ExtensionMethods
{
    internal static class Mapper
    {
        /// <summary>
        /// Converts <see cref="ToDoCreationDto"> to <see cref="ToDo">
        /// </summary>
        public static ToDo ToToDo(this ToDoCreationDto dto)
        {
            Log.Logger.Debug("Mapping ToDoCreationDto to ToDo.");

            return new ToDo
            {
                Title = dto.Title,
                Description = dto.Description,
                CompletedOn = dto.CompletedOn,
                StatusId = dto.StatusId,
            };
        }
    }
}