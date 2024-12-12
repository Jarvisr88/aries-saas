namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DataControlBaseWrapper : ISelectorWrapper<DataControlBase>, IItemsControlWrapper<DataControlBase>, ITargetWrapper<DataControlBase>
    {
        public event EventHandler SelectionChanged
        {
            add
            {
                this.Target.CurrentItemChanged += new CurrentItemChangedEventHandler(value.Invoke);
            }
            remove
            {
                this.Target.CurrentItemChanged -= new CurrentItemChangedEventHandler(value.Invoke);
            }
        }

        public DataControlBase Target { get; set; }

        public object ItemsSource
        {
            get => 
                this.Target.ItemsSource;
            set => 
                this.Target.ItemsSource = value;
        }

        public object SelectedItem
        {
            get => 
                this.Target.CurrentItem;
            set => 
                this.Target.CurrentItem = value;
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

