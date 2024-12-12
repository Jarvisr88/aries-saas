namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class ContentContainer : DXContentPresenter
    {
        public static readonly DependencyProperty ForegroundSolidColor1Property;
        public static readonly DependencyProperty ForegroundSolidColor2Property;
        public static readonly DependencyProperty ForegroundSolidColor3Property;
        public static readonly DependencyProperty ForegroundSolidColor4Property;
        public static readonly DependencyProperty ForegroundSolidColor5Property;
        public static readonly DependencyProperty ForegroundSolidColor6Property;
        private static List<DependencyProperty> foregroundSolidColorProperties;

        static ContentContainer()
        {
            ForegroundSolidColor1Property = DependencyProperty.Register("ForegroundSolidColor1", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            ForegroundSolidColor2Property = DependencyProperty.Register("ForegroundSolidColor2", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            ForegroundSolidColor3Property = DependencyProperty.Register("ForegroundSolidColor3", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            ForegroundSolidColor4Property = DependencyProperty.Register("ForegroundSolidColor4", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            ForegroundSolidColor5Property = DependencyProperty.Register("ForegroundSolidColor5", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            ForegroundSolidColor6Property = DependencyProperty.Register("ForegroundSolidColor6", typeof(Color), typeof(ContentContainer), new PropertyMetadata((d, e) => ((ContentContainer) d).UpdateForeground()));
            List<DependencyProperty> list1 = new List<DependencyProperty>();
            list1.Add(ForegroundSolidColor1Property);
            list1.Add(ForegroundSolidColor2Property);
            list1.Add(ForegroundSolidColor3Property);
            list1.Add(ForegroundSolidColor4Property);
            list1.Add(ForegroundSolidColor5Property);
            list1.Add(ForegroundSolidColor6Property);
            foregroundSolidColorProperties = list1;
        }

        public ContentContainer()
        {
            FocusHelper2.SetFocusable(this, false);
        }

        private void SetSolidForeground(Color color)
        {
            if (color == Colors.Transparent)
            {
                base.ClearValue(Control.ForegroundProperty);
            }
            else
            {
                base.Foreground = new SolidColorBrush(color);
            }
        }

        private void UpdateForeground()
        {
            using (List<DependencyProperty>.Enumerator enumerator = foregroundSolidColorProperties.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    DependencyProperty current = enumerator.Current;
                    ValueSource valueSource = DependencyPropertyHelper.GetValueSource(this, current);
                    if (valueSource.BaseValueSource == BaseValueSource.Default)
                    {
                        if (!valueSource.IsAnimated)
                        {
                            continue;
                        }
                        Color color = new Color();
                        if (!(((Color) base.GetValue(current)) != color))
                        {
                            continue;
                        }
                    }
                    this.SetSolidForeground((Color) base.GetValue(current));
                    return;
                }
            }
            base.ClearValue(Control.ForegroundProperty);
        }

        public Color ForegroundSolidColor1
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor1Property);
            set => 
                base.SetValue(ForegroundSolidColor1Property, value);
        }

        public Color ForegroundSolidColor2
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor2Property);
            set => 
                base.SetValue(ForegroundSolidColor2Property, value);
        }

        public Color ForegroundSolidColor3
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor3Property);
            set => 
                base.SetValue(ForegroundSolidColor3Property, value);
        }

        public Color ForegroundSolidColor4
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor4Property);
            set => 
                base.SetValue(ForegroundSolidColor4Property, value);
        }

        public Color ForegroundSolidColor5
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor5Property);
            set => 
                base.SetValue(ForegroundSolidColor5Property, value);
        }

        public Color ForegroundSolidColor6
        {
            get => 
                (Color) base.GetValue(ForegroundSolidColor6Property);
            set => 
                base.SetValue(ForegroundSolidColor6Property, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentContainer.<>c <>9 = new ContentContainer.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }

            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }

            internal void <.cctor>b__7_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }

            internal void <.cctor>b__7_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }

            internal void <.cctor>b__7_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ContentContainer) d).UpdateForeground();
            }
        }
    }
}

