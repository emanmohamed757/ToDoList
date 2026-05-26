using System;
using System.Collections.Generic;

namespace ToDoList.Core.Models.Data.Entities
{
    public class ToDo : IAuditable, IDeletable
    {
        public int Id { get; set; }

        /// <summary>
        /// The full path from the root ToDo to this ToDo, represented as a
        /// separator-delimited string of ancestor IDs.
        /// </summary>
        /// <remarks>
        /// For example, a ToDo at depth 3 might have a path of <c>/1/4/9/</c>, indicating
        /// its ancestors are nodes 1 and 4.
        /// </remarks>
        // Materialized Path pattern was used so that it is efficient to know when a ToDo is about to be moved to it's ancester.
        // Also because I wanted to learn its implementation.
        public string MaterializedPath { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Contains additional details when <see cref="Title"/> is not enough.
        /// </summary>
        public string Description { get; set; }

        public DateTime? CompletedOn { get; set; }

        public StatusOption StatusId { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        //public List<ToDo> SubToDos { get; set; } Maybe this is not necessary because the calling code could query for it from the DB.

        // --- Interface properties.
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}