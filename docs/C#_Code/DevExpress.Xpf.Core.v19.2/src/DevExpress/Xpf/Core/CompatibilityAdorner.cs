namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CompatibilityAdorner : Canvas
    {
        private Point offset;

        public CompatibilityAdorner(UIElement child)
        {
            base.IsHitTestVisible = false;
            base.Children.Add(child);
        }

        public void Destroy()
        {
            base.Children.Clear();
        }

        protected virtual void OnOffsetChanged()
        {
            SetLeft(this.Child, this.Offset.X);
            SetTop(this.Child, this.Offset.Y);
        }

        protected UIElement Child =>
            base.Children[0];

        public Point Offset
        {
            get => 
                this.offset;
            set
            {
                if (this.Offset != value)
                {
                    this.offset = value;
                    this.OnOffsetChanged();
                }
            }
        }
    }
}

