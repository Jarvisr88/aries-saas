namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    internal abstract class BaseResizeCalculator : IResizeCalculator
    {
        protected BaseResizeCalculator()
        {
        }

        protected double GetActualLength(DefinitionBase definition)
        {
            ColumnDefinition definition2 = definition as ColumnDefinition;
            return ((definition2 == null) ? ((RowDefinition) definition).ActualHeight : definition2.ActualWidth);
        }

        protected virtual double GetMin(BaseLayoutItem item)
        {
            bool flag = this.Orientation == System.Windows.Controls.Orientation.Horizontal;
            return (!(item is FixedItem) ? (!(item is LayoutPanel) ? (!(item is TabbedGroup) ? (!(item is LayoutControlItem) ? 12.0 : (flag ? ((double) 0x18) : ((double) 10))) : 24.0) : (flag ? ((double) 12) : ((double) 0x12))) : 5.0);
        }

        public virtual void Init(DevExpress.Xpf.Docking.SplitBehavior splitBehavior)
        {
            this.SplitBehavior = splitBehavior;
        }

        public abstract void Resize(BaseLayoutItem item1, BaseLayoutItem item2, double change);

        public DevExpress.Xpf.Docking.SplitBehavior SplitBehavior { get; set; }

        public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}

