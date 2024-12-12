namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class ProcessStartConfirmationService : SafeProcess.IConfirmationService
    {
        private static readonly SafeProcess.IConfirmationService Instance;

        static ProcessStartConfirmationService();
        private ProcessStartConfirmationService();
        bool? SafeProcess.IConfirmationService.Confirm(SafeProcess.IConfirmationInfo info);
        internal static void Register();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProcessStartConfirmationService.<>c <>9;
            public static Func<SafeProcess.IConfirmationInfo, string> <>9__3_0;
            public static Func<string> <>9__3_1;

            static <>c();
            internal string <DevExpress.Data.Utils.SafeProcess.IConfirmationService.Confirm>b__3_0(SafeProcess.IConfirmationInfo x);
            internal string <DevExpress.Data.Utils.SafeProcess.IConfirmationService.Confirm>b__3_1();
        }
    }
}

