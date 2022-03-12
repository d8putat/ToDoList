using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoList.Interfaces;
using ToDoList.Models;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Specialized;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly string _jsonTasksFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");
        private string StringTasks { get; set; }
        private ObservableCollection<ToDoTask> Tasks { get; set; } = new ObservableCollection<ToDoTask>();
        public ObservableCollection<ToDoTask> GetAllTasks()
        {
            return Tasks;
        }

        public TaskService()
        {
            StringTasks = ReadData();
            if (StringTasks != "")
                Tasks = JsonConvert.DeserializeObject<ObservableCollection<ToDoTask>>(StringTasks);
            Tasks.CollectionChanged += ReWriteData;
        }

        private void ReWriteData(object sender, NotifyCollectionChangedEventArgs e)
        {
            var tasks = JsonConvert.SerializeObject(Tasks);
            using (var stream = new FileStream(_jsonTasksFileName, FileMode.Truncate))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(tasks);
                }
            }
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
        public void SaveTaskInDatabase(ToDoTask newTask)
        {
            Tasks.Add(newTask);
        }

        public void DeleteTaskFromDatabase(ToDoTask removableTask)
        {
            Tasks.RemoveAt(Tasks.IndexOf(removableTask));
        }

        public void EditStateTask(ToDoTask editTask)
        {
            DeleteTaskFromDatabase(editTask);
            SaveTaskInDatabase(editTask);
        }
    }
}
