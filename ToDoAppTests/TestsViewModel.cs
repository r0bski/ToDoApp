using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ViewModels;
using Core;
using Models;

namespace ToDoAppTests
{
    [TestClass]
    public class TestsViewModel
    {
        [TestMethod]
        public void Constructor_Should_Load_Empty_Task_List()
        {
            var storage = new MockTodoStorageService();

            var vm = new MainViewModel(storage);

            Assert.AreEqual(0, vm.Tasks.Count);
        }

        [TestMethod]
        public void AddTaskCommand_CanExecute_When_Text_Is_Not_Empty()
        {
            var storage = new MockTodoStorageService();
            var vm = new MainViewModel(storage);
            vm.NewTaskText = "Learn Unit Testing";

            Assert.IsTrue(vm.AddTaskCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddTaskCommand_Cannot_Execute_When_Text_Is_Empty()
        {
            var storage = new MockTodoStorageService();
            var vm = new MainViewModel(storage);
            vm.NewTaskText = "";

            Assert.IsFalse(vm.AddTaskCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddTaskCommand_Should_Add_Task()
        {
            var storage = new MockTodoStorageService();
            var vm = new MainViewModel(storage);
            vm.NewTaskText = "Learn MVVM";

            vm.AddTaskCommand.Execute(null);

            Assert.AreEqual(1, vm.Tasks.Count);
            Assert.AreEqual("Learn MVVM", vm.Tasks[0].Title);
        }
        [TestMethod]
        public void DeleteTaskCommand_CanExecute_When_Task_Is_Selected()
        {
            var storage = new MockTodoStorageService();
            var vm = new MainViewModel(storage);
            var task = new ToDoItem("Task 1", false);

            vm.Tasks.Add(task);
            vm.SelectedTask = task;

            Assert.IsTrue(vm.DeleteTaskCommand.CanExecute(null));
        }
        [TestMethod]
        public void DeleteTask_Should_Clear_SelectedTask()
        {
            var storage = new MockTodoStorageService();
            var vm = new MainViewModel(storage);
            var task = new ToDoItem("Delete Me", false);

            vm.Tasks.Add(task);
            vm.SelectedTask = task;

            vm.DeleteTaskCommand.Execute(null);

            Assert.IsNull(vm.SelectedTask);
        }
    }
}
