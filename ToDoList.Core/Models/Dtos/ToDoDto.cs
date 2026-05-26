using System;
using System.Collections.Generic;
using ToDoList.Core.Models.Data.Entities;

namespace ToDoList.Core.Models.Dtos
{
    internal abstract class ToDoDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CompletedOn { get; set; }

        public StatusOption StatusId { get; set; }

        public ICollection<TagDto> Tags { get; set; }
    }
}