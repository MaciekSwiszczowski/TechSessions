using System;

namespace ProfileOptimization
{
    public partial class App
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
