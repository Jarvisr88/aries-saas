namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class BarSplitButtonColorEditItemLinkControl : BarSplitButtonItemLinkControl
    {
        private Border colorIndicator;

        public BarSplitButtonColorEditItemLinkControl(BarSplitButtonItemLink link) : base(link)
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.UpdateColorIndicatorColor();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ColorIndicator != null)
            {
                if (e.NewSize.Height >= 32.0)
                {
                    this.ColorIndicator.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
                    this.ColorIndicator.Width = 28.0;
                    this.ColorIndicator.Height = 4.0;
                }
                else
                {
                    this.ColorIndicator.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
                    this.ColorIndicator.Width = 16.0;
                    this.ColorIndicator.Height = 3.0;
                }
            }
        }

        protected internal void UpdateColorIndicator()
        {
            if (this.UpdateColorIndicatorColor())
            {
                base.ClosePopup();
            }
        }

        internal bool UpdateColorIndicatorColor()
        {
            if (this.ColorIndicator == null)
            {
                return false;
            }
            if (base.Link == null)
            {
                return false;
            }
            BarSplitButtonEditItem item = base.Link.Item as BarSplitButtonEditItem;
            if (item == null)
            {
                return false;
            }
            if (item.EditValue == null)
            {
                return false;
            }
            System.Windows.Media.Color transparent = Colors.Transparent;
            if (item.EditValue is System.Windows.Media.Color)
            {
                transparent = (System.Windows.Media.Color) item.EditValue;
            }
            else if (item.EditValue is System.Drawing.Color)
            {
                System.Drawing.Color editValue = (System.Drawing.Color) item.EditValue;
                transparent = System.Windows.Media.Color.FromArgb(editValue.A, editValue.R, editValue.G, editValue.B);
            }
            this.ColorIndicator.Background = new SolidColorBrush(transparent);
            return true;
        }

        private Border ColorIndicator
        {
            get
            {
                Border colorIndicator = this.colorIndicator;
                if (this.colorIndicator == null)
                {
                    Border local1 = this.colorIndicator;
                    colorIndicator = this.colorIndicator = LayoutHelper.FindElementByName(this, "colorIndicator") as Border;
                }
                return colorIndicator;
            }
        }
    }
}

