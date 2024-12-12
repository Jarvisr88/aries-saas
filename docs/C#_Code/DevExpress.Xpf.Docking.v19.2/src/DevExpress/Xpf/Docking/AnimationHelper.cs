namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    internal static class AnimationHelper
    {
        public static void BeginAnimation(DependencyObject dObj, DependencyProperty property, Timeline animation)
        {
            if (animation != null)
            {
                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget(animation, dObj);
                Storyboard.SetTargetProperty(animation, new PropertyPath(property));
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
        }
    }
}

