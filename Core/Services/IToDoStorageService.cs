using Models;

namespace Core.Services
{
    public interface ITodoStorageService
    {
        void Save(IEnumerable<ToDoItem> tasks);
        List<ToDoItem> Load();
    }
}