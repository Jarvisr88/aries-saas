namespace DevExpress.Utils.CommonDialogs
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    internal static class CommonDialogRuntimeHelper
    {
        internal static readonly MethodInfo runDialog = typeof(CommonDialog).GetMethod("RunDialog", BindingFlags.NonPublic | BindingFlags.Instance);
        private static MethodInfo onHelpRequestMethodInfo = typeof(CommonDialog).GetMethod("OnHelpRequest", BindingFlags.NonPublic | BindingFlags.Instance);
        private static MethodInfo onFileOkMethodInfo = typeof(FileDialog).GetMethod("OnFileOk", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void InvokeFileOk<TDialog>(TDialog dialog, CancelEventArgs args) where TDialog: FileDialog
        {
            if (dialog != null)
            {
                object[] parameters = new object[] { args };
                onFileOkMethodInfo.Invoke(dialog, parameters);
            }
        }

        public static void InvokeHelpRequest<TDialog>(TDialog dialog, EventArgs args) where TDialog: CommonDialog
        {
            if (dialog != null)
            {
                object[] parameters = new object[] { args };
                onHelpRequestMethodInfo.Invoke(dialog, parameters);
            }
        }

        public static bool RunDialog<TDialog>(TDialog dialog, IntPtr hwndOwner) where TDialog: CommonDialog
        {
            if (dialog == null)
            {
                return false;
            }
            object[] parameters = new object[] { hwndOwner };
            return (bool) runDialog.Invoke(dialog, parameters);
        }
    }
}

