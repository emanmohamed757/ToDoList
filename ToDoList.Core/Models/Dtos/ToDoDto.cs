using System;
using System.Collections.Generic;
using ToDoList.Core.Models.Data.Entities;

namespace ToDoList.Core.Models.Dtos
{
    // TODO: Reasons for having a DTO.
    // 1. In order to prevent unnecessary properties from being submitted by a malicious client.
    public abstract class ToDoDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? CompletedOn { get; set; }

        public StatusOption StatusId { get; set; }

        public List<string> Tags { get; set; }
    }
}