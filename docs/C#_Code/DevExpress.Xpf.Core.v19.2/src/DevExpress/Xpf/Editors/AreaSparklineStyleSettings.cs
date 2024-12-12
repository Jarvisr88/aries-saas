namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class AreaSparklineStyleSettings : LineSparklineStyleSettings
    {
        public static readonly DependencyProperty AreaOpacityProperty;

        static AreaSparklineStyleSettings()
        {
            Type ownerType = typeof(AreaSparklineStyleSettings);
            AreaOpacityProperty = DependencyPropertyManager.Register("AreaOpacity", typeof(double), ownerType, new FrameworkPropertyMetadata(0.52941176470588236));
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((SparklinePropertyProvider) editor.PropertyProvider).AreaOpacity = this.AreaOpacity;
        }

        public double AreaOpacity
        {
            get => 
                (double) base.GetValue(AreaOpacityProperty);
            set => 
                base.SetValue(AreaOpacityProperty, value);
        }

        protected override SparklineViewType ViewType =>
            SparklineViewType.Area;
    }
}

