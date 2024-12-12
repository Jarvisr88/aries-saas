namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ColumnChooserControlBase : Control, ILogicalOwnerProvider
    {
        public static readonly DependencyProperty OwnerProperty;

        static ColumnChooserControlBase()
        {
            Type ownerType = typeof(ColumnChooserControlBase);
            OwnerProperty = DependencyPropertyManager.Register("Owner", typeof(ILogicalOwner), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnChooserControlBase) d).OnOwnerChanged((ILogicalOwner) e.OldValue, (ILogicalOwner) e.NewValue)));
        }

        public ColumnChooserControlBase()
        {
            CommandManager.AddCanExecuteHandler(this, new System.Windows.Input.CanExecuteRoutedEventHandler(this.CanExecuteRoutedEventHandler));
            CommandManager.AddExecutedHandler(this, new System.Windows.Input.ExecutedRoutedEventHandler(this.ExecutedRoutedEventHandler));
        }

        private void CanExecuteRoutedEventHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            RoutedCommand command = e.Command as RoutedCommand;
            if (command != null)
            {
                e.CanExecute = command.CanExecute(e.Parameter, this.Owner);
            }
        }

        ILogicalOwner ILogicalOwnerProvider.GetLogicalOwner() => 
            this.GetLogicalOwnerCore();

        protected virtual void ExecutedRoutedEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            RoutedCommand command = e.Command as RoutedCommand;
            if (command != null)
            {
                command.Execute(e.Parameter, this.Owner);
            }
        }

        protected virtual ILogicalOwner GetLogicalOwnerCore() => 
            this.Owner;

        protected virtual void OnOwnerChanged(ILogicalOwner oldView, ILogicalOwner newView)
        {
            if (oldView != null)
            {
                oldView.RemoveChild(this);
            }
            if ((LogicalTreeHelper.GetParent(this) == null) && (newView != null))
            {
                newView.AddChild(this);
            }
        }

        [Browsable(false)]
        public ILogicalOwner Owner
        {
            get => 
                (ILogicalOwner) base.GetValue(OwnerProperty);
            set => 
                base.SetValue(OwnerProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnChooserControlBase.<>c <>9 = new ColumnChooserControlBase.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnChooserControlBase) d).OnOwnerChanged((ILogicalOwner) e.OldValue, (ILogicalOwner) e.NewValue);
            }
        }
    }
}

