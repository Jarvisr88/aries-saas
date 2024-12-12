namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class LayoutItemDragAndDropController : LayoutItemDragAndDropControllerBase
    {
        private ILayoutGroup _DestinationGroup;
        private int _DestinationGroupTabIndex;
        private LayoutItemInsertionInfo _InsertionInfo;
        public static int GroupExpandingDelay = 0x3e8;
        private Storyboard _GroupExpandingDelayTimer;
        public static int TabSelectionDelay = 750;
        private Storyboard _TabSelectionDelayTimer;

        public LayoutItemDragAndDropController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint, FrameworkElement dragControl) : base(controller, startDragPoint, dragControl)
        {
            this._DestinationGroupTabIndex = -1;
        }

        protected virtual LayoutItemDragAndDropIndicator CreateIndicator() => 
            new LayoutItemDragAndDropIndicator(base.DragControlPlaceHolder);

        public override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            this.InsertionInfo = this.Controller.GetInsertionInfo(base.DragControlPlaceHolder, p);
            if (this.DestinationGroup != null)
            {
                this.DestinationGroupTabIndex = this.DestinationGroup.GetTabIndex(this.Controller.Control.MapPoint(p, null));
            }
        }

        public override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            this.FinalizeIndicator();
            this.DestinationGroup = null;
            if (accept && (this.InsertionInfo.InsertionPoint != null))
            {
                this.Controller.DropElement(base.DragControl, this.InsertionInfo.InsertionPoint, this.InsertionInfo.InsertionKind);
            }
        }

        protected void FinalizeIndicator()
        {
            this.Controller.CustomizationController.CustomizationCanvas.Children.Remove(this.Indicator);
            this.Indicator = null;
        }

        protected void InitializeIndicator()
        {
            this.Indicator = this.CreateIndicator();
            this.Indicator.InsertionPointIndicatorStyle = this.Controller.ItemInsertionPointIndicatorStyle;
            this.Indicator.Visibility = Visibility.Collapsed;
            this.Controller.CustomizationController.CustomizationCanvas.Children.Add(this.Indicator);
        }

        protected virtual bool IsGroupExpandingDelayNeeded() => 
            (this.DestinationGroup != null) && (this.DestinationGroup.CollapseMode == LayoutGroupCollapseMode.NoChildrenVisible);

        protected virtual bool IsTabSelectionDelayNeeded() => 
            (this.DestinationGroup != null) && ((this.DestinationGroupTabIndex != -1) && (this.DestinationGroup.SelectedTabIndex != this.DestinationGroupTabIndex));

        public override void OnArrange(Size finalSize)
        {
            base.OnArrange(finalSize);
            LayoutItemInsertionInfo info = new LayoutItemInsertionInfo();
            this.InsertionInfo = info;
        }

        protected virtual void OnDestinationGroupChanged()
        {
            this.DestinationGroupTabIndex = -1;
            base.CheckDelayTimer(ref this._GroupExpandingDelayTimer, GroupExpandingDelay, new Func<bool>(this.IsGroupExpandingDelayNeeded), new Action(this.OnGroupExpandingDelayExpired));
        }

        protected virtual void OnDestinationGroupTabIndexChanged()
        {
            base.CheckDelayTimer(ref this._TabSelectionDelayTimer, TabSelectionDelay, new Func<bool>(this.IsTabSelectionDelayNeeded), new Action(this.OnTabSelectionDelayExpired));
        }

        protected virtual void OnGroupExpandingDelayExpired()
        {
            this.DestinationGroup.IsCollapsed = false;
        }

        protected virtual void OnInsertionInfoChanged()
        {
            this.Indicator.InsertionInfo = this.InsertionInfo;
            this.Indicator.SetVisible(this.InsertionInfo.DestinationItem != null);
            this.DestinationGroup = this.InsertionInfo.DestinationItem.IsLayoutGroup() ? ((ILayoutGroup) this.InsertionInfo.DestinationItem) : null;
        }

        protected virtual void OnTabSelectionDelayExpired()
        {
            this.DestinationGroup.SelectedTabIndex = this.DestinationGroupTabIndex;
        }

        public override void StartDragAndDrop(Point p)
        {
            this.InitializeIndicator();
            base.StartDragAndDrop(p);
        }

        protected LayoutControlController Controller =>
            (LayoutControlController) base.Controller;

        protected ILayoutGroup DestinationGroup
        {
            get => 
                this._DestinationGroup;
            set
            {
                if (!ReferenceEquals(this.DestinationGroup, value))
                {
                    this._DestinationGroup = value;
                    this.OnDestinationGroupChanged();
                }
            }
        }

        protected int DestinationGroupTabIndex
        {
            get => 
                this._DestinationGroupTabIndex;
            set
            {
                if (this.DestinationGroupTabIndex != value)
                {
                    this._DestinationGroupTabIndex = value;
                    this.OnDestinationGroupTabIndexChanged();
                }
            }
        }

        protected LayoutItemInsertionInfo InsertionInfo
        {
            get => 
                this._InsertionInfo;
            set
            {
                if (!this.InsertionInfo.Equals(value))
                {
                    this._InsertionInfo = value;
                    this.OnInsertionInfoChanged();
                }
            }
        }

        protected LayoutItemDragAndDropIndicator Indicator { get; private set; }
    }
}

