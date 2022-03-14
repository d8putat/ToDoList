using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Helpers;
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
        public ICommand CreateTaskCommand => SingleExecutionCommand.FromFunc(NewTask);

        public ICommand DisplayTasksCommand => new Command(TemplateTasks);
        public ICommand BackWithoutLabelCommand => SingleExecutionCommand.FromFunc(BackToTheMainPageWithoutLabel);



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

        private async Task NewTask()
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

        private async Task BackToTheMainPageWithoutLabel()
        {
            await CoreMethods.PopPageModel();
        }

        #endregion Private methods
    }
}
