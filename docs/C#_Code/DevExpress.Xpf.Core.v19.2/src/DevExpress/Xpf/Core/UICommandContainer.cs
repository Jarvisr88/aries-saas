namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;

    public class UICommandContainer : INotifyPropertyChanged
    {
        private readonly WeakReference uiCommandRef;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public UICommandContainer(DevExpress.Mvvm.UICommand uiCommand)
        {
            this.uiCommandRef = new WeakReference(uiCommand);
        }

        internal static DevExpress.Mvvm.UICommand GetUICommand(object commandSourceItem)
        {
            UICommandContainer container = commandSourceItem as UICommandContainer;
            if (container != null)
            {
                return container.UICommand;
            }
            DialogButton button = commandSourceItem as DialogButton;
            return ((button == null) ? (commandSourceItem as DevExpress.Mvvm.UICommand) : button.UICommand);
        }

        public DevExpress.Mvvm.UICommand UICommand =>
            (DevExpress.Mvvm.UICommand) this.uiCommandRef.Target;
    }
}

