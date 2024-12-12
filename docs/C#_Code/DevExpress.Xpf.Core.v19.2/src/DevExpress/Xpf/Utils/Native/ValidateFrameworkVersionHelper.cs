namespace DevExpress.Xpf.Utils.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.InteropServices;

    public static class ValidateFrameworkVersionHelper
    {
        public static bool Perform(string errorMessage)
        {
            Version version = new Version(3, 5, 0x7809, 1);
            Version frameworkVersion = Helpers.GetFrameworkVersion();
            if ((frameworkVersion != null) && (frameworkVersion >= version))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                WinFormsMessageBoxHelper.Show(string.Format(errorMessage, version));
            }
            return false;
        }

        private static class WinFormsMessageBoxHelper
        {
            [DllImport("user32.dll", CharSet=CharSet.Auto)]
            internal static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
            public static void Show(string message)
            {
                uint uType = 0x10010;
                MessageBox(IntPtr.Zero, message, "", uType);
            }
        }
    }
}

