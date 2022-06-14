﻿using System;
using SQLite;

namespace DoToo.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime Due { get; set; }

        public TodoItem()
        {
            
        }

        public TodoItem Copy()
        {
            return (TodoItem)this.MemberwiseClone();

        }
    }
}
