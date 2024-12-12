namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class HorizontalAnimationElement : AnimationElementBase
    {
        protected override DoubleAnimation CreateAnimation()
        {
            double actualWidth = base.ActualWidth;
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = new double?(-this.ElementWidth);
            animation1.To = new double?(actualWidth);
            animation1.RepeatBehavior = RepeatBehavior.Forever;
            animation1.Duration = base.GetAnimationDuration(base.ActualWidth);
            DoubleAnimation element = animation1;
            Storyboard.SetTargetProperty(element, new PropertyPath(Canvas.LeftProperty));
            return element;
        }

        protected override void UpdateContent(FrameworkElement content)
        {
            double num = base.ActualWidth / 5.0;
            content.Width = this.ElementWidth;
            content.Height = base.ActualHeight;
        }

        private double ElementWidth =>
            base.ActualWidth / 5.0;
    }
}

