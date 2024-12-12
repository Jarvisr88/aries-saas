namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class FilterControlFocusVisualHelper
    {
        private FrameworkElement focusedElement;
        private Canvas focusVisualContainer;
        private Style focusVisualStyle;

        public FilterControlFocusVisualHelper(Canvas focusVisualContainer, Style focusVisualStyle)
        {
            this.focusVisualContainer = focusVisualContainer;
            this.focusVisualStyle = focusVisualStyle;
        }

        private FrameworkElement CreateFocusVisual()
        {
            ContentControl control1 = new ContentControl();
            control1.Style = this.focusVisualStyle;
            control1.IsHitTestVisible = false;
            ContentControl control = control1;
            control.Focusable = false;
            return control;
        }

        private void FocusedElementChanged(FrameworkElement oldValue)
        {
            if (oldValue != null)
            {
                this.HideFocusVisual();
                oldValue.LayoutUpdated -= new EventHandler(this.OnFocusedElementLayoutUpdated);
            }
            if (this.FocusedElement != null)
            {
                this.ShowFocusVisual();
                this.FocusedElement.LayoutUpdated += new EventHandler(this.OnFocusedElementLayoutUpdated);
            }
        }

        private void HideFocusVisual()
        {
            if (this.focusVisualContainer != null)
            {
                this.focusVisualContainer.Children.Clear();
            }
        }

        private void OnFocusedElementLayoutUpdated(object sender, EventArgs e)
        {
            this.UpdateFocusVisualBounds();
        }

        private void ShowFocusVisual()
        {
            if (this.focusVisualContainer != null)
            {
                this.FocusedElement.FocusVisualStyle = null;
                FrameworkElement element = this.CreateFocusVisual();
                this.focusVisualContainer.Children.Add(element);
                this.UpdateFocusVisualBounds();
            }
        }

        private void UpdateFocusVisualBounds()
        {
            Rect bounds = this.FocusedElement.GetBounds(this.focusVisualContainer);
            FrameworkElement element = (FrameworkElement) this.focusVisualContainer.Children[0];
            Canvas.SetLeft(element, bounds.Left);
            Canvas.SetTop(element, bounds.Top);
            element.Width = this.FocusedElement.ActualWidth;
            element.Height = this.FocusedElement.ActualHeight;
        }

        public FrameworkElement FocusedElement
        {
            get => 
                this.focusedElement;
            set
            {
                if (!ReferenceEquals(value, this.focusedElement))
                {
                    FrameworkElement focusedElement = this.focusedElement;
                    this.focusedElement = value;
                    this.FocusedElementChanged(focusedElement);
                }
            }
        }
    }
}

