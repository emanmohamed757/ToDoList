using System;
using System.Collections.Generic;

namespace ToDoList.Core.Models.Data.Entities
{
    public class Notebook : IAuditable, IDeletable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }

        // --- Interface properties.
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}