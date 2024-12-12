namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Shapes;

    public class FlowLayoutLayerSizingController : DragAndDropController
    {
        private double _LayerWidthChange;

        public FlowLayoutLayerSizingController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint, LayerSeparator dragSeparator) : base(controller, startDragPoint)
        {
            this.DragSeparator = dragSeparator;
        }

        private IEnumerable<UIElement> BaseGetInternalElements() => 
            base.GetInternalElements();

        protected void CreateChildCover()
        {
            this.ChildCover = new Rectangle();
            this.ChildCover.Fill = this.Controller.ILayoutControl.LayerSizingCoverBrush;
            this.ChildCover.SetSize(this.Controller.Control.GetSize());
            this.ChildCover.SetZIndex(0x63);
            this.Controller.IPanel.Children.Add(this.ChildCover);
        }

        protected void DestroyChildCover()
        {
            this.Controller.IPanel.Children.Remove(this.ChildCover);
            this.ChildCover = null;
        }

        public override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            this.LayerWidthChange = this.Controller.LayoutProvider.CalculateLayerWidthChange(this.DragSeparator, new Point(p.X - base.StartDragPoint.X, p.Y - base.StartDragPoint.Y));
        }

        public override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            this.DestroyChildCover();
            this.Controller.ILayoutControl.SendSeparatorsToBack();
            if (!accept)
            {
                this.Controller.Control.InvalidateArrange();
            }
            else
            {
                IFlowLayoutControl iLayoutControl = this.Controller.ILayoutControl;
                iLayoutControl.LayerWidth += this.LayerWidthChange;
            }
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__5))]
        public override IEnumerable<UIElement> GetInternalElements()
        {
            IEnumerator<UIElement> enumerator = this.BaseGetInternalElements().GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                UIElement current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                if (this.ChildCover != null)
                {
                    yield return this.ChildCover;
                }
            }
        }

        public override void OnArrange(Size finalSize)
        {
            base.OnArrange(finalSize);
            this.ChildCover.Arrange(RectHelper.New(this.ChildCover.DesiredSize));
        }

        public override void OnMeasure(Size availableSize)
        {
            base.OnMeasure(availableSize);
            this.ChildCover.Measure(availableSize);
        }

        public override void StartDragAndDrop(Point p)
        {
            this.Controller.ILayoutControl.BringSeparatorsToFront();
            this.CreateChildCover();
            base.StartDragAndDrop(p);
        }

        public override Cursor DragCursor =>
            this.DragSeparator.Cursor;

        public override bool IsImmediateDragAndDrop =>
            true;

        protected Rectangle ChildCover { get; set; }

        protected FlowLayoutController Controller =>
            (FlowLayoutController) base.Controller;

        protected LayerSeparator DragSeparator { get; private set; }

        protected double LayerWidthChange
        {
            get => 
                this._LayerWidthChange;
            set
            {
                value = Math.Max(this.LayerWidthMinChange, Math.Min(value, this.LayerWidthMaxChange));
                if (this._LayerWidthChange != value)
                {
                    this.Controller.LayoutProvider.OffsetLayerSeparators(value - this.LayerWidthChange);
                    this._LayerWidthChange = value;
                }
            }
        }

        protected double LayerWidthMinChange =>
            this.Controller.ILayoutControl.LayerMinWidth - this.Controller.ILayoutControl.LayerWidth;

        protected double LayerWidthMaxChange =>
            this.Controller.ILayoutControl.LayerMaxWidth - this.Controller.ILayoutControl.LayerWidth;

    }
}

