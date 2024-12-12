namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutItemSelectionIndicators : ElementPool<LayoutItemSelectionIndicator>
    {
        private bool _IsVisible;

        public LayoutItemSelectionIndicators(Panel container, ILayoutControl layoutControl) : base(container)
        {
            this._IsVisible = true;
            this.LayoutControl = layoutControl;
        }

        protected override LayoutItemSelectionIndicator CreateItem()
        {
            LayoutItemSelectionIndicator element = base.CreateItem();
            element.LayoutControl = this.LayoutControl;
            element.SetVisible(this.IsVisible);
            return element;
        }

        protected override void DeleteItem(LayoutItemSelectionIndicator item)
        {
            item.LayoutControl = null;
            base.DeleteItem(item);
        }

        public void Update(FrameworkElements selectedElements)
        {
            base.MarkItemsAsUnused();
            foreach (FrameworkElement element in selectedElements)
            {
                base.Add().Element = element;
            }
            base.DeleteUnusedItems();
        }

        public void UpdateBounds()
        {
            if (this.IsVisible)
            {
                Action<LayoutItemSelectionIndicator> action = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Action<LayoutItemSelectionIndicator> local1 = <>c.<>9__3_0;
                    action = <>c.<>9__3_0 = item => item.UpdateBounds();
                }
                base.Items.ForEach(action);
            }
        }

        public bool IsVisible
        {
            get => 
                this._IsVisible;
            set
            {
                if (this.IsVisible != value)
                {
                    this._IsVisible = value;
                    base.Items.ForEach(item => item.SetVisible(value));
                    if (this.IsVisible)
                    {
                        this.UpdateBounds();
                    }
                }
            }
        }

        public ILayoutControl LayoutControl { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemSelectionIndicators.<>c <>9 = new LayoutItemSelectionIndicators.<>c();
            public static Action<LayoutItemSelectionIndicator> <>9__3_0;

            internal void <UpdateBounds>b__3_0(LayoutItemSelectionIndicator item)
            {
                item.UpdateBounds();
            }
        }
    }
}

