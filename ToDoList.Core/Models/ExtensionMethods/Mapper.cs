using System;
using System.Collections.Generic;
using System.Linq;
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
            return new ToDo
            {
                Title = dto.Title,
                Description = dto.Description,
                CompletedOn = dto.CompletedOn,
                StatusId = dto.StatusId,
                Tags = dto.Tags?.Select(tagDto => tagDto.ToTag()).ToList(),
            };
        }

        /// <summary>
        /// Converts <see cref="TagDto"> to <see cref="Tag">
        /// </summary>
        private static Tag ToTag(this TagDto tag)
        {
            return new Tag
            {
                Name = tag.Name,
            };
        }
    }
}