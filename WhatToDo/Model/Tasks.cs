using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToDo.Model
{
    [Table("tasks")]
    public class Tasks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; } 

    }
}
