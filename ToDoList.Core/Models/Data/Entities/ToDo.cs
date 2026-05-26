using System;
using System.Collections.Generic;

namespace ToDoList.Core.Models.Data.Entities
{
    public class ToDo : IAuditable, IDeletable
    {
        public int Id { get; set; }

        public string MaterializedPath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CompletedOn { get; set; }

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