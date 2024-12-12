namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    [ToolboxItem(false)]
    public class DateEditCalendarTransferControl : TransferControl
    {
        public static readonly DependencyProperty TransferTypeProperty;
        public static readonly DependencyProperty AnimationTimeProperty;
        private List<Storyboard> executingAnimations = new List<Storyboard>();
        private Storyboard lastExecutingAnimation;

        static DateEditCalendarTransferControl()
        {
            Type forType = typeof(DateEditCalendarTransferControl);
            TransferTypeProperty = DependencyPropertyManager.Register("TransferType", typeof(DateEditCalendarTransferType), typeof(DateEditCalendarTransferControl), new FrameworkPropertyMetadata(DateEditCalendarTransferType.None, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(DateEditCalendarTransferControl.OnTransferTypeChanged)));
            AnimationTimeProperty = DependencyPropertyManager.Register("AnimationTime", typeof(double), typeof(DateEditCalendarTransferControl), new FrameworkPropertyMetadata(300.0, FrameworkPropertyMetadataOptions.None));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
        }

        private void BeginAnimation(DateEditCalendarContent value, DoubleAnimation element, string strPropertyPath)
        {
            Storyboard.SetTarget(element, value);
            Storyboard.SetTargetProperty(element, new PropertyPath(strPropertyPath, new object[0]));
            Storyboard storyboard = new Storyboard {
                Children = { element }
            };
            this.executingAnimations.Add(storyboard);
            storyboard.Completed += delegate (object <sender>, EventArgs <e>) {
                this.executingAnimations.Remove(storyboard);
                if (ReferenceEquals(storyboard, this.lastExecutingAnimation))
                {
                    this.lastExecutingAnimation = null;
                    ((DateEditCalendarContent) this.Content).UpdateCellInfos();
                    this.IsAnimationInProgress = false;
                }
            };
            this.lastExecutingAnimation = storyboard;
            this.IsAnimationInProgress = true;
            storyboard.Begin();
        }

        protected virtual DoubleAnimation CreateDoubleAnimation(double from, double to)
        {
            if (double.IsInfinity(from))
            {
                from = 0.0;
            }
            return this.CreateDoubleAnimationCore(from, to);
        }

        private DoubleAnimation CreateDoubleAnimationCore(double from, double to) => 
            new DoubleAnimation { 
                From = new double?(from),
                To = new double?(to),
                Duration = new Duration(TimeSpan.FromMilliseconds(this.AnimationTime)),
                FillBehavior = FillBehavior.HoldEnd
            };

        protected internal void DelayedNoneAnimation(TransferContentControl control)
        {
            if ((this.Calendar != null) && ((this.Calendar.ActiveContent != null) && (control != null)))
            {
                this.Calendar.ActiveContent.SetDelayedNone(control);
            }
        }

        protected internal void DelayedPrevZoomInAnimation(TransferContentControl control)
        {
            this.Calendar.ActiveContent.SetDelayedPrevZoomIn(control);
        }

        protected internal void DelayedPrevZoomOutAnimation(TransferContentControl control)
        {
            this.Calendar.ActiveContent.SetDelayedPrevZoomOut(control);
        }

        protected internal void DelayedZoomInAnimation(TransferContentControl control)
        {
            this.Calendar.ActiveContent.SetDelayedZoomIn(control);
        }

        protected internal void DelayedZoomOutAnimation(TransferContentControl control)
        {
            this.Calendar.ActiveContent.SetDelayedZoomOut(control);
        }

        private void FinishAllExecutingAnimations()
        {
            this.lastExecutingAnimation = null;
            foreach (Storyboard storyboard in this.executingAnimations)
            {
                storyboard.SkipToFill();
            }
            this.executingAnimations.Clear();
        }

        protected virtual DateEditCalendarContent GetCalendarContent(TransferContentControl control) => 
            control.Content as DateEditCalendarContent;

        protected virtual Rect GetFocusedCellRect(DateEditCalendarContent content)
        {
            FrameworkElement focusedCell = content.GetFocusedCell();
            return ((focusedCell != null) ? LayoutHelper.GetRelativeElementRect(focusedCell, content) : Rect.Empty);
        }

        protected virtual double GetScaleMinValue(Rect r, Size sz) => 
            Math.Min((double) (r.Width / sz.Width), (double) (r.Height / sz.Height));

        protected internal virtual void LeftToRightAnimation(TransferContentControl control)
        {
            this.MakeTranslateXAnimaion(control, -control.ContentPresenter.ActualWidth, 0.0);
        }

        protected virtual void MakeTranslateXAnimaion(TransferContentControl control, double from, double to)
        {
            DateEditCalendarContent calendarContent = this.GetCalendarContent(control);
            if (calendarContent != null)
            {
                calendarContent.RenderTransform = new TranslateTransform();
                this.BeginAnimation(calendarContent, this.CreateDoubleAnimationCore(from, to), "(UIElement.RenderTransform).(TranslateTransform.X)");
            }
        }

        protected virtual void MakeZoomInOutAnimation(TransferContentControl control, double scaleFrom, double scaleTo, double opacityFrom, double opacityTo, bool zoomIn)
        {
            DateEditCalendarContent calendarContent = this.GetCalendarContent(control);
            if (calendarContent != null)
            {
                Rect focusedCellRect = this.GetFocusedCellRect(zoomIn ? this.Calendar.PrevContent : this.Calendar.ActiveContent);
                if (double.IsNaN(scaleTo))
                {
                    scaleTo = this.GetScaleMinValue(focusedCellRect, calendarContent.DesiredSize);
                }
                if (double.IsNaN(scaleFrom))
                {
                    scaleFrom = this.GetScaleMinValue(focusedCellRect, calendarContent.DesiredSize);
                }
                ScaleTransform transform = new ScaleTransform();
                calendarContent.RenderTransform = transform;
                transform.CenterX = focusedCellRect.X + (focusedCellRect.Width / 2.0);
                transform.CenterY = focusedCellRect.Y + (focusedCellRect.Height / 2.0);
                this.BeginAnimation(calendarContent, this.CreateDoubleAnimation(scaleFrom, scaleTo), "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                this.BeginAnimation(calendarContent, this.CreateDoubleAnimation(scaleFrom, scaleTo), "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                this.BeginAnimation(calendarContent, this.CreateDoubleAnimation(opacityFrom, opacityTo), "(UIElement.Opacity)");
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if ((oldContent != null) && (oldContent is FrameworkElement))
            {
                ((FrameworkElement) oldContent).IsHitTestVisible = false;
            }
            if ((newContent != null) && (newContent is FrameworkElement))
            {
                ((FrameworkElement) newContent).IsHitTestVisible = true;
            }
        }

        protected internal override void OnCurrentContentChanged(TransferContentControl control)
        {
            this.FinishAllExecutingAnimations();
            if (this.Calendar != null)
            {
                switch (this.TransferType)
                {
                    case DateEditCalendarTransferType.ShiftLeft:
                        this.RightToLeftAnimation(control);
                        return;

                    case DateEditCalendarTransferType.ShiftRight:
                        this.LeftToRightAnimation(control);
                        return;

                    case DateEditCalendarTransferType.ZoomIn:
                        this.DelayedZoomInAnimation(control);
                        return;

                    case DateEditCalendarTransferType.ZoomOut:
                        this.DelayedZoomOutAnimation(control);
                        return;
                }
            }
        }

        protected internal override void OnPrevContentChanged(TransferContentControl control)
        {
            switch (this.TransferType)
            {
                case DateEditCalendarTransferType.None:
                    this.DelayedNoneAnimation(control);
                    return;

                case DateEditCalendarTransferType.ShiftLeft:
                    this.PrevRightToLeftAnimation(control);
                    return;

                case DateEditCalendarTransferType.ShiftRight:
                    this.PrevLeftToRightAnimation(control);
                    return;

                case DateEditCalendarTransferType.ZoomIn:
                    this.DelayedPrevZoomInAnimation(control);
                    return;

                case DateEditCalendarTransferType.ZoomOut:
                    this.DelayedPrevZoomOutAnimation(control);
                    return;
            }
        }

        protected virtual void OnTransferTypeChanged()
        {
        }

        protected static void OnTransferTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEditCalendarTransferControl) obj).OnTransferTypeChanged();
        }

        protected internal virtual void PrevLeftToRightAnimation(TransferContentControl control)
        {
            this.MakeTranslateXAnimaion(control, 0.0, control.ContentPresenter.ActualWidth);
        }

        protected internal virtual void PrevRightToLeftAnimation(TransferContentControl control)
        {
            this.MakeTranslateXAnimaion(control, 0.0, -control.ContentPresenter.ActualWidth);
        }

        protected internal virtual void PrevZoomInAnimation(TransferContentControl control)
        {
            this.MakeZoomInOutAnimation(control, 1.0, 5.0, 1.0, 0.0, true);
        }

        protected internal virtual void PrevZoomOutAnimation(TransferContentControl control)
        {
            this.MakeZoomInOutAnimation(control, 1.0, double.NaN, 1.0, 0.0, false);
        }

        protected internal virtual void RightToLeftAnimation(TransferContentControl control)
        {
            this.MakeTranslateXAnimaion(control, control.ContentPresenter.ActualWidth, 0.0);
        }

        protected internal virtual void ZoomInAnimation(TransferContentControl control)
        {
            this.MakeZoomInOutAnimation(control, double.NaN, 1.0, 0.0, 1.0, true);
        }

        protected internal virtual void ZoomOutAnimation(TransferContentControl control)
        {
            this.MakeZoomInOutAnimation(control, 5.0, 1.0, 0.0, 1.0, false);
        }

        public double AnimationTime
        {
            get => 
                (double) base.GetValue(AnimationTimeProperty);
            set => 
                base.SetValue(AnimationTimeProperty, value);
        }

        public DateEditCalendarTransferType TransferType
        {
            get => 
                (DateEditCalendarTransferType) base.GetValue(TransferTypeProperty);
            set => 
                base.SetValue(TransferTypeProperty, value);
        }

        internal bool IsAnimationInProgress { get; private set; }

        public DateEdit OwnerDateEdit =>
            (DateEdit) BaseEdit.GetOwnerEdit(this);

        public DateEditCalendar Calendar =>
            DateEditCalendar.GetCalendar(this);

        protected internal bool HasExecutingAnimations =>
            this.lastExecutingAnimation != null;

        protected override bool SkipLongAnimations =>
            false;
    }
}

