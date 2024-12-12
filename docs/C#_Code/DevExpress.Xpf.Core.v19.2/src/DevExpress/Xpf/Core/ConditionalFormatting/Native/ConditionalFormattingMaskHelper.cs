namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ConditionalFormattingMaskHelper
    {
        internal const ConditionalFormatMask All = (ConditionalFormatMask.TextDecorations | ConditionalFormatMask.FontWeight | ConditionalFormatMask.FontStretch | ConditionalFormatMask.FontFamily | ConditionalFormatMask.FontStyle | ConditionalFormatMask.FontSize | ConditionalFormatMask.Foreground | ConditionalFormatMask.Background | ConditionalFormatMask.DataBarOrIcon);

        public static ConditionalFormatMask AnimationMaskToConditionalFormatMask(AnimationMask mask)
        {
            if (mask > AnimationMask.FontStyle)
            {
                if (mask > AnimationMask.FontStretch)
                {
                    if (mask == AnimationMask.FontWeight)
                    {
                        return ConditionalFormatMask.FontWeight;
                    }
                    if (mask == AnimationMask.Icon)
                    {
                        goto TR_0008;
                    }
                }
                else
                {
                    if (mask == AnimationMask.FontFamily)
                    {
                        return ConditionalFormatMask.FontFamily;
                    }
                    if (mask == AnimationMask.FontStretch)
                    {
                        return ConditionalFormatMask.FontStretch;
                    }
                }
                goto TR_0000;
            }
            else
            {
                switch (mask)
                {
                    case AnimationMask.None:
                        return ConditionalFormatMask.None;

                    case AnimationMask.Background:
                        return ConditionalFormatMask.Background;

                    case AnimationMask.Foreground:
                        return ConditionalFormatMask.Foreground;

                    case (AnimationMask.Foreground | AnimationMask.Background):
                    case (AnimationMask.ValuePosition | AnimationMask.Background):
                    case (AnimationMask.ValuePosition | AnimationMask.Foreground):
                    case (AnimationMask.ValuePosition | AnimationMask.Foreground | AnimationMask.Background):
                        break;

                    case AnimationMask.ValuePosition:
                    case AnimationMask.IconOpacity:
                        goto TR_0008;

                    default:
                        if (mask == AnimationMask.FontSize)
                        {
                            return ConditionalFormatMask.FontSize;
                        }
                        if (mask != AnimationMask.FontStyle)
                        {
                            break;
                        }
                        return ConditionalFormatMask.FontStyle;
                }
                goto TR_0000;
            }
            goto TR_0008;
        TR_0000:
            return (ConditionalFormatMask.TextDecorations | ConditionalFormatMask.FontWeight | ConditionalFormatMask.FontStretch | ConditionalFormatMask.FontFamily | ConditionalFormatMask.FontStyle | ConditionalFormatMask.FontSize | ConditionalFormatMask.Foreground | ConditionalFormatMask.Background | ConditionalFormatMask.DataBarOrIcon);
        TR_0008:
            return ConditionalFormatMask.DataBarOrIcon;
        }

        public static ConditionalFormatMask GetConditionsMask(IList<FormatConditionBaseInfo> conditions)
        {
            if (conditions == null)
            {
                return ConditionalFormatMask.None;
            }
            Func<FormatConditionBaseInfo, ConditionalFormatMask> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<FormatConditionBaseInfo, ConditionalFormatMask> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = x => x.FormatMask;
            }
            return conditions.Select<FormatConditionBaseInfo, ConditionalFormatMask>(selector).Aggregate<ConditionalFormatMask, ConditionalFormatMask>(ConditionalFormatMask.None, (<>c.<>9__0_1 ??= (sum, x) => (sum | x)));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalFormattingMaskHelper.<>c <>9 = new ConditionalFormattingMaskHelper.<>c();
            public static Func<FormatConditionBaseInfo, ConditionalFormatMask> <>9__0_0;
            public static Func<ConditionalFormatMask, ConditionalFormatMask, ConditionalFormatMask> <>9__0_1;

            internal ConditionalFormatMask <GetConditionsMask>b__0_0(FormatConditionBaseInfo x) => 
                x.FormatMask;

            internal ConditionalFormatMask <GetConditionsMask>b__0_1(ConditionalFormatMask sum, ConditionalFormatMask x) => 
                sum | x;
        }
    }
}

