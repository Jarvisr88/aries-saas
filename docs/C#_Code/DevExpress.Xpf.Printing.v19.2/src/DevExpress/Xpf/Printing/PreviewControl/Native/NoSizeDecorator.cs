namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System.Windows;
    using System.Windows.Controls;

    public class NoSizeDecorator : Decorator
    {
        protected override Size MeasureOverride(Size constraint)
        {
            this.Child.Measure(new Size(0.0, 0.0));
            return new Size(0.0, 0.0);
        }
    }
}

