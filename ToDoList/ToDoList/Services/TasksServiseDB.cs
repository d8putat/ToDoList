using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TasksServiceDb : ITaskService
    {
        private static TasksDB _tasksDataBase;

        private static TasksDB TasksDataBase
        {
            get
            {
                if (_tasksDataBase == null)
                {
                    _tasksDataBase = new TasksDB(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "tasksDataBase.db3"));
                }

                return _tasksDataBase;
            }
        }
        public void DeleteTaskFromDatabase(ToDoTask task)
        {
            TasksDataBase.DeleteTaskAsync(task);
        }

        public void EditStateTask(ToDoTask task)
        {
            TasksDataBase.EditTaskAsync(task);
        }

        public ObservableCollection<ToDoTask> GetAllTasks()
        {
            var tasksList = TasksDataBase.GetTasksAsync().Result;
            var tasksObservableCollection = new ObservableCollection<ToDoTask>(tasksList);
            return tasksObservableCollection;
        }

        public void SaveTaskInDatabase(ToDoTask task)
        {
            TasksDataBase.SaveTaskAsync(task);
        }
    }
}
