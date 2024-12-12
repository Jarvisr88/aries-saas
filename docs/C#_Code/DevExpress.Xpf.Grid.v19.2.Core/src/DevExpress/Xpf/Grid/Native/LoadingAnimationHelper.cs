namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;

    public class LoadingAnimationHelper
    {
        private ISupportLoadingAnimation owner;

        public LoadingAnimationHelper(ISupportLoadingAnimation owner)
        {
            this.owner = owner;
        }

        public void ApplyAnimation()
        {
            if (!this.owner.IsReady)
            {
                this.ApplyNotLoadedAnimation();
            }
            else
            {
                this.ApplyLoadedAnimation();
            }
        }

        private void ApplyLoadedAnimation()
        {
            if (this.owner.Element != null)
            {
                this.owner.Element.ClearValue(UIElement.OpacityProperty);
                if (this.owner.DataView.RowAnimationKind == RowAnimationKind.None)
                {
                    if (this.StoryToLoadedState != null)
                    {
                        this.StoryToLoadedState.Stop();
                        this.StoryToLoadedState = null;
                    }
                }
                else
                {
                    this.StopAnimation();
                    this.ResetCustomAnimationProperties();
                    this.RaiseRowAnimationBegin();
                    RowAnimationKind realAnimationKind = this.GetRealAnimationKind();
                    this.SetStoryboardTargetProperty(realAnimationKind);
                    if (realAnimationKind == RowAnimationKind.Custom)
                    {
                        this.owner.Element.Effect = this.AddedEffect;
                    }
                    this.StoryToLoadedState.Children.Add(this.CreateAnimation(realAnimationKind));
                    Storyboard.SetTargetProperty(this.StoryToLoadedState, this.CreatePropertyPath(realAnimationKind));
                    this.StoryToLoadedState.Begin();
                }
            }
        }

        private void ApplyNotLoadedAnimation()
        {
            if (this.owner.Element != null)
            {
                if (this.StoryToLoadedState != null)
                {
                    this.StoryToLoadedState.Stop();
                }
                if (this.owner.DataView.RowAnimationKind != RowAnimationKind.None)
                {
                    this.owner.Element.Opacity = 0.0;
                }
            }
        }

        private Timeline CreateAnimation(RowAnimationKind animationKind) => 
            (animationKind == RowAnimationKind.Opacity) ? this.CreateOpacityAnimation() : ((animationKind == RowAnimationKind.Custom) ? this.CustomAnimation : null);

        private DoubleAnimation CreateOpacityAnimation() => 
            new DoubleAnimation { 
                From = 0.0,
                To = new double?((double) 1),
                FillBehavior = FillBehavior.Stop,
                Duration = this.owner.DataView.RowOpacityAnimationDuration
            };

        private PropertyPath CreatePropertyPath(RowAnimationKind animationKind) => 
            (animationKind == RowAnimationKind.Opacity) ? new PropertyPath("Opacity", new object[0]) : ((animationKind == RowAnimationKind.Custom) ? this.CustomPropertyPath : null);

        private RowAnimationKind GetRealAnimationKind()
        {
            RowAnimationKind rowAnimationKind = this.owner.DataView.RowAnimationKind;
            if ((rowAnimationKind == RowAnimationKind.Custom) && ((this.CustomPropertyPath == null) || (this.CustomAnimation == null)))
            {
                rowAnimationKind = RowAnimationKind.Opacity;
            }
            return rowAnimationKind;
        }

        private void RaiseRowAnimationBegin()
        {
            if (this.owner.DataView.RowAnimationKind == RowAnimationKind.Custom)
            {
                this.owner.DataView.RaiseRowAnimationBegin(this, this.owner.IsGroupRow);
            }
        }

        private void ResetCustomAnimationProperties()
        {
            this.CustomAnimation = null;
            this.CustomPropertyPath = null;
            this.AddedEffect = null;
        }

        private void SetStoryboardTargetProperty(RowAnimationKind kind)
        {
            if (kind == RowAnimationKind.Opacity)
            {
                Storyboard.SetTarget(this.StoryToLoadedState, this.owner.Element);
            }
            else
            {
                Storyboard.SetTarget(this.StoryToLoadedState, this.owner as DependencyObject);
            }
        }

        private void StopAnimation()
        {
            if (this.StoryToLoadedState == null)
            {
                this.StoryToLoadedState = new Storyboard();
            }
            else
            {
                this.StoryToLoadedState.Stop();
                this.StoryToLoadedState.Children.Clear();
            }
        }

        public Storyboard StoryToLoadedState { get; set; }

        public Timeline CustomAnimation { get; set; }

        public PropertyPath CustomPropertyPath { get; set; }

        public Effect AddedEffect { get; set; }
    }
}

