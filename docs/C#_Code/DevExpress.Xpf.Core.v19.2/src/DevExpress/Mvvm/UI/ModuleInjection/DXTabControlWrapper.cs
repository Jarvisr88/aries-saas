namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Core;
    using System;

    public class DXTabControlWrapper : ItemsControlWrapper, ISelectorWrapper<DXTabControl>, IItemsControlWrapper<DXTabControl>, ITargetWrapper<DXTabControl>
    {
        public event EventHandler SelectionChanged
        {
            add
            {
                this.Target.SelectionChanged += new TabControlSelectionChangedEventHandler(value.Invoke);
            }
            remove
            {
                this.Target.SelectionChanged -= new TabControlSelectionChangedEventHandler(value.Invoke);
            }
        }

        public DXTabControl Target
        {
            get => 
                (DXTabControl) base.Target;
            set => 
                base.Target = value;
        }

        public object SelectedItem
        {
            get => 
                this.Target.SelectedItem;
            set => 
                this.Target.SelectItem(value);
        }
    }
}

