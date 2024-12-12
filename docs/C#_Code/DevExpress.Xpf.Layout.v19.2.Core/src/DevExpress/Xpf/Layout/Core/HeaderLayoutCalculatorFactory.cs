namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public static class HeaderLayoutCalculatorFactory
    {
        public static ITabHeaderLayoutCalculator GetCalculator(TabHeaderLayoutType type)
        {
            switch (type)
            {
                case TabHeaderLayoutType.Trim:
                    return new TrimLayoutCalculator();

                case TabHeaderLayoutType.Scroll:
                    return new ScrollLayoutCalculator();

                case TabHeaderLayoutType.MultiLine:
                    return new MultiLineLayoutCalculator();
            }
            return new ScrollLayoutCalculator();
        }
    }
}

