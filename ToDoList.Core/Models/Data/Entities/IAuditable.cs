using System;

namespace ToDoList.Core.Models.Data.Entities
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime UpdatedOn { get; set; }

        string UpdatedBy { get; set; }
    }
}