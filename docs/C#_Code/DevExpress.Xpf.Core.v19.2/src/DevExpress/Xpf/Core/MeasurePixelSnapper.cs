namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class MeasurePixelSnapper : Decorator
    {
        public static readonly DependencyProperty SnapperTypeProperty = DependencyPropertyManager.Register("SnapperType", typeof(DevExpress.Xpf.Core.SnapperType), typeof(MeasurePixelSnapper), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.SnapperType.Ceil, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size MeasureOverride(Size constraint) => 
            MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), this.SnapperType);

        public DevExpress.Xpf.Core.SnapperType SnapperType
        {
            get => 
                (DevExpress.Xpf.Core.SnapperType) base.GetValue(SnapperTypeProperty);
            set => 
                base.SetValue(SnapperTypeProperty, value);
        }
    }
}

