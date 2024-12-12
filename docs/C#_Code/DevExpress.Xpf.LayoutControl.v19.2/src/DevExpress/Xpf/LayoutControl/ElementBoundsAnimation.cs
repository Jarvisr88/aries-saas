namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class ElementBoundsAnimation
    {
        protected static readonly DependencyProperty ClipRectWidthProperty = DependencyProperty.RegisterAttached("ClipRectWidth", typeof(double), typeof(ElementBoundsAnimation), new PropertyMetadata(new PropertyChangedCallback(ElementBoundsAnimation.OnClipRectSizeChanged)));
        protected static readonly DependencyProperty ClipRectHeightProperty = DependencyProperty.RegisterAttached("ClipRectHeight", typeof(double), typeof(ElementBoundsAnimation), new PropertyMetadata(new PropertyChangedCallback(ElementBoundsAnimation.OnClipRectSizeChanged)));

        public ElementBoundsAnimation(FrameworkElements elements)
        {
            this.Elements = elements;
        }

        protected void AnimateElementPosition(System.Windows.Media.Animation.Storyboard storyboard, FrameworkElement element, Point oldPosition, Point newPosition)
        {
            if (newPosition != oldPosition)
            {
                element.RenderTransform = new TranslateTransform();
                storyboard.Children.Add(this.CreateDoubleAnimation(element, "(RenderTransform).X", oldPosition.X - newPosition.X, 0.0));
                storyboard.Children.Add(this.CreateDoubleAnimation(element, "(RenderTransform).Y", oldPosition.Y - newPosition.Y, 0.0));
            }
        }

        protected void AnimateElementSize(System.Windows.Media.Animation.Storyboard storyboard, FrameworkElement element, Size oldSize, Size newSize)
        {
            if ((newSize != oldSize) && ((newSize.Width >= oldSize.Width) && (newSize.Height >= oldSize.Height)))
            {
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = oldSize.ToRect();
                element.Clip = geometry1;
                storyboard.Children.Add(this.CreateDoubleAnimation(element, ClipRectWidthProperty, oldSize.Width, newSize.Width));
                storyboard.Children.Add(this.CreateDoubleAnimation(element, ClipRectHeightProperty, oldSize.Height, newSize.Height));
            }
        }

        public bool Begin(TimeSpan duration, IEasingFunction easingFunction = null, Action onCompleted = null)
        {
            if (this.IsActive)
            {
                return false;
            }
            this.Storyboard = new System.Windows.Media.Animation.Storyboard();
            foreach (FrameworkElement element in this.OldElementBounds.Keys)
            {
                Rect rect = this.OldElementBounds[element];
                Rect rect2 = this.NewElementBounds[element];
                this.AnimateElementPosition(this.Storyboard, element, rect.Location(), rect2.Location());
                this.AnimateElementSize(this.Storyboard, element, rect.Size(), rect2.Size());
            }
            if (this.Storyboard.Children.Count == 0)
            {
                this.Storyboard = null;
                if (onCompleted != null)
                {
                    onCompleted();
                }
                return false;
            }
            foreach (DoubleAnimation animation in this.Storyboard.Children)
            {
                animation.Duration = duration;
                animation.EasingFunction = easingFunction;
            }
            this.StoryboardCompleted = delegate {
                this.Storyboard.Stop();
                foreach (FrameworkElement element in this.OldElementBounds.Keys)
                {
                    if (element.IsPropertyAssigned(UIElement.RenderTransformProperty))
                    {
                        element.ClearValue(UIElement.RenderTransformProperty);
                    }
                    if (element.IsPropertyAssigned(UIElement.ClipProperty))
                    {
                        element.ClearValue(UIElement.ClipProperty);
                    }
                }
                this.StoryboardCompleted = null;
                this.Storyboard = null;
                if (onCompleted != null)
                {
                    onCompleted();
                }
            };
            this.Storyboard.Completed += delegate (object <sender>, EventArgs <e>) {
                if (this.StoryboardCompleted != null)
                {
                    this.StoryboardCompleted();
                }
            };
            this.Storyboard.Begin();
            return true;
        }

        protected DoubleAnimation CreateDoubleAnimation(FrameworkElement element, object property, double from, double to)
        {
            DoubleAnimation animation = new DoubleAnimation();
            System.Windows.Media.Animation.Storyboard.SetTarget(animation, element);
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(animation, (property is string) ? new PropertyPath((string) property, new object[0]) : new PropertyPath(property));
            animation.From = new double?(from);
            animation.To = new double?(to);
            return animation;
        }

        protected ElementBounds GetElementBounds(FrameworkElement relativeTo = null)
        {
            ElementBounds bounds = new ElementBounds();
            foreach (FrameworkElement element in this.Elements)
            {
                bounds.Add(element, (relativeTo == null) ? element.GetBounds() : element.GetBounds(relativeTo));
            }
            return bounds;
        }

        private static void OnClipRectSizeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (!o.HasDefaultValue(e.Property))
            {
                UIElement element = o as UIElement;
                if (element != null)
                {
                    RectangleGeometry clip = element.Clip as RectangleGeometry;
                    if (clip != null)
                    {
                        Rect rect = clip.Rect;
                        if (ReferenceEquals(e.Property, ClipRectWidthProperty))
                        {
                            rect.Width = (double) e.NewValue;
                        }
                        else
                        {
                            rect.Height = (double) e.NewValue;
                        }
                        clip.Rect = rect;
                    }
                }
            }
        }

        public void Stop()
        {
            if (this.IsActive)
            {
                this.Storyboard.SkipToFill();
                this.StoryboardCompleted();
            }
        }

        public void StoreNewElementBounds(FrameworkElement relativeTo = null)
        {
            this.NewElementBounds = this.GetElementBounds(relativeTo);
        }

        public void StoreOldElementBounds(FrameworkElement relativeTo = null)
        {
            this.OldElementBounds = this.GetElementBounds(relativeTo);
        }

        public bool IsActive =>
            this.Storyboard != null;

        protected FrameworkElements Elements { get; private set; }

        protected ElementBounds NewElementBounds { get; private set; }

        protected ElementBounds OldElementBounds { get; private set; }

        protected System.Windows.Media.Animation.Storyboard Storyboard { get; private set; }

        protected Action StoryboardCompleted { get; private set; }

        protected class ElementBounds : Dictionary<FrameworkElement, Rect>
        {
        }
    }
}

