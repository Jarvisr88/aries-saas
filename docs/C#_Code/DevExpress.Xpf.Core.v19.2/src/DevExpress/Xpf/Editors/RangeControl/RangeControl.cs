namespace DevExpress.Xpf.Editors.RangeControl
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl.Internal;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    [ContentProperty("Client"), ToolboxTabName("DX.19.2: Common Controls"), DXToolboxBrowsable, LicenseProvider(typeof(DX_WPF_LicenseProvider))]
    public class RangeControl : Control
    {
        private const int DefaultPostDelay = 500;
        private const double AutoScrollVelocityFactor = 0.05;
        private const double MinNavigationOffset = 15.0;
        private static readonly DependencyPropertyKey IsSelectionMovingPropertyKey;
        protected static readonly DependencyPropertyKey OwnerRangeControlPropertyKey;
        public static readonly DependencyProperty OwnerRangeControlProperty;
        public static readonly DependencyProperty ClientProperty;
        public static readonly DependencyProperty SelectionRangeStartProperty;
        public static readonly DependencyProperty SelectionRangeEndProperty;
        public static readonly DependencyProperty VisibleRangeStartProperty;
        public static readonly DependencyProperty VisibleRangeEndProperty;
        public static readonly DependencyProperty RangeStartProperty;
        public static readonly DependencyProperty RangeEndProperty;
        public static readonly DependencyProperty AllowSnapToIntervalProperty;
        public static readonly DependencyProperty ShowRangeBarProperty;
        public static readonly DependencyProperty ShowRangeThumbsProperty;
        public static readonly DependencyProperty AllowImmediateRangeUpdateProperty;
        public static readonly DependencyProperty AllowScrollProperty;
        public static readonly DependencyProperty AllowZoomProperty;
        public static readonly DependencyProperty ShowSelectionRectangleProperty;
        public static readonly DependencyProperty EnableAnimationProperty;
        public static readonly DependencyProperty IsSelectionMovingProperty;
        public static readonly DependencyProperty ShowLabelsProperty;
        public static readonly DependencyProperty ShowNavigationButtonsProperty;
        public static readonly DependencyProperty LabelTemplateProperty;
        public static readonly DependencyProperty UpdateDelayProperty;
        public static readonly DependencyProperty ShadingModeProperty;
        protected static readonly DependencyPropertyKey SelectionRangePropertyKey;
        public static readonly DependencyProperty SelectionRangeProperty;
        public static readonly DependencyProperty ResetRangesOnClientItemsSourceChangedProperty;
        protected static readonly DependencyPropertyKey PropertyProviderPropertyKey;
        public static readonly DependencyProperty PropertyProviderProperty;
        private readonly List<object> logicalChildren = new List<object>();
        private PostponedAction startEndPropertyChangedAction;
        private RangeControlAnimator animator;
        private object actualSelectionStart;
        private object actualSelectionEnd;
        private object actualVisibleStart;
        private object actualVisibleEnd;
        private double normalizedSelectionStart = double.NaN;
        private double normalizedSelectionEnd = double.NaN;
        private Locker updateRangeLocker = new Locker();
        private bool shouldLockUpdate = true;
        private StartEndUpdateHelper RangeUpdater;
        private StartEndUpdateHelper VisibleRangeUpdater;
        private StartEndUpdateHelper SelectionRangeUpdater;
        private double precision = 1E-12;
        private Canvas clientPanel;
        private ScrollViewer scrollViewer;
        private RangeBar rangeBar;
        private Thumb leftSelectionThumb;
        private Thumb rightSelectionThumb;
        private Border leftSide;
        private Border rightSide;
        private Border selectionRectangle;
        private ContentPresenter content;
        private Canvas interactionArea;
        private Canvas layoutPanel;
        private Thumb draggedThumb;
        private Thumb fixedThumb;
        private Grid rootContainer;
        private Button leftNavigationButton;
        private Button rightNavigationButton;
        private ContentPresenter leftLabel;
        private ContentPresenter rightLabel;
        private Grid navigationButtonsContainer;
        private Canvas selectionRactangleContainer;
        private ContentControl contentControl;
        private Size nativeSize;
        private bool isSyncronizeHorizontalOffset;
        private const double MinLabelsOffset = 1.0;
        private DispatcherTimer autoScrollTimer;
        private Locker renderLocker = new Locker();
        private Queue<Action> postponeQueue = new Queue<Action>();
        private Locker updateSelectedRangeLocker = new Locker();
        private double lastScaleFactorSign;
        private bool isResizingDetected;
        private Locker animateLabelsLocker = new Locker();
        private double prevPosition = double.NaN;

        static RangeControl()
        {
            Type propertyType = typeof(DevExpress.Xpf.Editors.RangeControl.RangeControl);
            OwnerRangeControlPropertyKey = DependencyProperty.RegisterAttachedReadOnly("OwnerRangeControl", propertyType, propertyType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            OwnerRangeControlProperty = OwnerRangeControlPropertyKey.DependencyProperty;
            ClientProperty = DependencyProperty.Register("Client", typeof(IRangeControlClient), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnClientChanged(e.OldValue as IRangeControlClient, e.NewValue as IRangeControlClient)));
            SelectionRangeStartProperty = DependencyProperty.Register("SelectionRangeStart", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnSelectionRangeStartChanged()));
            SelectionRangeEndProperty = DependencyProperty.Register("SelectionRangeEnd", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnSelectionRangeEndChanged()));
            VisibleRangeStartProperty = DependencyProperty.Register("VisibleRangeStart", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnVisibleRangeStartChanged(e.OldValue)));
            VisibleRangeEndProperty = DependencyProperty.Register("VisibleRangeEnd", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnVisibleRangeEndChanged(e.OldValue)));
            RangeStartProperty = DependencyProperty.Register("RangeStart", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnRangeStartChanged()));
            RangeEndProperty = DependencyProperty.Register("RangeEnd", typeof(object), propertyType, new FrameworkPropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnRangeEndChanged()));
            AllowSnapToIntervalProperty = DependencyProperty.Register("AllowSnapToInterval", typeof(bool), propertyType, new FrameworkPropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnAllowSnapToIntervalChanged()));
            ShowRangeBarProperty = DependencyProperty.Register("ShowRangeBar", typeof(bool), propertyType, new FrameworkPropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowRangeBarChanged()));
            ShowRangeThumbsProperty = DependencyProperty.Register("ShowRangeThumbs", typeof(bool), propertyType, new FrameworkPropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowRangeThumbsChanged()));
            AllowImmediateRangeUpdateProperty = DependencyProperty.Register("AllowImmediateRangeUpdate", typeof(bool), propertyType, new FrameworkPropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnAllowImmediateUpdateChanged()));
            AllowScrollProperty = DependencyProperty.Register("AllowScroll", typeof(bool), propertyType, new FrameworkPropertyMetadata(true));
            AllowZoomProperty = DependencyProperty.Register("AllowZoom", typeof(bool), propertyType, new FrameworkPropertyMetadata(true));
            ShowSelectionRectangleProperty = DependencyProperty.Register("ShowSelectionRectangle", typeof(bool?), propertyType, new FrameworkPropertyMetadata(null));
            EnableAnimationProperty = DependencyProperty.Register("EnableAnimation", typeof(bool), propertyType, new FrameworkPropertyMetadata(true));
            IsSelectionMovingPropertyKey = DependencyProperty.RegisterReadOnly("IsSelectionMoving", typeof(bool), propertyType, new FrameworkPropertyMetadata(false));
            IsSelectionMovingProperty = IsSelectionMovingPropertyKey.DependencyProperty;
            ShowLabelsProperty = DependencyProperty.Register("ShowLabels", typeof(bool), propertyType, new FrameworkPropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowLabelsChanged()));
            ShowNavigationButtonsProperty = DependencyProperty.Register("ShowNavigationButtons", typeof(bool), propertyType, new FrameworkPropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowNavigationButtonsChanged()));
            LabelTemplateProperty = DependencyProperty.Register("LabelTemplate", typeof(DataTemplate), propertyType, new FrameworkPropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowLabelTemplateChanged()));
            UpdateDelayProperty = DependencyProperty.Register("UpdateDelay", typeof(int), propertyType, new FrameworkPropertyMetadata(500, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnUpdateDelayChangedChanged(), (d, o) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).CoerceUpdateDelay((int) o)));
            ShadingModeProperty = DependencyProperty.Register("ShadingMode", typeof(ShadingModes), propertyType, new FrameworkPropertyMetadata(ShadingModes.Both, (d, e) => ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShadingModeChanged()));
            SelectionRangePropertyKey = DependencyProperty.RegisterReadOnly("SelectionRange", typeof(EditRange), propertyType, new FrameworkPropertyMetadata(null));
            SelectionRangeProperty = SelectionRangePropertyKey.DependencyProperty;
            ResetRangesOnClientItemsSourceChangedProperty = DependencyProperty.Register("ResetRangesOnClientItemsSourceChanged", typeof(bool), propertyType, new FrameworkPropertyMetadata(false));
            PropertyProviderPropertyKey = DependencyProperty.RegisterAttachedReadOnly("PropertyProvider", typeof(RangeControlPropertyProvider), propertyType, new FrameworkPropertyMetadata(null));
            PropertyProviderProperty = PropertyProviderPropertyKey.DependencyProperty;
        }

        public RangeControl()
        {
            SetOwnerRangeControl(this, this);
            base.DefaultStyleKey = typeof(DevExpress.Xpf.Editors.RangeControl.RangeControl);
            base.Loaded += new RoutedEventHandler(this.RangeControlLoaded);
            this.AllowUpdateNormalizedRange = true;
            this.RangeUpdater = new StartEndUpdateHelper(this, RangeStartProperty, RangeEndProperty);
            this.VisibleRangeUpdater = new StartEndUpdateHelper(this, VisibleRangeStartProperty, VisibleRangeEndProperty);
            this.SelectionRangeUpdater = new StartEndUpdateHelper(this, SelectionRangeStartProperty, SelectionRangeEndProperty);
            this.startEndPropertyChangedAction = new PostponedAction(() => this.shouldLockUpdate);
            this.UpdateSelectionLocker = new Locker();
            base.SetValue(PropertyProviderPropertyKey, new RangeControlPropertyProvider());
        }

        protected internal void AddLogicalChild(object child)
        {
            if (!this.logicalChildren.Contains(child))
            {
                this.logicalChildren.Add(child);
                base.AddLogicalChild(child);
            }
        }

        private void AnimateDoubleTapZoom(object newVisibleStart, object newVisibleEnd)
        {
            this.Animator.CanAnimate = true;
            if (this.CanStartAnimation())
            {
                if (!this.animateLabelsLocker.IsLocked)
                {
                    this.animateLabelsLocker.Lock();
                }
                this.animator.AnimateDoubleTapZoom(this.GetNormalizedVisibleStart(), this.GetNormalizedVisibleEnd(), this.RealToNormalized(newVisibleStart), this.RealToNormalized(newVisibleEnd), (s, e) => this.SetActualVisibleRange(this.NormalizedToReal(s), this.NormalizedToReal(e), true));
            }
            else
            {
                this.SetActualVisibleRange(newVisibleStart, newVisibleEnd, true);
                this.InvalidateRender(this.CanStartAnimation());
                this.PostWithDelay(new Action(this.SetSelectionRangeByActual));
                this.PostWithDelay(new Action(this.SetVisibleRangeByActual));
                this.SyncronizeHorizontalOffset();
            }
        }

        private void AnimateScroll(double offset)
        {
            if (this.EnableAnimation)
            {
                this.Animator.AnimateScroll(this.scrollViewer.HorizontalOffset, offset, delegate (double o) {
                    this.scrollViewer.ScrollToHorizontalOffset(o);
                    this.Controller.Reset();
                });
            }
            else
            {
                this.scrollViewer.ScrollToHorizontalOffset(offset);
                this.Controller.Reset();
            }
        }

        private void CalcLeftSelection(Rect bounds, double thumbWidth, out double leftThumbPosition, out double leftSidePosition)
        {
            leftThumbPosition = this.CalcLeftThumbPosition(bounds, thumbWidth);
            leftSidePosition = this.CalcSelectionLeft(bounds) - bounds.Width;
        }

        private double CalcLeftThumbPosition(Rect bounds, double thumbWidth)
        {
            double num = -this.GetPixelVisibleStart(bounds) - thumbWidth;
            return this.ConstrainByNaN(Math.Max(num, this.CalcSelectionLeft(bounds) - (thumbWidth / 2.0)));
        }

        private void CalcRightSelection(Rect bounds, double thumbWidth, out double rightThumbPosition, out double rightSidePosition)
        {
            rightThumbPosition = this.CalcRightThumbPosition(bounds, thumbWidth);
            rightSidePosition = this.CalcSelectionRight(bounds);
        }

        private double CalcRightThumbPosition(Rect bounds, double thumbWidth)
        {
            double num = (bounds.Width + thumbWidth) - this.GetPixelVisibleStart(bounds);
            return this.ConstrainByNaN(Math.Min(num, this.CalcSelectionRight(bounds) - (thumbWidth / 2.0)));
        }

        private double CalcSelectionCenterPosition() => 
            this.GetCenterSelection() * this.Client.ClientBounds.Width;

        private double CalcSelectionEndPosition() => 
            this.NormalizedSelectionEnd * this.Client.ClientBounds.Width;

        private double CalcSelectionLeft(Rect bounds) => 
            (this.NormalizedSelectionStart - this.GetNormalizedVisibleStart()) * bounds.Width;

        private void CalcSelectionRangeDelta(double currentPosition, out bool isSelectionTypeChanged, out double rangeDelta, out double positionDelta)
        {
            currentPosition = this.ConstrainPosition(currentPosition);
            this.fixedThumb = this.draggedThumb.Equals(this.leftSelectionThumb) ? this.rightSelectionThumb : this.leftSelectionThumb;
            double normalizedValue = this.draggedThumb.Equals(this.leftSelectionThumb) ? this.NormalizedSelectionStart : this.NormalizedSelectionEnd;
            rangeDelta = currentPosition - this.RealToPixel(this.NormalizedToReal(normalizedValue));
            if (!double.IsNaN(this.prevPosition))
            {
                positionDelta = currentPosition - this.prevPosition;
            }
            else
            {
                this.prevPosition = currentPosition;
                positionDelta = currentPosition;
            }
            this.prevPosition = currentPosition;
            isSelectionTypeChanged = this.IsSelectionTypeChanged(currentPosition, positionDelta);
        }

        private void CalcSelectionRangeOnMove(double pixelDelta, out double newStart, out double newEnd)
        {
            double normalizedDelta = this.NormalizeByLength(pixelDelta.Round(false));
            newStart = this.NormalizedSelectionStart + normalizedDelta;
            newEnd = this.NormalizedSelectionEnd + normalizedDelta;
            this.ConstrainSelectionRange(ref newStart, ref newEnd, normalizedDelta);
            if (this.ConstrainSelectionRange(ref newStart, ref newEnd, normalizedDelta))
            {
                this.Controller.StopInetria();
            }
        }

        private Rect CalcSelectionRectangleBounds(Rect bounds)
        {
            double x = (this.NormalizedSelectionStart - this.GetNormalizedVisibleStart()) * bounds.Width;
            double num2 = (this.NormalizedSelectionEnd - this.GetNormalizedVisibleStart()) * bounds.Width;
            return new Rect(x, bounds.Top, num2 - x, bounds.Height);
        }

        private double CalcSelectionRight(Rect bounds) => 
            (this.NormalizedSelectionEnd - this.GetNormalizedVisibleStart()) * bounds.Width;

        private double CalcSelectionStartPosition() => 
            this.NormalizedSelectionStart * this.Client.ClientBounds.Width;

        private double[] CalcShaderBounds()
        {
            Rect clientBounds = this.Client.ClientBounds;
            double num = ((this.NormalizedSelectionStart - this.GetNormalizedVisibleStart()) * clientBounds.Width) / this.rootContainer.ActualWidth;
            double num2 = ((this.NormalizedSelectionEnd - this.GetNormalizedVisibleStart()) * clientBounds.Width) / this.rootContainer.ActualWidth;
            double num3 = clientBounds.Top / this.clientPanel.ActualHeight;
            double num4 = clientBounds.Bottom / this.clientPanel.ActualHeight;
            return new double[] { this.ConstrainNormalizedValue(num), this.ConstrainNormalizedValue(num3), this.ConstrainNormalizedValue(num2), this.ConstrainNormalizedValue(num4) };
        }

        private double CalcShadingOpacity() => 
            this.ShouldShading() ? ((double) 1) : ((double) 0);

        private bool CanAutoScrollOnSelectionMoving(object start, object end) => 
            (this.RealToNormalized(this.ActualSelectionStart) >= this.RealToNormalized(this.ActualVisibleStart)) && (this.RealToNormalized(this.ActualSelectionEnd) <= this.RealToNormalized(this.ActualVisibleEnd));

        private bool CanChangeVisibleRange(Point position)
        {
            RangeControlClientHitTestResult result = this.Client.HitTest(position);
            double oldVisibleStart = this.RealToNormalized(this.Client.VisibleStart);
            double oldVisibleEnd = this.RealToNormalized(this.Client.VisibleEnd);
            bool flag = this.Client.SetVisibleRange(result.RegionStart, result.RegionEnd, this.GetViewportSize());
            if (!this.DetectVisibleRangeWidthChange() || this.IsLastVisibleRangeEqualsCurrent(oldVisibleStart, oldVisibleEnd))
            {
                return false;
            }
            if (flag)
            {
                this.CenterSelectionInsideVisibleRange();
            }
            return true;
        }

        private bool CanGrayscale() => 
            this.Client.GrayOutNonSelectedRange && (this.ShadingMode != ShadingModes.Shading);

        private bool CanRender() => 
            this.IsRangeControlInitialized && ((this.rangeBar != null) && (this.CanRenderLeftSide() && (this.CanRenderRightSide() && this.IsClientBoundsCorrect())));

        private bool CanRenderLeftSide() => 
            (this.clientPanel != null) && ((this.leftSelectionThumb != null) && (this.leftSide != null));

        private bool CanRenderRightSide() => 
            (this.clientPanel != null) && ((this.rightSelectionThumb != null) && (this.rightSide != null));

        private bool CanSnapToInterval() => 
            this.AllowSnapToInterval || ((this.Client != null) && this.Client.SnapSelectionToGrid);

        private bool CanStartAnimation() => 
            this.EnableAnimation && this.Animator.CanAnimate;

        private void CenterSelectionInsideVisibleRange()
        {
            double num = this.RealToNormalized(this.Client.VisibleStart);
            double num2 = this.RealToNormalized(this.Client.VisibleEnd);
            double num5 = (num + ((num2 - num) / 2.0)) - (this.RealToNormalized(this.ActualSelectionStart) + ((this.RealToNormalized(this.ActualSelectionEnd) - this.RealToNormalized(this.ActualSelectionStart)) / 2.0));
            this.Client.SetVisibleRange(this.NormalizedToReal(num - num5), this.NormalizedToReal(num2 - num5), this.GetViewportSize());
        }

        private void ChangeActualSelection(double delta, bool isSelectionTypeChanged)
        {
            if (this.SelectionType == SelectionChangesType.Start)
            {
                double num = this.NormalizedSelectionStart + delta;
                this.SetNormalizedStartWithLock(isSelectionTypeChanged, this.ConstrainNormalizedValue(num));
                this.Render(false);
            }
            else
            {
                double num2 = this.NormalizedSelectionEnd + delta;
                this.SetNormalizedEndWithLock(isSelectionTypeChanged, this.ConstrainNormalizedValue(num2));
                this.Render(false);
            }
        }

        private object CoerceUpdateDelay(int baseValue) => 
            (Math.Sign(baseValue) != -1) ? baseValue : 0;

        private void CollapseRangeThumbs()
        {
            this.leftSelectionThumb.Opacity = 0.0;
            this.rightSelectionThumb.Opacity = 0.0;
        }

        private double ConstrainByNaN(double value) => 
            double.IsNaN(value) ? 0.0 : value;

        private double ConstrainNormalizedValue(double value) => 
            !double.IsNaN(value) ? Math.Max(Math.Min(value, 1.0), 0.0) : 0.0;

        private double ConstrainPosition(double currentPosition)
        {
            if (Math.Sign(currentPosition) == -1)
            {
                currentPosition = 0.0;
            }
            else if (currentPosition.Round(false) > this.ClientWidth)
            {
                currentPosition = this.ClientWidth;
            }
            return currentPosition;
        }

        private bool ConstrainSelectionRange(bool isStart)
        {
            bool flag = this.Client.SetSelectionRange(this.ActualSelectionStart, this.ActualSelectionEnd, this.GetViewportSize(), this.AllowSnapToInterval);
            if (flag)
            {
                if (isStart)
                {
                    this.ActualSelectionStart = this.Client.SelectionStart;
                }
                else
                {
                    this.ActualSelectionEnd = this.Client.SelectionEnd;
                }
            }
            return flag;
        }

        private bool ConstrainSelectionRange(ref double newStart, ref double newEnd, double normalizedDelta)
        {
            if (newStart < 0.0)
            {
                newEnd = this.NormalizedSelectionEnd + (Math.Sign(normalizedDelta) * this.NormalizedSelectionStart);
                newStart = 0.0;
                return true;
            }
            if (newEnd <= 1.0)
            {
                return false;
            }
            newStart = this.NormalizedSelectionStart + (Math.Sign(normalizedDelta) * (1.0 - this.NormalizedSelectionEnd));
            newEnd = 1.0;
            return true;
        }

        private bool ConstrainVisibleRange(bool isStart)
        {
            Size viewportSize = this.GetViewportSize();
            if (viewportSize.Width == 0.0)
            {
                return false;
            }
            bool flag = this.Client.SetVisibleRange(this.ActualVisibleStart, this.ActualVisibleEnd, viewportSize);
            if (isStart)
            {
                this.ActualVisibleStart = this.Client.VisibleStart;
            }
            else
            {
                this.ActualVisibleEnd = this.Client.VisibleEnd;
            }
            return flag;
        }

        private void ConstrainVisibleRange(ref double visibleStart, ref double visibleEnd)
        {
            if (visibleEnd > 1.0)
            {
                visibleStart = Math.Max((double) (visibleStart - Math.Abs((double) (1.0 - visibleEnd))), (double) 0.0);
                visibleEnd = 1.0;
            }
            else if (visibleStart < 0.0)
            {
                visibleEnd = Math.Min((double) (visibleEnd + Math.Abs(visibleStart)), (double) 1.0);
                visibleStart = 0.0;
            }
        }

        private GrayScaleEffect CreateShader()
        {
            GrayScaleEffect effect1 = new GrayScaleEffect();
            effect1.RFactor = 0.299;
            effect1.GFactor = 0.587;
            effect1.BFactor = 0.114;
            return effect1;
        }

        private bool DetectVisibleRangeWidthChange() => 
            !this.GetComparableRange(this.ActualVisibleStart, this.ActualVisibleEnd).AreClose(this.GetComparableRange(this.Client.VisibleStart, this.Client.VisibleEnd));

        private void EnqueueAction(Action action)
        {
            this.postponeQueue.Enqueue(action);
        }

        private void FindDraggedAndFixedThumbs(double currentPosition)
        {
            double num = this.RealToNormalized(this.PixelToReal(currentPosition));
            if (num <= this.NormalizedSelectionStart)
            {
                this.draggedThumb = this.leftSelectionThumb;
                this.fixedThumb = this.rightSelectionThumb;
                this.isResizingDetected = true;
            }
            else if (num >= this.NormalizedSelectionEnd)
            {
                this.draggedThumb = this.rightSelectionThumb;
                this.fixedThumb = this.leftSelectionThumb;
                this.isResizingDetected = true;
            }
        }

        private void FindElements()
        {
            this.UnSubscribeEvents();
            if (this.scrollViewer != null)
            {
                this.isSyncronizeHorizontalOffset = true;
            }
            this.contentControl = this.GetContentBorder();
            this.rootContainer = LayoutHelper.FindElementByName(this.contentControl.Content as FrameworkElement, "PART_RootContainer") as Grid;
            this.scrollViewer = LayoutHelper.FindElementByName(this.rootContainer, "PART_ScrollViewer") as ScrollViewer;
            this.rangeBar = LayoutHelper.FindElementByName(this.rootContainer, "PART_RangeBar") as RangeBar;
            this.selectionRectangle = LayoutHelper.FindElementByName(this.rootContainer, "PART_SelectionRectangle") as Border;
            this.selectionRactangleContainer = LayoutHelper.GetParent(this.selectionRectangle, false) as Canvas;
            this.layoutPanel = LayoutHelper.FindElementByName(this.rootContainer, "PART_LayoutPanel") as Canvas;
            this.clientPanel = LayoutHelper.FindElementByName((FrameworkElement) this.scrollViewer.Content, "PART_ClientPanel") as Canvas;
            this.interactionArea = LayoutHelper.FindElementByName((FrameworkElement) this.scrollViewer.Content, "PART_InteractionArea") as Canvas;
            this.InitializeElements();
            this.SubscribeEvents();
        }

        private object FormatText(object value) => 
            (this.Client != null) ? this.Client.FormatText(value) : string.Empty;

        private double GetActualVisibleRange() => 
            Math.Min(Math.Max((double) (this.GetNormalizedVisibleEnd() - this.GetNormalizedVisibleStart()), (double) 0.0), 1.0);

        private double GetCenterSelection() => 
            this.NormalizedSelectionStart + (this.GetSelectionRange() / 2.0);

        private object GetCenterSnappedValue(object value, bool isRight) => 
            ((this.Client == null) || !this.Client.ConvergeThumbsOnZoomingOut) ? value : this.SnapToCenter(value, isRight);

        private double GetComparableLength() => 
            this.Client.GetComparableValue(this.RangeEnd) - this.Client.GetComparableValue(this.RangeStart);

        private double GetComparableRange(object start, object end) => 
            this.Client.GetComparableValue(end) - this.Client.GetComparableValue(start);

        private ContentControl GetContentBorder() => 
            LayoutHelper.FindElementByName(this, "PART_Border") as ContentControl;

        private double GetDelay() => 
            this.AllowImmediateRangeUpdate ? ((double) 0) : ((double) this.UpdateDelay);

        private Rect GetLeftThumbBounds() => 
            TransformHelper.GetElementBounds(this.leftSelectionThumb, this.clientPanel);

        private double GetNormalizedVisibleEnd() => 
            this.RealToNormalized(this.ActualVisibleEnd);

        private double GetNormalizedVisibleStart() => 
            this.RealToNormalized(this.ActualVisibleStart);

        public static DevExpress.Xpf.Editors.RangeControl.RangeControl GetOwnerRangeControl(DependencyObject element) => 
            (element != null) ? ((DevExpress.Xpf.Editors.RangeControl.RangeControl) element.GetValue(OwnerRangeControlProperty)) : null;

        private double GetPixelVisibleStart(Rect bounds) => 
            this.GetNormalizedVisibleStart() * bounds.Width;

        public static RangeControlPropertyProvider GetPropertyProvider(DependencyObject element) => 
            (element != null) ? ((RangeControlPropertyProvider) element.GetValue(PropertyProviderProperty)) : null;

        private Rect GetRightThumbBounds() => 
            TransformHelper.GetElementBounds(this.rightSelectionThumb, this.clientPanel);

        internal FrameworkElement GetRootContainer() => 
            this.rootContainer;

        private double GetSelectionRange() => 
            this.NormalizedSelectionEnd - this.NormalizedSelectionStart;

        internal object GetSnappedValue(object value)
        {
            object snappedValue = this.Client.GetSnappedValue(value, true);
            object realValue = this.Client.GetSnappedValue(value, false);
            return ((((this.Client.GetComparableValue(value) - this.Client.GetComparableValue(snappedValue)) / (this.Client.GetComparableValue(realValue) - this.Client.GetComparableValue(snappedValue))) < 0.5) ? snappedValue : realValue);
        }

        private Size GetViewportSize()
        {
            if (this.nativeSize.Width != 0.0)
            {
                return new Size(this.scrollViewer.ActualWidth, this.scrollViewer.ActualHeight);
            }
            return new Size();
        }

        private void GoToMoveSelectionState()
        {
            this.IsSelectionMoving = true;
            if (this.IsSelectionRectangleVisible())
            {
                this.CollapseRangeThumbs();
            }
        }

        private void HideNavigationButton(Button button)
        {
            button.Visibility = Visibility.Collapsed;
        }

        internal RangeControlHitTestType HitTest(Point position)
        {
            if (this.Client == null)
            {
                return RangeControlHitTestType.None;
            }
            this.CurrentHitTest = this.Client.HitTest(position);
            return ((this.CurrentHitTest.RegionType != RangeControlClientRegionType.ItemInterval) ? RangeControlHitTestType.LabelArea : this.HitTestInsideClientBounds(position));
        }

        private RangeControlHitTestType HitTestInsideClientBounds(Point position) => 
            !this.IsInsideThumbsBounds(position) ? (!this.IsInsideSelectionArea(position) ? RangeControlHitTestType.ScrollableArea : RangeControlHitTestType.SelectionArea) : RangeControlHitTestType.ThumbsArea;

        private void Initialize()
        {
            this.Initialize(null, null);
        }

        private void Initialize(object start, object end)
        {
            this.IsRangeControlInitialized = this.InitializeStartEnd(start, end);
            if (this.IsRangeControlInitialized)
            {
                this.SetUpShaderEffect();
                this.InitializeVisibleRange();
                this.InitializeSelection();
                this.SyncronizeHorizontalOffset();
                this.Invalidate();
            }
        }

        private void InitializeElements()
        {
            if ((this.clientPanel != null) && ((this.clientPanel.Children.Count > 0) && (this.layoutPanel != null)))
            {
                this.leftSelectionThumb = LayoutHelper.FindElementByName(this.layoutPanel, "PART_SelectionLeftThumb") as Thumb;
                this.rightSelectionThumb = LayoutHelper.FindElementByName(this.layoutPanel, "PART_SelectionRightThumb") as Thumb;
                this.draggedThumb = this.leftSelectionThumb;
                this.fixedThumb = this.rightSelectionThumb;
                this.leftSide = LayoutHelper.FindElementByName(this.layoutPanel, "PART_LeftSideBorder") as Border;
                this.rightSide = LayoutHelper.FindElementByName(this.layoutPanel, "PART_RightSideBorder") as Border;
                this.content = LayoutHelper.FindElementByName(this.clientPanel, "PART_Content") as ContentPresenter;
                this.InitLabels();
                this.InitNavigationButtons();
            }
        }

        private void InitializeSelection()
        {
            object start = this.SelectionRangeStart ?? this.Client.SelectionStart;
            object obj3 = this.SelectionRangeEnd ?? this.Client.SelectionEnd;
            start = start ?? this.RangeStart;
            object end = obj3;
            if (obj3 == null)
            {
                object local4 = obj3;
                end = this.RangeEnd;
            }
            this.SetActualSelectionRangeInternal(start, end, true);
            this.UpdateNormalizedSelection();
        }

        private bool InitializeStartEnd(object newStart, object newEnd)
        {
            object rangeStart = this.RangeStart;
            object rangeEnd = this.RangeEnd;
            if (this.RangeStart == null)
            {
                rangeStart = (newStart == null) ? this.Client.Start : newStart;
                this.IsStartManualBehavior = false;
            }
            if (this.RangeEnd == null)
            {
                rangeEnd = (newEnd == null) ? this.Client.End : newEnd;
                this.IsEndManualBehavior = false;
            }
            this.SetClientRange(rangeStart, rangeEnd);
            return ((this.RangeStart != null) && (this.RangeEnd != null));
        }

        private void InitializeVisibleRange()
        {
            object newVisibleStart = (this.ActualVisibleStart == null) ? this.Client.Start : this.ActualVisibleStart;
            this.SetActualVisibleRange(newVisibleStart, (this.ActualVisibleEnd == null) ? this.Client.End : this.ActualVisibleEnd, false);
            if (this.VisibleRangeStart == null)
            {
                this.SetVisibleStartByActual();
            }
            if (this.VisibleRangeEnd == null)
            {
                this.SetVisibleEndByActual();
            }
        }

        private void InitLabels()
        {
            this.leftLabel = LayoutHelper.FindElementByName(this.rootContainer, "PART_LeftLabel") as ContentPresenter;
            this.rightLabel = LayoutHelper.FindElementByName(this.rootContainer, "PART_RightLabel") as ContentPresenter;
            this.UpdateLabelsVisibility();
        }

        private void InitNavigationButtons()
        {
            this.navigationButtonsContainer = LayoutHelper.FindElementByName(this.rootContainer, "PART_NavigationButtonsContainer") as Grid;
            this.leftNavigationButton = LayoutHelper.FindElementByName(this.layoutPanel, "PART_LeftNavigationButton") as Button;
            this.rightNavigationButton = LayoutHelper.FindElementByName(this.layoutPanel, "PART_RightNavigationButton") as Button;
            this.leftNavigationButton.Click += new RoutedEventHandler(this.OnLeftNavigationButtonClick);
            this.rightNavigationButton.Click += new RoutedEventHandler(this.OnRightNavigationButtonClick);
            this.UpdateNavigationButtonsVisibility();
        }

        private void Invalidate()
        {
            this.InvalidateRender(false);
        }

        private void InvalidateClient()
        {
            this.Client.Invalidate(this.GetViewportSize());
        }

        private void InvalidateRender(bool isAnimate)
        {
            if (!this.CanRender())
            {
                this.RenderDefault();
            }
            else
            {
                this.Render(isAnimate);
            }
        }

        private bool IsClientBoundsCorrect() => 
            (this.Client != null) && (this.Client.ClientBounds.Width > 0.0);

        internal bool IsInsideSelectionArea(Point position)
        {
            double num = this.NormalizedSelectionStart * this.ClientWidth;
            double num2 = this.NormalizedSelectionEnd * this.ClientWidth;
            return (this.Client.ClientBounds.Contains(position) && ((position.X >= num) && (position.X <= num2)));
        }

        private bool IsInsideThumbsBounds(Point position) => 
            this.GetLeftThumbBounds().Contains(position) || this.GetRightThumbBounds().Contains(position);

        private bool IsLabelsVisible() => 
            this.ShowLabels && ((this.Client != null) ? this.Client.AllowThumbs : true);

        private bool IsLastVisibleRangeEqualsCurrent(double oldVisibleStart, double oldVisibleEnd) => 
            oldVisibleStart.AreClose(this.RealToNormalized(this.Client.VisibleStart)) && oldVisibleEnd.AreClose(this.RealToNormalized(this.Client.VisibleEnd));

        private bool IsLeftNavigationButtonsVisible() => 
            this.ShowNavigationButtons && this.IsSelectionStartLessVisibleStart(!this.IsAutoScrollInProcess);

        private bool IsLeftThumbDragged(Point position, double dragDelta)
        {
            Rect leftThumbBounds = this.GetLeftThumbBounds();
            Rect rightThumbBounds = this.GetRightThumbBounds();
            return (Math.Abs((double) (TransformHelper.GetElementCenter(this.leftSelectionThumb, this.clientPanel) - position.X)) < Math.Abs((double) (TransformHelper.GetElementCenter(this.rightSelectionThumb, this.clientPanel) - position.X)));
        }

        internal bool IsOutsideClientBounds(Point point) => 
            (this.Client != null) ? !this.Client.ClientBounds.Contains(point) : false;

        internal bool IsPositionOutOfBounds(double position) => 
            (position < 0.0) || (position > this.layoutPanel.ActualWidth);

        private bool IsRightNavigationButtonsVisible() => 
            this.ShowNavigationButtons && this.IsSelectionEndGreaterVisibleEnd(!this.IsAutoScrollInProcess);

        private bool IsSelectionEndGreaterVisibleEnd(bool isStrong) => 
            isStrong ? this.NormalizedSelectionEnd.GreaterThan(this.GetNormalizedVisibleEnd()) : (this.NormalizedSelectionEnd + this.precision).GreaterThanOrClose(this.GetNormalizedVisibleEnd());

        private bool IsSelectionRangeGreaterVisibleRange() => 
            this.GetSelectionRange() > this.GetActualVisibleRange();

        private bool IsSelectionRectangleVisible() => 
            !this.IsSelectionMoving ? false : (((this.ShowSelectionRectangle != null) || this.Client.AllowThumbs) ? ((this.ShowSelectionRectangle == null) ? false : this.ShowSelectionRectangle.Value) : true);

        private bool IsSelectionStartLessVisibleStart(bool isStrong) => 
            isStrong ? this.NormalizedSelectionStart.LessThan(this.GetNormalizedVisibleStart()) : (this.NormalizedSelectionStart - this.precision).LessThanOrClose(this.GetNormalizedVisibleStart());

        private bool IsSelectionTypeChanged(double currentPosition)
        {
            double elementCenter = TransformHelper.GetElementCenter(this.fixedThumb, this.clientPanel);
            SelectionChangesType type = (currentPosition >= elementCenter) ? SelectionChangesType.End : SelectionChangesType.Start;
            bool flag = this.SelectionType != type;
            this.SelectionType = type;
            return flag;
        }

        private bool IsSelectionTypeChanged(double currentPosition, double delta)
        {
            double elementCenter = TransformHelper.GetElementCenter(this.fixedThumb, this.clientPanel);
            SelectionChangesType selectionType = this.SelectionType;
            if (ReferenceEquals(this.draggedThumb, this.rightSelectionThumb) && ((Math.Sign(delta) == -1) && (currentPosition < elementCenter)))
            {
                selectionType = SelectionChangesType.Start;
            }
            if (ReferenceEquals(this.draggedThumb, this.leftSelectionThumb) && ((Math.Sign(delta) == 1) && (currentPosition >= elementCenter)))
            {
                selectionType = SelectionChangesType.End;
            }
            bool flag = this.SelectionType != selectionType;
            this.SelectionType = selectionType;
            return flag;
        }

        private bool IsThumbsVisible() => 
            this.ShowRangeThumbs && ((this.Client != null) ? this.Client.AllowThumbs : true);

        private bool IsViewPortValid()
        {
            Size viewportSize = this.GetViewportSize();
            return ((viewportSize.Width != 0.0) && !(viewportSize.Height == 0.0));
        }

        internal bool IsWholeRangeVisible() => 
            (this.Client.GetComparableValue(this.ActualVisibleStart) == this.Client.GetComparableValue(this.RangeStart)) && (this.Client.GetComparableValue(this.ActualVisibleEnd) == this.Client.GetComparableValue(this.RangeEnd));

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();
            if (double.IsInfinity(constraint.Width) || double.IsInfinity(constraint.Height))
            {
                if (this.rightNavigationButton != null)
                {
                    Visibility visibility = this.rightNavigationButton.Visibility;
                    this.rightNavigationButton.Visibility = Visibility.Visible;
                    this.rightNavigationButton.Measure(constraint);
                    size = new Size(this.rightNavigationButton.DesiredSize.Width * 2.0, this.rightNavigationButton.DesiredSize.Height);
                    this.rightNavigationButton.Visibility = visibility;
                }
                if (this.ShowRangeBar && (this.rangeBar != null))
                {
                    this.rangeBar.Measure(constraint);
                    double height = this.rangeBar.DesiredSize.Height + size.Height;
                    size = new Size(size.Width, height);
                }
            }
            double width = double.IsInfinity(constraint.Width) ? size.Width : constraint.Width;
            Size availableSize = new Size(width, double.IsInfinity(constraint.Height) ? size.Height : constraint.Height);
            if (this.contentControl != null)
            {
                this.contentControl.Measure(availableSize);
            }
            return availableSize;
        }

        internal void MoveSelection(double pixelDelta)
        {
            if (this.Client != null)
            {
                double num;
                double num2;
                this.CalcSelectionRangeOnMove(pixelDelta, out num, out num2);
                this.SetNormalizedRange(num, num2);
            }
        }

        private void MoveToActualVisibleEnd()
        {
            double autoScrollDelta = this.AutoScrollDelta;
            this.ProcessMove(autoScrollDelta);
        }

        private void MoveToActualVisibleStart()
        {
            double scrollDelta = -this.AutoScrollDelta;
            this.ProcessMove(scrollDelta);
        }

        internal double NormalizeByLength(double value)
        {
            double elementWidth = TransformHelper.GetElementWidth(this.clientPanel);
            return (((elementWidth == 0.0) || (this.GetActualVisibleRange() <= 0.0)) ? 0.0 : (value / elementWidth));
        }

        internal object NormalizedToReal(double normalizedValue)
        {
            if (this.Client == null)
            {
                return null;
            }
            double comparable = (this.ConstrainNormalizedValue(normalizedValue) * this.GetComparableLength()) + this.Client.GetComparableValue(this.RangeStart);
            return this.Client.GetRealValue(comparable);
        }

        private void OnActualSelectionEndChanged()
        {
            if (this.Client.GetComparableValue(this.Client.SelectionEnd) != this.Client.GetComparableValue(this.ActualSelectionEnd))
            {
                this.ConstrainSelectionRange(false);
            }
            this.UpdateInvertThumbRight();
            this.UpdateNormalizedSelectionEnd();
        }

        private void OnActualSelectionStartChanged()
        {
            if (this.Client.GetComparableValue(this.Client.SelectionStart) != this.Client.GetComparableValue(this.ActualSelectionStart))
            {
                this.ConstrainSelectionRange(true);
            }
            this.UpdateInvertThumbLeft();
            this.UpdateNormalizedSelectionStart();
        }

        private void OnActualVisibleEndChanged()
        {
            if (this.Client.GetComparableValue(this.Client.VisibleEnd) != this.Client.GetComparableValue(this.ActualVisibleEnd))
            {
                this.ConstrainVisibleRange(false);
            }
            this.OnActualVisibleRangeChanged();
        }

        protected virtual void OnActualVisibleRangeChanged()
        {
            if (!this.renderLocker.IsLocked)
            {
                this.SetClientPanelWidth();
                this.SyncronizeHorizontalOffset();
                this.Invalidate();
            }
        }

        private void OnActualVisibleStartChanged()
        {
            if (this.Client.GetComparableValue(this.Client.VisibleStart) != this.Client.GetComparableValue(this.ActualVisibleStart))
            {
                this.ConstrainVisibleRange(true);
            }
            this.OnActualVisibleRangeChanged();
        }

        protected virtual void OnAllowImmediateUpdateChanged()
        {
        }

        protected virtual void OnAllowSnapToIntervalChanged()
        {
            if (this.CanSnapToInterval())
            {
                object centerSnappedValue = this.GetCenterSnappedValue(this.ActualSelectionStart, false);
                this.SetActualSelectionRangeInternal(centerSnappedValue, this.GetCenterSnappedValue(this.ActualSelectionEnd, true), false);
                this.Invalidate();
            }
        }

        private void OnAnimationCompleted(object sender, AnimationEventArgs e)
        {
            this.animateLabelsLocker.Unlock();
            if (e.AnimationType == AnimationTypes.Zoom)
            {
                this.PostWithDelay(new Action(this.SetSelectionRangeByActual));
                this.PostWithDelay(new Action(this.SetVisibleRangeByActual));
                this.SyncronizeHorizontalOffset();
            }
            else if (e.AnimationType == AnimationTypes.Label)
            {
                ContentPresenter target = e.Target as ContentPresenter;
                double num = ReferenceEquals(target, this.leftLabel) ? this.LeftLabelLeft : this.RightLabelLeft;
                target.SetValue(Canvas.LeftProperty, num);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.FindElements();
            base.InvalidateMeasure();
            this.rangeBar.Owner = this;
            this.Controller = new RangeControlController(this.clientPanel, this, LayoutHelper.FindElementByName(this.rootContainer, "PART_ClientPanelArea") as Grid);
            this.SetUpShaderEffect();
            this.Invalidate();
        }

        protected virtual void OnClientChanged(IRangeControlClient oldClient, IRangeControlClient newClient)
        {
            if (oldClient != null)
            {
                oldClient.LayoutChanged -= new EventHandler<LayoutChangedEventArgs>(this.OnClientLayoutChanged);
                this.RemoveLogicalChild(oldClient);
            }
            if (newClient != null)
            {
                this.AddLogicalChild(newClient);
            }
            this.IsRangeControlInitialized = false;
            if (newClient != null)
            {
                this.UpdatePropertiesOnClientChanged();
                if (this.IsViewPortValid())
                {
                    this.Initialize();
                }
            }
        }

        private void OnClientLayoutChanged(object sender, LayoutChangedEventArgs e)
        {
            if ((e != null) && this.IsViewPortValid())
            {
                if (!this.IsRangeControlInitialized)
                {
                    this.Initialize(e.Start, e.End);
                }
                else
                {
                    this.PerformPostpone();
                    if (e.ChangeType != LayoutChangedType.Data)
                    {
                        this.SyncronizeVisibleRange();
                    }
                    else
                    {
                        this.Animator.StopAnimation();
                        this.SyncronizeWithClient(e.Start, e.End, true);
                    }
                    this.InvalidateClient();
                }
            }
        }

        internal void OnControllerReset(RangeControlStateType state)
        {
            this.UpdateByControllerState(state);
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.shouldLockUpdate = false;
            using (this.updateRangeLocker.Lock())
            {
                this.startEndPropertyChangedAction.PerformForce(() => this.RangeUpdater.Update<double>(StartEndUpdateSource.ISupportInitialize));
                this.startEndPropertyChangedAction.PerformForce(() => this.VisibleRangeUpdater.Update<double>(StartEndUpdateSource.ISupportInitialize));
                this.startEndPropertyChangedAction.PerformForce(() => this.SelectionRangeUpdater.Update<double>(StartEndUpdateSource.ISupportInitialize));
            }
            base.OnInitialized(e);
        }

        private void OnLabelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.CanRender())
            {
                this.RenderDefault();
            }
            else
            {
                this.RenderLabels(false);
            }
        }

        internal void OnLabelTapped(Point point)
        {
            if (this.Client != null)
            {
                this.Client.HitTest(point);
            }
        }

        private void OnLeftNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            this.Controller.StopInetria();
            double offset = this.IsSelectionRangeGreaterVisibleRange() ? (this.CalcSelectionStartPosition() - 15.0) : (this.CalcSelectionCenterPosition() - (this.GetViewportSize().Width / 2.0));
            this.HideNavigationButton(this.leftNavigationButton);
            this.AnimateScroll(offset);
        }

        private void OnNormalizedRangeChanged()
        {
            this.IsNormalizedRangeSnappedToCenter = false;
            if (!this.renderLocker.IsLocked)
            {
                this.InvalidateRender(this.CanStartAnimation());
            }
        }

        internal void OnRangeBarSliderMoved(double delta)
        {
            if ((this.scrollViewer != null) && this.AllowScroll)
            {
                this.scrollViewer.ScrollToHorizontalOffset(this.scrollViewer.HorizontalOffset + (delta * this.scrollViewer.ExtentWidth));
            }
        }

        internal void OnRangeBarViewPortChanged()
        {
            if (this.AllowZoom)
            {
                this.PostWithDelay(new Action(this.SetVisibleRangeByActual));
            }
        }

        internal void OnRangeBarViewPortResizing(double newStart, double newEnd, bool changeOffset)
        {
            if ((this.scrollViewer != null) && this.AllowZoom)
            {
                this.UpdateVisibleRange(this.NormalizedToReal(newStart), this.NormalizedToReal(newEnd), true);
                this.PostponeSnapNormalizedRangeToCenter();
            }
        }

        protected virtual void OnRangeEndChanged()
        {
            this.IsEndManualBehavior = true;
            if ((this.Client != null) && !this.updateRangeLocker)
            {
                this.RangeUpdater.EndValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.RangeEnd), this.RangeEnd, false);
                using (this.updateRangeLocker.Lock())
                {
                    this.startEndPropertyChangedAction.PerformPostpone(() => this.RangeUpdater.Update<double>(StartEndUpdateSource.EndChanged));
                }
                if (!this.shouldLockUpdate)
                {
                    this.StartEndChanged();
                }
            }
        }

        protected virtual void OnRangeStartChanged()
        {
            this.IsStartManualBehavior = true;
            if ((this.Client != null) && !this.updateRangeLocker)
            {
                this.RangeUpdater.StartValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.RangeStart), this.RangeStart, false);
                using (this.updateRangeLocker.Lock())
                {
                    this.startEndPropertyChangedAction.PerformPostpone(() => this.RangeUpdater.Update<double>(StartEndUpdateSource.StartChanged));
                }
                if (!this.shouldLockUpdate)
                {
                    this.StartEndChanged();
                }
            }
        }

        private void OnRightNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            this.Controller.StopInetria();
            double offset = this.IsSelectionRangeGreaterVisibleRange() ? ((this.CalcSelectionEndPosition() - this.GetViewportSize().Width) + 15.0) : (this.CalcSelectionCenterPosition() - (this.GetViewportSize().Width / 2.0));
            this.HideNavigationButton(this.rightNavigationButton);
            this.AnimateScroll(offset);
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if ((this.scrollViewer.ExtentWidth != 0.0) && this.IsRangeControlInitialized)
            {
                if (this.isSyncronizeHorizontalOffset)
                {
                    this.isSyncronizeHorizontalOffset = false;
                    this.SyncronizeHorizontalOffset();
                    this.InvalidateClient();
                    this.Invalidate();
                }
                else
                {
                    if (this.IsAutoScrollInProcess)
                    {
                        this.UpdateNavigationButtonsVisibility();
                    }
                    object newStart = this.NormalizedToReal(this.scrollViewer.HorizontalOffset / this.scrollViewer.ExtentWidth);
                    object newEnd = this.NormalizedToReal((this.scrollViewer.HorizontalOffset + this.scrollViewer.ViewportWidth) / this.scrollViewer.ExtentWidth);
                    if (!newStart.Equals(this.ActualVisibleStart))
                    {
                        this.UpdateVisibleRange(newStart, newEnd, false);
                        this.InvalidateClient();
                        this.Invalidate();
                    }
                }
            }
        }

        private void OnSelectionRangeChangedByController(bool isAnimate)
        {
            this.AllowUpdateNormalizedRange = true;
            this.Animator.CanAnimate = isAnimate;
            if (this.IsSelectionRectangleVisible())
            {
                this.UpdateThumbVisibility();
            }
            this.IsSelectionMoving = false;
            this.UpdateSelectionRectangleVisibility();
            this.SetNormalizedRangeInternal(this.RealToNormalized(this.ActualSelectionStart), this.RealToNormalized(this.ActualSelectionEnd));
            this.InvalidateRender(this.CanStartAnimation());
            this.PostWithDelay(new Action(this.SetSelectionRangeByActual));
            this.Animator.CanAnimate = false;
        }

        protected virtual void OnSelectionRangeEndChanged()
        {
            if (!this.updateRangeLocker)
            {
                if (this.Client != null)
                {
                    this.SelectionRangeUpdater.EndValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.SelectionRangeEnd), this.SelectionRangeEnd, false);
                    using (this.updateRangeLocker.Lock())
                    {
                        this.startEndPropertyChangedAction.PerformPostpone(() => this.SelectionRangeUpdater.Update<double>(StartEndUpdateSource.EndChanged));
                    }
                    if (this.shouldLockUpdate)
                    {
                        return;
                    }
                }
                this.SnapAndUpdateActualSelectionEnd();
            }
        }

        protected virtual void OnSelectionRangeStartChanged()
        {
            if (!this.updateRangeLocker)
            {
                if (this.Client != null)
                {
                    this.SelectionRangeUpdater.StartValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.SelectionRangeStart), this.SelectionRangeStart, false);
                    using (this.updateRangeLocker.Lock())
                    {
                        this.startEndPropertyChangedAction.PerformPostpone(() => this.SelectionRangeUpdater.Update<double>(StartEndUpdateSource.StartChanged));
                    }
                    if (this.shouldLockUpdate)
                    {
                        return;
                    }
                }
                this.SnapAndUpdateActualSelectionStart();
            }
        }

        private void OnSelectionResizingCompleted()
        {
            this.AllowUpdateNormalizedRange = true;
            if (this.CanSnapToInterval())
            {
                this.Animator.CanAnimate = true;
            }
            this.SetNormalizedRangeInternal(this.RealToNormalized(this.ActualSelectionStart), this.RealToNormalized(this.ActualSelectionEnd));
            this.InvalidateRender(this.CanStartAnimation());
            this.PostWithDelay(new Action(this.SetSelectionRangeByActual));
            this.Animator.CanAnimate = false;
        }

        private void OnSelectionResizingStarted()
        {
            this.AllowUpdateNormalizedRange = false;
            this.prevPosition = double.NaN;
            object start = this.NormalizedSelectionStart.AreClose(this.RealToNormalized(this.ActualSelectionStart)) ? this.ActualSelectionStart : this.NormalizedToReal(this.NormalizedSelectionStart);
            this.SetActualSelectionRangeInternal(start, this.NormalizedSelectionEnd.AreClose(this.RealToNormalized(this.ActualSelectionEnd)) ? this.ActualSelectionEnd : this.NormalizedToReal(this.NormalizedSelectionEnd), false);
        }

        private void OnShadingModeChanged()
        {
            this.Invalidate();
        }

        private void OnShowLabelsChanged()
        {
            this.UpdateLabelsVisibility();
            this.Invalidate();
        }

        private void OnShowLabelTemplateChanged()
        {
            this.Invalidate();
        }

        private void OnShowNavigationButtonsChanged()
        {
            this.UpdateNavigationButtonsVisibility();
            this.Invalidate();
        }

        protected virtual void OnShowRangeBarChanged()
        {
            base.InvalidateArrange();
        }

        protected virtual void OnShowRangeThumbsChanged()
        {
            this.UpdateThumbVisibility();
        }

        private void OnUpdateDelayChangedChanged()
        {
        }

        protected virtual void OnVisibleRangeEndChanged(object oldValue)
        {
            if (!this.updateRangeLocker)
            {
                if (this.Client != null)
                {
                    this.VisibleRangeUpdater.EndValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.VisibleRangeEnd), this.VisibleRangeEnd, false);
                    using (this.updateRangeLocker.Lock())
                    {
                        this.startEndPropertyChangedAction.PerformPostpone(() => this.VisibleRangeUpdater.Update<double>(StartEndUpdateSource.EndChanged));
                    }
                    if (this.shouldLockUpdate)
                    {
                        return;
                    }
                }
                this.ActualVisibleEnd = this.VisibleRangeEnd;
                if (this.SelectionRangeEnd != null)
                {
                    this.SnapAndUpdateActualSelectionEnd();
                }
            }
        }

        protected virtual void OnVisibleRangeStartChanged(object oldValue)
        {
            if (!this.updateRangeLocker)
            {
                if (this.Client != null)
                {
                    this.VisibleRangeUpdater.StartValue = new IComparableObjectWrapper(this.Client.GetComparableValue(this.VisibleRangeStart), this.VisibleRangeStart, false);
                    using (this.updateRangeLocker.Lock())
                    {
                        this.startEndPropertyChangedAction.PerformPostpone(() => this.VisibleRangeUpdater.Update<double>(StartEndUpdateSource.StartChanged));
                    }
                    if (this.shouldLockUpdate)
                    {
                        return;
                    }
                }
                this.ActualVisibleStart = this.VisibleRangeStart;
                if (this.SelectionRangeStart != null)
                {
                    this.SnapAndUpdateActualSelectionStart();
                }
            }
        }

        private void PerformPostpone()
        {
            while (this.postponeQueue.Count != 0)
            {
                Action action = this.postponeQueue.Dequeue();
                action();
            }
        }

        private object PixelToReal(double currentPosition)
        {
            double normalizedValue = currentPosition / this.Client.ClientBounds.Width;
            return this.NormalizedToReal(normalizedValue);
        }

        private void PostponeSnapNormalizedRangeToCenter()
        {
            this.EnqueueAction(new Action(this.SnapNormalizedRangeToCenter));
        }

        private void PostWithDelay(Action action)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(this.GetDelay());
            DispatcherTimer timer = timer1;
            timer.Tick += delegate (object s, EventArgs e) {
                ((DispatcherTimer) s).Stop();
                if (this.StopPosting)
                {
                    this.StopPosting = false;
                }
                else
                {
                    action();
                }
            };
            timer.Start();
        }

        internal void PrepareMoveSelection()
        {
            this.GoToMoveSelectionState();
            this.IsSelectionRangeLessVisibleRange = (this.RealToNormalized(this.ActualVisibleEnd) - this.RealToNormalized(this.ActualVisibleStart)).GreaterThanOrClose(this.NormalizedSelectionEnd - this.NormalizedSelectionStart);
            this.AllowUpdateNormalizedRange = false;
            this.Invalidate();
        }

        internal void PrepareResizeSelection()
        {
            this.InvalidateRender(this.CanStartAnimation());
            this.Animator.CanAnimate = false;
            this.AllowUpdateNormalizedRange = false;
            this.isResizingDetected = false;
            this.OnSelectionResizingStarted();
        }

        internal void PrepareZoom()
        {
            this.LastVisibleRangeWidth = 0.0;
        }

        private void ProccesAutoScroll(double scrollDelta, double normalizedValue)
        {
            bool flag;
            this.ProcessScroll(scrollDelta, out flag);
            if (this.SelectionType == SelectionChangesType.Start)
            {
                this.SetNormalizedStartWithLock(flag, normalizedValue);
            }
            else
            {
                this.SetNormalizedEndWithLock(flag, normalizedValue);
            }
        }

        private void ProccesMoveCore(double scrollDelta, double start, double end)
        {
            this.ProcessScroll(scrollDelta);
            this.SetNormalizedRangeInternal(start, end);
            this.SetActualSelectionStart();
            this.SetActualSelectionEnd();
        }

        private void ProcessMove(double scrollDelta)
        {
            if ((this.NormalizedSelectionStart != 0.0) && (this.NormalizedSelectionEnd != 1.0))
            {
                double newStart = this.NormalizedSelectionStart + scrollDelta;
                double newEnd = this.NormalizedSelectionEnd + scrollDelta;
                this.ConstrainSelectionRange(ref newStart, ref newEnd, scrollDelta);
                this.ProccesMoveCore(scrollDelta, newStart, newEnd);
            }
        }

        private void ProcessScroll(double scrollDelta)
        {
            bool flag;
            this.ProcessScroll(scrollDelta, out flag);
        }

        private void ProcessScroll(double scrollDelta, out bool isSelectionTypeChanged)
        {
            isSelectionTypeChanged = false;
            double offset = this.scrollViewer.HorizontalOffset + (scrollDelta * this.scrollViewer.ExtentWidth);
            if ((offset >= 0.0) && (offset <= this.scrollViewer.ExtentWidth))
            {
                isSelectionTypeChanged = this.IsSelectionTypeChanged(Mouse.GetPosition(this.clientPanel).X);
                this.scrollViewer.ScrollToHorizontalOffset(offset);
            }
            else
            {
                this.autoScrollTimer.Stop();
                this.autoScrollTimer = null;
            }
        }

        internal void ProcessSelectionResizing(double currentPosition)
        {
            bool flag;
            double num;
            double num2;
            this.CalcSelectionRangeDelta(currentPosition, out flag, out num, out num2);
            if (Math.Sign(num2) == Math.Sign(num))
            {
                this.ChangeActualSelection(this.NormalizeByLength(num.Round(false)), flag);
            }
        }

        private bool ProcessZoom(double position, double factor)
        {
            if (!this.AllowZoom || (!this.HasVisibleRangeWidthChange && (Math.Sign((double) (factor - 1.0)) == this.lastScaleFactorSign)))
            {
                return false;
            }
            this.lastScaleFactorSign = Math.Sign((double) (factor - 1.0));
            double num = this.NormalizeByLength(position);
            double num2 = this.GetActualVisibleRange() * factor;
            double visibleStart = num - (((num - this.GetNormalizedVisibleStart()) / this.GetActualVisibleRange()) * num2);
            double visibleEnd = num + (((this.GetNormalizedVisibleEnd() - num) / this.GetActualVisibleRange()) * num2);
            this.ConstrainVisibleRange(ref visibleStart, ref visibleEnd);
            this.LastVisibleRangeWidth = this.GetActualVisibleRange();
            if ((visibleEnd - visibleStart).AreClose(this.LastVisibleRangeWidth))
            {
                return false;
            }
            this.UpdateVisibleRange(this.NormalizedToReal(visibleStart), this.NormalizedToReal(visibleEnd), true);
            return true;
        }

        private void RangeControlLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateVisibility();
            this.Invalidate();
        }

        internal double RealToNormalized(object value) => 
            (value != null) ? this.ConstrainNormalizedValue((this.Client.GetComparableValue(value) - this.Client.GetComparableValue(this.RangeStart)) / this.GetComparableLength()) : 0.0;

        private double RealToPixel(object realValue) => 
            this.RealToNormalized(realValue) * this.ClientWidth;

        private void ReassignThumbs()
        {
            Thumb draggedThumb = this.draggedThumb;
            this.draggedThumb = this.fixedThumb;
            this.fixedThumb = draggedThumb;
        }

        protected internal void RemoveLogicalChild(object child)
        {
            if (this.logicalChildren.Contains(child))
            {
                this.logicalChildren.Remove(child);
                base.RemoveLogicalChild(child);
            }
        }

        private void Render(bool isAnimate)
        {
            if (!this.renderLocker.IsLocked)
            {
                this.RenderLeftSelectionSide(this.Client.ClientBounds, isAnimate);
                this.RenderRightSelectionSide(this.Client.ClientBounds, isAnimate);
                this.layoutPanel.ClipToBounds();
                this.RenderSelectionRectangle(this.Client.ClientBounds);
                this.RenderLabels(isAnimate);
                this.RenderNavigationButtons();
                this.RenderShader();
                this.RenderRangeBar();
            }
        }

        private void RenderDefault()
        {
            if ((this.leftSide != null) && ((this.leftSelectionThumb != null) && (this.rightSelectionThumb != null)))
            {
                this.RenderSelectionDefault();
            }
        }

        private void RenderLabels(bool isAnimate)
        {
            if (this.IsLabelsVisible())
            {
                bool flag;
                bool flag2;
                this.SetLabelsText();
                Thickness margin = this.leftLabel.Margin;
                Thickness thickness2 = this.rightLabel.Margin;
                this.RenderLabelsCore(isAnimate, out flag, out flag2);
                this.IsLastLeftThumbOutOfBounds = flag;
                this.IsLastRightThumbOutOfBounds = flag2;
            }
        }

        private void RenderLabelsCore(bool isAnimate, out bool isLeftThumbOutOfBounds, out bool isRightThumbOutOfBounds)
        {
            double actualWidth = this.leftSelectionThumb.ActualWidth;
            Rect clientBounds = this.Client.ClientBounds;
            double num2 = this.CalcLeftThumbPosition(clientBounds, actualWidth) + (actualWidth / 2.0);
            double num3 = (this.CalcRightThumbPosition(clientBounds, actualWidth) + (actualWidth / 2.0)) - this.rightLabel.DesiredSize.Width;
            this.LeftLabelLeft = num2 - this.leftLabel.DesiredSize.Width;
            this.RightLabelLeft = num3 + this.rightLabel.DesiredSize.Width;
            isLeftThumbOutOfBounds = (this.Client.GetComparableValue(this.VisibleRangeStart) == this.Client.GetComparableValue(this.RangeStart)) && (this.LeftLabelLeft < 0.0);
            isRightThumbOutOfBounds = (this.Client.GetComparableValue(this.VisibleRangeEnd) == this.Client.GetComparableValue(this.RangeEnd)) && ((this.RightLabelLeft + this.rightLabel.ActualWidth) > base.ActualWidth);
            this.leftLabel.Height = this.rightLabel.Height = this.Client.ClientBounds.Height;
            this.leftLabel.SetValue(Canvas.TopProperty, this.Client.ClientBounds.Top);
            this.rightLabel.SetValue(Canvas.TopProperty, this.Client.ClientBounds.Top);
            if (isLeftThumbOutOfBounds && !this.animateLabelsLocker.IsLocked)
            {
                this.LeftLabelLeft += this.leftLabel.DesiredSize.Width;
            }
            if (isRightThumbOutOfBounds && !this.animateLabelsLocker.IsLocked)
            {
                this.RightLabelLeft -= this.rightLabel.DesiredSize.Width;
            }
            if (isAnimate && !this.animateLabelsLocker.IsLocked)
            {
                this.Animator.AnimateLabel(this.leftLabel, this.ConstrainByNaN(this.LeftLabelLeft));
                this.Animator.AnimateLabel(this.rightLabel, this.ConstrainByNaN(this.RightLabelLeft));
            }
            else
            {
                if ((isLeftThumbOutOfBounds != this.IsLastLeftThumbOutOfBounds) && !this.animateLabelsLocker.IsLocked)
                {
                    this.Animator.AnimateLabel(this.leftLabel, this.ConstrainByNaN(this.LeftLabelLeft));
                }
                else
                {
                    this.leftLabel.SetValue(Canvas.LeftProperty, this.LeftLabelLeft);
                }
                if ((isRightThumbOutOfBounds != this.IsLastRightThumbOutOfBounds) && !this.animateLabelsLocker.IsLocked)
                {
                    this.Animator.AnimateLabel(this.rightLabel, this.ConstrainByNaN(this.RightLabelLeft));
                }
                else
                {
                    this.rightLabel.SetValue(Canvas.LeftProperty, this.RightLabelLeft);
                }
            }
        }

        private void RenderLeftSelectionSide(Rect bounds, bool isAnimate)
        {
            double num;
            double num2;
            this.CalcLeftSelection(bounds, TransformHelper.GetElementWidth(this.leftSelectionThumb), out num, out num2);
            this.leftSelectionThumb.SetValue(Canvas.TopProperty, bounds.Top);
            this.leftSide.SetValue(Canvas.TopProperty, bounds.Top);
            this.leftSide.Width = bounds.Width;
            this.leftSide.Visibility = this.ShouldShading() ? Visibility.Visible : Visibility.Collapsed;
            this.leftSelectionThumb.Height = this.leftSide.Height = (this.ActualSelectionStart != null) ? bounds.Height : 0.0;
            if (isAnimate)
            {
                this.Animator.AnimateSelection(this.leftSelectionThumb, this.leftSide, num2, num, this);
            }
            else
            {
                this.leftSelectionThumb.SetValue(Canvas.LeftProperty, num);
                this.leftSide.SetValue(Canvas.LeftProperty, num2);
            }
        }

        private void RenderNavigationButtons()
        {
            if (!this.IsAutoScrollInProcess)
            {
                this.UpdateNavigationButtonsVisibility();
            }
            this.navigationButtonsContainer.SetValue(Canvas.TopProperty, this.Client.ClientBounds.Top);
            this.navigationButtonsContainer.Height = this.Client.ClientBounds.Height;
            this.navigationButtonsContainer.Width = this.layoutPanel.ActualWidth;
        }

        private void RenderRangeBar()
        {
            if (this.rangeBar != null)
            {
                this.rangeBar.ViewPortStart = this.GetNormalizedVisibleStart();
                this.rangeBar.ViewPortEnd = this.GetNormalizedVisibleEnd();
                this.rangeBar.Minimum = this.IsNormalizedRangeSnappedToCenter ? this.RealToNormalized(this.ActualSelectionStart) : this.NormalizedSelectionStart;
                this.rangeBar.Maximum = this.IsNormalizedRangeSnappedToCenter ? this.RealToNormalized(this.ActualSelectionEnd) : this.NormalizedSelectionEnd;
                if (this.ShowRangeBar)
                {
                    this.rangeBar.Invalidate();
                }
            }
        }

        private void RenderRightSelectionSide(Rect bounds, bool isAnimate)
        {
            double num;
            double num2;
            this.CalcRightSelection(bounds, TransformHelper.GetElementWidth(this.rightSelectionThumb), out num, out num2);
            this.rightSelectionThumb.SetValue(Canvas.TopProperty, bounds.Top);
            this.rightSide.SetValue(Canvas.TopProperty, bounds.Top);
            this.rightSide.Width = bounds.Width;
            this.rightSide.Visibility = this.ShouldShading() ? Visibility.Visible : Visibility.Collapsed;
            this.rightSide.Height = this.rightSelectionThumb.Height = (this.ActualSelectionStart != null) ? bounds.Height : 0.0;
            if (isAnimate)
            {
                this.Animator.AnimateSelection(this.rightSelectionThumb, this.rightSide, num2, num, this);
            }
            else
            {
                this.rightSelectionThumb.SetValue(Canvas.LeftProperty, num);
                this.rightSide.SetValue(Canvas.LeftProperty, num2);
            }
        }

        private void RenderSelectionDefault()
        {
            double num2;
            double num3;
            this.leftSide.Width = this.nativeSize.Width;
            this.leftSide.Height = this.clientPanel.ActualHeight;
            this.leftSide.SetValue(Canvas.LeftProperty, 0.0);
            this.leftLabel.SetValue(Canvas.LeftProperty, 0.0);
            this.rightLabel.SetValue(Canvas.LeftProperty, 0.0);
            this.leftSelectionThumb.Height = num3 = 0.0;
            this.rightSelectionThumb.Height = num2 = num3;
            this.leftLabel.Height = this.rightLabel.Height = num2;
        }

        private void RenderSelectionRectangle(Rect bounds)
        {
            if (this.IsSelectionRectangleVisible())
            {
                this.UpdateSelectionRectangleVisibility();
                this.SetSelectionRectangleContainerClipping();
                Rect rect = this.CalcSelectionRectangleBounds(bounds);
                this.selectionRectangle.SetValue(Canvas.LeftProperty, rect.Left);
                this.selectionRectangle.SetValue(Canvas.TopProperty, rect.Top);
                this.selectionRectangle.Height = rect.Height;
                this.selectionRectangle.Width = rect.Width;
            }
        }

        private void RenderShader()
        {
            if (this.ShaderEffect != null)
            {
                this.ShaderEffect.Invalidate(this.CalcShaderBounds());
            }
        }

        private void ResetAutoScroll()
        {
            if (this.autoScrollTimer != null)
            {
                this.IsAutoScrollInProcess = false;
                this.UpdateNavigationButtonsVisibility();
                this.autoScrollTimer.Stop();
                this.autoScrollTimer = null;
            }
        }

        internal void ResizeSelection(double currentPosition, bool isTouch = false)
        {
            if (isTouch)
            {
                currentPosition = this.RealToPixel(this.ActualVisibleStart) + currentPosition;
            }
            if (this.Client != null)
            {
                this.StopAnimation();
                this.Invalidate();
                if (this.isResizingDetected)
                {
                    this.ProcessSelectionResizing(currentPosition);
                }
                else
                {
                    this.FindDraggedAndFixedThumbs(currentPosition);
                    this.SelectionType = this.draggedThumb.Equals(this.leftSelectionThumb) ? SelectionChangesType.Start : SelectionChangesType.End;
                }
            }
        }

        private void RootContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateClientPanelWidth(e.NewSize);
            bool flag = e.NewSize.Width == 0.0;
            if (flag)
            {
                this.shouldLockUpdate = flag;
            }
            else if (this.shouldLockUpdate)
            {
                this.shouldLockUpdate = false;
                if (!this.IsRangeControlInitialized)
                {
                    this.Initialize();
                }
                else
                {
                    this.SyncronizeWithClient(this.RangeStart, this.RangeEnd, false);
                    this.Invalidate();
                }
            }
        }

        internal void ScrollByDelta(double pixelDelta)
        {
            if ((this.scrollViewer != null) && this.AllowScroll)
            {
                this.scrollViewer.ScrollToHorizontalOffset(this.scrollViewer.HorizontalOffset + pixelDelta);
            }
        }

        internal void ScrollRangeBar(double normalOffset)
        {
            if (this.AllowScroll)
            {
                double offset = normalOffset * this.ClientWidth;
                this.AnimateScroll(offset);
            }
        }

        private void ScrollToActualVisibleEnd()
        {
            double autoScrollDelta = this.AutoScrollDelta;
            this.ProccesAutoScroll(autoScrollDelta, Math.Max((double) (this.RealToNormalized(this.ActualVisibleEnd) + autoScrollDelta), (double) 0.0));
        }

        private void ScrollToActualVisibleStart()
        {
            double scrollDelta = -this.AutoScrollDelta;
            this.ProccesAutoScroll(scrollDelta, Math.Max((double) (this.RealToNormalized(this.ActualVisibleStart) + scrollDelta), (double) 0.0));
        }

        internal void SelectByHitTest()
        {
            if (this.Client != null)
            {
                this.Animator.CanAnimate = this.Client.AllowThumbs;
                this.SetActualSelectionRangeInternal(this.CurrentHitTest.RegionStart, this.CurrentHitTest.RegionEnd, false);
                this.SetNormalizedRangeInternal(this.RealToNormalized(this.ActualSelectionStart), this.RealToNormalized(this.ActualSelectionEnd));
            }
        }

        internal void SelectGroupInterval()
        {
            this.SetActualSelectionRangeInternal(this.CurrentHitTest.RegionStart, this.CurrentHitTest.RegionEnd, false);
            this.SetNormalizedRangeInternal(this.RealToNormalized(this.ActualSelectionStart), this.RealToNormalized(this.ActualSelectionEnd));
            this.Invalidate();
        }

        private void SetActualSelectionEnd()
        {
            this.ActualSelectionEnd = this.CanSnapToInterval() ? this.SnapEnd() : this.NormalizedToReal(this.NormalizedSelectionEnd);
            this.SetSelectionEndByActual();
        }

        private void SetActualSelectionRangeInternal(object start, object end, bool syncWithActual = false)
        {
            this.renderLocker.DoLockedAction(delegate {
                if (this.Client != null)
                {
                    this.Client.SetSelectionRange(start, end, this.GetViewportSize(), this.AllowSnapToInterval);
                    if ((this.Client.SelectionStart != null) && (this.Client.SelectionEnd != null))
                    {
                        this.ActualSelectionStart = this.Client.SelectionStart;
                        this.ActualSelectionEnd = this.Client.SelectionEnd;
                        if (syncWithActual)
                        {
                            this.SetSelectionRangeByActual();
                        }
                    }
                }
            });
        }

        private void SetActualSelectionStart()
        {
            this.ActualSelectionStart = this.CanSnapToInterval() ? this.SnapStart() : this.NormalizedToReal(this.NormalizedSelectionStart);
            this.SetSelectionStartByActual();
        }

        private void SetActualVisibleRange(object newVisibleStart, object newVisibleEnd, bool checkBeforeSet = false)
        {
            if ((this.Client != null) && (this.IsViewPortValid() && ((newVisibleStart != null) && (newVisibleEnd != null))))
            {
                this.renderLocker.DoLockedAction(delegate {
                    this.IsVisibleRangeCorrected = this.Client.SetVisibleRange(newVisibleStart, newVisibleEnd, this.GetViewportSize());
                    if ((this.IsVisibleRangeCorrected & checkBeforeSet) && !this.DetectVisibleRangeWidthChange())
                    {
                        this.StopAnimation();
                    }
                    else
                    {
                        this.SetActualVisibleRangeAfterCorrection();
                    }
                });
            }
        }

        private void SetActualVisibleRangeAfterCorrection()
        {
            this.ActualVisibleStart = this.Client.VisibleStart;
            this.ActualVisibleEnd = this.Client.VisibleEnd;
            this.SetClientPanelWidth();
        }

        private void SetClientPanelWidth()
        {
            if (this.clientPanel != null)
            {
                double width = this.clientPanel.Width;
                double num2 = this.nativeSize.Width * this.ScaleFactor;
                this.clientPanel.Width = num2;
                if (!double.IsNaN(width) && !width.Round(false).AreClose(this.clientPanel.Width.Round(false)))
                {
                    this.isSyncronizeHorizontalOffset = true;
                }
                if (this.IsRangeControlInitialized)
                {
                    this.content.Width = num2;
                    this.content.Height = this.clientPanel.ActualHeight;
                    this.interactionArea.Width = num2;
                    this.interactionArea.Height = this.clientPanel.ActualHeight;
                }
            }
        }

        private void SetClientRange(object start, object end)
        {
            if (this.Client.SetRange(start, end, this.GetViewportSize()))
            {
                this.SetStartEndInternal(this.Client.Start, this.Client.End, this.IsStartManualBehavior, this.IsEndManualBehavior);
            }
        }

        private void SetLabelsText()
        {
            this.leftLabel.Content = this.FormatText(this.ActualSelectionStart);
            this.rightLabel.Content = this.FormatText(this.ActualSelectionEnd);
        }

        private void SetNormalizedEndWithLock(bool isSelectionTypeChanged, double newEnd)
        {
            this.renderLocker.DoLockedAction(delegate {
                if (isSelectionTypeChanged)
                {
                    this.NormalizedSelectionStart = this.RealToNormalized(this.ActualSelectionEnd);
                    this.ActualSelectionStart = this.ActualSelectionEnd;
                    this.SetSelectionStartByActual();
                    this.ReassignThumbs();
                }
                this.NormalizedSelectionEnd = (newEnd >= this.NormalizedSelectionStart) ? newEnd : this.NormalizedSelectionStart;
                this.SetActualSelectionEnd();
            });
        }

        private void SetNormalizedRange(double newStart, double newEnd)
        {
            this.SetNormalizedRangeInternal(newStart, newEnd);
            this.SetActualSelectionStart();
            this.SetActualSelectionEnd();
            this.Render(false);
        }

        private void SetNormalizedRangeInternal(double newStart, double newEnd)
        {
            this.renderLocker.DoLockedAction(delegate {
                this.NormalizedSelectionStart = newStart;
                this.NormalizedSelectionEnd = newEnd;
            });
        }

        private void SetNormalizedStartWithLock(bool isSelectionTypeChanged, double newStart)
        {
            this.renderLocker.DoLockedAction(delegate {
                if (isSelectionTypeChanged)
                {
                    this.NormalizedSelectionEnd = this.RealToNormalized(this.ActualSelectionStart);
                    this.ActualSelectionEnd = this.ActualSelectionStart;
                    this.SetSelectionEndByActual();
                    this.ReassignThumbs();
                }
                this.NormalizedSelectionStart = (newStart <= this.NormalizedSelectionEnd) ? newStart : this.NormalizedSelectionEnd;
                this.SetActualSelectionStart();
            });
        }

        internal static void SetOwnerRangeControl(DependencyObject element, DevExpress.Xpf.Editors.RangeControl.RangeControl value)
        {
            if (element != null)
            {
                element.SetValue(OwnerRangeControlPropertyKey, value);
            }
        }

        internal static void SetPropertyProvider(DependencyObject element, RangeControlPropertyProvider value)
        {
            if (element != null)
            {
                element.SetValue(PropertyProviderPropertyKey, value);
            }
        }

        private void SetRangeEnd(object end)
        {
            base.SetCurrentValue(RangeEndProperty, end);
        }

        private void SetRangeStart(object start)
        {
            base.SetCurrentValue(RangeStartProperty, start);
        }

        private void SetRangeThumbsVisible()
        {
            this.leftSelectionThumb.Opacity = 1.0;
            this.rightSelectionThumb.Opacity = 1.0;
        }

        private void SetSelectionEndByActual()
        {
            if (this.AllowImmediateRangeUpdate)
            {
                this.SetSelectionEndCore();
            }
        }

        private void SetSelectionEndCore()
        {
            this.UpdateSelectionLocker.DoLockedAction(() => base.SetCurrentValue(SelectionRangeEndProperty, this.ActualSelectionEnd));
            if (!this.updateSelectedRangeLocker.IsLocked)
            {
                this.UpdateSelectedRange();
            }
        }

        private void SetSelectionRangeByActual()
        {
            this.updateSelectedRangeLocker.DoLockedAction(delegate {
                this.SetSelectionStartCore();
                this.SetSelectionEndCore();
            });
            this.UpdateSelectedRange();
        }

        private void SetSelectionRectangleContainerClipping()
        {
            if (this.selectionRactangleContainer != null)
            {
                Point location = new Point();
                Rect rect = new Rect(location, new Size(this.selectionRactangleContainer.ActualWidth, this.selectionRactangleContainer.ActualHeight));
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = rect;
                this.selectionRactangleContainer.Clip = geometry1;
            }
        }

        private void SetSelectionStartByActual()
        {
            if (this.AllowImmediateRangeUpdate)
            {
                this.SetSelectionStartCore();
            }
        }

        private void SetSelectionStartCore()
        {
            this.UpdateSelectionLocker.DoLockedAction(() => base.SetCurrentValue(SelectionRangeStartProperty, this.ActualSelectionStart));
            if (!this.updateSelectedRangeLocker.IsLocked)
            {
                this.UpdateSelectedRange();
            }
        }

        private void SetStartEndInternal(object start, object end, bool isStartManual, bool isEndManual)
        {
            this.renderLocker.DoLockedAction(delegate {
                this.SetRangeStart(start);
                this.SetRangeEnd(end);
                this.IsStartManualBehavior = isStartManual;
                this.IsEndManualBehavior = isEndManual;
            });
        }

        private void SetUpShaderEffect()
        {
            if ((this.Client != null) && (this.scrollViewer != null))
            {
                this.ShaderEffect ??= this.CreateShader();
                this.scrollViewer.Effect = this.ShaderEffect;
                this.ShaderEffect.IsEnable = this.CanGrayscale();
            }
        }

        private void SetVisibleEndByActual()
        {
            base.SetCurrentValue(VisibleRangeEndProperty, this.ActualVisibleEnd);
        }

        private void SetVisibleRange(object newVisibleStart, object newVisibleEnd)
        {
            this.SetActualVisibleRange(newVisibleStart, newVisibleEnd, false);
            if (!this.Animator.IsProcessAnimation)
            {
                this.SetVisibleRangeByActual();
            }
        }

        private void SetVisibleRangeByActual()
        {
            this.SetVisibleStartByActual();
            this.SetVisibleEndByActual();
        }

        private void SetVisibleStartByActual()
        {
            base.SetCurrentValue(VisibleRangeStartProperty, this.ActualVisibleStart);
        }

        private bool ShouldShading() => 
            this.Client.GrayOutNonSelectedRange && (this.ShadingMode != ShadingModes.Grayscale);

        private void SnapActualSelectionEnd()
        {
            object obj2 = this.SelectionRangeEnd ?? this.RangeEnd;
            this.ActualSelectionEnd = (this.UpdateSelectionLocker.IsLocked || !this.CanSnapToInterval()) ? obj2 : this.GetCenterSnappedValue(obj2, true);
        }

        private void SnapActualSelectionStart()
        {
            object obj2 = this.SelectionRangeStart ?? this.RangeStart;
            this.ActualSelectionStart = (this.UpdateSelectionLocker.IsLocked || !this.CanSnapToInterval()) ? obj2 : this.GetCenterSnappedValue(obj2, false);
        }

        private void SnapAndUpdateActualSelectionEnd()
        {
            this.SnapActualSelectionEnd();
            if (!this.updateSelectedRangeLocker.IsLocked)
            {
                this.UpdateSelectedRange();
            }
        }

        private void SnapAndUpdateActualSelectionStart()
        {
            this.SnapActualSelectionStart();
            if (!this.updateSelectedRangeLocker.IsLocked)
            {
                this.UpdateSelectedRange();
            }
        }

        private object SnapEnd() => 
            this.GetSnappedValue(this.NormalizedToReal(this.NormalizedSelectionEnd));

        private void SnapNormalizedRangeToCenter()
        {
            if (this.CanSnapToInterval())
            {
                double newStart = this.RealToNormalized(this.GetCenterSnappedValue(this.ActualSelectionStart, false));
                double newEnd = this.RealToNormalized(this.GetCenterSnappedValue(this.ActualSelectionEnd, true));
                if (newStart > newEnd)
                {
                    newStart = newEnd;
                }
                this.SetNormalizedRangeInternal(newStart, newEnd);
                this.IsNormalizedRangeSnappedToCenter = true;
            }
        }

        private object SnapStart() => 
            this.GetSnappedValue(this.NormalizedToReal(this.NormalizedSelectionStart));

        private object SnapToCenter(object value, bool isRight)
        {
            object snappedValue = this.GetSnappedValue(value);
            return (((snappedValue == null) || snappedValue.Equals(value)) ? snappedValue : this.Client.GetSnappedValue(value, isRight));
        }

        private void StartAutoScroll(Action action)
        {
            if (this.autoScrollTimer == null)
            {
                DispatcherTimer timer1 = new DispatcherTimer(DispatcherPriority.Render);
                timer1.Interval = TimeSpan.FromMilliseconds(50.0);
                this.autoScrollTimer = timer1;
                this.autoScrollTimer.Tick += (t, e) => action();
                this.autoScrollTimer.Start();
            }
        }

        internal void StartAutoScroll(double position, bool isResize)
        {
            Action action = null;
            this.IsAutoScrollInProcess = true;
            action = !isResize ? ((position < 0.0) ? new Action(this.MoveToActualVisibleStart) : new Action(this.MoveToActualVisibleEnd)) : ((position < 0.0) ? new Action(this.ScrollToActualVisibleStart) : new Action(this.ScrollToActualVisibleEnd));
            this.StartAutoScroll(action);
        }

        private void StartEndChanged()
        {
            if (this.Client != null)
            {
                if (!this.renderLocker.IsLocked)
                {
                    this.SetClientRange(this.RangeStart, this.RangeEnd);
                    this.SetVisibleRange(this.Client.Start, this.Client.End);
                    this.SetNormalizedRangeInternal(this.RealToNormalized(this.ActualSelectionStart), this.RealToNormalized(this.ActualSelectionEnd));
                }
                this.UpdateInvertThumbs();
                this.Invalidate();
            }
        }

        private void StopAnimation()
        {
            this.Animator.StopAnimation();
        }

        internal void StopAutoScroll()
        {
            this.ResetAutoScroll();
        }

        private void StopPost()
        {
            this.StopPosting = true;
        }

        private void SubscribeEvents()
        {
            this.rootContainer.SizeChanged += new SizeChangedEventHandler(this.RootContainerSizeChanged);
            this.scrollViewer.ScrollChanged += new ScrollChangedEventHandler(this.OnScrollViewerScrollChanged);
            this.leftLabel.SizeChanged += new SizeChangedEventHandler(this.OnLabelSizeChanged);
            this.rightLabel.SizeChanged += new SizeChangedEventHandler(this.OnLabelSizeChanged);
        }

        private void SyncronizeHorizontalOffset()
        {
            if (this.scrollViewer != null)
            {
                double offset = this.GetNormalizedVisibleStart() * this.scrollViewer.ExtentWidth;
                this.scrollViewer.ScrollToHorizontalOffset(offset);
            }
        }

        private void SyncronizeVisibleRange()
        {
            if (!this.IsInteraction)
            {
                this.SetVisibleRange(this.Client.VisibleStart, this.Client.VisibleEnd);
            }
            this.Invalidate();
        }

        private void SyncronizeWithClient(object newStart, object newEnd, bool resetSelection = false)
        {
            object start = this.IsStartManualBehavior ? this.RangeStart : newStart;
            this.SetStartEndInternal(start, this.IsEndManualBehavior ? this.RangeEnd : newEnd, this.IsStartManualBehavior, this.IsEndManualBehavior);
            this.SetClientRange(this.RangeStart, this.RangeEnd);
            this.UpdateRanges();
            this.SyncronizeHorizontalOffset();
            if (resetSelection)
            {
                this.SetSelectionRangeByActual();
            }
            this.Invalidate();
        }

        internal void ThumbDragStarted(Point position, double dragDelta)
        {
            bool flag = this.IsLeftThumbDragged(position, dragDelta);
            this.draggedThumb = flag ? this.leftSelectionThumb : this.rightSelectionThumb;
            this.SelectionType = flag ? SelectionChangesType.Start : SelectionChangesType.End;
            this.OnSelectionResizingStarted();
        }

        private void UnSubscribeEvents()
        {
            if (this.rootContainer != null)
            {
                this.rootContainer.SizeChanged -= new SizeChangedEventHandler(this.RootContainerSizeChanged);
            }
            if (this.scrollViewer != null)
            {
                this.scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(this.OnScrollViewerScrollChanged);
            }
            if (this.leftLabel != null)
            {
                this.leftLabel.SizeChanged -= new SizeChangedEventHandler(this.OnLabelSizeChanged);
            }
            if (this.rightLabel != null)
            {
                this.rightLabel.SizeChanged -= new SizeChangedEventHandler(this.OnLabelSizeChanged);
            }
        }

        private void UpdateByControllerState(RangeControlStateType state)
        {
            switch (state)
            {
                case RangeControlStateType.MoveSelection:
                    this.OnSelectionRangeChangedByController(true);
                    return;

                case RangeControlStateType.Zoom:
                    this.UpdateVisibleOnControllerReset();
                    return;

                case RangeControlStateType.Scrolling:
                    this.UpdateVisibleOnControllerReset();
                    return;

                case RangeControlStateType.Selection:
                    this.OnSelectionResizingCompleted();
                    return;

                case RangeControlStateType.Click:
                    this.OnSelectionRangeChangedByController(true);
                    return;

                case RangeControlStateType.ThumbDragging:
                    this.OnSelectionResizingCompleted();
                    return;
            }
        }

        private void UpdateClientPanelWidth(Size newSize)
        {
            this.nativeSize = newSize;
            if (this.clientPanel != null)
            {
                this.SetClientPanelWidth();
                this.Invalidate();
            }
        }

        private void UpdateInvertThumbLeft()
        {
            this.PropertyProvider.InvertLeftThumb = this.Client.GetComparableValue(this.ActualSelectionStart) == this.Client.GetComparableValue(this.RangeStart);
        }

        private void UpdateInvertThumbRight()
        {
            this.PropertyProvider.InvertRightThumb = this.Client.GetComparableValue(this.ActualSelectionEnd) == this.Client.GetComparableValue(this.RangeEnd);
        }

        private void UpdateInvertThumbs()
        {
            this.UpdateInvertThumbLeft();
            this.UpdateInvertThumbRight();
        }

        private void UpdateLabelsVisibility()
        {
            if ((this.leftLabel != null) && (this.rightLabel != null))
            {
                this.leftLabel.Visibility = this.rightLabel.Visibility = this.IsLabelsVisible() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateNavigationButtonsVisibility()
        {
            if (this.leftNavigationButton != null)
            {
                this.leftNavigationButton.Visibility = this.IsLeftNavigationButtonsVisible() ? Visibility.Visible : Visibility.Collapsed;
            }
            if (this.rightNavigationButton != null)
            {
                this.rightNavigationButton.Visibility = this.IsRightNavigationButtonsVisible() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateNormalizedSelection()
        {
            this.UpdateNormalizedSelectionStart();
            this.UpdateNormalizedSelectionEnd();
        }

        private void UpdateNormalizedSelectionEnd()
        {
            if (this.AllowUpdateNormalizedRange)
            {
                this.NormalizedSelectionEnd = this.RealToNormalized(this.ActualSelectionEnd);
            }
        }

        private void UpdateNormalizedSelectionStart()
        {
            if (this.AllowUpdateNormalizedRange)
            {
                this.NormalizedSelectionStart = this.RealToNormalized(this.ActualSelectionStart);
            }
        }

        private void UpdateOutOfRangeBorderVisibility()
        {
            if ((this.leftSide != null) && (this.rightSide != null))
            {
                if (this.Client != null)
                {
                    this.rightSide.Visibility = this.leftSide.Visibility = this.Client.AllowThumbs ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    this.rightSide.Visibility = this.leftSide.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UpdatePropertiesOnClientChanged()
        {
            this.Client.LayoutChanged += new EventHandler<LayoutChangedEventArgs>(this.OnClientLayoutChanged);
            this.UpdateVisibility();
        }

        private void UpdateRanges()
        {
            if (this.ResetRangesOnClientItemsSourceChanged)
            {
                this.SetActualSelectionRangeInternal(this.Client.Start, this.Client.End, true);
                this.SetNormalizedRangeInternal(this.RealToNormalized(this.Client.Start), this.RealToNormalized(this.Client.End));
                this.SetVisibleRange(this.Client.Start, this.Client.End);
            }
            else
            {
                this.SetActualSelectionRangeInternal(this.Client.SelectionStart, this.Client.SelectionEnd, true);
                this.SetNormalizedRangeInternal(this.RealToNormalized(this.Client.SelectionStart), this.RealToNormalized(this.Client.SelectionEnd));
                this.SetVisibleRange(this.Client.VisibleStart, this.Client.VisibleEnd);
            }
        }

        private void UpdateSelectedRange()
        {
            this.UpdateSelectedRange(this.SelectionRangeStart, this.SelectionRangeEnd);
        }

        private void UpdateSelectedRange(object start, object end)
        {
            this.SelectionRange = new EditRange(start, end);
        }

        private void UpdateSelectionRectangleVisibility()
        {
            this.selectionRectangle.Visibility = this.IsSelectionRectangleVisible() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void UpdateThumbVisibility()
        {
            if ((this.leftSelectionThumb != null) && (this.rightSelectionThumb != null))
            {
                this.leftSelectionThumb.Opacity = this.rightSelectionThumb.Opacity = this.IsThumbsVisible() ? ((double) 1) : ((double) 0);
            }
        }

        private void UpdateVisibility()
        {
            this.UpdateThumbVisibility();
            this.UpdateOutOfRangeBorderVisibility();
        }

        private void UpdateVisibleOnControllerReset()
        {
            this.IsInteraction = true;
            this.PostWithDelay(delegate {
                this.IsInteraction = false;
                this.SetVisibleRangeByActual();
            });
        }

        private void UpdateVisibleRange(object newStart, object newEnd, bool isCheckBeforeSet = true)
        {
            this.SetActualVisibleRange(newStart, newEnd, isCheckBeforeSet);
            if (this.AllowImmediateRangeUpdate)
            {
                this.PostWithDelay(new Action(this.SetVisibleRangeByActual));
            }
        }

        internal void ZoomByDoubleTap(Point position)
        {
            if (this.AllowZoom && this.CanChangeVisibleRange(position))
            {
                this.StopPost();
                this.AnimateDoubleTapZoom(this.Client.VisibleStart, this.Client.VisibleEnd);
            }
        }

        internal void ZoomByPinch(double scale, double position)
        {
            position = this.RealToPixel(this.ActualVisibleStart) + position;
            double factor = 2.0 / scale;
            if (this.ProcessZoom(position, factor))
            {
                this.EnqueueAction(new Action(this.SnapNormalizedRangeToCenter));
            }
        }

        internal void ZoomByWheel(int wheelDelta, double position)
        {
            if (this.AllowZoom && (wheelDelta != 0))
            {
                double factor = (Math.Sign(wheelDelta) == -1) ? 2.0 : 0.5;
                if (this.ProcessZoom(position, factor))
                {
                    this.PostponeSnapNormalizedRangeToCenter();
                }
                this.UpdateVisibleOnControllerReset();
            }
        }

        public IRangeControlClient Client
        {
            get => 
                (IRangeControlClient) base.GetValue(ClientProperty);
            set => 
                base.SetValue(ClientProperty, value);
        }

        public bool ResetRangesOnClientItemsSourceChanged
        {
            get => 
                (bool) base.GetValue(ResetRangesOnClientItemsSourceChangedProperty);
            set => 
                base.SetValue(ResetRangesOnClientItemsSourceChangedProperty, value);
        }

        public EditRange SelectionRange
        {
            get => 
                (EditRange) base.GetValue(SelectionRangeProperty);
            private set => 
                base.SetValue(SelectionRangePropertyKey, value);
        }

        public object SelectionRangeStart
        {
            get => 
                base.GetValue(SelectionRangeStartProperty);
            set => 
                base.SetValue(SelectionRangeStartProperty, value);
        }

        public object SelectionRangeEnd
        {
            get => 
                base.GetValue(SelectionRangeEndProperty);
            set => 
                base.SetValue(SelectionRangeEndProperty, value);
        }

        public object VisibleRangeStart
        {
            get => 
                base.GetValue(VisibleRangeStartProperty);
            set => 
                base.SetValue(VisibleRangeStartProperty, value);
        }

        public object VisibleRangeEnd
        {
            get => 
                base.GetValue(VisibleRangeEndProperty);
            set => 
                base.SetValue(VisibleRangeEndProperty, value);
        }

        public object RangeStart
        {
            get => 
                base.GetValue(RangeStartProperty);
            set => 
                base.SetValue(RangeStartProperty, value);
        }

        public object RangeEnd
        {
            get => 
                base.GetValue(RangeEndProperty);
            set => 
                base.SetValue(RangeEndProperty, value);
        }

        public bool AllowSnapToInterval
        {
            get => 
                (bool) base.GetValue(AllowSnapToIntervalProperty);
            set => 
                base.SetValue(AllowSnapToIntervalProperty, value);
        }

        public bool ShowRangeBar
        {
            get => 
                (bool) base.GetValue(ShowRangeBarProperty);
            set => 
                base.SetValue(ShowRangeBarProperty, value);
        }

        public bool ShowRangeThumbs
        {
            get => 
                (bool) base.GetValue(ShowRangeThumbsProperty);
            set => 
                base.SetValue(ShowRangeThumbsProperty, value);
        }

        public bool AllowImmediateRangeUpdate
        {
            get => 
                (bool) base.GetValue(AllowImmediateRangeUpdateProperty);
            set => 
                base.SetValue(AllowImmediateRangeUpdateProperty, value);
        }

        public bool AllowScroll
        {
            get => 
                (bool) base.GetValue(AllowScrollProperty);
            set => 
                base.SetValue(AllowScrollProperty, value);
        }

        public bool AllowZoom
        {
            get => 
                (bool) base.GetValue(AllowZoomProperty);
            set => 
                base.SetValue(AllowZoomProperty, value);
        }

        public bool EnableAnimation
        {
            get => 
                (bool) base.GetValue(EnableAnimationProperty);
            set => 
                base.SetValue(EnableAnimationProperty, value);
        }

        public bool? ShowSelectionRectangle
        {
            get => 
                (bool?) base.GetValue(ShowSelectionRectangleProperty);
            set => 
                base.SetValue(ShowSelectionRectangleProperty, value);
        }

        public bool IsSelectionMoving
        {
            get => 
                (bool) base.GetValue(IsSelectionMovingProperty);
            private set => 
                base.SetValue(IsSelectionMovingPropertyKey, value);
        }

        public bool ShowLabels
        {
            get => 
                (bool) base.GetValue(ShowLabelsProperty);
            set => 
                base.SetValue(ShowLabelsProperty, value);
        }

        public bool ShowNavigationButtons
        {
            get => 
                (bool) base.GetValue(ShowNavigationButtonsProperty);
            set => 
                base.SetValue(ShowNavigationButtonsProperty, value);
        }

        public DataTemplate LabelTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LabelTemplateProperty);
            set => 
                base.SetValue(LabelTemplateProperty, value);
        }

        public int UpdateDelay
        {
            get => 
                (int) base.GetValue(UpdateDelayProperty);
            set => 
                base.SetValue(UpdateDelayProperty, value);
        }

        public ShadingModes ShadingMode
        {
            get => 
                (ShadingModes) base.GetValue(ShadingModeProperty);
            set => 
                base.SetValue(ShadingModeProperty, value);
        }

        public RangeControlPropertyProvider PropertyProvider =>
            (RangeControlPropertyProvider) base.GetValue(PropertyProviderProperty);

        private bool HasVisibleRangeWidthChange =>
            !this.GetActualVisibleRange().AreClose(this.LastVisibleRangeWidth);

        private double LastVisibleRangeWidth { get; set; }

        private RangeControlAnimator Animator
        {
            get
            {
                if (this.animator == null)
                {
                    this.animator = new RangeControlAnimator();
                    this.animator.AnimationCompleted += new EventHandler<AnimationEventArgs>(this.OnAnimationCompleted);
                }
                return this.animator;
            }
        }

        private bool AllowUpdateNormalizedRange { get; set; }

        private SelectionChangesType SelectionType { get; set; }

        private RangeControlController Controller { get; set; }

        private bool IsStartManualBehavior { get; set; }

        private bool IsEndManualBehavior { get; set; }

        private bool IsVisibleRangeCorrected { get; set; }

        private bool IsAutoScrollInProcess { get; set; }

        private Locker UpdateSelectionLocker { get; set; }

        private double ScaleFactor
        {
            get
            {
                double actualVisibleRange = this.GetActualVisibleRange();
                return ((actualVisibleRange > 0.0) ? (1.0 / actualVisibleRange) : 1.0);
            }
        }

        internal object ActualSelectionStart
        {
            get => 
                this.actualSelectionStart;
            set
            {
                if (this.actualSelectionStart != value)
                {
                    this.actualSelectionStart = value;
                    if (this.Client != null)
                    {
                        this.OnActualSelectionStartChanged();
                    }
                }
            }
        }

        internal object ActualSelectionEnd
        {
            get => 
                this.actualSelectionEnd;
            set
            {
                if (this.actualSelectionEnd != value)
                {
                    this.actualSelectionEnd = value;
                    if (this.Client != null)
                    {
                        this.OnActualSelectionEndChanged();
                    }
                }
            }
        }

        internal object ActualVisibleStart
        {
            get => 
                this.actualVisibleStart;
            set
            {
                if (this.actualVisibleStart != value)
                {
                    this.actualVisibleStart = value;
                    if (value == null)
                    {
                        this.Invalidate();
                    }
                    else if (this.Client != null)
                    {
                        this.OnActualVisibleStartChanged();
                    }
                }
            }
        }

        internal object ActualVisibleEnd
        {
            get => 
                this.actualVisibleEnd;
            set
            {
                if (this.actualVisibleEnd != value)
                {
                    this.actualVisibleEnd = value;
                    if (value == null)
                    {
                        this.Invalidate();
                    }
                    else if (this.Client != null)
                    {
                        this.OnActualVisibleEndChanged();
                    }
                }
            }
        }

        internal double NormalizedSelectionStart
        {
            get => 
                this.normalizedSelectionStart;
            set
            {
                if (this.normalizedSelectionStart != value)
                {
                    this.normalizedSelectionStart = value;
                    this.OnNormalizedRangeChanged();
                }
            }
        }

        internal double NormalizedSelectionEnd
        {
            get => 
                this.normalizedSelectionEnd;
            set
            {
                if (this.normalizedSelectionEnd != value)
                {
                    this.normalizedSelectionEnd = value;
                    this.OnNormalizedRangeChanged();
                }
            }
        }

        private bool IsRangeControlInitialized { get; set; }

        private RangeControlClientHitTestResult CurrentHitTest { get; set; }

        private bool StopPosting { get; set; }

        private bool IsInteraction { get; set; }

        private bool IsSelectionRangeLessVisibleRange { get; set; }

        private bool IsNormalizedRangeSnappedToCenter { get; set; }

        private double AutoScrollDelta =>
            0.05 * this.GetActualVisibleRange();

        private double ClientWidth =>
            this.clientPanel.ActualWidth;

        protected override IEnumerator LogicalChildren =>
            this.logicalChildren.GetEnumerator();

        private GrayScaleEffect ShaderEffect { get; set; }

        private bool IsLastLeftThumbOutOfBounds { get; set; }

        private bool IsLastRightThumbOutOfBounds { get; set; }

        private double LeftLabelLeft { get; set; }

        private double RightLabelLeft { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Editors.RangeControl.RangeControl.<>c <>9 = new DevExpress.Xpf.Editors.RangeControl.RangeControl.<>c();

            internal void <.cctor>b__32_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnClientChanged(e.OldValue as IRangeControlClient, e.NewValue as IRangeControlClient);
            }

            internal void <.cctor>b__32_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnSelectionRangeStartChanged();
            }

            internal void <.cctor>b__32_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnAllowImmediateUpdateChanged();
            }

            internal void <.cctor>b__32_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowLabelsChanged();
            }

            internal void <.cctor>b__32_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowNavigationButtonsChanged();
            }

            internal void <.cctor>b__32_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowLabelTemplateChanged();
            }

            internal void <.cctor>b__32_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnUpdateDelayChangedChanged();
            }

            internal object <.cctor>b__32_15(DependencyObject d, object o) => 
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).CoerceUpdateDelay((int) o);

            internal void <.cctor>b__32_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShadingModeChanged();
            }

            internal void <.cctor>b__32_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnSelectionRangeEndChanged();
            }

            internal void <.cctor>b__32_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnVisibleRangeStartChanged(e.OldValue);
            }

            internal void <.cctor>b__32_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnVisibleRangeEndChanged(e.OldValue);
            }

            internal void <.cctor>b__32_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnRangeStartChanged();
            }

            internal void <.cctor>b__32_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnRangeEndChanged();
            }

            internal void <.cctor>b__32_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnAllowSnapToIntervalChanged();
            }

            internal void <.cctor>b__32_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowRangeBarChanged();
            }

            internal void <.cctor>b__32_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.RangeControl.RangeControl) d).OnShowRangeThumbsChanged();
            }
        }
    }
}

