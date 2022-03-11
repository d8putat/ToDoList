using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Pages;
using ToDoList.Services;
using Xamarin.Forms;

namespace ToDoList.PageModels
{
    public class TasksPageModel : FreshBasePageModel
    {
        #region Fields & Properties

        private readonly ITaskService _taskService;
        public bool IsPendingVisible { get; set; }
        public bool IsCompletedVisible { get; set; }
        public bool IsFirstNotificationVisible { get; set; } = true;

        public ObservableCollection<ToDoTask> Tasks { get; set; }

        public ObservableCollection<ToDoTask> CompletedTasks { get; set; } = new ObservableCollection<ToDoTask>();

        public ObservableCollection<ToDoTask> IncompletedTasks { get; set; } = new ObservableCollection<ToDoTask>();

        public DateTime Date { get; set; } = DateTime.Now;

        public ICommand PickTaskCommand => new Command<ToDoTask>(OnPickTask);

        public ICommand DeleteTaskCommand => new Command<ToDoTask>(OnDeleteTask);

        public ICommand AddTaskCommand => new Command(GoToAddTask);

        #endregion Fields & Properties

        #region Constructors and Inits

        public TasksPageModel(ITaskService taskService)
        {
            _taskService = taskService;
            CompletedTasks.CollectionChanged += CompletedChanged;
            IncompletedTasks.CollectionChanged += PendingChanged;
        }

        public override void Init(object initData)
        {
            InitTasks();
        }

        public override void ReverseInit(object returnedData)
        {
            if (returnedData is string taskText)
            {
                AddTask(taskText);
            }
        }

        #endregion Constructors and Inits

        #region Public & Protected methods



        #endregion Public & Protected methods

        #region Private methods

        private void InitTasks()
        {
            if ((Tasks = _taskService.GetAllTasks()) != null)
            {
                foreach (var task in Tasks)
                {
                    if (task.IsCompleted == true)
                        CompletedTasks.Add(task);
                    else
                        IncompletedTasks.Add(task);
                }
            }
        }
        private void OnPickTask(ToDoTask task)
        {

            if (task.IsCompleted)
            {
                IncompletedTasks.Remove(task);
                CompletedTasks.Add(task);
            }
            else
            {
                CompletedTasks.Remove(task);
                IncompletedTasks.Add(task);
            }
            RaisePropertyChanged(nameof(IncompletedTasks));
            _taskService.EditStateTask(task);
        }

        private async void GoToAddTask()
        {
            await CoreMethods.PushPageModel<NewTaskPageModel>();
        }

        private void AddTask(string textTask)
        {
            ToDoTask newTask = new ToDoTask() { Id = System.Guid.NewGuid().ToString(), Description = textTask };
            IncompletedTasks.Add(newTask);
            _taskService.SaveTaskInDatabase(newTask);
        }
        private void OnDeleteTask(ToDoTask task)
        {
            if (IncompletedTasks.Contains(task))
            {
                IncompletedTasks.Remove(task);
            }
            else
            {
                CompletedTasks.Remove(task);
            }
            _taskService.DeleteTaskFromDatabase(task);
        }

        private void PendingChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IncompletedTasks.Count > 0)
            {
                IsPendingVisible = true;
                IsFirstNotificationVisible = false;
            }
            else
            {
                IsPendingVisible = false;
                if (CompletedTasks.Count == 0) { IsFirstNotificationVisible = true; } else { IsFirstNotificationVisible = false; }
            }
        }

        private void CompletedChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CompletedTasks.Count > 0)
            {
                IsCompletedVisible = true;
                IsFirstNotificationVisible = false;
            }
            else
            {
                IsCompletedVisible = false;
                if (IncompletedTasks.Count == 0) { IsFirstNotificationVisible = true; } else { IsFirstNotificationVisible = false; }
            }
        }

        #endregion Private methods
    }
}
