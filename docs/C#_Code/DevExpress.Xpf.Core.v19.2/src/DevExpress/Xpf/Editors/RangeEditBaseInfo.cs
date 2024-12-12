namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class RangeEditBaseInfo : DependencyObject
    {
        public static readonly DependencyProperty AnimationProgressProperty;
        public static readonly DependencyProperty LayoutInfoProperty;
        public static readonly DependencyProperty LeftSidePositionProperty;
        public static readonly DependencyProperty RightSidePositionProperty;
        public static readonly DependencyProperty DisplayValueProperty;

        static RangeEditBaseInfo()
        {
            Type propertyType = typeof(RangeEditBaseInfo);
            LayoutInfoProperty = DependencyPropertyManager.RegisterAttached("LayoutInfo", propertyType, propertyType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            AnimationProgressProperty = DependencyPropertyManager.Register("AnimationProgress", typeof(double), propertyType);
            LeftSidePositionProperty = DependencyPropertyManager.Register("LeftSidePosition", typeof(double), propertyType);
            RightSidePositionProperty = DependencyPropertyManager.Register("RightSidePosition", typeof(double), propertyType);
            DisplayValueProperty = DependencyPropertyManager.Register("DisplayValue", typeof(double), propertyType);
        }

        public static RangeEditBaseInfo GetLayoutInfo(DependencyObject d) => 
            (RangeEditBaseInfo) d.GetValue(LayoutInfoProperty);

        public static void SetLayoutInfo(DependencyObject d, RangeEditBaseInfo info)
        {
            d.SetValue(LayoutInfoProperty, info);
        }

        public double DisplayValue
        {
            get => 
                (double) base.GetValue(DisplayValueProperty);
            set => 
                base.SetValue(DisplayValueProperty, value);
        }

        public double AnimationProgress
        {
            get => 
                (double) base.GetValue(AnimationProgressProperty);
            set => 
                base.SetValue(AnimationProgressProperty, value);
        }

        public double LeftSidePosition
        {
            get => 
                (double) base.GetValue(LeftSidePositionProperty);
            set => 
                base.SetValue(LeftSidePositionProperty, value);
        }

        public double RightSidePosition
        {
            get => 
                (double) base.GetValue(RightSidePositionProperty);
            set => 
                base.SetValue(RightSidePositionProperty, value);
        }
    }
}

