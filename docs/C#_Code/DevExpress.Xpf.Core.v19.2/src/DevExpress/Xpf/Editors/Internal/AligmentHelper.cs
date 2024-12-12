namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class AligmentHelper
    {
        private static List<Tuple<DevExpress.XtraPrinting.TextAlignment, VerticalAlignment, HorizontalAlignment>> list = new List<Tuple<DevExpress.XtraPrinting.TextAlignment, VerticalAlignment, HorizontalAlignment>>();

        static AligmentHelper()
        {
            AddRow(DevExpress.XtraPrinting.TextAlignment.BottomCenter, VerticalAlignment.Bottom, HorizontalAlignment.Center);
            AddRow(DevExpress.XtraPrinting.TextAlignment.BottomJustify, VerticalAlignment.Bottom, HorizontalAlignment.Stretch);
            AddRow(DevExpress.XtraPrinting.TextAlignment.BottomLeft, VerticalAlignment.Bottom, HorizontalAlignment.Left);
            AddRow(DevExpress.XtraPrinting.TextAlignment.BottomRight, VerticalAlignment.Bottom, HorizontalAlignment.Right);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleCenter, VerticalAlignment.Center, HorizontalAlignment.Center);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleJustify, VerticalAlignment.Center, HorizontalAlignment.Stretch);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleLeft, VerticalAlignment.Center, HorizontalAlignment.Left);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleRight, VerticalAlignment.Center, HorizontalAlignment.Right);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleCenter, VerticalAlignment.Stretch, HorizontalAlignment.Center);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleJustify, VerticalAlignment.Stretch, HorizontalAlignment.Stretch);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleLeft, VerticalAlignment.Stretch, HorizontalAlignment.Left);
            AddRow(DevExpress.XtraPrinting.TextAlignment.MiddleRight, VerticalAlignment.Stretch, HorizontalAlignment.Right);
            AddRow(DevExpress.XtraPrinting.TextAlignment.TopCenter, VerticalAlignment.Top, HorizontalAlignment.Center);
            AddRow(DevExpress.XtraPrinting.TextAlignment.TopJustify, VerticalAlignment.Top, HorizontalAlignment.Stretch);
            AddRow(DevExpress.XtraPrinting.TextAlignment.TopLeft, VerticalAlignment.Top, HorizontalAlignment.Left);
            AddRow(DevExpress.XtraPrinting.TextAlignment.TopRight, VerticalAlignment.Top, HorizontalAlignment.Right);
        }

        private static void AddRow(DevExpress.XtraPrinting.TextAlignment textAlignment, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
            list.Add(new Tuple<DevExpress.XtraPrinting.TextAlignment, VerticalAlignment, HorizontalAlignment>(textAlignment, verticalAlignment, horizontalAlignment));
        }

        public static DevExpress.XtraPrinting.TextAlignment GetFullAligment(VerticalAlignment verticalContentAlignment, HorizontalAlignment horizontalContentAlignment)
        {
            DevExpress.XtraPrinting.TextAlignment alignment;
            using (List<Tuple<DevExpress.XtraPrinting.TextAlignment, VerticalAlignment, HorizontalAlignment>>.Enumerator enumerator = list.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        throw new ArgumentException();
                    }
                    Tuple<DevExpress.XtraPrinting.TextAlignment, VerticalAlignment, HorizontalAlignment> current = enumerator.Current;
                    if ((((VerticalAlignment) current.Item2) == verticalContentAlignment) && (((HorizontalAlignment) current.Item3) == horizontalContentAlignment))
                    {
                        alignment = current.Item1;
                        break;
                    }
                }
            }
            return alignment;
        }
    }
}

