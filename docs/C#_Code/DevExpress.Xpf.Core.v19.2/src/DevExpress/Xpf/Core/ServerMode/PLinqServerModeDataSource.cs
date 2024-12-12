namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.PLinq;
    using DevExpress.Xpf.Core.DataSources;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows;

    public class PLinqServerModeDataSource : PLinqDataSourceBase
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty ElementTypeProperty;
        public static readonly DependencyProperty ListSourceProperty;

        static PLinqServerModeDataSource()
        {
            Type ownerType = typeof(PLinqServerModeDataSource);
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(PLinqServerModeDataSource.OnDefaultSortingChanged)));
            ElementTypeProperty = DependencyProperty.Register("ElementType", typeof(Type), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(PLinqServerModeDataSource.OnElementTypeChanged)));
            ListSourceProperty = DependencyProperty.Register("ListSource", typeof(IListSource), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(PLinqServerModeDataSource.OnListSourceChanged)));
        }

        public PLinqServerModeDataSource()
        {
            this.ResetPLinqServerModeSource();
        }

        protected override Type GetDataObjectType()
        {
            IEnumerable enumerable = (this.ListSource != null) ? this.ListSource.GetList() : base.ItemsSource;
            Type elementType = this.ElementType;
            Type type2 = elementType;
            if (elementType == null)
            {
                Type local1 = elementType;
                type2 = DataSourceHelper.ExtractEnumerableType(enumerable);
            }
            return type2;
        }

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.PLinqServerModeDataSource.png";

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PLinqServerModeSource data = ((PLinqServerModeDataSource) d).Data as PLinqServerModeSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnElementTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PLinqServerModeDataSource element = (PLinqServerModeDataSource) d;
            PLinqServerModeSource data = element.Data as PLinqServerModeSource;
            if (data != null)
            {
                data.ElementType = (Type) e.NewValue;
            }
            if (DesignerProperties.GetIsInDesignMode(element))
            {
                element.UpdateData();
            }
        }

        private static void OnListSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PLinqServerModeDataSource) d).UpdateData();
        }

        public void Reload()
        {
            if (base.Data != null)
            {
                ((PLinqServerModeSource) base.Data).Reload();
            }
        }

        private void ResetPLinqServerModeSource()
        {
            if (base.Data != null)
            {
                ((PLinqServerModeSource) base.Data).Dispose();
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                PLinqServerModeSource source2 = new PLinqServerModeSource {
                    DefaultSorting = this.DefaultSorting,
                    ElementType = this.ElementType
                };
                if (base.ItemsSource != null)
                {
                    source2.Source = base.ItemsSource;
                }
                if (this.ListSource != null)
                {
                    source2.Source = this.ListSource.GetList();
                }
                base.Data = source2;
            }
        }

        protected override object UpdateDataCore()
        {
            this.ResetPLinqServerModeSource();
            return base.Data;
        }

        [Category("Data")]
        public string DefaultSorting
        {
            get => 
                (string) base.GetValue(DefaultSortingProperty);
            set => 
                base.SetValue(DefaultSortingProperty, value);
        }

        [Category("Data")]
        public Type ElementType
        {
            get => 
                (Type) base.GetValue(ElementTypeProperty);
            set => 
                base.SetValue(ElementTypeProperty, value);
        }

        [Category("Data")]
        public IListSource ListSource
        {
            get => 
                (IListSource) base.GetValue(ListSourceProperty);
            set => 
                base.SetValue(ListSourceProperty, value);
        }
    }
}

