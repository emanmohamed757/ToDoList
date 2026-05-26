using FluentResults;
using Serilog;
using System;
using ToDoList.Core.Models.Data;
using ToDoList.Core.Models.Data.Entities;
using ToDoList.Core.Models.Dtos;
using ToDoList.Core.Models.ExtensionMethods;

namespace ToDoList.Core.Models.Services
{
    internal class ToDoService
    {
        /// <summary>
        /// Logger instance for recording ToDoService operations and diagnostics.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Handles all business logic related to ToDos.
        /// </summary>
        public ToDoService()
        {
            _logger = Log.Logger.ForContext<ToDoService>();
        }

        /// <summary>
        /// Creates a ToDo item.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Result CreateToDo(ToDoCreationDto toDoDto)
        {
            if (toDoDto == null)
            {
                throw new ArgumentNullException(nameof(toDoDto), $"{nameof(toDoDto)} is required.");
            }

            ToDo toDo = toDoDto.ToToDo();

            using (var dbContext = new MainDbContext())
            {
                dbContext.ToDos.Add(toDo);
                dbContext.SaveChanges();
            }

            _logger.ForContext("ToDo", toDo)
                .Information("ToDo added.");

            return Result.Ok();
        }
    }
}