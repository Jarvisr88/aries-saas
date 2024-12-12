namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Markup;

    public class BrickPagePairToDisplayTextConverter : MarkupExtension, IMultiValueConverter
    {
        private const int CharsCount = 80;
        private const int CharCountBeforeMarchedText = 20;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Func<object, bool> predicate = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__2_0;
                predicate = <>c.<>9__2_0 = x => (x == null) || (x == DependencyProperty.UnsetValue);
            }
            if (values.Any<object>(predicate))
            {
                return null;
            }
            bool flag = (bool) values[2];
            string str = (string) values[3];
            VisualBrick brick = ((BrickPagePair) values[1]).GetBrick(((PageList) values[0])) as VisualBrick;
            string input = null;
            if (brick != null)
            {
                string text = brick.Text;
                int index = brick.Text.IndexOf(str, flag ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                if (index <= 20)
                {
                    input = text.Substring(0, Math.Min(80, text.Length));
                }
                else
                {
                    int num2 = index;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= 20)
                        {
                            index = (num2 == index) ? (index - 20) : (num2 + 1);
                            input = "..." + text.Substring(index, Math.Min(0x4d, text.Length - index));
                            break;
                        }
                        if (char.IsWhiteSpace(text[index - num3]))
                        {
                            num2 = index - num3;
                        }
                        num3++;
                    }
                }
            }
            if (input == null)
            {
                return null;
            }
            List<Inline> list2 = new List<Inline>();
            string[] strArray = Regex.Split(input, str, RegexOptions.IgnoreCase);
            Match match = Regex.Match(input, str, RegexOptions.IgnoreCase);
            for (int i = 0; i < strArray.Length; i++)
            {
                list2.Add(new System.Windows.Documents.Run(strArray[i]));
                if (i < (strArray.Length - 1))
                {
                    System.Windows.Documents.Run item = new System.Windows.Documents.Run(match.Value);
                    item.FontWeight = FontWeights.Bold;
                    list2.Add(item);
                }
            }
            return list2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrickPagePairToDisplayTextConverter.<>c <>9 = new BrickPagePairToDisplayTextConverter.<>c();
            public static Func<object, bool> <>9__2_0;

            internal bool <Convert>b__2_0(object x) => 
                (x == null) || (x == DependencyProperty.UnsetValue);
        }
    }
}

