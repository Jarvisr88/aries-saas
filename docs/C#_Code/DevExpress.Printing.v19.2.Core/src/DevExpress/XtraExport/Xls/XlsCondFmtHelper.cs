namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.IO;

    internal static class XlsCondFmtHelper
    {
        private static readonly ushort[] dateTemplateValues = new ushort[] { 0, 6, 1, 2, 5, 8, 3, 7, 4, 9 };

        public static byte CfvoTypeToCode(XlCondFmtValueObjectType objectType)
        {
            switch (objectType)
            {
                case XlCondFmtValueObjectType.Number:
                    return 1;

                case XlCondFmtValueObjectType.Percent:
                    return 4;

                case XlCondFmtValueObjectType.Max:
                    return 3;

                case XlCondFmtValueObjectType.Min:
                    return 2;

                case XlCondFmtValueObjectType.Formula:
                    return 7;

                case XlCondFmtValueObjectType.Percentile:
                    return 5;
            }
            return 0;
        }

        public static byte IconSetTypeToCode(XlCondFmtIconSetType type)
        {
            switch (type)
            {
                case XlCondFmtIconSetType.Arrows3:
                    return 0;

                case XlCondFmtIconSetType.ArrowsGray3:
                    return 1;

                case XlCondFmtIconSetType.Flags3:
                    return 2;

                case XlCondFmtIconSetType.TrafficLights3:
                    return 3;

                case XlCondFmtIconSetType.TrafficLights3Black:
                    return 5;

                case XlCondFmtIconSetType.Signs3:
                    return 4;

                case XlCondFmtIconSetType.Symbols3:
                    return 6;

                case XlCondFmtIconSetType.Symbols3Circled:
                    return 7;

                case XlCondFmtIconSetType.Arrows4:
                    return 8;

                case XlCondFmtIconSetType.ArrowsGray4:
                    return 9;

                case XlCondFmtIconSetType.RedToBlack4:
                    return 10;

                case XlCondFmtIconSetType.Rating4:
                    return 11;

                case XlCondFmtIconSetType.TrafficLights4:
                    return 12;

                case XlCondFmtIconSetType.Arrows5:
                    return 13;

                case XlCondFmtIconSetType.ArrowsGray5:
                    return 14;

                case XlCondFmtIconSetType.Rating5:
                    return 15;

                case XlCondFmtIconSetType.Quarters5:
                    return 0x10;
            }
            return 0xff;
        }

        public static bool IsSupportedIconSet(XlCondFmtIconSetType type) => 
            IconSetTypeToCode(type) != 0xff;

        public static byte OperatorToCode(XlCondFmtOperator oper)
        {
            switch (oper)
            {
                case XlCondFmtOperator.Between:
                    return 1;

                case XlCondFmtOperator.Equal:
                    return 3;

                case XlCondFmtOperator.GreaterThan:
                    return 5;

                case XlCondFmtOperator.GreaterThanOrEqual:
                    return 7;

                case XlCondFmtOperator.LessThan:
                    return 6;

                case XlCondFmtOperator.LessThanOrEqual:
                    return 8;

                case XlCondFmtOperator.NotBetween:
                    return 2;

                case XlCondFmtOperator.NotEqual:
                    return 4;
            }
            return 0;
        }

        public static byte RuleTypeToCode(XlCondFmtType ruleType)
        {
            if (ruleType <= XlCondFmtType.ColorScale)
            {
                if (ruleType == XlCondFmtType.CellIs)
                {
                    return 1;
                }
                if (ruleType == XlCondFmtType.ColorScale)
                {
                    return 3;
                }
            }
            else
            {
                switch (ruleType)
                {
                    case XlCondFmtType.DataBar:
                        return 4;

                    case XlCondFmtType.DuplicateValues:
                    case XlCondFmtType.EndsWith:
                        break;

                    case XlCondFmtType.Expression:
                        return 2;

                    case XlCondFmtType.IconSet:
                        return 6;

                    default:
                        if (ruleType != XlCondFmtType.Top10)
                        {
                            break;
                        }
                        return 5;
                }
            }
            return 0;
        }

        public static short SpecificTextTypeToCode(XlCondFmtSpecificTextType type) => 
            (type == XlCondFmtSpecificTextType.BeginsWith) ? 2 : ((type == XlCondFmtSpecificTextType.EndsWith) ? 3 : ((type != XlCondFmtSpecificTextType.NotContains) ? 0 : 1));

        public static void WriteColor(BinaryWriter writer, XlColor color)
        {
            if (color.IsEmpty)
            {
                writer.Write(4);
                writer.Write(0);
                writer.Write((double) 0.0);
            }
            else
            {
                switch (color.ColorType)
                {
                    case XlColorType.Rgb:
                    {
                        writer.Write(2);
                        Color rgb = color.Rgb;
                        writer.Write(rgb.R);
                        writer.Write(rgb.G);
                        writer.Write(rgb.B);
                        writer.Write((byte) 0xff);
                        writer.Write((double) 0.0);
                        return;
                    }
                    case XlColorType.Theme:
                        writer.Write(3);
                        writer.Write((int) color.ThemeColor);
                        writer.Write(color.Tint);
                        return;

                    case XlColorType.Auto:
                        writer.Write(0);
                        writer.Write(0);
                        writer.Write((double) 0.0);
                        return;

                    case XlColorType.Indexed:
                        writer.Write(1);
                        writer.Write(color.ColorIndex);
                        writer.Write((double) 0.0);
                        return;
                }
            }
        }

        public static void WriteTemplateParams(BinaryWriter writer, IXlsCondFmtWithRuleTemplate source)
        {
            writer.Write((byte) 0x10);
            switch (source.RuleTemplate)
            {
                case XlsCFRuleTemplate.Filter:
                {
                    byte num = 0;
                    if (source.FilterTop)
                    {
                        num = (byte) (num | 1);
                    }
                    if (source.FilterPercent)
                    {
                        num = (byte) (num | 2);
                    }
                    writer.Write(num);
                    writer.Write((ushort) source.FilterValue);
                    writer.Write(new byte[13]);
                    return;
                }
                case XlsCFRuleTemplate.ContainsText:
                    writer.Write(SpecificTextTypeToCode(source.TextRule));
                    writer.Write(new byte[14]);
                    return;

                case XlsCFRuleTemplate.Today:
                case XlsCFRuleTemplate.Tomorrow:
                case XlsCFRuleTemplate.Yesterday:
                case XlsCFRuleTemplate.Last7Days:
                case XlsCFRuleTemplate.LastMonth:
                case XlsCFRuleTemplate.NextMonth:
                case XlsCFRuleTemplate.ThisWeek:
                case XlsCFRuleTemplate.NextWeek:
                case XlsCFRuleTemplate.LastWeek:
                case XlsCFRuleTemplate.ThisMonth:
                    writer.Write(dateTemplateValues[(int) (source.RuleTemplate - XlsCFRuleTemplate.Today)]);
                    writer.Write(new byte[14]);
                    return;

                case XlsCFRuleTemplate.AboveAverage:
                case XlsCFRuleTemplate.BelowAverage:
                case XlsCFRuleTemplate.AboveOrEqualToAverage:
                case XlsCFRuleTemplate.BelowOrEqualToAverage:
                    writer.Write((ushort) source.StdDev);
                    writer.Write(new byte[14]);
                    return;
            }
            writer.Write(new byte[0x10]);
        }

        private enum XColorType
        {
            Auto,
            Indexed,
            Rgb,
            Themed,
            Ninched
        }
    }
}

