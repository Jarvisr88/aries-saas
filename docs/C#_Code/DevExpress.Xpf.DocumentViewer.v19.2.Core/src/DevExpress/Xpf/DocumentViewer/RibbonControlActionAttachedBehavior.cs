namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RibbonControlActionAttachedBehavior : Behavior<RibbonControl>
    {
        public static readonly DependencyProperty ActionsProperty;
        private DocumentViewerRibbonController controller = new DocumentViewerRibbonController();

        static RibbonControlActionAttachedBehavior()
        {
            ActionsProperty = DependencyPropertyManager.Register("Actions", typeof(ObservableCollection<IControllerAction>), typeof(RibbonControlActionAttachedBehavior), new PropertyMetadata(null, (obj, args) => ((RibbonControlActionAttachedBehavior) obj).OnActionsChanged((ObservableCollection<IControllerAction>) args.OldValue, (ObservableCollection<IControllerAction>) args.NewValue)));
        }

        private void OnActionsChanged(ObservableCollection<IControllerAction> oldValue, ObservableCollection<IControllerAction> newValue)
        {
            oldValue.Do<ObservableCollection<IControllerAction>>(delegate (ObservableCollection<IControllerAction> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
            if (newValue != null)
            {
                this.controller.ActionContainer.Actions.Clear();
                foreach (IControllerAction action in newValue)
                {
                    if (action is IBarManagerControllerAction)
                    {
                        this.controller.ActionContainer.Actions.Add((IBarManagerControllerAction) action);
                    }
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
                foreach (IBarManagerControllerAction action in e.OldItems.OfType<IBarManagerControllerAction>())
                {
                    this.controller.ActionContainer.Actions.Remove(action);
                }
            }
            if (e.NewItems != null)
            {
                foreach (IBarManagerControllerAction action2 in e.NewItems.OfType<IBarManagerControllerAction>())
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
            public static readonly RibbonControlActionAttachedBehavior.<>c <>9 = new RibbonControlActionAttachedBehavior.<>c();

            internal void <.cctor>b__10_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((RibbonControlActionAttachedBehavior) obj).OnActionsChanged((ObservableCollection<IControllerAction>) args.OldValue, (ObservableCollection<IControllerAction>) args.NewValue);
            }
        }
    }
}

