namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Media;

    internal class BrushSizeTickBar : TickBar
    {
        protected override void OnRender(DrawingContext dc)
        {
            FontFamily fontFamily = TextElement.GetFontFamily(this);
            double fontSize = TextElement.GetFontSize(this);
            SolidColorBrush foreground = new SolidColorBrush(Colors.Black);
            double num2 = base.Maximum - base.Minimum;
            double num3 = base.ReservedSpace * 0.5;
            FormattedText formattedText = null;
            double x = 0.0;
            for (double i = base.Minimum; i <= base.Maximum; i += base.TickFrequency * 2.0)
            {
                formattedText = new FormattedText(i.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), fontSize, foreground);
                if (base.Minimum == i)
                {
                    x = 0.0;
                }
                else
                {
                    double width = formattedText.Width;
                    x += (base.ActualWidth / ((num2 / base.TickFrequency) / 2.0)) - (width / 4.0);
                }
                dc.DrawText(formattedText, new Point(x, 0.0));
            }
        }
    }
}

