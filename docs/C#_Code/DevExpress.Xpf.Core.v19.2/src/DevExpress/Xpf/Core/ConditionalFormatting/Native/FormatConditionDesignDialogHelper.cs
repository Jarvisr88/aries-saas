namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class FormatConditionDesignDialogHelper
    {
        [IteratorStateMachine(typeof(<GetColorScaleDesignInfo>d__1))]
        public static IEnumerable<DesignFormatInfo> GetColorScaleDesignInfo()
        {
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_GreenYellowRedColorScale, "GreenYellowRedColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_RedYellowGreenColorScale, "RedYellowGreenColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_GreenWhiteRedColorScale, "GreenWhiteRedColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_RedWhiteGreenColorScale, "RedWhiteGreenColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_BlueWhiteRedColorScale, "BlueWhiteRedColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_RedWhiteBlueColorScale, "RedWhiteBlueColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_WhiteRedColorScale, "WhiteRedColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_RedWhiteColorScale, "RedWhiteColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_GreenWhiteColorScale, "GreenWhiteColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_WhiteGreenColorScale, "WhiteGreenColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_GreenYellowColorScale, "GreenYellowColorScale");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_YellowGreenColorScale, "YellowGreenColorScale");
        }

        [IteratorStateMachine(typeof(<GetDataBarDesignInfo>d__0))]
        public static IEnumerable<DesignFormatInfo> GetDataBarDesignInfo()
        {
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_BlueGradientDataBar, "BlueGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_GreenGradientDataBar, "GreenGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_RedGradientDataBar, "RedGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_OrangeGradientDataBar, "OrangeGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_LightBlueGradientDataBar, "LightBlueGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_PurpleGradientDataBar, "PurpleGradientDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_BlueSolidDataBar, "BlueSolidDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_GreenSolidDataBar, "GreenSolidDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_RedSolidDataBar, "RedSolidDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_OrangeSolidDataBar, "OrangeSolidDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_LightBlueSolidDataBar, "LightBlueSolidDataBar");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_PurpleSolidDataBar, "PurpleSolidDataBar");
        }

        [IteratorStateMachine(typeof(<GetIconSetDesignInfo>d__2))]
        public static IEnumerable<DesignFormatInfo> GetIconSetDesignInfo()
        {
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows3ColoredIconSet, "Arrows3ColoredIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows3GrayIconSet, "Arrows3GrayIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Triangles3IconSet, "Triangles3IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows4GrayIconSet, "Arrows4GrayIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows4ColoredIconSet, "Arrows4ColoredIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows5GrayIconSet, "Arrows5GrayIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Arrows5ColoredIconSet, "Arrows5ColoredIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_TrafficLights3UnrimmedIconSet, "TrafficLights3UnrimmedIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_TrafficLights3RimmedIconSet, "TrafficLights3RimmedIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Signs3IconSet, "Signs3IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_TrafficLights4IconSet, "TrafficLights4IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_RedToBlackIconSet, "RedToBlackIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Symbols3CircledIconSet, "Symbols3CircledIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Symbols3UncircledIconSet, "Symbols3UncircledIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Flags3IconSet, "Flags3IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Stars3IconSet, "Stars3IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Ratings4IconSet, "Ratings4IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Quarters5IconSet, "Quarters5IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Ratings5IconSet, "Ratings5IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Boxes5IconSet, "Boxes5IconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_PositiveNegativeArrowsColoredIconSet, "PositiveNegativeArrowsColoredIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_PositiveNegativeArrowsGrayIconSet, "PositiveNegativeArrowsGrayIconSet");
            yield return new DesignFormatInfo(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_PositiveNegativeTrianglesIconSet, "PositiveNegativeTrianglesIconSet");
        }



    }
}

