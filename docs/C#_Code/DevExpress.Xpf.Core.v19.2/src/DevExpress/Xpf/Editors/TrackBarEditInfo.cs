namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class TrackBarEditInfo : RangeEditBaseInfo
    {
        public static readonly DependencyProperty SelectionLengthProperty;
        public static readonly DependencyProperty ReservedSpaceProperty;

        static TrackBarEditInfo()
        {
            Type ownerType = typeof(TrackBarEditInfo);
            SelectionLengthProperty = DependencyPropertyManager.Register("SelectionLength", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ReservedSpaceProperty = DependencyPropertyManager.Register("ReservedSpace", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
        }

        public double SelectionLength
        {
            get => 
                (double) base.GetValue(SelectionLengthProperty);
            set => 
                base.SetValue(SelectionLengthProperty, value);
        }

        public double ReservedSpace
        {
            get => 
                (double) base.GetValue(ReservedSpaceProperty);
            set => 
                base.SetValue(ReservedSpaceProperty, value);
        }
    }
}

