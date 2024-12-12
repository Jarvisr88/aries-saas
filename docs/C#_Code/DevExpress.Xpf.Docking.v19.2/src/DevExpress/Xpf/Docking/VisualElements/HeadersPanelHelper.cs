namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class HeadersPanelHelper
    {
        private static void ApplyMeasure(ITabHeaderInfo[] headers)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo info = headers[i];
                ITabHeader tabHeader = info.TabHeader as ITabHeader;
                if (tabHeader != null)
                {
                    tabHeader.Apply(info);
                }
            }
        }

        private static int Compare(ITabHeaderInfo info1, ITabHeaderInfo info2) => 
            !ReferenceEquals(info1, info2) ? ((BaseHeaderInfo) info1).Index.CompareTo(((BaseHeaderInfo) info2).Index) : 0;

        private static ITabHeaderInfo[] GetHeaderInfos(UIElementCollection children, Size size)
        {
            ITabHeaderInfo[] array = new ITabHeaderInfo[children.Count];
            for (int i = 0; i < array.Length; i++)
            {
                ITabHeader header = children[i] as ITabHeader;
                if (header != null)
                {
                    array[i] = header.CreateInfo(size);
                }
            }
            Array.Sort<ITabHeaderInfo>(array, new Comparison<ITabHeaderInfo>(HeadersPanelHelper.Compare));
            return array;
        }

        public static Orientation GetOrientation(CaptionLocation captionLocation) => 
            ((captionLocation == CaptionLocation.Left) || (captionLocation == CaptionLocation.Right)) ? Orientation.Vertical : Orientation.Horizontal;

        public static ITabHeaderLayoutResult Measure(UIElementCollection children, ITabHeaderLayoutCalculator calculator, ITabHeaderLayoutOptions options)
        {
            Size size = options.IsHorizontal ? new Size(options.Size.Width, double.PositiveInfinity) : new Size(double.PositiveInfinity, options.Size.Height);
            ITabHeaderInfo[] headerInfos = GetHeaderInfos(children, size);
            ITabHeaderLayoutResult result = calculator.Calc(headerInfos, options);
            ApplyMeasure(headerInfos);
            return result;
        }
    }
}

