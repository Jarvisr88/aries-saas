namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class PdfLineBreakAnalyzer
    {
        private static LineBreakClass[] lineBreakClasses = new LineBreakClass[0x10fffe];
        private static RuleCollection rules;

        static PdfLineBreakAnalyzer()
        {
            SetRange(lineBreakClasses, 0, lineBreakClasses.Length - 1, LineBreakClass.XX);
            SetRange(lineBreakClasses, 0x3400, 0x4dbf, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0x4e00, 0x9fff, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0xf900, 0xfaff, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0x20000, 0x2fffd, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0x30000, 0x3fffd, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0x1f000, 0x1fffd, LineBreakClass.ID);
            SetRange(lineBreakClasses, 0x20a0, 0x20cf, LineBreakClass.PR);
            ParseLineBreakClasses(lineBreakClasses);
            RuleBuilder builder = new RuleBuilder();
            LineBreakClass[] right = new LineBreakClass[] { LineBreakClass.EOT };
            builder.AddBeforetRule(right, DXBreakCondition.MustBreak, 3);
            LineBreakClass[] left = new LineBreakClass[] { LineBreakClass.BK };
            builder.AddAfterRule(left, DXBreakCondition.MustBreak, 4);
            LineBreakClass[] classArray3 = new LineBreakClass[] { LineBreakClass.CR };
            LineBreakClass[] classArray4 = new LineBreakClass[] { LineBreakClass.LF };
            builder.AddPairRule(classArray3, classArray4, DXBreakCondition.MayNotBreak, 5);
            LineBreakClass[] classArray5 = new LineBreakClass[] { LineBreakClass.CR };
            builder.AddAfterRule(classArray5, DXBreakCondition.MustBreak, 5);
            LineBreakClass[] classArray6 = new LineBreakClass[] { LineBreakClass.LF };
            builder.AddAfterRule(classArray6, DXBreakCondition.MustBreak, 5);
            LineBreakClass[] classArray7 = new LineBreakClass[] { LineBreakClass.NL };
            builder.AddAfterRule(classArray7, DXBreakCondition.MustBreak, 5);
            LineBreakClass[] classArray8 = new LineBreakClass[] { LineBreakClass.BK, LineBreakClass.CR, LineBreakClass.LF, LineBreakClass.NL };
            builder.AddBeforetRule(classArray8, DXBreakCondition.MayNotBreak, 6);
            LineBreakClass[] classArray9 = new LineBreakClass[] { LineBreakClass.SP, LineBreakClass.ZW };
            builder.AddBeforetRule(classArray9, DXBreakCondition.MayNotBreak, 7);
            LineBreakClass[] classArray10 = new LineBreakClass[] { LineBreakClass.ZWJ };
            builder.AddAfterRule(classArray10, DXBreakCondition.MayNotBreak, 8);
            LineBreakClass[] classArray11 = new LineBreakClass[] { LineBreakClass.WJ };
            builder.AddBeforetRule(classArray11, DXBreakCondition.MayNotBreak, 11);
            LineBreakClass[] classArray12 = new LineBreakClass[] { LineBreakClass.WJ };
            builder.AddAfterRule(classArray12, DXBreakCondition.MayNotBreak, 11);
            LineBreakClass[] classArray13 = new LineBreakClass[] { LineBreakClass.GL };
            builder.AddAfterRule(classArray13, DXBreakCondition.MayNotBreak, 12);
            LineBreakClass[] classArray14 = new LineBreakClass[] { LineBreakClass.GL };
            builder.AddPairRule((from v in RuleBuilder.AllClases
                where (v != LineBreakClass.SP) && ((v != LineBreakClass.BA) && (v != LineBreakClass.HY))
                select v).ToList<LineBreakClass>(), classArray14, DXBreakCondition.MayNotBreak, 12);
            LineBreakClass[] classArray15 = new LineBreakClass[] { LineBreakClass.CL, LineBreakClass.CP, LineBreakClass.EX, LineBreakClass.IS, LineBreakClass.SY };
            builder.AddBeforetRule(classArray15, DXBreakCondition.MayNotBreak, 13);
            LineBreakClass[] classArray16 = new LineBreakClass[] { LineBreakClass.SP };
            builder.AddAfterRule(classArray16, DXBreakCondition.CanBreak, 0x12);
            LineBreakClass[] classArray17 = new LineBreakClass[] { LineBreakClass.QU };
            builder.AddBeforetRule(classArray17, DXBreakCondition.MayNotBreak, 0x13);
            LineBreakClass[] classArray18 = new LineBreakClass[] { LineBreakClass.QU };
            builder.AddAfterRule(classArray18, DXBreakCondition.MayNotBreak, 0x13);
            LineBreakClass[] classArray19 = new LineBreakClass[] { LineBreakClass.CB };
            builder.AddBeforetRule(classArray19, DXBreakCondition.CanBreak, 20);
            LineBreakClass[] classArray20 = new LineBreakClass[] { LineBreakClass.CB };
            builder.AddAfterRule(classArray20, DXBreakCondition.CanBreak, 20);
            LineBreakClass[] classArray21 = new LineBreakClass[] { LineBreakClass.BA };
            builder.AddBeforetRule(classArray21, DXBreakCondition.MayNotBreak, 0x15);
            LineBreakClass[] classArray22 = new LineBreakClass[] { LineBreakClass.HY };
            builder.AddBeforetRule(classArray22, DXBreakCondition.MayNotBreak, 0x15);
            LineBreakClass[] classArray23 = new LineBreakClass[] { LineBreakClass.NS };
            builder.AddBeforetRule(classArray23, DXBreakCondition.MayNotBreak, 0x15);
            LineBreakClass[] classArray24 = new LineBreakClass[] { LineBreakClass.BB };
            builder.AddAfterRule(classArray24, DXBreakCondition.MayNotBreak, 0x15);
            LineBreakClass[] classArray25 = new LineBreakClass[] { LineBreakClass.SY };
            LineBreakClass[] classArray26 = new LineBreakClass[] { LineBreakClass.HL };
            builder.AddPairRule(classArray25, classArray26, DXBreakCondition.MayNotBreak, 0x15);
            LineBreakClass[] classArray27 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            LineBreakClass[] classArray28 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray27, classArray28, DXBreakCondition.MayNotBreak, 0x16);
            LineBreakClass[] classArray29 = new LineBreakClass[] { LineBreakClass.EX };
            LineBreakClass[] classArray30 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray29, classArray30, DXBreakCondition.MayNotBreak, 0x16);
            LineBreakClass[] classArray31 = new LineBreakClass[] { LineBreakClass.IN };
            LineBreakClass[] classArray32 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray31, classArray32, DXBreakCondition.MayNotBreak, 0x16);
            LineBreakClass[] classArray33 = new LineBreakClass[] { LineBreakClass.ID, LineBreakClass.EB, LineBreakClass.EM };
            LineBreakClass[] classArray34 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray33, classArray34, DXBreakCondition.MayNotBreak, 0x16);
            LineBreakClass[] classArray35 = new LineBreakClass[] { LineBreakClass.NU };
            LineBreakClass[] classArray36 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray35, classArray36, DXBreakCondition.MayNotBreak, 0x16);
            LineBreakClass[] classArray37 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            LineBreakClass[] classArray38 = new LineBreakClass[] { LineBreakClass.NU };
            builder.AddPairRule(classArray37, classArray38, DXBreakCondition.MayNotBreak, 0x17);
            LineBreakClass[] classArray39 = new LineBreakClass[] { LineBreakClass.NU };
            LineBreakClass[] classArray40 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            builder.AddPairRule(classArray39, classArray40, DXBreakCondition.MayNotBreak, 0x17);
            LineBreakClass[] classArray41 = new LineBreakClass[] { LineBreakClass.PR };
            LineBreakClass[] classArray42 = new LineBreakClass[] { LineBreakClass.ID, LineBreakClass.EB, LineBreakClass.EM };
            builder.AddPairRule(classArray41, classArray42, DXBreakCondition.MayNotBreak, 0x17);
            LineBreakClass[] classArray43 = new LineBreakClass[] { LineBreakClass.ID, LineBreakClass.EB, LineBreakClass.EM };
            LineBreakClass[] classArray44 = new LineBreakClass[] { LineBreakClass.PO };
            builder.AddPairRule(classArray43, classArray44, DXBreakCondition.MayNotBreak, 0x17);
            LineBreakClass[] classArray45 = new LineBreakClass[] { LineBreakClass.PR, LineBreakClass.PO };
            LineBreakClass[] classArray46 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            builder.AddPairRule(classArray45, classArray46, DXBreakCondition.MayNotBreak, 0x18);
            LineBreakClass[] classArray47 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            LineBreakClass[] classArray48 = new LineBreakClass[] { LineBreakClass.PR, LineBreakClass.PO };
            builder.AddPairRule(classArray47, classArray48, DXBreakCondition.MayNotBreak, 0x18);
            LineBreakClass[] classArray49 = new LineBreakClass[] { LineBreakClass.JL };
            LineBreakClass[] classArray50 = new LineBreakClass[] { LineBreakClass.JL, LineBreakClass.JV, LineBreakClass.H2, LineBreakClass.H3 };
            builder.AddPairRule(classArray49, classArray50, DXBreakCondition.MayNotBreak, 0x1a);
            LineBreakClass[] classArray51 = new LineBreakClass[] { LineBreakClass.JV, LineBreakClass.H2 };
            LineBreakClass[] classArray52 = new LineBreakClass[] { LineBreakClass.JV, LineBreakClass.JT };
            builder.AddPairRule(classArray51, classArray52, DXBreakCondition.MayNotBreak, 0x1a);
            LineBreakClass[] classArray53 = new LineBreakClass[] { LineBreakClass.JT, LineBreakClass.H3 };
            LineBreakClass[] classArray54 = new LineBreakClass[] { LineBreakClass.JT };
            builder.AddPairRule(classArray53, classArray54, DXBreakCondition.MayNotBreak, 0x1a);
            LineBreakClass[] classArray55 = new LineBreakClass[] { LineBreakClass.JL, LineBreakClass.JV, LineBreakClass.JT, LineBreakClass.H2, LineBreakClass.H3 };
            LineBreakClass[] classArray56 = new LineBreakClass[] { LineBreakClass.IN };
            builder.AddPairRule(classArray55, classArray56, DXBreakCondition.MayNotBreak, 0x1b);
            LineBreakClass[] classArray57 = new LineBreakClass[] { LineBreakClass.JL, LineBreakClass.JV, LineBreakClass.JT, LineBreakClass.H2, LineBreakClass.H3 };
            LineBreakClass[] classArray58 = new LineBreakClass[] { LineBreakClass.PO };
            builder.AddPairRule(classArray57, classArray58, DXBreakCondition.MayNotBreak, 0x1b);
            LineBreakClass[] classArray59 = new LineBreakClass[] { LineBreakClass.PR };
            LineBreakClass[] classArray60 = new LineBreakClass[] { LineBreakClass.JL, LineBreakClass.JV, LineBreakClass.JT, LineBreakClass.H2, LineBreakClass.H3 };
            builder.AddPairRule(classArray59, classArray60, DXBreakCondition.MayNotBreak, 0x1b);
            LineBreakClass[] classArray61 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            LineBreakClass[] classArray62 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            builder.AddPairRule(classArray61, classArray62, DXBreakCondition.MayNotBreak, 0x1c);
            LineBreakClass[] classArray63 = new LineBreakClass[] { LineBreakClass.IS };
            LineBreakClass[] classArray64 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL };
            builder.AddPairRule(classArray63, classArray64, DXBreakCondition.MayNotBreak, 0x1d);
            LineBreakClass[] classArray65 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL, LineBreakClass.NU };
            LineBreakClass[] classArray66 = new LineBreakClass[] { LineBreakClass.OP };
            builder.AddPairRule(classArray65, classArray66, DXBreakCondition.MayNotBreak, 30);
            LineBreakClass[] classArray67 = new LineBreakClass[] { LineBreakClass.CP };
            LineBreakClass[] classArray68 = new LineBreakClass[] { LineBreakClass.AL, LineBreakClass.HL, LineBreakClass.NU };
            builder.AddPairRule(classArray67, classArray68, DXBreakCondition.MayNotBreak, 30);
            LineBreakClass[] classArray69 = new LineBreakClass[] { LineBreakClass.EB };
            LineBreakClass[] classArray70 = new LineBreakClass[] { LineBreakClass.EM };
            builder.AddPairRule(classArray69, classArray70, DXBreakCondition.MayNotBreak, 30);
            rules = builder.GetCollection();
        }

        public static IList<DXBreakCondition> GetBreakPoints(string text)
        {
            LineBreakClass aL;
            LineBreakClass[] breakClasess = new LineBreakClass[text.Length];
            AnalyzerContext context = new AnalyzerContext(text.Length);
            SpaceGroupRule rule = new SpaceGroupRule(LineBreakClass.OP, 15);
            SpaceGroupRule rule2 = new SpaceGroupRule(LineBreakClass.NS, 0x10);
            SpaceGroupRule rule3 = new SpaceGroupRule(LineBreakClass.B2, 0x11);
            LB25 lb = new LB25(text.Length);
            int length = breakClasess.Length;
            int index = 0;
            goto TR_0032;
        TR_001A:
            breakClasess[index] = aL;
            index++;
        TR_0032:
            while (true)
            {
                if (index >= length)
                {
                    for (int i = 0; i < length; i++)
                    {
                        LineBreakClass left = breakClasess[i];
                        rules.Match(i, left, (i < (length - 1)) ? breakClasess[i + 1] : LineBreakClass.EOT, context);
                        LineBreakClass class6 = breakClasess[i];
                        if (class6 > LineBreakClass.ZW)
                        {
                            if (class6 == LineBreakClass.B2)
                            {
                                rule3.Match(i, breakClasess, context);
                            }
                            else if ((class6 == LineBreakClass.RI) && ((i == 0) || (breakClasess[i - 1] != LineBreakClass.RI)))
                            {
                                int num5 = i + 2;
                                bool flag = true;
                                while ((num5 < length) && (breakClasess[num5] == LineBreakClass.RI))
                                {
                                    if (flag)
                                    {
                                        context.Assign(num5 - 1, DXBreakCondition.MayNotBreak, 30);
                                    }
                                    flag = !flag;
                                    num5 += 2;
                                }
                            }
                        }
                        else
                        {
                            switch (class6)
                            {
                                case LineBreakClass.QU:
                                    rule.Match(i, breakClasess, context);
                                    break;

                                case LineBreakClass.AL:
                                case LineBreakClass.IS:
                                case LineBreakClass.SY:
                                case LineBreakClass.NL:
                                case LineBreakClass.GL:
                                case LineBreakClass.AI:
                                case LineBreakClass.BB:
                                    break;

                                case LineBreakClass.PR:
                                case LineBreakClass.PO:
                                    lb.MatchPRPO(i, breakClasess, context);
                                    break;

                                case LineBreakClass.OP:
                                    MatchLB14(i, breakClasess, context);
                                    lb.MatchOPNY(i, breakClasess, context);
                                    break;

                                case LineBreakClass.CP:
                                case LineBreakClass.CL:
                                    rule2.Match(i, breakClasess, context);
                                    break;

                                case LineBreakClass.HY:
                                    lb.MatchOPNY(i, breakClasess, context);
                                    break;

                                case LineBreakClass.NU:
                                    lb.Match(i, breakClasess, context);
                                    break;

                                case LineBreakClass.HL:
                                    MatchLB21A(i, breakClasess, context);
                                    break;

                                default:
                                    if (class6 == LineBreakClass.ZW)
                                    {
                                        MatchLB8(i, breakClasess, context);
                                    }
                                    break;
                            }
                        }
                        context.Assign(i, DXBreakCondition.CanBreak, 0xff);
                    }
                    return context.BreakConditionAfter;
                }
                char c = text[index];
                if (!char.IsHighSurrogate(c) || (index >= (length - 1)))
                {
                    aL = (!char.IsLowSurrogate(c) || (index <= 0)) ? lineBreakClasses[c] : breakClasess[index - 1];
                }
                else
                {
                    int num3 = (((c & 0x3ff) << 10) | (text[index + 1] & 'Ͽ')) + 0x10000;
                    aL = (num3 < lineBreakClasses.Length) ? lineBreakClasses[num3] : LineBreakClass.XX;
                    context.Assign(index, DXBreakCondition.MayNotBreak, 1);
                }
                if (aL > LineBreakClass.AI)
                {
                    if (aL == LineBreakClass.SA)
                    {
                        UnicodeCategory unicodeCategory = char.GetUnicodeCategory(text[index]);
                        aL = ((unicodeCategory == UnicodeCategory.NonSpacingMark) || (unicodeCategory == UnicodeCategory.SpacingCombiningMark)) ? LineBreakClass.CM : LineBreakClass.AL;
                        goto TR_001A;
                    }
                    else if (aL != LineBreakClass.ZWJ)
                    {
                        switch (aL)
                        {
                            case LineBreakClass.CJ:
                                aL = LineBreakClass.NS;
                                goto TR_001A;

                            case LineBreakClass.SG:
                            case LineBreakClass.XX:
                                break;

                            default:
                                goto TR_001A;
                        }
                        break;
                    }
                }
                else if (aL != LineBreakClass.CM)
                {
                    if (aL == LineBreakClass.AI)
                    {
                        break;
                    }
                    goto TR_001A;
                }
                if (aL == LineBreakClass.ZWJ)
                {
                    context.Assign(index, DXBreakCondition.MayNotBreak, 8);
                }
                if (index <= 0)
                {
                    aL = LineBreakClass.AL;
                }
                else
                {
                    LineBreakClass class3 = breakClasess[index - 1];
                    if ((class3 != LineBreakClass.BK) && ((class3 != LineBreakClass.CR) && ((class3 != LineBreakClass.LF) && ((class3 != LineBreakClass.NL) && ((class3 != LineBreakClass.SP) && (class3 != LineBreakClass.ZW))))))
                    {
                        context.Assign(index - 1, DXBreakCondition.MayNotBreak, 10);
                    }
                    else
                    {
                        class3 = LineBreakClass.AL;
                    }
                    aL = class3;
                }
                goto TR_001A;
            }
            aL = LineBreakClass.AL;
            goto TR_001A;
        }

        private static void MatchLB14(int i, LineBreakClass[] breakClasess, AnalyzerContext context)
        {
            int length = breakClasess.Length;
            while ((i < (length - 1)) && (breakClasess[i + 1] == LineBreakClass.SP))
            {
                context.Assign(i, DXBreakCondition.MayNotBreak, 14);
                i++;
            }
            context.Assign(i, DXBreakCondition.MayNotBreak, 14);
        }

        private static void MatchLB21A(int i, LineBreakClass[] breakClasess, AnalyzerContext context)
        {
            if (i < (breakClasess.Length - 1))
            {
                LineBreakClass class2 = breakClasess[i + 1];
                if ((class2 == LineBreakClass.HY) || (class2 == LineBreakClass.BA))
                {
                    context.Assign(i + 1, DXBreakCondition.MayNotBreak, 0x15);
                }
            }
        }

        private static void MatchLB8(int i, LineBreakClass[] breakClasess, AnalyzerContext context)
        {
            context.Assign(i, DXBreakCondition.CanBreak, 8);
            int length = breakClasess.Length;
            while ((i < (length - 1)) && (breakClasess[i + 1] == LineBreakClass.SP))
            {
                i++;
            }
            context.Assign(i, DXBreakCondition.CanBreak, 8);
        }

        private static void ParseLineBreakClasses(LineBreakClass[] lineBreakClasses)
        {
            using (Stream stream = PdfEmbeddedResourceProvider.GetEmbeddedResourceStream("ContentGeneration.Text.LineBreak.LineBreakClasses.bin"))
            {
                PdfBigEndianStreamReader reader = new PdfBigEndianStreamReader(stream);
                while (!reader.Finish)
                {
                    byte num = reader.ReadByte();
                    LineBreakClass class2 = (LineBreakClass) ((byte) (num & 0x3f));
                    bool isLong = (num & 0x80) != 0;
                    if ((num & 0x40) == 0)
                    {
                        lineBreakClasses[ReadCharCode(reader, isLong)] = class2;
                        continue;
                    }
                    int num3 = ReadCharCode(reader, isLong);
                    for (int i = ReadCharCode(reader, isLong); i <= num3; i++)
                    {
                        lineBreakClasses[i] = class2;
                    }
                }
            }
        }

        private static int ReadCharCode(PdfBigEndianStreamReader reader, bool isLong) => 
            isLong ? reader.ReadInt32() : reader.ReadInt16();

        private static void SetRange(LineBreakClass[] lineBreakClasses, int start, int end, LineBreakClass value)
        {
            for (int i = start; i < end; i++)
            {
                lineBreakClasses[i] = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfLineBreakAnalyzer.<>c <>9 = new PdfLineBreakAnalyzer.<>c();

            internal bool <.cctor>b__12_0(PdfLineBreakAnalyzer.LineBreakClass v) => 
                (v != PdfLineBreakAnalyzer.LineBreakClass.SP) && ((v != PdfLineBreakAnalyzer.LineBreakClass.BA) && (v != PdfLineBreakAnalyzer.LineBreakClass.HY));
        }

        private class AnalyzerContext
        {
            public AnalyzerContext(int count)
            {
                this.<Count>k__BackingField = count;
                this.<ResovedPriority>k__BackingField = new byte[count];
                this.<BreakConditionAfter>k__BackingField = new DXBreakCondition[count];
            }

            public void Assign(int index, DXBreakCondition value, byte priority)
            {
                if ((this.ResovedPriority[index] == 0) || (priority < this.ResovedPriority[index]))
                {
                    this.BreakConditionAfter[index] = value;
                    this.ResovedPriority[index] = priority;
                }
            }

            public DXBreakCondition[] BreakConditionAfter { get; }

            public byte[] ResovedPriority { get; }

            public int Count { get; }
        }

        private class LB25
        {
            private readonly bool[] isProcessed;

            public LB25(int length)
            {
                this.isProcessed = new bool[length];
            }

            private static PdfLineBreakAnalyzer.LineBreakClass GetNext(int i, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess) => 
                (i < (breakClasess.Length - 1)) ? breakClasess[i + 1] : PdfLineBreakAnalyzer.LineBreakClass.EOT;

            public void Match(int i, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                if (!this.isProcessed[i])
                {
                    this.Match(i, i, breakClasess, context);
                }
            }

            private void Match(int start, int end, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                while (true)
                {
                    PdfLineBreakAnalyzer.LineBreakClass next = GetNext(end, breakClasess);
                    if ((next == PdfLineBreakAnalyzer.LineBreakClass.CL) || (next == PdfLineBreakAnalyzer.LineBreakClass.CP))
                    {
                        end++;
                        next = GetNext(end, breakClasess);
                        if ((next == PdfLineBreakAnalyzer.LineBreakClass.PR) || (next == PdfLineBreakAnalyzer.LineBreakClass.PO))
                        {
                            end++;
                        }
                    }
                    else if ((next == PdfLineBreakAnalyzer.LineBreakClass.PR) || (next == PdfLineBreakAnalyzer.LineBreakClass.PO))
                    {
                        end++;
                    }
                    else if ((next == PdfLineBreakAnalyzer.LineBreakClass.NU) || ((next == PdfLineBreakAnalyzer.LineBreakClass.SY) || (next == PdfLineBreakAnalyzer.LineBreakClass.IS)))
                    {
                        end++;
                        continue;
                    }
                    while (start < end)
                    {
                        this.isProcessed[start] = true;
                        context.Assign(start, DXBreakCondition.MayNotBreak, 0x19);
                        start++;
                    }
                    return;
                }
            }

            public void MatchOPNY(int i, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                if (!this.isProcessed[i] && (GetNext(i, breakClasess) == PdfLineBreakAnalyzer.LineBreakClass.NU))
                {
                    this.Match(i, i + 1, breakClasess, context);
                }
            }

            public void MatchPRPO(int i, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                if (!this.isProcessed[i])
                {
                    PdfLineBreakAnalyzer.LineBreakClass next = GetNext(i, breakClasess);
                    if ((next != PdfLineBreakAnalyzer.LineBreakClass.OP) && (next != PdfLineBreakAnalyzer.LineBreakClass.HY))
                    {
                        if (next == PdfLineBreakAnalyzer.LineBreakClass.NU)
                        {
                            this.Match(i, i + 1, breakClasess, context);
                        }
                    }
                    else if (GetNext(i + 1, breakClasess) == PdfLineBreakAnalyzer.LineBreakClass.NU)
                    {
                        this.Match(i, i + 2, breakClasess, context);
                    }
                }
            }
        }

        private enum LineBreakClass : byte
        {
            SOT = 0,
            EOT = 1,
            CM = 2,
            BA = 3,
            LF = 4,
            BK = 5,
            CR = 6,
            SP = 7,
            EX = 8,
            QU = 9,
            AL = 10,
            PR = 11,
            PO = 12,
            OP = 13,
            CP = 14,
            IS = 15,
            HY = 0x10,
            SY = 0x11,
            NU = 0x12,
            CL = 0x13,
            NL = 20,
            GL = 0x15,
            AI = 0x16,
            BB = 0x17,
            HL = 0x18,
            SA = 0x19,
            JL = 0x1a,
            JV = 0x1b,
            JT = 0x1c,
            NS = 0x1d,
            ZW = 30,
            ZWJ = 0x1f,
            B2 = 0x20,
            IN = 0x21,
            WJ = 0x22,
            ID = 0x23,
            EB = 0x24,
            CJ = 0x25,
            H2 = 0x26,
            H3 = 0x27,
            SG = 40,
            XX = 0x29,
            CB = 0x2a,
            RI = 0x2b,
            EM = 0x2c
        }

        private class RuleBuilder
        {
            private readonly PdfLineBreakAnalyzer.RuleInfo[] ruleLookupTable = new PdfLineBreakAnalyzer.RuleInfo[0xb6d];

            static RuleBuilder()
            {
                foreach (PdfLineBreakAnalyzer.LineBreakClass class2 in Enum.GetValues(typeof(PdfLineBreakAnalyzer.LineBreakClass)))
                {
                    AllClases.Add(class2);
                }
            }

            public void AddAfterRule(IList<PdfLineBreakAnalyzer.LineBreakClass> left, DXBreakCondition value, byte priority)
            {
                this.AddPairRule(left, AllClases, value, priority);
            }

            public void AddBeforetRule(IList<PdfLineBreakAnalyzer.LineBreakClass> right, DXBreakCondition value, byte priority)
            {
                this.AddPairRule(AllClases, right, value, priority);
            }

            public void AddPairRule(IList<PdfLineBreakAnalyzer.LineBreakClass> left, IList<PdfLineBreakAnalyzer.LineBreakClass> right, DXBreakCondition value, byte priority)
            {
                PdfLineBreakAnalyzer.RuleInfo info = new PdfLineBreakAnalyzer.RuleInfo(priority, value);
                foreach (PdfLineBreakAnalyzer.LineBreakClass class2 in left)
                {
                    foreach (PdfLineBreakAnalyzer.LineBreakClass class3 in right)
                    {
                        int index = (((byte) class2) << 6) | ((int) class3);
                        if ((this.ruleLookupTable[index].Priority == 0) || (this.ruleLookupTable[index].Priority > priority))
                        {
                            this.ruleLookupTable[index] = info;
                        }
                    }
                }
            }

            public PdfLineBreakAnalyzer.RuleCollection GetCollection() => 
                new PdfLineBreakAnalyzer.RuleCollection(this.ruleLookupTable);

            public static IList<PdfLineBreakAnalyzer.LineBreakClass> AllClases { get; }
        }

        private class RuleCollection
        {
            private readonly PdfLineBreakAnalyzer.RuleInfo[] ruleLookupTable;

            public RuleCollection(PdfLineBreakAnalyzer.RuleInfo[] ruleLookupTable)
            {
                this.ruleLookupTable = ruleLookupTable;
            }

            public void Match(int i, PdfLineBreakAnalyzer.LineBreakClass left, PdfLineBreakAnalyzer.LineBreakClass right, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                PdfLineBreakAnalyzer.RuleInfo info = this.ruleLookupTable[(((byte) left) << 6) | ((int) right)];
                if (info.Priority > 0)
                {
                    context.Assign(i, info.Value, info.Priority);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RuleInfo
        {
            public byte Priority { get; }
            public DXBreakCondition Value { get; }
            public RuleInfo(byte priority, DXBreakCondition value)
            {
                this.<Priority>k__BackingField = priority;
                this.<Value>k__BackingField = value;
            }
        }

        private class SpaceGroupRule
        {
            private readonly PdfLineBreakAnalyzer.LineBreakClass breakClass;
            private readonly byte priority;

            public SpaceGroupRule(PdfLineBreakAnalyzer.LineBreakClass breakClass, byte priority)
            {
                this.breakClass = breakClass;
                this.priority = priority;
            }

            public void Match(int i, PdfLineBreakAnalyzer.LineBreakClass[] breakClasess, PdfLineBreakAnalyzer.AnalyzerContext context)
            {
                int index = i;
                int length = breakClasess.Length;
                while ((i < (length - 1)) && (breakClasess[i + 1] == PdfLineBreakAnalyzer.LineBreakClass.SP))
                {
                    i++;
                }
                if ((i < (length - 1)) && (breakClasess[i + 1] == this.breakClass))
                {
                    while (index <= i)
                    {
                        context.Assign(index, DXBreakCondition.MayNotBreak, this.priority);
                        index++;
                    }
                }
            }
        }
    }
}

