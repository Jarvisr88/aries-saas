namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class WinLossSparklineStyleSettings : SparklineStyleSettings
    {
        public static readonly DependencyProperty BarDistanceProperty;

        static WinLossSparklineStyleSettings()
        {
            Type ownerType = typeof(WinLossSparklineStyleSettings);
            BarDistanceProperty = DependencyProperty.Register("BarDistance", typeof(int), ownerType, new FrameworkPropertyMetadata(2));
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((SparklinePropertyProvider) editor.PropertyProvider).BarDistance = this.BarDistance;
        }

        protected override SparklineViewType ViewType =>
            SparklineViewType.WinLoss;

        public int BarDistance
        {
            get => 
                (int) base.GetValue(BarDistanceProperty);
            set => 
                base.SetValue(BarDistanceProperty, value);
        }
    }
}

