namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class EnumMemberInfoPresenter : Control
    {
        public static readonly DependencyProperty EnumMemberInfoProperty = DependencyProperty.Register("EnumMemberInfo", typeof(DevExpress.Mvvm.EnumMemberInfo), typeof(EnumMemberInfoPresenter), new PropertyMetadata(null));

        static EnumMemberInfoPresenter()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumMemberInfoPresenter), new FrameworkPropertyMetadata(typeof(EnumMemberInfoPresenter)));
        }

        public DevExpress.Mvvm.EnumMemberInfo EnumMemberInfo
        {
            get => 
                (DevExpress.Mvvm.EnumMemberInfo) base.GetValue(EnumMemberInfoProperty);
            set => 
                base.SetValue(EnumMemberInfoProperty, value);
        }
    }
}

