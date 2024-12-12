namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Actions")]
    public class ActionGroup : DXFrameworkContentElement, IActionContainer, IControllerAction
    {
        public static readonly DependencyProperty ActionsTemplateProperty;
        private DependencyObject associatedObject;
        private IActionContainer container;
        private ObservableCollection<IControllerAction> actions;
        private ObservableCollection<ActionTrigger> triggers;
        private List<object> logicalChildren;

        static ActionGroup();
        public ActionGroup();
        protected virtual void AttachTriggers(IList newItems);
        private bool CheckLogicalParent(object element, Func<object, bool> checkParentFunc);
        protected virtual void DetachTriggers(IList oldItems);
        void IControllerAction.Execute(DependencyObject context);
        protected internal void Execute(DependencyObject context, ActionExecutionMode reason);
        protected virtual void ExecuteCore(DependencyObject context);
        private DependencyObject GetLogicalParent(object element);
        protected virtual void OnActionsChanged(ObservableCollection<IControllerAction> oldValue, ObservableCollection<IControllerAction> newValue);
        private void OnActionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected internal virtual void OnActionsTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        private void OnAssociatedObjectChanged();
        protected virtual void OnElementAdded(IControllerAction element);
        protected virtual void OnElementRemoved(IControllerAction element);
        protected virtual void OnTriggersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void ResetElementProperties(IControllerAction element);
        protected virtual void SetElementProperties(IControllerAction element);

        public DataTemplate ActionsTemplate { get; set; }

        public ActionExecutionMode ExecutionMode { get; set; }

        public ActionExecutionMode NewActionsExecutionMode { get; set; }

        public ObservableCollection<ActionTrigger> Triggers { get; }

        public ObservableCollection<IControllerAction> Actions { get; set; }

        protected override IEnumerator LogicalChildren { get; }

        DependencyObject IActionContainer.AssociatedObject { get; set; }

        IActionContainer IControllerAction.Container { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ActionGroup.<>c <>9;
            public static Func<object, bool> <>9__34_0;
            public static Action<DependencyObject> <>9__46_0;

            static <>c();
            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <ExecuteCore>b__46_0(DependencyObject x);
            internal bool <OnElementAdded>b__34_0(object x);
        }
    }
}

