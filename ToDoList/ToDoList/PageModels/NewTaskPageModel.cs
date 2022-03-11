using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ToDoList.LocalizationResources;
using ToDoList.Models;
using ToDoList.Pages;
using Xamarin.Forms;

namespace ToDoList.PageModels
{
    public class NewTaskPageModel : FreshBasePageModel
    {
        #region Fields & Properties

        public string Text { get; set; }
        public ICommand CreateTaskCommand => new Command(NewTask);
        public ICommand BackCommand => new Command(BackToTheMainPage);

        public ICommand DisplayTasksCommand => new Command(TemplateTasks);
        public ICommand BackWithoutLabelCommand => new Command(BackToTheMainPageWithoutLabel);



        #endregion Fields & Properties

        #region Constructors and Inits

        public NewTaskPageModel() { }

        #endregion Constructors and Inits

        #region Public & Protected methods



        #endregion Public & Protected methods

        #region Private methods

        private async void TemplateTasks()
        {
            var action = await CoreMethods.DisplayActionSheet(Resources.AddExercise, Resources.Cancel, null, Resources.Pullups, Resources.Pushups, Resources.Squats, Resources.Run);
            if (action == Resources.Pullups || action == Resources.Pushups || action == Resources.Squats || action == Resources.Run)
            {
                await CoreMethods.PopPageModel(action);
            }
        }

        private async void NewTask()
        {
            if (Text != null)
            {
                await CoreMethods.PopPageModel(Text);
            }
            else
            {
                await CoreMethods.DisplayAlert(Resources.Notification, Resources.DescriptionMessage, Resources.Ok);
            }
        }

        private async void BackToTheMainPage()
        {
            await CoreMethods.PopPageModel(Text);
        }

        private async void BackToTheMainPageWithoutLabel()
        {
            await CoreMethods.PopPageModel();
        }

        #endregion Private methods
    }
}
