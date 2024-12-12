namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PositionStackPanel : StackPanel
    {
        public static readonly DependencyProperty PositionProperty = DependencyPropertyManager.RegisterAttached("Position", typeof(StackPanelElementPosition), typeof(PositionStackPanel), new FrameworkPropertyMetadata(StackPanelElementPosition.Middle));
        public static readonly DependencyProperty IndexProperty = DependencyPropertyManager.RegisterAttached("Index", typeof(int), typeof(PositionStackPanel), new FrameworkPropertyMetadata(-1));

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Size size = base.ArrangeOverride(arrangeSize);
            this.UpdateChildrenProperties();
            return size;
        }

        public static StackPanelElementPosition CalculatePosition(int index, int count) => 
            (count != 1) ? ((index != 0) ? ((index != (count - 1)) ? StackPanelElementPosition.Middle : StackPanelElementPosition.Last) : StackPanelElementPosition.First) : StackPanelElementPosition.Single;

        public static int GetIndex(DependencyObject element) => 
            (int) element.GetValue(IndexProperty);

        public static StackPanelElementPosition GetPosition(DependencyObject element) => 
            (StackPanelElementPosition) element.GetValue(PositionProperty);

        public static void SetIndex(DependencyObject element, int value)
        {
            element.SetValue(IndexProperty, value);
        }

        public static void SetPosition(DependencyObject element, StackPanelElementPosition value)
        {
            element.SetValue(PositionProperty, value);
        }

        private void UpdateChildrenProperties()
        {
            if (base.Children.Count != 0)
            {
                for (int i = 0; i < base.Children.Count; i++)
                {
                    base.Children[i].SetValue(PositionProperty, CalculatePosition(i, base.Children.Count));
                    base.Children[i].SetValue(IndexProperty, i);
                }
            }
        }
    }
}

