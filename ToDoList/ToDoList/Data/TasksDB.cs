using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class TasksDB
    {
        private readonly SQLiteAsyncConnection _db;

        public TasksDB(string connection)
        {
            _db = new SQLiteAsyncConnection(connection);
            _db.CreateTableAsync<ToDoTask>().Wait();
        }

        public Task<List<ToDoTask>> GetTasksAsync()
        {
            return _db.Table<ToDoTask>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(ToDoTask task)
        {
            return _db.InsertAsync(task);
        }

        public Task<int> DeleteTaskAsync(ToDoTask task)
        {
            return _db.DeleteAsync(task);
        }

        public Task<int> EditTaskAsync(ToDoTask task)
        {
            return _db.UpdateAsync(task);
        }
    }
}
