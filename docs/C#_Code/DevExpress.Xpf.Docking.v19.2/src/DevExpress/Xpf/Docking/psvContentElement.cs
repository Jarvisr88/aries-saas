namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class psvContentElement : DependencyObject
    {
        public void BeginAnimation(DependencyProperty property, Timeline animation)
        {
            AnimationHelper.BeginAnimation(this, property, animation);
        }
    }
}

