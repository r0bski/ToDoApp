using System;
using System.Collections.Generic;
using System.Text;
using Core.Services;
using Models;

namespace ToDoAppTests
{
    public class MockTodoStorageService : ITodoStorageService
    {
        public List<ToDoItem> Tasks { get; set; } = new();

        public List<ToDoItem> Load()
        {
            return Tasks;
        }

        public void Save(IEnumerable<ToDoItem> tasks)
        {
            Tasks = tasks.ToList();
        }
    }
}