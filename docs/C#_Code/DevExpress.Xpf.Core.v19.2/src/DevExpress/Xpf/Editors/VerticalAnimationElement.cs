namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class VerticalAnimationElement : AnimationElementBase
    {
        protected override DoubleAnimation CreateAnimation()
        {
            double actualHeight = base.ActualHeight;
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = new double?(actualHeight);
            animation1.To = new double?(-this.ElementHeight);
            animation1.RepeatBehavior = RepeatBehavior.Forever;
            animation1.Duration = base.GetAnimationDuration(actualHeight);
            DoubleAnimation element = animation1;
            Storyboard.SetTargetProperty(element, new PropertyPath(Canvas.TopProperty));
            return element;
        }

        protected override void UpdateContent(FrameworkElement content)
        {
            content.Width = base.ActualWidth;
            content.Height = this.ElementHeight;
        }

        private double ElementHeight =>
            base.ActualHeight / 5.0;
    }
}

