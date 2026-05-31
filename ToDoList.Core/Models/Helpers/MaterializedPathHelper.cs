using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList.Core.Models.Helpers
{
    /// <summary>
    /// This class enforces invariants related to materialized path formation, path level, and path replacement.
    /// </summary>
    public static class MaterializedPathHelper
    {
        public static string BuildPath(string parentPath, int toDoId)
        {
            ValidatePath(parentPath);
            return $"{parentPath}{toDoId}/";
        }

        /// <summary>
        /// Gets the level of the ToDo that would have the given materialized path.
        /// </summary>
        /// <param name="path">Materialized path</param>
        public static int GetLevel(string path)
        {
            ValidatePath(path);

            // The number of slashes in the parent path equals the level of the
            // children due to leading and trailing slashes.
            return path.Count(c => c == '/') - 1;
        }

        public static bool IsDescendantOf(string pathToCheck, string potentialAncestorPath)
        {
            ValidatePath(pathToCheck);
            ValidatePath(potentialAncestorPath);

            return potentialAncestorPath.StartsWith(pathToCheck);
        }

        private static void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path is required.");
            }

            if (!path.StartsWith("/"))
            {
                throw new FormatException(
                    "Path must start with '/'.");
            }

            if (!path.EndsWith("/"))
            {
                throw new FormatException(
                    "Path must end with '/'.");
            }

            if (path.Contains("//"))
            {
                throw new FormatException(
                    "Path cannot contain double slashes.");
            }
        }
    }
}