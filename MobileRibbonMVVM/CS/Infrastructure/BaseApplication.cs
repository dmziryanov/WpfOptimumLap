using System;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;


namespace OptimumLap.Infrastructure
{
    public abstract class BaseApplication : System.Windows.Application
    {
        public BaseApplication()
        {

        }

        protected abstract UnityBootstrapper GetBootstrapper();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SplashScreen splashScreen = new SplashScreen("Images\\Splashscreen.png");
            splashScreen.Show(true);
            var bootstrapper = GetBootstrapper();
            bootstrapper.Run();
            
        }

        protected override void OnExit(ExitEventArgs e)
        {

        }

        private void Log(Exception ex)
        {

        }
    }
}
