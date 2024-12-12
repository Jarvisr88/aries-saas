namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class BarSparklineStyleSettings : SparklineStyleSettings
    {
        public static readonly DependencyProperty HighlightNegativePointsProperty;
        public static readonly DependencyProperty BarDistanceProperty;

        static BarSparklineStyleSettings()
        {
            Type ownerType = typeof(BarSparklineStyleSettings);
            HighlightNegativePointsProperty = DependencyProperty.Register("HighlightNegativePoints", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            BarDistanceProperty = DependencyProperty.Register("BarDistance", typeof(int), ownerType, new FrameworkPropertyMetadata(2));
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            SparklinePropertyProvider propertyProvider = (SparklinePropertyProvider) editor.PropertyProvider;
            propertyProvider.HighlightNegativePoints = this.HighlightNegativePoints;
            propertyProvider.BarDistance = this.BarDistance;
        }

        protected override SparklineViewType ViewType =>
            SparklineViewType.Bar;

        [Category("Behavior"), TypeConverter(typeof(BooleanTypeConverter))]
        public bool HighlightNegativePoints
        {
            get => 
                (bool) base.GetValue(HighlightNegativePointsProperty);
            set => 
                base.SetValue(HighlightNegativePointsProperty, value);
        }

        public int BarDistance
        {
            get => 
                (int) base.GetValue(BarDistanceProperty);
            set => 
                base.SetValue(BarDistanceProperty, value);
        }
    }
}

