namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    public class DragAndDropController
    {
        private bool _OriginalAllowAutoScrolling;
        private int _DragImageChildIndex;
        private Panel _DragImageParent;
        private TransparentPopup _DragImagePopup;

        public DragAndDropController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint)
        {
            this.Controller = controller;
            this.StartDragPoint = startDragPoint;
        }

        protected void CheckDelayTimer(ref Storyboard timer, int durationInMilliseconds, Func<bool> isDelayNeeded, Action onDelayExpired)
        {
            this.StopDelayTimer(ref timer);
            if (isDelayNeeded())
            {
                timer = this.StartDelayTimer(durationInMilliseconds, onDelayExpired);
            }
        }

        protected virtual FrameworkElement CreateDragImage() => 
            null;

        protected virtual Container CreateDragImageContainer() => 
            new Container();

        public virtual void DragAndDrop(Point p)
        {
            if (this.DragImage != null)
            {
                this.MoveDragImage(p);
            }
        }

        public virtual void EndDragAndDrop(bool accept)
        {
            this.Controller.AllowAutoScrolling = this._OriginalAllowAutoScrolling;
            if (this.DragImage != null)
            {
                this.HideDragImage();
                this.FinalizeDragImage();
                this.DragImage = null;
            }
        }

        protected virtual void FinalizeDragImage()
        {
            if (this._DragImageParent != null)
            {
                int num;
                if (this._DragImageParent is IPanel)
                {
                    FrameworkElements logicalChildren = ((IPanel) this._DragImageParent).GetLogicalChildren(false);
                    num = (this._DragImageChildIndex >= logicalChildren.Count) ? ((logicalChildren.Count <= 0) ? 0 : (this._DragImageParent.Children.IndexOf(logicalChildren[logicalChildren.Count - 1]) + 1)) : this._DragImageParent.Children.IndexOf(logicalChildren[this._DragImageChildIndex]);
                }
                else
                {
                    num = this._DragImageChildIndex;
                }
                this._DragImageParent.Children.Insert(num, this.DragImage);
            }
        }

        protected virtual Point GetDragImageOffset() => 
            new Point(0.0, 0.0);

        protected virtual Panel GetDragImageParent() => 
            this.DragImage.GetParent() as Panel;

        protected virtual Point GetDragImagePosition(Point p)
        {
            Point dragImageOffset = this.GetDragImageOffset();
            PointHelper.Offset(ref p, dragImageOffset.X, dragImageOffset.Y);
            return p;
        }

        public virtual IEnumerable<UIElement> GetInternalElements() => 
            new UIElement[0];

        private void HideDragImage()
        {
            this._DragImagePopup.IsOpen = false;
            ((Panel) this._DragImagePopup.Child).Children.Remove(this.DragImage);
            this._DragImagePopup.Child = null;
            this._DragImagePopup = null;
        }

        protected virtual void InitializeDragImage()
        {
            this._DragImageParent = this.GetDragImageParent();
            if (this._DragImageParent != null)
            {
                this._DragImageChildIndex = !(this._DragImageParent is IPanel) ? this._DragImageParent.Children.IndexOf(this.DragImage) : ((IPanel) this._DragImageParent).GetLogicalChildren(false).IndexOf(this.DragImage);
                this._DragImageParent.Children.Remove(this.DragImage);
            }
        }

        private void MoveDragImage(Point p)
        {
            p = this.GetDragImagePosition(p);
            this._DragImagePopup.SetOffset(p);
        }

        public virtual void OnArrange(Size finalSize)
        {
        }

        public virtual void OnMeasure(Size availableSize)
        {
        }

        private void ShowDragImage(Point p)
        {
            this._DragImagePopup = new TransparentPopup();
            this._DragImagePopup.PlacementTarget = this.Controller.Control;
            this._DragImagePopup.Child = this.CreateDragImageContainer();
            this._DragImagePopup.Child.IsHitTestVisible = false;
            ((Panel) this._DragImagePopup.Child).Children.Add(this.DragImage);
            this._DragImagePopup.IsOpen = true;
            this.MoveDragImage(p);
        }

        private Storyboard StartDelayTimer(int durationInMilliseconds, Action onDelayExpired)
        {
            Storyboard storyboard1 = new Storyboard();
            storyboard1.Duration = TimeSpan.FromMilliseconds((double) durationInMilliseconds);
            Storyboard storyboard = storyboard1;
            storyboard.Completed += delegate (object o, EventArgs e) {
                onDelayExpired();
            };
            storyboard.Begin();
            return storyboard;
        }

        public virtual void StartDragAndDrop(Point p)
        {
            this.DragImage = this.CreateDragImage();
            if (this.DragImage != null)
            {
                this.InitializeDragImage();
                this.ShowDragImage(p);
            }
            this._OriginalAllowAutoScrolling = this.Controller.AllowAutoScrolling;
            this.Controller.AllowAutoScrolling = this.AllowAutoScrolling;
        }

        private void StopDelayTimer(ref Storyboard timer)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }

        public virtual Cursor DragCursor =>
            null;

        public virtual bool IsImmediateDragAndDrop =>
            false;

        protected virtual bool AllowAutoScrolling =>
            false;

        protected DevExpress.Xpf.Core.Controller Controller { get; private set; }

        protected Point StartDragPoint { get; private set; }

        protected FrameworkElement DragImage { get; private set; }
    }
}

