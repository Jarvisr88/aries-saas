namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class IAsyncCommandExtensions
    {
        private static void VerifyService(IAsyncCommand service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }

        public static void Wait(this IAsyncCommand service)
        {
            VerifyService(service);
            service.Wait(TimeSpan.FromMilliseconds(-1.0));
        }
    }
}

