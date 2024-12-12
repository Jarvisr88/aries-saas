namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class OpenFileDialogServiceExtensions
    {
        public static string GetFullFileName(this IOpenFileDialogService service)
        {
            VerifyService(service);
            Func<IFileInfo, string> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<IFileInfo, string> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.GetFullName();
            }
            return service.File.Return<IFileInfo, string>(evaluator, (<>c.<>9__3_1 ??= () => string.Empty));
        }

        public static bool ShowDialog(this IOpenFileDialogService service)
        {
            VerifyService(service);
            return service.ShowDialog(null, null);
        }

        public static bool ShowDialog(this IOpenFolderDialogService service)
        {
            VerifyService(service);
            return service.ShowDialog(null, null);
        }

        public static bool ShowDialog(this IOpenFileDialogService service, Action<CancelEventArgs> fileOK)
        {
            VerifyService(service);
            return service.ShowDialog(fileOK, null);
        }

        public static bool ShowDialog(this IOpenFileDialogService service, string directoryName)
        {
            VerifyService(service);
            return service.ShowDialog(null, directoryName);
        }

        public static bool ShowDialog(this IOpenFolderDialogService service, Action<CancelEventArgs> fileOK)
        {
            VerifyService(service);
            return service.ShowDialog(fileOK, null);
        }

        public static bool ShowDialog(this IOpenFolderDialogService service, string directoryName)
        {
            VerifyService(service);
            return service.ShowDialog(null, directoryName);
        }

        private static void VerifyService(object service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OpenFileDialogServiceExtensions.<>c <>9 = new OpenFileDialogServiceExtensions.<>c();
            public static Func<IFileInfo, string> <>9__3_0;
            public static Func<string> <>9__3_1;

            internal string <GetFullFileName>b__3_0(IFileInfo x) => 
                x.GetFullName();

            internal string <GetFullFileName>b__3_1() => 
                string.Empty;
        }
    }
}

