namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class HeaderDragElementBase : CustomDragElement
    {
        public const int RemoveColumnDragIconMargin = 50;
        private double widthCorrectionOffset;
        private double heightCorrectionOffset;
        private bool allowTransparency;

        public HeaderDragElementBase(FrameworkElement headerElement, DependencyObject dataContext, Point offset);
        public HeaderDragElementBase(FrameworkElement headerElement, DependencyObject dataContext, Point offset, FloatingMode mode);
        protected abstract void AddGridChild(object child);
        private void ChangeTouchEnabled(bool isPopupContainer);
        protected override Point CorrectLocation(Point newPos);
        public override void Destroy();
        protected abstract void RemoveGridChild(object child);
        protected virtual void SetDragElementAllowTransparency(FrameworkElement elem, bool allowTransparency);
        protected abstract void SetDragElementSize(FrameworkElement elem, Size size);

        public bool IsTouchEnabled { get; private set; }

        public ContentPresenter DragPreviewElement { get; private set; }

        public FloatingWindowContainer WindowContainer { get; }

        public DevExpress.Xpf.Core.FloatingContainer FloatingContainer { get; }

        protected FrameworkElement HeaderElement { get; private set; }

        protected abstract FrameworkElement HeaderButton { get; }

        protected abstract string DragElementTemplatePropertyName { get; }
    }
}

