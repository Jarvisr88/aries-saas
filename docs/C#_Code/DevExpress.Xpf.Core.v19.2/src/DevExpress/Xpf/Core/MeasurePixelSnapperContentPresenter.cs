namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [Browsable(false)]
    public class MeasurePixelSnapperContentPresenter : ContentPresenter
    {
        static MeasurePixelSnapperContentPresenter()
        {
            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(MeasurePixelSnapperContentPresenter), new FrameworkPropertyMetadata(null, (d, e) => ((MeasurePixelSnapperContentPresenter) d).OnDataContextChanged(e), (d, e) => ((MeasurePixelSnapperContentPresenter) d).CoerceDataContext(e)));
        }

        protected virtual object CoerceDataContext(object e) => 
            ((this.Snapper == null) || (this.Snapper.ContentTemplate == null)) ? e : this.Snapper.DataContext;

        protected virtual void OnDataContextChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected internal MeasurePixelSnapperContentControl Snapper { protected get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MeasurePixelSnapperContentPresenter.<>c <>9 = new MeasurePixelSnapperContentPresenter.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MeasurePixelSnapperContentPresenter) d).OnDataContextChanged(e);
            }

            internal object <.cctor>b__4_1(DependencyObject d, object e) => 
                ((MeasurePixelSnapperContentPresenter) d).CoerceDataContext(e);
        }
    }
}

