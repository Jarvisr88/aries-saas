namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CalculatorGauge : Control
    {
        public static readonly DependencyProperty TextProperty;

        static CalculatorGauge()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CalculatorGauge), new PropertyMetadata((d, e) => ((CalculatorGauge) d).PropertyChangedText((string) e.OldValue)));
        }

        public CalculatorGauge()
        {
            this.SetDefaultStyleKey(typeof(CalculatorGauge));
        }

        protected virtual CalculatorGaugeSegment CreateSegment() => 
            new CalculatorGaugeSegment();

        protected virtual string GetTextToDisplay()
        {
            if (this.Text == null)
            {
                return null;
            }
            int startIndex = 0;
            string decimalSeparator = this.DecimalSeparator;
            while (startIndex < this.Text.Length)
            {
                if (this.Text.IndexOf(decimalSeparator, startIndex) == startIndex)
                {
                    startIndex += decimalSeparator.Length;
                    continue;
                }
                if ((this.Text[startIndex] != '-') && !char.IsDigit(this.Text[startIndex]))
                {
                    return "Error";
                }
                startIndex++;
            }
            return this.Text;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SegmentPanel = base.GetTemplateChild("ElementSegmentPanel") as Panel;
            this.UpdateSegments();
        }

        protected virtual void PropertyChangedText(string oldValue)
        {
            this.UpdateSegments();
        }

        protected virtual void UpdateSegments()
        {
            if (this.SegmentPanel != null)
            {
                string textToDisplay = this.GetTextToDisplay();
                if (textToDisplay == null)
                {
                    this.SegmentPanel.Children.Clear();
                }
                else
                {
                    int num = 0;
                    int startIndex = 0;
                    string decimalSeparator = this.DecimalSeparator;
                    while (startIndex < textToDisplay.Length)
                    {
                        if (textToDisplay.IndexOf(decimalSeparator, startIndex) == 0)
                        {
                            throw new Exception();
                        }
                        if (this.SegmentPanel.Children.Count == num)
                        {
                            this.SegmentPanel.Children.Add(this.CreateSegment());
                        }
                        CalculatorGaugeSegment segment = (CalculatorGaugeSegment) this.SegmentPanel.Children[num];
                        num++;
                        segment.Char = textToDisplay[startIndex];
                        startIndex++;
                        segment.ShowDot = textToDisplay.IndexOf(decimalSeparator, startIndex) == startIndex;
                        if (segment.ShowDot)
                        {
                            startIndex += decimalSeparator.Length;
                        }
                    }
                    while (this.SegmentPanel.Children.Count > num)
                    {
                        this.SegmentPanel.Children.RemoveAt(this.SegmentPanel.Children.Count - 1);
                    }
                }
            }
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        private string DecimalSeparator =>
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        private Panel SegmentPanel { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalculatorGauge.<>c <>9 = new CalculatorGauge.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalculatorGauge) d).PropertyChangedText((string) e.OldValue);
            }
        }
    }
}

