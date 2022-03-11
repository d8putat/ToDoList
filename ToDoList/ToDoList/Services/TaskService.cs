using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoList.Interfaces;
using ToDoList.Models;
using Newtonsoft.Json;
using System.IO;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly string _jsonTasksFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");
        private List<ToDoTask> _temporaryTasksList = new List<ToDoTask>();
        private string _stringTasks;
        public ObservableCollection<ToDoTask> GetAllTasks()
        {
            _stringTasks = ReadData();
            ObservableCollection<ToDoTask> tasks = JsonConvert.DeserializeObject<ObservableCollection<ToDoTask>>(_stringTasks);
            return tasks;
        }

        private string ReadData()
        {
            using (var stream = new FileStream(_jsonTasksFileName, FileMode.OpenOrCreate))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private void WriteData(string newTasks)
        {
            using (var stream = new FileStream(_jsonTasksFileName, FileMode.Truncate))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(newTasks);
                }
            }
        }
        public void SaveTaskInDatabase(ToDoTask newTask)
        {
            _stringTasks = ReadData();
            if(_stringTasks != "")
            {
                _temporaryTasksList = JsonConvert.DeserializeObject<List<ToDoTask>>(_stringTasks);
            }

            if (newTask != null & _temporaryTasksList!=null)
            {
                _temporaryTasksList.Add(newTask);
            }
            var desCollTasks = JsonConvert.SerializeObject(_temporaryTasksList);
            WriteData(desCollTasks);
        }

        public void DeleteTaskFromDatabase(ToDoTask removableTask)
        {
            _stringTasks = ReadData();
            _temporaryTasksList = JsonConvert.DeserializeObject<List<ToDoTask>>(_stringTasks);
            if (_temporaryTasksList != null)
            {
                var index = _temporaryTasksList.FindIndex((a) => removableTask.Id == a.Id);
                _temporaryTasksList.RemoveAt(index);
                string desCollTasks = JsonConvert.SerializeObject(_temporaryTasksList);
                WriteData(desCollTasks);
            }
        }

        public void EditStateTask(ToDoTask editTask)
        {
            DeleteTaskFromDatabase(editTask);
            SaveTaskInDatabase(editTask);
        }
    }
}
