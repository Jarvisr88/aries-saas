namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class PixelSnapperBase : Decorator
    {
        private UIElement topElement;
        private double delta;
        private Point prevOffset;

        public PixelSnapperBase();
        protected virtual Point ApplyLeftRotation(Point pt);
        protected virtual Point ApplyRightRotation(Point pt);
        protected override Size ArrangeOverride(Size arrangeSize);
        protected virtual Point GetCorrectedRenderOffset();
        private bool IsIdentityTransform(Transform transform);
        protected virtual bool IsRotatedLeft(Matrix mat);
        protected virtual bool IsRotatedRight(Matrix mat);
        private void OnPixelSnapperUnloaded(object sender, RoutedEventArgs e);
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);
        protected virtual void ResetTopElement();
        protected virtual void SetTopElement(UIElement topElement);
        protected virtual void UpdateRenderOffset();

        protected UIElement TopElement { get; }
    }
}

