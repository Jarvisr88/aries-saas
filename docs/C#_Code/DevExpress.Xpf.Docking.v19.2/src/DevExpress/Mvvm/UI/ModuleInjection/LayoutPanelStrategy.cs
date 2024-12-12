namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutPanelStrategy : ContentPresenterStrategy<LayoutPanel, LayoutPanelWrapper>
    {
        protected override object GetParentViewModel()
        {
            DependencyObject obj2 = LayoutTreeHelper.GetVisualParents(base.Target, null).FirstOrDefault<DependencyObject>();
            Func<FrameworkElement, object> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<FrameworkElement, object> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.DataContext;
            }
            object local2 = (obj2 as FrameworkElement).With<FrameworkElement, object>(evaluator);
            object local5 = local2;
            if (local2 == null)
            {
                object local3 = local2;
                local5 = (obj2 as FrameworkContentElement).With<FrameworkContentElement, object>(<>c.<>9__0_1 ??= x => x.DataContext);
            }
            return local5;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutPanelStrategy.<>c <>9 = new LayoutPanelStrategy.<>c();
            public static Func<FrameworkElement, object> <>9__0_0;
            public static Func<FrameworkContentElement, object> <>9__0_1;

            internal object <GetParentViewModel>b__0_0(FrameworkElement x) => 
                x.DataContext;

            internal object <GetParentViewModel>b__0_1(FrameworkContentElement x) => 
                x.DataContext;
        }
    }
}

