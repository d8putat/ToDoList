using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
