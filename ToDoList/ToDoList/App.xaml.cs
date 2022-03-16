using System;
using System.Threading;
using ToDoList.PageModels;
using ToDoList.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using ToDoList.Interfaces;
using ToDoList.Services;
using ToDoList.LocalizationResources;

namespace ToDoList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitServices();
            Thread.Sleep(3000);
            MainPage = InitStartPage();
        }

        private void InitServices()
        {
            FreshIOC.Container.Register<ITaskService, TasksServiceDb>().AsSingleton();
        }

        private Page InitStartPage()
        {
            var page = FreshPageModelResolver.ResolvePageModel<TasksPageModel>();
            return new FreshNavigationContainer(page);
        }
    }
}
