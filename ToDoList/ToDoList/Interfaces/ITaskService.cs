using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITaskService
    {
        ObservableCollection<ToDoTask> GetAllTasks();
        void SaveTaskInDatabase(ToDoTask task);
        void DeleteTaskFromDatabase(ToDoTask task);
        void EditStateTask(ToDoTask task);
    }
}
