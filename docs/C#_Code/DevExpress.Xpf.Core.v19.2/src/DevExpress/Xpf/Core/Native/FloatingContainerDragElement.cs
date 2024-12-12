namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public abstract class FloatingContainerDragElement : IDragElement
    {
        protected readonly FloatingContainer container;

        protected FloatingContainerDragElement(FloatingContainer container);
        protected abstract Point CorrectLocation(Point newPos);
        public virtual void Destroy();
        protected virtual void UpdateContainer();
        public void UpdateLocation(Point newPos);
    }
}

