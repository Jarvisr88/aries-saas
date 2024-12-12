namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScrollBarAnnotationsAppearance : DependencyObject
    {
        public static readonly DependencyProperty FocusedRowProperty;
        public static readonly DependencyProperty InvalidRowsProperty;
        public static readonly DependencyProperty InvalidCellsProperty;
        public static readonly DependencyProperty SelectedProperty;
        public static readonly DependencyProperty SearchResultProperty;

        static ScrollBarAnnotationsAppearance()
        {
            Type ownerType = typeof(ScrollBarAnnotationsAppearance);
            FocusedRowProperty = DependencyProperty.Register("FocusedRow", typeof(ScrollBarAnnotationInfo), ownerType, new PropertyMetadata(null, (d, e) => ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.FocusedRow, e)));
            InvalidRowsProperty = DependencyProperty.Register("InvalidRows", typeof(ScrollBarAnnotationInfo), ownerType, new PropertyMetadata(null, (d, e) => ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.InvalidRows, e)));
            InvalidCellsProperty = DependencyProperty.Register("InvalidCells", typeof(ScrollBarAnnotationInfo), ownerType, new PropertyMetadata(null, (d, e) => ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.InvalidCells, e)));
            SelectedProperty = DependencyProperty.Register("Selected", typeof(ScrollBarAnnotationInfo), ownerType, new PropertyMetadata(null, (d, e) => ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.Selected, e)));
            SearchResultProperty = DependencyProperty.Register("SearchResult", typeof(ScrollBarAnnotationInfo), ownerType, new PropertyMetadata(null, (d, e) => ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.SearchResult, e)));
        }

        private void ChangedAppearance(ScrollBarAnnotationMode mode, DependencyPropertyChangedEventArgs e)
        {
            ITableView view = ((this.Owner == null) || !this.Owner.IsAlive) ? null : (this.Owner.Target as ITableView);
            if (view != null)
            {
                view.ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(mode, false);
            }
            ScrollBarAnnotationInfo newValue = e.NewValue as ScrollBarAnnotationInfo;
            if (newValue != null)
            {
                newValue.Owner = this.Owner;
                newValue.Mode = new ScrollBarAnnotationMode?(mode);
            }
        }

        protected internal WeakReference Owner { get; set; }

        public ScrollBarAnnotationInfo FocusedRow
        {
            get => 
                (ScrollBarAnnotationInfo) base.GetValue(FocusedRowProperty);
            set => 
                base.SetValue(FocusedRowProperty, value);
        }

        public ScrollBarAnnotationInfo InvalidRows
        {
            get => 
                (ScrollBarAnnotationInfo) base.GetValue(InvalidRowsProperty);
            set => 
                base.SetValue(InvalidRowsProperty, value);
        }

        public ScrollBarAnnotationInfo InvalidCells
        {
            get => 
                (ScrollBarAnnotationInfo) base.GetValue(InvalidCellsProperty);
            set => 
                base.SetValue(InvalidCellsProperty, value);
        }

        public ScrollBarAnnotationInfo Selected
        {
            get => 
                (ScrollBarAnnotationInfo) base.GetValue(SelectedProperty);
            set => 
                base.SetValue(SelectedProperty, value);
        }

        public ScrollBarAnnotationInfo SearchResult
        {
            get => 
                (ScrollBarAnnotationInfo) base.GetValue(SearchResultProperty);
            set => 
                base.SetValue(SearchResultProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollBarAnnotationsAppearance.<>c <>9 = new ScrollBarAnnotationsAppearance.<>c();

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.FocusedRow, e);
            }

            internal void <.cctor>b__9_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.InvalidRows, e);
            }

            internal void <.cctor>b__9_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.InvalidCells, e);
            }

            internal void <.cctor>b__9_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.Selected, e);
            }

            internal void <.cctor>b__9_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationsAppearance) d).ChangedAppearance(ScrollBarAnnotationMode.SearchResult, e);
            }
        }
    }
}

