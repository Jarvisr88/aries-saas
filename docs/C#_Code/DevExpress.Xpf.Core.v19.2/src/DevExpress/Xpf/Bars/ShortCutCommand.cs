namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    [Browsable(false)]
    public class ShortCutCommand : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged;

        public ShortCutCommand();
        public ShortCutCommand(ICommandSourceServiceSupport item);
        private KeyGestureWorkingMode GetKeyGestureWorkingMode(object element);
        bool ICommand.CanExecute(object parameter);
        void ICommand.Execute(object parameter);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICommandSourceServiceSupport Item { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string BarItemName { get; set; }
    }
}

