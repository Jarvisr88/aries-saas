namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class SelectorWrapper : ItemsControlWrapper, ISelectorWrapper<Selector>, IItemsControlWrapper<Selector>, ITargetWrapper<Selector>
    {
        public event EventHandler SelectionChanged
        {
            add
            {
                this.Target.SelectionChanged += new SelectionChangedEventHandler(value.Invoke);
            }
            remove
            {
                this.Target.SelectionChanged -= new SelectionChangedEventHandler(value.Invoke);
            }
        }

        public Selector Target
        {
            get => 
                (Selector) base.Target;
            set => 
                base.Target = value;
        }

        public object SelectedItem
        {
            get => 
                this.Target.SelectedItem;
            set => 
                this.Target.SelectedItem = value;
        }
    }
}

