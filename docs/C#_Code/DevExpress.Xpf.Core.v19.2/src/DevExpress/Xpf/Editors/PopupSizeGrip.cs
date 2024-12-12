namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class PopupSizeGrip : Control
    {
        private const string ThumbName = "PART_Thumb";
        private PostponedAction updateThumbPositionAction;
        private PopupBaseEdit popupBaseEdit;

        public PopupSizeGrip()
        {
            this.updateThumbPositionAction = new PostponedAction(new Func<bool>(this.ShouldPostponeUpdateThumbPosition));
        }

        public Point GetDragDelta(double horizontalChange, double verticalChange) => 
            (!this.PopupViewModel.IsLeft || this.PopupViewModel.DropOpposite) ? ((!this.PopupViewModel.IsLeft || !this.PopupViewModel.DropOpposite) ? ((this.PopupViewModel.IsLeft || !this.PopupViewModel.DropOpposite) ? new Point(horizontalChange, verticalChange) : new Point(verticalChange, -horizontalChange)) : new Point(horizontalChange, -verticalChange)) : new Point(verticalChange, horizontalChange);

        public Point GetOffsetForDragDelta(double horizontalChange, double verticalChange) => 
            (!this.PopupViewModel.IsLeft || this.PopupViewModel.DropOpposite) ? ((!this.PopupViewModel.IsLeft || !this.PopupViewModel.DropOpposite) ? ((this.PopupViewModel.IsLeft || !this.PopupViewModel.DropOpposite) ? new Point(horizontalChange, verticalChange) : new Point(-verticalChange, horizontalChange)) : new Point(horizontalChange, -verticalChange)) : new Point(verticalChange, horizontalChange);

        private Point GetSize(FrameworkElement element)
        {
            if (element != null)
            {
                return new Point(element.ActualWidth, element.ActualHeight);
            }
            return new Point();
        }

        public override void OnApplyTemplate()
        {
            this.UnsubscribeEvents();
            this.Thumb = (System.Windows.Controls.Primitives.Thumb) base.GetTemplateChild("PART_Thumb");
            this.SubscribeEvents();
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.OwnerEdit.PopupSettings.IsResizing = false;
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if ((this.OwnerEdit != null) && (this.OwnerEdit.Popup != null))
            {
                this.Thumb_DragDelta(sender, e);
            }
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            this.OwnerEdit.PopupSettings.IsResizing = true;
            this.SetStartSize();
        }

        private void SetStartSize()
        {
            if (double.IsNaN(this.OwnerEdit.PopupWidth))
            {
                this.OwnerEdit.PopupWidth = ((FrameworkElement) this.OwnerEdit.Popup.Child).ActualWidth;
            }
            if (double.IsNaN(this.OwnerEdit.PopupHeight))
            {
                this.OwnerEdit.PopupHeight = ((FrameworkElement) this.OwnerEdit.Popup.Child).ActualHeight;
            }
        }

        private bool ShouldPostponeUpdateThumbPosition() => 
            false;

        private void SubscribeEvents()
        {
            if (this.Thumb != null)
            {
                this.Thumb.DragStarted += new DragStartedEventHandler(this.OnDragStarted);
                this.Thumb.DragDelta += new DragDeltaEventHandler(this.OnDragDelta);
                this.Thumb.DragCompleted += new DragCompletedEventHandler(this.OnDragCompleted);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            this.updateThumbPositionAction.PerformPostpone(delegate {
                Point dragDelta = this.GetDragDelta(e.HorizontalChange, e.VerticalChange);
                bool flag = false;
                if (0.0 < Math.Abs(dragDelta.X))
                {
                    this.OwnerEdit.PopupSettings.SetHorizontalPopupSizeChange(dragDelta.X);
                    flag = true;
                }
                if (0.0 < Math.Abs(dragDelta.Y))
                {
                    this.OwnerEdit.PopupSettings.SetVerticalPopupSizeChange(dragDelta.Y);
                    flag = true;
                }
                if (flag)
                {
                    this.OwnerEdit.InvalidateVisual();
                }
            });
        }

        private void UnsubscribeEvents()
        {
            if (this.Thumb != null)
            {
                this.Thumb.DragStarted -= new DragStartedEventHandler(this.OnDragStarted);
                this.Thumb.DragDelta -= new DragDeltaEventHandler(this.OnDragDelta);
                this.Thumb.DragCompleted -= new DragCompletedEventHandler(this.OnDragCompleted);
            }
        }

        private PopupBaseEdit OwnerEdit
        {
            get
            {
                PopupBaseEdit popupBaseEdit = this.popupBaseEdit;
                if (this.popupBaseEdit == null)
                {
                    PopupBaseEdit local1 = this.popupBaseEdit;
                    popupBaseEdit = this.popupBaseEdit = (PopupBaseEdit) BaseEdit.GetOwnerEdit(this);
                }
                return popupBaseEdit;
            }
        }

        private DevExpress.Xpf.Editors.PopupViewModel PopupViewModel =>
            ((PopupBaseEditPropertyProvider) this.OwnerEdit.PropertyProvider).PopupViewModel;

        private System.Windows.Controls.Primitives.Thumb Thumb { get; set; }
    }
}

