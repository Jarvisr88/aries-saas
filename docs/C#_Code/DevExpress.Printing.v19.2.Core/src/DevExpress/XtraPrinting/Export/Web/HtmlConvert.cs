namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;

    public class HtmlConvert
    {
        private static readonly Dictionary<GraphicsUnit, string> unitHT = CreateUnitTable();
        private static readonly Dictionary<StringAlignment, string> htmlAlignHT = CreateHorizontalAlignmentTable();
        private static readonly Dictionary<StringAlignment, string> htmlVAlignHT = CreateVerticalAlignmentTable();

        private static Dictionary<StringAlignment, string> CreateHorizontalAlignmentTable() => 
            new Dictionary<StringAlignment, string> { 
                { 
                    StringAlignment.Near,
                    "left"
                },
                { 
                    StringAlignment.Center,
                    "center"
                },
                { 
                    StringAlignment.Far,
                    "right"
                }
            };

        private static Dictionary<GraphicsUnit, string> CreateUnitTable() => 
            new Dictionary<GraphicsUnit, string> { 
                { 
                    GraphicsUnit.Inch,
                    "in"
                },
                { 
                    GraphicsUnit.Millimeter,
                    "mm"
                },
                { 
                    GraphicsUnit.Pixel,
                    "px"
                },
                { 
                    GraphicsUnit.Point,
                    "pt"
                }
            };

        private static Dictionary<StringAlignment, string> CreateVerticalAlignmentTable() => 
            new Dictionary<StringAlignment, string> { 
                { 
                    StringAlignment.Near,
                    "top"
                },
                { 
                    StringAlignment.Center,
                    "middle"
                },
                { 
                    StringAlignment.Far,
                    "bottom"
                }
            };

        private static float DocumentsToPointsF(float val) => 
            (val * 6f) / 25f;

        public static string FontSizeToString(Font font) => 
            FontSizeToString(font.Size, font.Unit);

        public static string FontSizeToString(float size, GraphicsUnit unit)
        {
            string str;
            if (unit == GraphicsUnit.Document)
            {
                return (Math.Round((double) DocumentsToPointsF(size), 1).ToString(CultureInfo.InvariantCulture) + "pt");
            }
            if (!unitHT.TryGetValue(unit, out str))
            {
                str = "pt";
            }
            return (((str == "px") ? ((double) ((int) size)) : Math.Round((double) size, 1)).ToString(CultureInfo.InvariantCulture) + str);
        }

        public static string FontSizeToStringInPixels(float size, GraphicsUnit unit) => 
            ((int) Math.Round((double) GraphicsUnitConverter.Convert(size, GraphicsDpi.UnitToDpi(unit), (float) 96f))).ToString() + "px";

        public static string ToHtml(Color color)
        {
            Color white = DXColor.Blend(color, DXColor.White);
            if (color == DXColor.Transparent)
            {
                return "transparent";
            }
            if (DXColor.IsTransparentColor(white))
            {
                white = DXColor.White;
            }
            return DXColor.ToHtml(white);
        }

        public static string ToHtml(int value) => 
            value + "px";

        public static string ToHtmlAlign(StringAlignment val)
        {
            string str;
            return (!htmlAlignHT.TryGetValue(val, out str) ? string.Empty : str);
        }

        public static string ToHtmlVAlign(StringAlignment val)
        {
            string str;
            return (!htmlVAlignHT.TryGetValue(val, out str) ? string.Empty : str);
        }
    }
}

