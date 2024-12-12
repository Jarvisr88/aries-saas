namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Actions")]
    public class ControllerBehavior : Behavior
    {
        public static readonly DependencyProperty ActionsProperty;
        protected static readonly DependencyPropertyKey TriggersPropertyKey;
        public static readonly DependencyProperty TriggersProperty;
        public static readonly DependencyProperty NewActionsExecutionModeProperty;
        public static readonly DependencyProperty ExecutionModeProperty;
        public static readonly DependencyProperty ForcedDataContextProperty;
        private ActionGroup rootGroup;
        private readonly List<DependencyObject> addedLogicalChildren;

        static ControllerBehavior();
        public ControllerBehavior();
        private void AddLogicalChild(DependencyObject child);
        private void ClearLogicalChildren();
        public void Execute(DependencyObject context = null);
        protected virtual void ExecuteOnAssociatedObjectChanged();
        protected virtual void OnActionsChanged(ObservableCollection<IControllerAction> oldValue);
        protected override void OnAttached();
        protected override void OnDetaching();
        private void OnEventTriggersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnForcedDataContextChanged(object oldValue, object newValue);
        private void RemoveLogicalChild(DependencyObject child);

        private ActionGroup RootGroup { get; }

        private IActionContainer RootActionContainer { get; }

        public ActionExecutionMode ExecutionMode { get; set; }

        public ActionExecutionMode NewActionsExecutionMode { get; set; }

        public object ForcedDataContext { get; set; }

        public ObservableCollection<IControllerAction> Actions { get; set; }

        public ObservableCollection<ActionTrigger> Triggers { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ControllerBehavior.<>c <>9;

            static <>c();
            internal void <.cctor>b__11_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__11_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

