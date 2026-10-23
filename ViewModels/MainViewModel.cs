using Core;
using Core.Services;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> Tasks { get; }

        public RelayCommand AddTaskCommand { get; }
        public RelayCommand DeleteTaskCommand { get; }
        private readonly ITodoStorageService _storageService;

        private string _newTaskText = string.Empty;
        public string NewTaskText
        {
            get => _newTaskText;
            set
            {
                if (_newTaskText == value)
                    return;

                _newTaskText = value;

                AddTaskCommand.FireCanExecuteChanged();

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(NewTaskText)));
            }
        }

        private ToDoItem? _selectedTask;
        public ToDoItem? SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask == value)
                    return;

                _selectedTask = value;

                DeleteTaskCommand.FireCanExecuteChanged();

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(SelectedTask)));
            }
        }

        public MainViewModel(ITodoStorageService storageService)
        {
            _storageService = storageService;

            AddTaskCommand = new RelayCommand(AddTask, NewTaskCanExecute);
            DeleteTaskCommand = new RelayCommand(DeleteTask, DeleteTaskCanExecute);

            Tasks = new ObservableCollection<ToDoItem>(_storageService.Load());
        }

        public MainViewModel(): this(new JsonTodoStorageService())
        {
        }

        private void AddTask()
        {
            Tasks.Add(new ToDoItem(NewTaskText, false));
            _storageService.Save(Tasks);
            NewTaskText = string.Empty;
        }

        private bool NewTaskCanExecute()
        {
            return !string.IsNullOrWhiteSpace(NewTaskText);
        }

        private void DeleteTask()
        {
            if (SelectedTask == null)
                return;

            Tasks.Remove(SelectedTask);
            _storageService.Save(Tasks);
            SelectedTask = null;
        }

        private bool DeleteTaskCanExecute()
        {
            return SelectedTask != null;
        }

        public void SaveTasks()
        {
            _storageService.Save(Tasks);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
