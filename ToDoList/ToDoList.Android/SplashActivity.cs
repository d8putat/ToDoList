using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Java.Lang;

namespace ToDoList.Droid
{
    [Activity(Label = "ToDoList", Icon = "@mipmap/ToDoListAppIcon", MainLauncher = true,Theme = "@style/ToDoListTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Thread.Sleep(1000);
            StartActivity(typeof(MainActivity));
        }

        //protected override async void OnResume()
        //{
        //    base.OnResume();
        //    await SimulateStartup();
        //}

        //private async Task SimulateStartup()
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(5));
        //    StartActivity(new Intent(ApplicationContext,typeof(MainActivity)));
        //}
    }
}