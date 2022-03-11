using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ToDoList.Models;
using ToDoList.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ToDoList.Helpers
{
    public class IndexToColorConverter : IValueConverter
    {
        public Color EvenColor { get; set; }
        public Color OddColor { get; set; }
        //protected override Color OnSelectTemplate(object item, BindableObject container)
        //{
        //    var vm = container.BindingContext as TasksPageModel;
        //    if (vm == null) return null;
        //    var stackLayout = item as StackLayout;
        //    return vm.IncompletedTasks.Cast<object>().IndexOf(item) % 2 == 0 ? EvenTemplate : OddTemplate;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bindableObject = (BindableObject)parameter;
            var pageModel = bindableObject.BindingContext as TasksPageModel;
            if (pageModel == null) return null;
            var stackLayout = value as StackLayout;
            return pageModel.IncompletedTasks.Cast<object>().IndexOf(value) % 2 == 0 ? EvenColor : OddColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
