using FluentResults;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            // Decided to not do dependency injection for this, as this project will not have tests,
            // and I will not use any other logging library.
            _logger = Log.Logger.ForContext<ToDoService>();
        }

        public Result<List<ToDo>> GetToDos(string parentPath)
        {
            if (string.IsNullOrEmpty(parentPath))
            {
                throw new ArgumentNullException(nameof(parentPath), $"{nameof(parentPath)} is required.");
            }

            using (var dbContext = new MainDbContext())
            {
                List<ToDo> toDos = dbContext.ToDos
                    .Where(toDo => toDo.MaterializedPath.StartsWith(parentPath))
                    .ToList();

                _logger.Information("{ToDoCount} ToDos retrieved.", toDos.Count);

                return toDos;
            }
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

            if (string.IsNullOrEmpty(toDoDto.Title))
            {
                _logger.ForContext("ToDoDto", toDoDto)
                    .Warning("Title is missing.");
                return Result.Fail("Title is required.");
            }

            ToDo toDo = toDoDto.ToToDo();

            using (var dbContext = new MainDbContext())
            using (var transaction = dbContext.Database.BeginTransaction()) // Transaction needed for double save (explained below)
            {
                AddTags(toDoDto, toDo, dbContext);

                // Save the whole thing (ToDo and its Tags).
                try
                {
                    dbContext.ToDos.Add(toDo);
                    dbContext.SaveChanges();

                    // The ID is set after the first SaveChanges call and is used to build the MaterializedPath.
                    toDo.MaterializedPath = $"{toDoDto.MaterializedPathOfParent}/{toDo.Id}/";
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (DataException ex)
                {
                    transaction.Rollback();
                    _logger.ForContext("ToDo", toDo)
                        .Error(ex, "Error occurred while adding ToDo to database.");
                    throw;
                }
            }

            _logger.ForContext("ToDo", toDo)
                .Information("ToDo added.");
            return Result.Ok();
        }

        /// <summary>
        /// Updates/edits the fields of a ToDo.
        /// </summary>
        public Result UpdateToDo(ToDoUpdateDto toDoUpdate)
        {
            ToDo toDo;
            using (var dbContext = new MainDbContext())
            {
                if (!TryFindToDo(toDoUpdate.Id, dbContext, out toDo))
                {
                    return Result.Fail("No ToDo found with that ID.");
                }

                toDo.Update(toDoUpdate);
                AddTags(toDoUpdate, toDo, dbContext);

                dbContext.SaveChanges();
            }

            _logger.ForContext("ToDo", toDo)
                .Information(messageTemplate: "ToDo updated.");
            return Result.Ok();
        }

        /// <summary>
        /// Soft-deletes a ToDo from the database.
        /// </summary>
        public Result DeleteToDo(int id)
        {
            ToDo toDo;
            using (var dbContext = new MainDbContext())
            {
                if (!TryFindToDo(id, dbContext, out toDo))
                {
                    return Result.Fail("No ToDo found with that ID.");
                }

                toDo.IsDeleted = true;
                List<ToDo> descendants = GetAllDescendants(toDo);
                descendants.ForEach(descendant => descendant.IsDeleted = true);

                _logger.ForContext("ToDo", toDo)
                    .Debug("{DescendantCount} descendants deleted.", descendants.Count);

                dbContext.SaveChanges();
            }

            _logger.ForContext("ToDo", toDo)
                .Information("ToDo deleted.");
            return Result.Ok();
        }

        public Result MoveToDo(int toDoBeingMovedId, string newParentMaterializedPath)
        {
            ToDo toDo;
            using (var dbContext = new MainDbContext())
            {
                if (!TryFindToDo(toDoBeingMovedId, dbContext, out toDo))
                {
                    return Result.Fail("No ToDo found with that ID.");
                }

                string oldPath = toDo.MaterializedPath;
                toDo.MaterializedPath = $"{newParentMaterializedPath}/{toDo.Id}/";

                // The materialized parentPath of all descendants will change.
                List<ToDo> descendants = GetAllDescendants(toDo);
                descendants.ForEach(descendant =>
                {
                    descendant.MaterializedPath.Replace(oldPath, toDo.MaterializedPath);
                });

                _logger.ForContext("ToDo", toDo)
                    .Debug("MaterializedPath of {DescendantCount} descendants changed.", descendants.Count);
            }

            _logger.ForContext("ToDo", toDo)
                .Information("ToDo moved to {MaterializedPath}.", toDo.MaterializedPath);
            return Result.Ok();
        }

        /// <summary>
        /// Returns all ToDos contained in a given ToDo.
        /// </summary>
        private List<ToDo> GetAllDescendants(ToDo toDo)
        {
            using (var dbContext = new MainDbContext())
            {
                return dbContext.ToDos
                    .Where(t => t.MaterializedPath.StartsWith(toDo.MaterializedPath))
                    .ToList();
            }
        }

        private bool TryFindToDo(int id, MainDbContext dbContext, out ToDo toDo)
        {
            toDo = dbContext.ToDos.Find(id);
            if (toDo == null)
            {
                _logger.Error("No ToDo found with that ID.");
                return false;
            }

            return true;
        }

        private static void AddTags(ToDoDto toDoDto, ToDo toDo, MainDbContext dbContext)
        {
            // Add existing and new tags.
            // Finds existing tags from the database and adds them.
            // New tags are new'd up so they will be created.
            toDo.Tags = new List<Tag>();

            Dictionary<string, Tag> existingTags = dbContext.Tags
                .Where(tag => toDoDto.Tags.Contains(tag.Name))
                .ToDictionary(tag => tag.Name);

            foreach (string tagName in toDoDto.Tags)
            {
                toDo.Tags.Add(
                    existingTags.TryGetValue(tagName, out Tag existing)
                        ? existing
                        : new Tag { Name = tagName }
                );
            }
        }
    }
}