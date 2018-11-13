using System;
using System.Windows;
using System.Runtime;

namespace ProfileOptimization
{
    public partial class App : Application
    {
        public static DateTime Timer { get; set; }

        public App()
        {
            Timer = DateTime.Now;

            System.Runtime.ProfileOptimization.SetProfileRoot("C:\\tmp\\");
            System.Runtime.ProfileOptimization.StartProfile("profile");
        }
    }
}
