namespace DevExpress.Data.Utils
{
    using System;

    internal sealed class DefaultConfirmationService : SafeProcess.IConfirmationService
    {
        public static readonly SafeProcess.IConfirmationService Instance;

        static DefaultConfirmationService();
        private DefaultConfirmationService();
        bool? SafeProcess.IConfirmationService.Confirm(SafeProcess.IConfirmationInfo info);

        private static bool IsWPF { get; }
    }
}

