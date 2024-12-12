namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class ResizeHelper : IDisposable
    {
        private IResizeHelperOwner owner;
        private Thumb gripper;
        private double startWidth;

        public ResizeHelper(IResizeHelperOwner owner)
        {
            this.owner = owner;
        }

        public void Dispose()
        {
            this.Init(null);
        }

        protected virtual void HookupEvents()
        {
            if (this.Gripper != null)
            {
                this.Gripper.DragStarted += new DragStartedEventHandler(this.OnDragStarted);
                this.Gripper.DragDelta += new DragDeltaEventHandler(this.OnResize);
                this.Gripper.DragCompleted += new DragCompletedEventHandler(this.OnDragCompleted);
                this.Gripper.MouseDoubleClick += new MouseButtonEventHandler(this.OnDoubleClicked);
            }
        }

        public void Init(Thumb gripper)
        {
            this.UnhookEvents();
            this.gripper = gripper;
            this.HookupEvents();
        }

        private void OnDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            this.Owner.OnDoubleClick();
            e.Handled = true;
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.Owner.SetIsResizing(false);
            if (e.Canceled)
            {
                this.Width = this.startWidth;
            }
            this.SetHandledTrue(e);
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            this.startWidth = this.Width;
            this.Owner.SetIsResizing(true);
            this.SetHandledTrue(e);
        }

        private void OnResize(object sender, DragDeltaEventArgs e)
        {
            this.Owner.ChangeSize(this.Owner.SizeHelper.GetDefinePoint(new Point(e.HorizontalChange, e.VerticalChange)));
            this.SetHandledTrue(e);
        }

        private void SetHandledTrue(RoutedEventArgs e)
        {
            e.Handled = true;
        }

        protected virtual void UnhookEvents()
        {
            if (this.Gripper != null)
            {
                this.Gripper.DragStarted -= new DragStartedEventHandler(this.OnDragStarted);
                this.Gripper.DragDelta -= new DragDeltaEventHandler(this.OnResize);
                this.Gripper.DragCompleted -= new DragCompletedEventHandler(this.OnDragCompleted);
                this.Gripper.MouseDoubleClick -= new MouseButtonEventHandler(this.OnDoubleClicked);
            }
        }

        public bool IsResizing =>
            (this.gripper != null) ? this.gripper.IsDragging : false;

        protected IResizeHelperOwner Owner =>
            this.owner;

        protected double Width
        {
            get => 
                this.Owner.ActualSize;
            set => 
                this.Owner.ActualSize = value;
        }

        protected Thumb Gripper =>
            this.gripper;
    }
}

