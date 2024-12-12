namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface ICommandSourceServiceSupport : ICommandSource, IFrameworkInputElement, IInputElement
    {
        void ExecuteCommand();
        void UpdateCanExecute(bool shortcutRequest);

        DevExpress.Xpf.Bars.Native.CommandSourceHelper CommandSourceHelper { get; }

        System.Windows.Input.KeyGesture KeyGesture { get; }
    }
}

