namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class DialogServiceExtensions
    {
        internal static IMessageButtonLocalizer GetLocalizer(IDialogService service)
        {
            IMessageButtonLocalizer localizer1 = service as IMessageButtonLocalizer;
            IMessageButtonLocalizer local5 = localizer1;
            if (localizer1 == null)
            {
                IMessageButtonLocalizer local1 = localizer1;
                IMessageButtonLocalizer local3 = (service as IMessageBoxButtonLocalizer).With<IMessageBoxButtonLocalizer, IMessageButtonLocalizer>(<>c.<>9__7_0 ??= x => x.ToMessageButtonLocalizer());
                local5 = local3;
                if (local3 == null)
                {
                    IMessageButtonLocalizer local4 = local3;
                    local5 = new DefaultMessageButtonLocalizer();
                }
            }
            return local5;
        }

        private static MessageResult GetMessageResult(UICommand result) => 
            (result != null) ? ((MessageResult) result.Tag) : MessageResult.None;

        public static MessageResult ShowDialog(this IDialogService service, MessageButton dialogButtons, string title, object viewModel)
        {
            VerifyService(service);
            MessageResult? defaultButton = null;
            defaultButton = null;
            return GetMessageResult(service.ShowDialog(UICommand.GenerateFromMessageButton(dialogButtons, GetLocalizer(service), defaultButton, defaultButton), title, null, viewModel, null, null));
        }

        public static UICommand ShowDialog(this IDialogService service, IEnumerable<UICommand> dialogCommands, string title, object viewModel)
        {
            VerifyService(service);
            return service.ShowDialog(dialogCommands, title, null, viewModel, null, null);
        }

        public static MessageResult ShowDialog(this IDialogService service, MessageButton dialogButtons, string title, string documentType, object viewModel)
        {
            VerifyService(service);
            MessageResult? defaultButton = null;
            defaultButton = null;
            return GetMessageResult(service.ShowDialog(UICommand.GenerateFromMessageButton(dialogButtons, GetLocalizer(service), defaultButton, defaultButton), title, documentType, viewModel, null, null));
        }

        public static UICommand ShowDialog(this IDialogService service, IEnumerable<UICommand> dialogCommands, string title, string documentType, object viewModel)
        {
            VerifyService(service);
            return service.ShowDialog(dialogCommands, title, documentType, viewModel, null, null);
        }

        public static MessageResult ShowDialog(this IDialogService service, MessageButton dialogButtons, string title, string documentType, object parameter, object parentViewModel)
        {
            VerifyService(service);
            MessageResult? defaultButton = null;
            defaultButton = null;
            return GetMessageResult(service.ShowDialog(UICommand.GenerateFromMessageButton(dialogButtons, GetLocalizer(service), defaultButton, defaultButton), title, documentType, null, parameter, parentViewModel));
        }

        public static UICommand ShowDialog(this IDialogService service, IEnumerable<UICommand> dialogCommands, string title, string documentType, object parameter, object parentViewModel)
        {
            VerifyService(service);
            return service.ShowDialog(dialogCommands, title, documentType, null, parameter, parentViewModel);
        }

        internal static void VerifyService(IDialogService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DialogServiceExtensions.<>c <>9 = new DialogServiceExtensions.<>c();
            public static Func<IMessageBoxButtonLocalizer, IMessageButtonLocalizer> <>9__7_0;

            internal IMessageButtonLocalizer <GetLocalizer>b__7_0(IMessageBoxButtonLocalizer x) => 
                x.ToMessageButtonLocalizer();
        }
    }
}

