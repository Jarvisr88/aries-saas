namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class SaveFileDialogServiceExtensions
    {
        public static string GetFullFileName(this ISaveFileDialogService service)
        {
            VerifyService(service);
            if (service.File == null)
            {
                return string.Empty;
            }
            string directoryName = service.File.DirectoryName;
            if (!directoryName.EndsWith(@"\"))
            {
                directoryName = directoryName + @"\";
            }
            return (directoryName + service.File.Name);
        }

        public static Stream OpenFile(this ISaveFileDialogService service)
        {
            VerifyService(service);
            Func<IFileInfo, FileStream> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<IFileInfo, FileStream> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.Open(FileMode.Create, FileAccess.Write);
            }
            return service.File.Return<IFileInfo, FileStream>(evaluator, (<>c.<>9__3_1 ??= ((Func<FileStream>) (() => null))));
        }

        public static string SafeFileName(this ISaveFileDialogService service)
        {
            VerifyService(service);
            Func<IFileInfo, string> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<IFileInfo, string> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.Name;
            }
            return service.File.Return<IFileInfo, string>(evaluator, (<>c.<>9__2_1 ??= () => string.Empty));
        }

        public static bool ShowDialog(this ISaveFileDialogService service, Action<CancelEventArgs> fileOK = null, IFileInfo fileInfo = null)
        {
            VerifyService(service);
            Func<IFileInfo, string> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IFileInfo, string> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.DirectoryName;
            }
            return service.ShowDialog(fileOK, fileInfo.With<IFileInfo, string>(evaluator), fileInfo.With<IFileInfo, string>(<>c.<>9__0_1 ??= x => x.Name));
        }

        private static void VerifyService(ISaveFileDialogService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SaveFileDialogServiceExtensions.<>c <>9 = new SaveFileDialogServiceExtensions.<>c();
            public static Func<IFileInfo, string> <>9__0_0;
            public static Func<IFileInfo, string> <>9__0_1;
            public static Func<IFileInfo, string> <>9__2_0;
            public static Func<string> <>9__2_1;
            public static Func<IFileInfo, FileStream> <>9__3_0;
            public static Func<FileStream> <>9__3_1;

            internal FileStream <OpenFile>b__3_0(IFileInfo x) => 
                x.Open(FileMode.Create, FileAccess.Write);

            internal FileStream <OpenFile>b__3_1() => 
                null;

            internal string <SafeFileName>b__2_0(IFileInfo x) => 
                x.Name;

            internal string <SafeFileName>b__2_1() => 
                string.Empty;

            internal string <ShowDialog>b__0_0(IFileInfo x) => 
                x.DirectoryName;

            internal string <ShowDialog>b__0_1(IFileInfo x) => 
                x.Name;
        }
    }
}

