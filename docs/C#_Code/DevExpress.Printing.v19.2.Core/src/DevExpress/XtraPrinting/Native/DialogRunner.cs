namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.CommonDialogs;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Windows.Forms;

    public class DialogRunner
    {
        private static DialogRunner instance;

        static DialogRunner();
        private static DialogResult Convert(DialogResult result);
        protected virtual IWin32Window GetDefaultOwnerWindow();
        public static IWin32Window GetOwnerWindow();
        protected virtual DialogResult RunDialog(ICommonDialog dialog, IWin32Window ownerWindow);
        protected virtual DialogResult RunDialog(CommonDialog dialog, IWin32Window ownerWindow);
        protected virtual DialogResult RunDialog(Form form, IServiceProvider provider);
        protected virtual DialogResult RunDialog(Form form, IWin32Window ownerWindow);
        public static DialogResult ShowDialog(ICommonDialog dlg);
        public static DialogResult ShowDialog(CommonDialog dlg);
        public static DialogResult ShowDialog(Form form);
        public static DialogResult ShowDialog(ICommonDialog dlg, IServiceProvider provider);
        public static DialogResult ShowDialog(ICommonDialog dlg, IWin32Window owner);
        public static DialogResult ShowDialog(CommonDialog dlg, IServiceProvider provider);
        public static DialogResult ShowDialog(CommonDialog dlg, IWin32Window owner);
        public static DialogResult ShowDialog(Form form, IServiceProvider provider);
        public static DialogResult ShowDialog(Form form, IWin32Window owner);

        public static DialogRunner Instance { get; set; }

        private class DummyWin32Window : IWin32Window
        {
            private IntPtr handle;

            public DummyWin32Window(IntPtr handle);

            IntPtr IWin32Window.Handle { get; }
        }
    }
}

