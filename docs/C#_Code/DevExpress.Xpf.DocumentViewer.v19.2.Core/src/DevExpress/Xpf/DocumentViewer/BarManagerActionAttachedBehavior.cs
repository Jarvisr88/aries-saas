namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarManagerActionAttachedBehavior : Behavior<BarManager>
    {
        public static readonly DependencyProperty ActionsProperty;
        private BarManagerController controller = new BarManagerController();

        static BarManagerActionAttachedBehavior()
        {
            ActionsProperty = DependencyPropertyManager.Register("Actions", typeof(ObservableCollection<IBarManagerControllerAction>), typeof(BarManagerActionAttachedBehavior), new PropertyMetadata(null, (obj, args) => ((BarManagerActionAttachedBehavior) obj).OnActionsChanged((ObservableCollection<IBarManagerControllerAction>) args.OldValue, (ObservableCollection<IBarManagerControllerAction>) args.NewValue)));
        }

        private void OnActionsChanged(ObservableCollection<IBarManagerControllerAction> oldValue, ObservableCollection<IBarManagerControllerAction> newValue)
        {
            oldValue.Do<ObservableCollection<IBarManagerControllerAction>>(delegate (ObservableCollection<IBarManagerControllerAction> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
            if (newValue != null)
            {
                this.controller.ActionContainer.Actions.Clear();
                foreach (IBarManagerControllerAction action in newValue)
                {
                    this.controller.ActionContainer.Actions.Add(action);
                }
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Controllers.Add(this.controller);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (IBarManagerControllerAction action in e.OldItems)
                {
                    this.controller.ActionContainer.Actions.Remove(action);
                }
            }
            if (e.NewItems != null)
            {
                foreach (IBarManagerControllerAction action2 in e.NewItems)
                {
                    this.controller.ActionContainer.Actions.Add(action2);
                }
                this.controller.Execute(null);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.Controllers.Remove(this.controller);
        }

        public ObservableCollection<IBarManagerControllerAction> Actions
        {
            get => 
                (ObservableCollection<IBarManagerControllerAction>) base.GetValue(ActionsProperty);
            set => 
                base.SetValue(ActionsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManagerActionAttachedBehavior.<>c <>9 = new BarManagerActionAttachedBehavior.<>c();

            internal void <.cctor>b__10_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((BarManagerActionAttachedBehavior) obj).OnActionsChanged((ObservableCollection<IBarManagerControllerAction>) args.OldValue, (ObservableCollection<IBarManagerControllerAction>) args.NewValue);
            }
        }
    }
}

