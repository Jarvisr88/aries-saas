namespace DevExpress.Xpf.Editors.RangeControl
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class CalendarGroupItem : CalendarItemBase
    {
        private TextBlock textContainer;
        private Thickness defaultTextMargin;

        public CalendarGroupItem()
        {
            base.DefaultStyleKey = typeof(CalendarGroupItem);
        }

        internal double GetDefaulTextOffset() => 
            this.defaultTextMargin.Left;

        internal Size GetRealTextSize()
        {
            if (this.textContainer == null)
            {
                return new Size();
            }
            FormattedText text = new FormattedText(base.Text, CultureInfo.CurrentCulture, this.textContainer.FlowDirection, new Typeface(this.textContainer.FontFamily, this.textContainer.FontStyle, this.textContainer.FontWeight, this.textContainer.FontStretch), this.textContainer.FontSize, null);
            return new Size(text.Width, text.Height);
        }

        internal Thickness GetTextMargin() => 
            this.defaultTextMargin;

        public override void OnApplyTemplate()
        {
            this.textContainer = LayoutHelper.FindElementByType(this, typeof(TextBlock)) as TextBlock;
            if (this.textContainer != null)
            {
                this.defaultTextMargin = this.textContainer.Margin;
            }
        }

        internal void SetTextOffset(double offset)
        {
            if (this.textContainer != null)
            {
                this.textContainer.Margin = new Thickness(this.defaultTextMargin.Left + offset, this.defaultTextMargin.Top, this.defaultTextMargin.Right, this.defaultTextMargin.Bottom);
            }
        }
    }
}

