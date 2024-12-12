namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class SharedSizeGroup
    {
        private List<FrameworkElement> elements = new List<FrameworkElement>();

        private double FindHeaderMaxWidth()
        {
            double negativeInfinity = double.NegativeInfinity;
            foreach (FrameworkElement element in this.elements)
            {
                if (element.ActualWidth > negativeInfinity)
                {
                    negativeInfinity = element.ActualWidth;
                }
            }
            return negativeInfinity;
        }

        private void InvalidateWidth()
        {
            double num = this.FindHeaderMaxWidth();
            foreach (FrameworkElement element in this.elements)
            {
                element.Width = num;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateWidth();
        }

        public void RegisterElement(FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            this.elements.Add(element);
            element.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        public void UnregisterElement(FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            this.elements.Remove(element);
            element.SizeChanged -= new SizeChangedEventHandler(this.OnSizeChanged);
        }
    }
}

