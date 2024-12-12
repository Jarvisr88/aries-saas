namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Tooltip", Type=typeof(ToolTip))]
    public class MarginThumb : GridSplitter
    {
        public const string PART_Tooltip = "PART_Tooltip";
        private Point startMousePosition;
        private ToolTip toolTip;
        public static readonly DependencyProperty PageMarginProperty;
        public static readonly DependencyPropertyKey ActualSizePropertyKey;
        public static readonly DependencyProperty ActualSizeProperty;
        public static readonly DependencyProperty SetPageMarginCommandProperty;

        static MarginThumb()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(MarginThumb), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<MarginThumb> registrator1 = DependencyPropertyRegistrator<MarginThumb>.New().RegisterReadOnly<double>(System.Linq.Expressions.Expression.Lambda<Func<MarginThumb, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(MarginThumb.get_ActualSize)), parameters), out ActualSizePropertyKey, out ActualSizeProperty, 0.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(MarginThumb), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<MarginThumb> registrator2 = registrator1.Register<PageMarginModel>(System.Linq.Expressions.Expression.Lambda<Func<MarginThumb, PageMarginModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(MarginThumb.get_PageMargin)), expressionArray2), out PageMarginProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(MarginThumb), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<MarginThumb, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(MarginThumb.get_SetPageMarginCommand)), expressionArray3), out SetPageMarginCommandProperty, null, frameworkOptions).OverrideDefaultStyleKey();
        }

        public MarginThumb()
        {
            this.ToolTipLocker = new Locker();
            base.DragStarted += new DragStartedEventHandler(this.OnDragStarted);
            base.DragCompleted += new DragCompletedEventHandler(this.OnDragCompleted);
            base.DragDelta += new DragDeltaEventHandler(this.OnDragDelta);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            base.AddHandler(UIElement.PreviewMouseRightButtonDownEvent, delegate (object s, RoutedEventArgs e) {
                if (this.ToolTipLocker.IsLocked)
                {
                    base.CancelDrag();
                }
            }, true);
        }

        private double GetActualSize(double horizontalChange, double verticalChange)
        {
            horizontalChange /= this.PageMargin.Zoom;
            verticalChange /= this.PageMargin.Zoom;
            float num = ((float) this.ActualSize).DocToDip();
            double num2 = this.IsVertical ? verticalChange : horizontalChange;
            num2 = this.Reverse ? -num2 : num2;
            double num3 = num + num2;
            double num4 = GraphicsUnitConverter.DipToDoc((num3 < 0.0) ? 0f : ((float) num3));
            return ((num4 > this.PageMargin.MaxSizeF) ? this.PageMargin.MaxSizeF : num4);
        }

        private Point GetPositionsByPreview() => 
            Mouse.GetPosition(LayoutHelper.FindParentObject<DocumentPresenterControl>(this));

        private double GetToolTipOffset()
        {
            Point positionsByPreview = this.GetPositionsByPreview();
            double num = this.IsVertical ? (positionsByPreview.Y - this.startMousePosition.Y) : (positionsByPreview.X - this.startMousePosition.X);
            if (!this.Reverse)
            {
                if (num < -this.PageMargin.Size)
                {
                    return -this.PageMargin.Size;
                }
                if (num > (this.PageMargin.MaxSize - this.PageMargin.Size))
                {
                    return (this.PageMargin.MaxSize - this.PageMargin.Size);
                }
            }
            else
            {
                if (num < -(this.PageMargin.MaxSize - this.PageMargin.Size))
                {
                    return -(this.PageMargin.MaxSize - this.PageMargin.Size);
                }
                if (num > this.PageMargin.Size)
                {
                    return this.PageMargin.Size;
                }
            }
            return num;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.toolTip = base.GetTemplateChild("PART_Tooltip") as ToolTip;
            this.toolTip.Do<ToolTip>(delegate (ToolTip x) {
                x.Opened += new RoutedEventHandler(this.OnToolTipOpened);
            });
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            Action<ToolTip> action = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Action<ToolTip> local1 = <>c.<>9__31_0;
                action = <>c.<>9__31_0 = x => x.IsOpen = false;
            }
            this.toolTip.Do<ToolTip>(action);
            this.ToolTipLocker.Unlock();
            if (!e.Canceled && ((this.SetPageMarginCommand != null) && (this.ActualSize != this.PageMargin.SizeF)))
            {
                this.SetPageMarginCommand.Execute(new PageMarginSettings(this.PageMargin.Side, this.ActualSize));
            }
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.ToolTipLocker.IsLocked)
            {
                this.ActualSize = this.GetActualSize(e.HorizontalChange, e.VerticalChange);
                this.toolTip.Do<ToolTip>(delegate (ToolTip x) {
                    x.IsOpen = true;
                    if (this.IsVertical)
                    {
                        x.VerticalOffset = this.GetToolTipOffset();
                    }
                    else
                    {
                        x.HorizontalOffset = this.GetToolTipOffset();
                    }
                });
            }
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            this.ToolTipLocker.LockOnce();
            if ((this.PageMargin == null) || (this.SetPageMarginCommand == null))
            {
                base.CancelDrag();
            }
            else
            {
                this.startMousePosition = this.GetPositionsByPreview();
                this.ActualSize = this.PageMargin.SizeF;
                this.toolTip.Do<ToolTip>(delegate (ToolTip x) {
                    ThemeManager.SetThemeName(this.toolTip, ThemeManager.ActualApplicationThemeName);
                    this.toolTip.UpdateLayout();
                    x.IsOpen = true;
                });
            }
        }

        private void OnToolTipOpened(object sender, RoutedEventArgs e)
        {
            if (!this.ToolTipLocker.IsLocked)
            {
                this.toolTip.IsOpen = false;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.toolTip.Do<ToolTip>(delegate (ToolTip x) {
                this.toolTip.IsOpen = false;
                x.Opened -= new RoutedEventHandler(this.OnToolTipOpened);
            });
        }

        private Locker ToolTipLocker { get; set; }

        public PageMarginModel PageMargin
        {
            get => 
                (PageMarginModel) base.GetValue(PageMarginProperty);
            set => 
                base.SetValue(PageMarginProperty, value);
        }

        public double ActualSize
        {
            get => 
                (double) base.GetValue(ActualSizeProperty);
            set => 
                base.SetValue(ActualSizePropertyKey, value);
        }

        public ICommand SetPageMarginCommand
        {
            get => 
                (ICommand) base.GetValue(SetPageMarginCommandProperty);
            set => 
                base.SetValue(SetPageMarginCommandProperty, value);
        }

        private bool IsVertical =>
            (this.PageMargin.Side == MarginSide.Top) || (this.PageMargin.Side == MarginSide.Bottom);

        private bool Reverse =>
            (this.PageMargin.Side == MarginSide.Right) || (this.PageMargin.Side == MarginSide.Bottom);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MarginThumb.<>c <>9 = new MarginThumb.<>c();
            public static Action<ToolTip> <>9__31_0;

            internal void <OnDragCompleted>b__31_0(ToolTip x)
            {
                x.IsOpen = false;
            }
        }
    }
}

