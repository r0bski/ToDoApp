using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Models;


namespace Core.Services
{
    public class JsonTodoStorageService : ITodoStorageService
    {
        private readonly string _filePath = "tasks.json";

        public void Save(IEnumerable<ToDoItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);

            File.WriteAllText(_filePath, json);
        }

        public List<ToDoItem> Load()
        {
            if (!File.Exists(_filePath))
            {
                return new List<ToDoItem>();
            }

            string json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<ToDoItem>>(json)?? new List<ToDoItem>();
        }
    }
}