namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PageControlItem : Control
    {
        public static readonly DependencyProperty PositionProperty;
        public static readonly DependencyProperty IsCoverPageProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty PageNumberProperty;
        public static readonly DependencyProperty PageWrapperProperty;

        static PageControlItem()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControlItem), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            PositionProperty = DependencyPropertyRegistrator.Register<PageControlItem, PageControlItemPosition>(System.Linq.Expressions.Expression.Lambda<Func<PageControlItem, PageControlItemPosition>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControlItem.get_Position)), parameters), PageControlItemPosition.Middle, (d, oldValue, newValue) => d.OnPositionChanged(newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControlItem), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            IsCoverPageProperty = DependencyPropertyRegistrator.Register<PageControlItem, bool>(System.Linq.Expressions.Expression.Lambda<Func<PageControlItem, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControlItem.get_IsCoverPage)), expressionArray2), false, (d, oldValue, newValue) => d.OnIsCoverPageChanged(newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControlItem), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            IsSelectedProperty = DependencyPropertyRegistrator.Register<PageControlItem, bool>(System.Linq.Expressions.Expression.Lambda<Func<PageControlItem, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControlItem.get_IsSelected)), expressionArray3), false);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControlItem), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            PageNumberProperty = DependencyPropertyRegistrator.Register<PageControlItem, int>(System.Linq.Expressions.Expression.Lambda<Func<PageControlItem, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControlItem.get_PageNumber)), expressionArray4), -1);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControlItem), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            PageWrapperProperty = DependencyPropertyRegistrator.Register<PageControlItem, DevExpress.Xpf.DocumentViewer.PageWrapper>(System.Linq.Expressions.Expression.Lambda<Func<PageControlItem, DevExpress.Xpf.DocumentViewer.PageWrapper>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControlItem.get_PageWrapper)), expressionArray5), null, (d, oldValue, newValue) => d.OnPageWrapperChanged(newValue));
        }

        public PageControlItem()
        {
            base.DefaultStyleKey = typeof(PageControlItem);
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            IPage dataContext = base.DataContext as IPage;
            return (((this.DocumentViewer == null) || (dataContext == null)) ? base.MeasureOverride(constraint) : (this.IsVertical ? new Size(dataContext.PageSize.Width * this.DocumentViewer.ZoomFactor, (dataContext.PageSize.Height * this.DocumentViewer.ZoomFactor) + this.DocumentViewer.VerticalPageSpacing) : new Size(dataContext.PageSize.Height * this.DocumentViewer.ZoomFactor, (dataContext.PageSize.Width * this.DocumentViewer.ZoomFactor) + this.DocumentViewer.VerticalPageSpacing)));
        }

        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IPage newValue = e.NewValue as IPage;
            Func<IPage, int> evaluator = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<IPage, int> local1 = <>c.<>9__28_0;
                evaluator = <>c.<>9__28_0 = x => x.PageIndex;
            }
            this.PageNumber = newValue.Return<IPage, int>(evaluator, (<>c.<>9__28_1 ??= () => 0)) + 1;
        }

        protected virtual void OnIsCoverPageChanged(bool newValue)
        {
            this.UpdateMargin((base.DataContext != null) ? ((IPage) base.DataContext).Margin : new Thickness(0.0));
        }

        protected virtual void OnPageWrapperChanged(DevExpress.Xpf.DocumentViewer.PageWrapper pageWrapper)
        {
            IPage dataContext = base.DataContext as IPage;
            this.IsCoverPage = pageWrapper.IsCoverPage;
            int index = pageWrapper.Pages.ToList<IPage>().IndexOf(dataContext);
            this.Position = (index != 0) ? ((index != (this.PageWrapper.Pages.Count<IPage>() - 1)) ? PageControlItemPosition.Middle : PageControlItemPosition.Right) : PageControlItemPosition.Left;
            this.UpdateMargin(dataContext.Margin);
        }

        protected virtual void OnPositionChanged(PageControlItemPosition newValue)
        {
            this.UpdateMargin((base.DataContext != null) ? ((IPage) base.DataContext).Margin : new Thickness(0.0));
        }

        internal unsafe void UpdateMargin(Thickness margin)
        {
            if (this.IsCoverPage)
            {
                base.Margin = margin;
            }
            else
            {
                switch (this.Position)
                {
                    case PageControlItemPosition.Left:
                    {
                        Func<IDocumentViewerControl, double> evaluator = <>c.<>9__29_0;
                        if (<>c.<>9__29_0 == null)
                        {
                            Func<IDocumentViewerControl, double> local1 = <>c.<>9__29_0;
                            evaluator = <>c.<>9__29_0 = x => x.HorizontalPageSpacing;
                        }
                        margin.Right = this.DocumentViewer.Return<IDocumentViewerControl, double>(evaluator, (<>c.<>9__29_1 ??= () => 0.0)) / 2.0;
                        break;
                    }
                    case PageControlItemPosition.Middle:
                    {
                        Func<IDocumentViewerControl, double> evaluator = <>c.<>9__29_2;
                        if (<>c.<>9__29_2 == null)
                        {
                            Func<IDocumentViewerControl, double> local3 = <>c.<>9__29_2;
                            evaluator = <>c.<>9__29_2 = x => x.HorizontalPageSpacing;
                        }
                        margin.Left = this.DocumentViewer.Return<IDocumentViewerControl, double>(evaluator, (<>c.<>9__29_3 ??= () => 0.0)) / 2.0;
                        Thickness* thicknessPtr1 = &margin;
                        if (<>c.<>9__29_4 == null)
                        {
                            Thickness* local5 = &margin;
                            thicknessPtr1 = (Thickness*) (<>c.<>9__29_4 = x => x.HorizontalPageSpacing);
                        }
                        margin.Right = ((IDocumentViewerControl) &margin).Return<IDocumentViewerControl, double>(((Func<IDocumentViewerControl, double>) thicknessPtr1), (<>c.<>9__29_5 ??= () => 0.0)) / 2.0;
                        break;
                    }
                    case PageControlItemPosition.Right:
                    {
                        Func<IDocumentViewerControl, double> evaluator = <>c.<>9__29_6;
                        if (<>c.<>9__29_6 == null)
                        {
                            Func<IDocumentViewerControl, double> local7 = <>c.<>9__29_6;
                            evaluator = <>c.<>9__29_6 = x => x.HorizontalPageSpacing;
                        }
                        margin.Left = this.DocumentViewer.Return<IDocumentViewerControl, double>(evaluator, (<>c.<>9__29_7 ??= () => 0.0)) / 2.0;
                        break;
                    }
                    default:
                        break;
                }
                base.Margin = margin;
            }
        }

        public int PageNumber
        {
            get => 
                (int) base.GetValue(PageNumberProperty);
            set => 
                base.SetValue(PageNumberProperty, value);
        }

        public PageControlItemPosition Position
        {
            get => 
                (PageControlItemPosition) base.GetValue(PositionProperty);
            set => 
                base.SetValue(PositionProperty, value);
        }

        public bool IsCoverPage
        {
            get => 
                (bool) base.GetValue(IsCoverPageProperty);
            set => 
                base.SetValue(IsCoverPageProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.PageWrapper PageWrapper
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PageWrapper) base.GetValue(PageWrapperProperty);
            set => 
                base.SetValue(PageWrapperProperty, value);
        }

        protected bool IsVertical
        {
            get
            {
                Func<IDocumentViewerControl, BehaviorProvider> evaluator = <>c.<>9__25_0;
                if (<>c.<>9__25_0 == null)
                {
                    Func<IDocumentViewerControl, BehaviorProvider> local1 = <>c.<>9__25_0;
                    evaluator = <>c.<>9__25_0 = x => x.ActualBehaviorProvider;
                }
                Func<BehaviorProvider, bool> func2 = <>c.<>9__25_1;
                if (<>c.<>9__25_1 == null)
                {
                    Func<BehaviorProvider, bool> local2 = <>c.<>9__25_1;
                    func2 = <>c.<>9__25_1 = x => ((x.RotateAngle / 90.0) % 2.0) == 0.0;
                }
                return this.DocumentViewer.With<IDocumentViewerControl, BehaviorProvider>(evaluator).Return<BehaviorProvider, bool>(func2, (<>c.<>9__25_2 ??= () => true));
            }
        }

        protected IDocumentViewerControl DocumentViewer =>
            DocumentViewerControl.GetActualViewer(this);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageControlItem.<>c <>9 = new PageControlItem.<>c();
            public static Func<IDocumentViewerControl, BehaviorProvider> <>9__25_0;
            public static Func<BehaviorProvider, bool> <>9__25_1;
            public static Func<bool> <>9__25_2;
            public static Func<IPage, int> <>9__28_0;
            public static Func<int> <>9__28_1;
            public static Func<IDocumentViewerControl, double> <>9__29_0;
            public static Func<double> <>9__29_1;
            public static Func<IDocumentViewerControl, double> <>9__29_2;
            public static Func<double> <>9__29_3;
            public static Func<IDocumentViewerControl, double> <>9__29_4;
            public static Func<double> <>9__29_5;
            public static Func<IDocumentViewerControl, double> <>9__29_6;
            public static Func<double> <>9__29_7;

            internal void <.cctor>b__31_0(PageControlItem d, PageControlItemPosition oldValue, PageControlItemPosition newValue)
            {
                d.OnPositionChanged(newValue);
            }

            internal void <.cctor>b__31_1(PageControlItem d, bool oldValue, bool newValue)
            {
                d.OnIsCoverPageChanged(newValue);
            }

            internal void <.cctor>b__31_2(PageControlItem d, PageWrapper oldValue, PageWrapper newValue)
            {
                d.OnPageWrapperChanged(newValue);
            }

            internal BehaviorProvider <get_IsVertical>b__25_0(IDocumentViewerControl x) => 
                x.ActualBehaviorProvider;

            internal bool <get_IsVertical>b__25_1(BehaviorProvider x) => 
                ((x.RotateAngle / 90.0) % 2.0) == 0.0;

            internal bool <get_IsVertical>b__25_2() => 
                true;

            internal int <OnDataContextChanged>b__28_0(IPage x) => 
                x.PageIndex;

            internal int <OnDataContextChanged>b__28_1() => 
                0;

            internal double <UpdateMargin>b__29_0(IDocumentViewerControl x) => 
                x.HorizontalPageSpacing;

            internal double <UpdateMargin>b__29_1() => 
                0.0;

            internal double <UpdateMargin>b__29_2(IDocumentViewerControl x) => 
                x.HorizontalPageSpacing;

            internal double <UpdateMargin>b__29_3() => 
                0.0;

            internal double <UpdateMargin>b__29_4(IDocumentViewerControl x) => 
                x.HorizontalPageSpacing;

            internal double <UpdateMargin>b__29_5() => 
                0.0;

            internal double <UpdateMargin>b__29_6(IDocumentViewerControl x) => 
                x.HorizontalPageSpacing;

            internal double <UpdateMargin>b__29_7() => 
                0.0;
        }
    }
}

