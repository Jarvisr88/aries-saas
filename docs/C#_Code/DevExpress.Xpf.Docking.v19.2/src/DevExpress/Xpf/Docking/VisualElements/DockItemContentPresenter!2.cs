namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DockItemContentPresenter<TVisual, TLogical> : BasePanePresenter<TVisual, TLogical> where TVisual: DependencyObject, IDisposable where TLogical: BaseLayoutItem
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsControlItemsHostProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsDataBoundProperty;

        static DockItemContentPresenter()
        {
            DependencyPropertyRegistrator<DockItemContentPresenter<TVisual, TLogical>> registrator = new DependencyPropertyRegistrator<DockItemContentPresenter<TVisual, TLogical>>();
            registrator.Register<bool>("IsControlItemsHost", ref DockItemContentPresenter<TVisual, TLogical>.IsControlItemsHostProperty, false, (dObj, ea) => ((DockItemContentPresenter<TVisual, TLogical>) dObj).OnIsControlItemsHostChanged((bool) ea.NewValue), null);
            registrator.Register<bool>("IsDataBound", ref DockItemContentPresenter<TVisual, TLogical>.IsDataBoundProperty, false, (dObj, ea) => ((DockItemContentPresenter<TVisual, TLogical>) dObj).OnIsDataBoundChanged((bool) ea.NewValue), null);
        }

        protected DockItemContentPresenter()
        {
        }

        protected override TLogical ConvertToLogicalItem(object content)
        {
            TLogical local1 = LayoutItemData.ConvertToBaseLayoutItem(content) as TLogical;
            TLogical local3 = local1;
            if (local1 == null)
            {
                TLogical local2 = local1;
                local3 = base.ConvertToLogicalItem(content);
            }
            return local3;
        }

        protected virtual void OnIsControlItemsHostChanged(bool value)
        {
            base.EnsureOwner(base.Owner);
        }

        protected virtual void OnIsDataBoundChanged(bool value)
        {
            base.EnsureOwner(base.Owner);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockItemContentPresenter<TVisual, TLogical>.<>c <>9;

            static <>c()
            {
                DockItemContentPresenter<TVisual, TLogical>.<>c.<>9 = new DockItemContentPresenter<TVisual, TLogical>.<>c();
            }

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockItemContentPresenter<TVisual, TLogical>) dObj).OnIsControlItemsHostChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockItemContentPresenter<TVisual, TLogical>) dObj).OnIsDataBoundChanged((bool) ea.NewValue);
            }
        }
    }
}

