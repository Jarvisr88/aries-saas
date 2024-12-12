namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class ErrorControl : ContentControl
    {
        private FrameworkElement contentPresenter;
        private Storyboard showErrorStoryboard;
        private Storyboard hideErrorStoryboard;

        static ErrorControl()
        {
            Type forType = typeof(ErrorControl);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            DataObjectBase.RaiseResetEventWhenObjectIsLoadedProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(true));
        }

        public ErrorControl()
        {
            DataObjectBase.AddResetHandler(this, new RoutedEventHandler(this.Reset));
            base.Loaded += new RoutedEventHandler(this.ErrorControl_Loaded);
        }

        private void ErrorControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateError();
        }

        private void HideError()
        {
            if (this.contentPresenter != null)
            {
                this.hideErrorStoryboard = this.contentPresenter.FindResource("hideErrorStoryboard") as Storyboard;
                if (this.hideErrorStoryboard != null)
                {
                    this.hideErrorStoryboard.Begin();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.contentPresenter = base.GetTemplateChild("content") as FrameworkElement;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, ContentControl.HasContentProperty))
            {
                this.UpdateError();
            }
        }

        private void Reset(object sender, RoutedEventArgs args)
        {
            if (this.showErrorStoryboard != null)
            {
                this.showErrorStoryboard.SkipToFill();
            }
            if (this.hideErrorStoryboard != null)
            {
                this.hideErrorStoryboard.SkipToFill();
            }
        }

        private void ShowError()
        {
            if (this.contentPresenter != null)
            {
                this.showErrorStoryboard = this.contentPresenter.FindResource("showErrorStoryboard") as Storyboard;
                if (this.showErrorStoryboard != null)
                {
                    this.showErrorStoryboard.Begin();
                }
            }
        }

        private void UpdateError()
        {
            if (base.HasContent)
            {
                this.ShowError();
            }
            else
            {
                this.HideError();
            }
        }
    }
}

