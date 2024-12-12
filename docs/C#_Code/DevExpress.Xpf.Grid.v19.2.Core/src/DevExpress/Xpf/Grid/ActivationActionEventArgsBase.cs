namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class ActivationActionEventArgsBase : EditorEventArgsBase
    {
        private RoutedEventArgs ActivationEventArgs;

        protected internal ActivationActionEventArgsBase(RoutedEvent routedEvent, DevExpress.Xpf.Editors.ActivationAction activationAction, RoutedEventArgs activationEventArgs, DependencyObject templateChild, DataViewBase view, int rowHandle, ColumnBase column) : base(routedEvent, view, rowHandle, column)
        {
            this.ActivationAction = activationAction;
            this.ActivationEventArgs = activationEventArgs;
            this.TemplateChild = templateChild;
        }

        public DevExpress.Xpf.Editors.ActivationAction ActivationAction { get; private set; }

        public KeyEventArgs KeyDownEventArgs =>
            this.ActivationEventArgs as KeyEventArgs;

        public TextCompositionEventArgs TextInputEventArgs =>
            this.ActivationEventArgs as TextCompositionEventArgs;

        public MouseButtonEventArgs MouseLeftButtonEventArgs =>
            this.ActivationEventArgs as MouseButtonEventArgs;

        public DependencyObject TemplateChild { get; private set; }
    }
}

