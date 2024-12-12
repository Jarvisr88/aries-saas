namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public static class SplashScreenServiceExtensions
    {
        public static void ShowSplashScreen(this ISplashScreenService service)
        {
            VerifyService(service);
            service.ShowSplashScreen(null);
        }

        private static void VerifyService(ISplashScreenService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

