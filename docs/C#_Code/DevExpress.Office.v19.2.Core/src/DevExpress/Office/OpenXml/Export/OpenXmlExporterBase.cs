namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;

    public abstract class OpenXmlExporterBase
    {
        private static Dictionary<DrawingTextAnchoringType, string> anchoringTypeTable;
        private static object syncAnchoringTypeTable = new object();
        private static Dictionary<DrawingTextHorizontalOverflowType, string> horizontalOverflowTypeTable;
        private static object syncHorizontalOverflowTypeTable = new object();
        private static Dictionary<DrawingTextVerticalOverflowType, string> verticalOverflowTypeTable;
        private static object syncVerticalOverflowTypeTable = new object();
        private static Dictionary<DrawingTextVerticalTextType, string> verticalTextTypeTable;
        private static object syncVerticalTextTypeTable = new object();
        private static Dictionary<DrawingTextWrappingType, string> textWrappingTypeTable;
        private static object syncTextWrappingTypeTable = new object();
        private static Dictionary<DrawingPresetTextWarp, string> presetTextWarpTable;
        private static object syncPresetTextWarpTable = new object();
        private static Dictionary<TileFlipType, string> tileFlipTypeTable;
        private static object syncTileFlipTypeTable = new object();
        private static Dictionary<GradientType, string> gradientTypeTable;
        private static object syncGradientTypeTable = new object();
        private static Dictionary<DrawingPatternType, string> drawingPatternTypeTable;
        private static object syncDrawingPatternTypeTable = new object();
        private static Dictionary<RectangleAlignType, string> rectangleAlignTypeTable;
        private static object syncRectangleAlignTypeTable = new object();
        private static Dictionary<CompressionState, string> compressionStateTable;
        private static object syncCompressionStateTable = new object();
        private static Dictionary<BlendMode, string> blendModeTable;
        private static object syncBlendModeTable = new object();
        private static Dictionary<PresetShadowType, string> presetShadowTypeTable;
        private static object syncPresetShadowTypeTable = new object();
        private static Dictionary<DrawingEffectContainerType, string> drawingEffectContainerTypeTable;
        private static object syncDrawingEffectContainerTypeTable = new object();
        private static Dictionary<OutlineStrokeAlignment, string> strokeAlignmentTable;
        private static object syncStrokeAlignmentTable = new object();
        private static Dictionary<OutlineEndCapStyle, string> endCapStyleTable;
        private static object syncEndCapStyleTable = new object();
        private static Dictionary<OutlineCompoundType, string> compoundTypeTable;
        private static object syncCompoundTypeTable = new object();
        private static Dictionary<OutlineDashing, string> presetDashTable;
        private static object syncPresetDashTable = new object();
        private static Dictionary<OutlineHeadTailSize, string> headTailSizeTable;
        private static object syncHeadTailSizeTable = new object();
        private static Dictionary<VMLStrokeArrowType, string> vmlStrokeArrowTypeTable;
        private static object syncVmlStrokeArrowTypeTable = new object();
        private static Dictionary<VMLStrokeArrowLength, string> vmlStrokeArrowLengthTable;
        private static object syncVmlStrokeArrowLengthTable = new object();
        private static Dictionary<VMLStrokeArrowWidth, string> vmlStrokeArrowWidthTable;
        private static object syncVmlStrokeArrowWidthTable = new object();
        private static Dictionary<OutlineHeadTailType, string> headTailTypeTable;
        private static object syncHeadTailTypeTable = new object();
        private static Dictionary<PresetCameraType, string> presetCameraTypeTable;
        private static object syncPresetCameraTypeTable = new object();
        private static Dictionary<LightRigDirection, string> lightRigDirectionTable;
        private static object syncLightRigDirectionTable = new object();
        private static Dictionary<LightRigPreset, string> lightRigPresetTable;
        private static object syncLightRigPresetTable = new object();
        private static Dictionary<PresetMaterialType, string> presetMaterialTypeTable;
        private static object syncPresetMaterialTypeTable = new object();
        private static Dictionary<PresetBevelType, string> presetBevelTypeTable;
        private static object syncPresetBevelTypeTable = new object();
        public static Dictionary<XlFontSchemeStyles, string> FontCollectionIndexTable = GetFontCollectionIndexTable();
        private static Dictionary<VmlStrokeJoinStyle, string> vmlStrokeJoinStyleTable;
        private static object syncVmlStrokeJoinStyleTable = new object();
        private static Dictionary<VmlFillType, string> vmlFillTypeTable;
        private static object syncVmlFillTypeTable = new object();
        private static Dictionary<VmlLineStyle, string> vmlLineStyleTable;
        private static object syncVmlLineStyleTable = new object();
        private static Dictionary<VmlDashStyle, string> vmlDashStyleTable;
        private static object syncVmlDashStyleTable = new object();
        private static Dictionary<VmlConnectType, string> vmlConnectTypeTable;
        private static object syncVmlConnectTypeTable = new object();
        private static Dictionary<VmlExtensionHandlingBehavior, string> vmlExtensionHandlingBehaviorTable;
        private static object syncVmlExtensionHandlingBehaviorTable = new object();
        private static Dictionary<VmlInsetMode, string> vmlInsetModeTable;
        private static object syncVmlInsetModeTable = new object();
        private static Dictionary<VmlFillMethod, string> vmlFillMethodTable;
        private static object syncVmlFillMethodTable = new object();
        private static Dictionary<VmlImageAspect, string> vmlImageAspectTable;
        private static object syncVmlImageAspectTable = new object();
        private static Dictionary<VmlBlackAndWhiteMode, string> vmlBlackAndWhiteModeTable;
        private static object syncVmlBlackAndWhiteModeTable = new object();
        private static Dictionary<VmlGroupEditAs, string> vmlGroupEditAsTable;
        private static object syncVmlGroupEditAsTable = new object();
        private static Dictionary<VmlShadowType, string> vmlShadowTypeTable;
        private static object syncVmlShadowTypeTable = new object();

        protected OpenXmlExporterBase()
        {
        }

        private static Dictionary<DrawingTextAnchoringType, string> CreateAnchoringTypeTable() => 
            new Dictionary<DrawingTextAnchoringType, string> { 
                { 
                    DrawingTextAnchoringType.Top,
                    "t"
                },
                { 
                    DrawingTextAnchoringType.Center,
                    "ctr"
                },
                { 
                    DrawingTextAnchoringType.Bottom,
                    "b"
                },
                { 
                    DrawingTextAnchoringType.Justified,
                    "just"
                },
                { 
                    DrawingTextAnchoringType.Distributed,
                    "dist"
                }
            };

        private static Dictionary<OutlineCompoundType, string> CreateCompoundTypeTable() => 
            new Dictionary<OutlineCompoundType, string> { 
                { 
                    OutlineCompoundType.Single,
                    "sng"
                },
                { 
                    OutlineCompoundType.Double,
                    "dbl"
                },
                { 
                    OutlineCompoundType.ThickThin,
                    "thickThin"
                },
                { 
                    OutlineCompoundType.ThinThick,
                    "thinThick"
                },
                { 
                    OutlineCompoundType.Triple,
                    "tri"
                }
            };

        private static Dictionary<CompressionState, string> CreateCompressionStateTable() => 
            new Dictionary<CompressionState, string> { 
                { 
                    CompressionState.Email,
                    "email"
                },
                { 
                    CompressionState.HighQualityPrinting,
                    "hqprint"
                },
                { 
                    CompressionState.None,
                    "none"
                },
                { 
                    CompressionState.Print,
                    "print"
                },
                { 
                    CompressionState.Screen,
                    "screen"
                }
            };

        private static Dictionary<DrawingPatternType, string> CreateDrawingPatternTypeTable() => 
            new Dictionary<DrawingPatternType, string> { 
                { 
                    DrawingPatternType.Cross,
                    "cross"
                },
                { 
                    DrawingPatternType.DashedDownwardDiagonal,
                    "dashDnDiag"
                },
                { 
                    DrawingPatternType.DashedHorizontal,
                    "dashHorz"
                },
                { 
                    DrawingPatternType.DashedUpwardDiagonal,
                    "dashUpDiag"
                },
                { 
                    DrawingPatternType.DashedVertical,
                    "dashVert"
                },
                { 
                    DrawingPatternType.DiagonalBrick,
                    "diagBrick"
                },
                { 
                    DrawingPatternType.DiagonalCross,
                    "diagCross"
                },
                { 
                    DrawingPatternType.Divot,
                    "divot"
                },
                { 
                    DrawingPatternType.DarkDownwardDiagonal,
                    "dkDnDiag"
                },
                { 
                    DrawingPatternType.DarkHorizontal,
                    "dkHorz"
                },
                { 
                    DrawingPatternType.DarkUpwardDiagonal,
                    "dkUpDiag"
                },
                { 
                    DrawingPatternType.DarkVertical,
                    "dkVert"
                },
                { 
                    DrawingPatternType.DownwardDiagonal,
                    "dnDiag"
                },
                { 
                    DrawingPatternType.DottedDiamond,
                    "dotDmnd"
                },
                { 
                    DrawingPatternType.DottedGrid,
                    "dotGrid"
                },
                { 
                    DrawingPatternType.Horizontal,
                    "horz"
                },
                { 
                    DrawingPatternType.HorizontalBrick,
                    "horzBrick"
                },
                { 
                    DrawingPatternType.LargeCheckerBoard,
                    "lgCheck"
                },
                { 
                    DrawingPatternType.LargeConfetti,
                    "lgConfetti"
                },
                { 
                    DrawingPatternType.LargeGrid,
                    "lgGrid"
                },
                { 
                    DrawingPatternType.LightDownwardDiagonal,
                    "ltDnDiag"
                },
                { 
                    DrawingPatternType.LightHorizontal,
                    "ltHorz"
                },
                { 
                    DrawingPatternType.LightUpwardDiagonal,
                    "ltUpDiag"
                },
                { 
                    DrawingPatternType.LightVertical,
                    "ltVert"
                },
                { 
                    DrawingPatternType.NarrowHorizontal,
                    "narHorz"
                },
                { 
                    DrawingPatternType.NarrowVertical,
                    "narVert"
                },
                { 
                    DrawingPatternType.OpenDiamond,
                    "openDmnd"
                },
                { 
                    DrawingPatternType.Percent10,
                    "pct10"
                },
                { 
                    DrawingPatternType.Percent20,
                    "pct20"
                },
                { 
                    DrawingPatternType.Percent25,
                    "pct25"
                },
                { 
                    DrawingPatternType.Percent30,
                    "pct30"
                },
                { 
                    DrawingPatternType.Percent40,
                    "pct40"
                },
                { 
                    DrawingPatternType.Percent5,
                    "pct5"
                },
                { 
                    DrawingPatternType.Percent50,
                    "pct50"
                },
                { 
                    DrawingPatternType.Percent60,
                    "pct60"
                },
                { 
                    DrawingPatternType.Percent70,
                    "pct70"
                },
                { 
                    DrawingPatternType.Percent75,
                    "pct75"
                },
                { 
                    DrawingPatternType.Percent80,
                    "pct80"
                },
                { 
                    DrawingPatternType.Percent90,
                    "pct90"
                },
                { 
                    DrawingPatternType.Plaid,
                    "plaid"
                },
                { 
                    DrawingPatternType.Shingle,
                    "shingle"
                },
                { 
                    DrawingPatternType.SmallCheckerBoard,
                    "smCheck"
                },
                { 
                    DrawingPatternType.SmallConfetti,
                    "smConfetti"
                },
                { 
                    DrawingPatternType.SmallGrid,
                    "smGrid"
                },
                { 
                    DrawingPatternType.SolidDiamond,
                    "solidDmnd"
                },
                { 
                    DrawingPatternType.Sphere,
                    "sphere"
                },
                { 
                    DrawingPatternType.Trellis,
                    "trellis"
                },
                { 
                    DrawingPatternType.UpwardDiagonal,
                    "upDiag"
                },
                { 
                    DrawingPatternType.Vertical,
                    "vert"
                },
                { 
                    DrawingPatternType.Wave,
                    "wave"
                },
                { 
                    DrawingPatternType.WideDownwardDiagonal,
                    "wdDnDiag"
                },
                { 
                    DrawingPatternType.WideUpwardDiagonal,
                    "wdUpDiag"
                },
                { 
                    DrawingPatternType.Weave,
                    "weave"
                },
                { 
                    DrawingPatternType.ZigZag,
                    "zigZag"
                }
            };

        private static Dictionary<OutlineEndCapStyle, string> CreateEndCapStyleTable() => 
            new Dictionary<OutlineEndCapStyle, string> { 
                { 
                    OutlineEndCapStyle.Flat,
                    "flat"
                },
                { 
                    OutlineEndCapStyle.Round,
                    "rnd"
                },
                { 
                    OutlineEndCapStyle.Square,
                    "sq"
                }
            };

        private static Dictionary<GradientType, string> CreateGradientTypeTable() => 
            new Dictionary<GradientType, string> { 
                { 
                    GradientType.Circle,
                    "circle"
                },
                { 
                    GradientType.Rectangle,
                    "rect"
                },
                { 
                    GradientType.Shape,
                    "shape"
                }
            };

        private static Dictionary<OutlineHeadTailSize, string> CreateHeadTailSizeTable() => 
            new Dictionary<OutlineHeadTailSize, string> { 
                { 
                    OutlineHeadTailSize.Large,
                    "lg"
                },
                { 
                    OutlineHeadTailSize.Medium,
                    "med"
                },
                { 
                    OutlineHeadTailSize.Small,
                    "sm"
                }
            };

        private static Dictionary<OutlineHeadTailType, string> CreateHeadTailTypeTable() => 
            new Dictionary<OutlineHeadTailType, string> { 
                { 
                    OutlineHeadTailType.None,
                    "none"
                },
                { 
                    OutlineHeadTailType.Arrow,
                    "arrow"
                },
                { 
                    OutlineHeadTailType.Diamond,
                    "diamond"
                },
                { 
                    OutlineHeadTailType.Oval,
                    "oval"
                },
                { 
                    OutlineHeadTailType.StealthArrow,
                    "stealth"
                },
                { 
                    OutlineHeadTailType.TriangleArrow,
                    "triangle"
                }
            };

        private static Dictionary<DrawingTextHorizontalOverflowType, string> CreateHorizontalOverflowTypeTable() => 
            new Dictionary<DrawingTextHorizontalOverflowType, string> { 
                { 
                    DrawingTextHorizontalOverflowType.Clip,
                    "clip"
                },
                { 
                    DrawingTextHorizontalOverflowType.Overflow,
                    "overflow"
                }
            };

        private static Dictionary<LightRigDirection, string> CreateLightRigDirectionTable() => 
            new Dictionary<LightRigDirection, string> { 
                { 
                    LightRigDirection.None,
                    "none"
                },
                { 
                    LightRigDirection.Bottom,
                    "b"
                },
                { 
                    LightRigDirection.BottomLeft,
                    "bl"
                },
                { 
                    LightRigDirection.BottomRight,
                    "br"
                },
                { 
                    LightRigDirection.Left,
                    "l"
                },
                { 
                    LightRigDirection.Right,
                    "r"
                },
                { 
                    LightRigDirection.Top,
                    "t"
                },
                { 
                    LightRigDirection.TopLeft,
                    "tl"
                },
                { 
                    LightRigDirection.TopRight,
                    "tr"
                }
            };

        private static Dictionary<LightRigPreset, string> CreateLightRigPresetTable() => 
            new Dictionary<LightRigPreset, string> { 
                { 
                    LightRigPreset.None,
                    "none"
                },
                { 
                    LightRigPreset.LegacyFlat1,
                    "legacyFlat1"
                },
                { 
                    LightRigPreset.LegacyFlat2,
                    "legacyFlat2"
                },
                { 
                    LightRigPreset.LegacyFlat3,
                    "legacyFlat3"
                },
                { 
                    LightRigPreset.LegacyFlat4,
                    "legacyFlat4"
                },
                { 
                    LightRigPreset.LegacyNormal1,
                    "legacyNormal1"
                },
                { 
                    LightRigPreset.LegacyNormal2,
                    "legacyNormal2"
                },
                { 
                    LightRigPreset.LegacyNormal3,
                    "legacyNormal3"
                },
                { 
                    LightRigPreset.LegacyNormal4,
                    "legacyNormal4"
                },
                { 
                    LightRigPreset.LegacyHarsh1,
                    "legacyHarsh1"
                },
                { 
                    LightRigPreset.LegacyHarsh2,
                    "legacyHarsh2"
                },
                { 
                    LightRigPreset.LegacyHarsh3,
                    "legacyHarsh3"
                },
                { 
                    LightRigPreset.LegacyHarsh4,
                    "legacyHarsh4"
                },
                { 
                    LightRigPreset.ThreePt,
                    "threePt"
                },
                { 
                    LightRigPreset.Balanced,
                    "balanced"
                },
                { 
                    LightRigPreset.Soft,
                    "soft"
                },
                { 
                    LightRigPreset.Harsh,
                    "harsh"
                },
                { 
                    LightRigPreset.Flood,
                    "flood"
                },
                { 
                    LightRigPreset.Contrasting,
                    "contrasting"
                },
                { 
                    LightRigPreset.Morning,
                    "morning"
                },
                { 
                    LightRigPreset.Sunrise,
                    "sunrise"
                },
                { 
                    LightRigPreset.Sunset,
                    "sunset"
                },
                { 
                    LightRigPreset.Chilly,
                    "chilly"
                },
                { 
                    LightRigPreset.Freezing,
                    "freezing"
                },
                { 
                    LightRigPreset.Flat,
                    "flat"
                },
                { 
                    LightRigPreset.TwoPt,
                    "twoPt"
                },
                { 
                    LightRigPreset.Glow,
                    "glow"
                },
                { 
                    LightRigPreset.BrightRoom,
                    "brightRoom"
                }
            };

        private static Dictionary<PresetBevelType, string> CreatePresetBevelTypeTable() => 
            new Dictionary<PresetBevelType, string> { 
                { 
                    PresetBevelType.None,
                    "none"
                },
                { 
                    PresetBevelType.RelaxedInset,
                    "relaxedInset"
                },
                { 
                    PresetBevelType.Circle,
                    "circle"
                },
                { 
                    PresetBevelType.Slope,
                    "slope"
                },
                { 
                    PresetBevelType.Cross,
                    "cross"
                },
                { 
                    PresetBevelType.Angle,
                    "angle"
                },
                { 
                    PresetBevelType.SoftRound,
                    "softRound"
                },
                { 
                    PresetBevelType.Convex,
                    "convex"
                },
                { 
                    PresetBevelType.CoolSlant,
                    "coolSlant"
                },
                { 
                    PresetBevelType.Divot,
                    "divot"
                },
                { 
                    PresetBevelType.Riblet,
                    "riblet"
                },
                { 
                    PresetBevelType.HardEdge,
                    "hardEdge"
                },
                { 
                    PresetBevelType.ArtDeco,
                    "artDeco"
                }
            };

        private static Dictionary<PresetCameraType, string> CreatePresetCameraTypeTable() => 
            new Dictionary<PresetCameraType, string> { 
                { 
                    PresetCameraType.None,
                    "none"
                },
                { 
                    PresetCameraType.LegacyObliqueTopLeft,
                    "legacyObliqueTopLeft"
                },
                { 
                    PresetCameraType.LegacyObliqueTop,
                    "legacyObliqueTop"
                },
                { 
                    PresetCameraType.LegacyObliqueTopRight,
                    "legacyObliqueTopRight"
                },
                { 
                    PresetCameraType.LegacyObliqueLeft,
                    "legacyObliqueLeft"
                },
                { 
                    PresetCameraType.LegacyObliqueFront,
                    "legacyObliqueFront"
                },
                { 
                    PresetCameraType.LegacyObliqueRight,
                    "legacyObliqueRight"
                },
                { 
                    PresetCameraType.LegacyObliqueBottomLeft,
                    "legacyObliqueBottomLeft"
                },
                { 
                    PresetCameraType.LegacyObliqueBottom,
                    "legacyObliqueBottom"
                },
                { 
                    PresetCameraType.LegacyObliqueBottomRight,
                    "legacyObliqueBottomRight"
                },
                { 
                    PresetCameraType.LegacyPerspectiveTopLeft,
                    "legacyPerspectiveTopLeft"
                },
                { 
                    PresetCameraType.LegacyPerspectiveTop,
                    "legacyPerspectiveTop"
                },
                { 
                    PresetCameraType.LegacyPerspectiveTopRight,
                    "legacyPerspectiveTopRight"
                },
                { 
                    PresetCameraType.LegacyPerspectiveLeft,
                    "legacyPerspectiveLeft"
                },
                { 
                    PresetCameraType.LegacyPerspectiveFront,
                    "legacyPerspectiveFront"
                },
                { 
                    PresetCameraType.LegacyPerspectiveRight,
                    "legacyPerspectiveRight"
                },
                { 
                    PresetCameraType.LegacyPerspectiveBottomLeft,
                    "legacyPerspectiveBottomLeft"
                },
                { 
                    PresetCameraType.LegacyPerspectiveBottom,
                    "legacyPerspectiveBottom"
                },
                { 
                    PresetCameraType.LegacyPerspectiveBottomRight,
                    "legacyPerspectiveBottomRight"
                },
                { 
                    PresetCameraType.OrthographicFront,
                    "orthographicFront"
                },
                { 
                    PresetCameraType.IsometricTopUp,
                    "isometricTopUp"
                },
                { 
                    PresetCameraType.IsometricTopDown,
                    "isometricTopDown"
                },
                { 
                    PresetCameraType.IsometricBottomUp,
                    "isometricBottomUp"
                },
                { 
                    PresetCameraType.IsometricBottomDown,
                    "isometricBottomDown"
                },
                { 
                    PresetCameraType.IsometricLeftUp,
                    "isometricLeftUp"
                },
                { 
                    PresetCameraType.IsometricLeftDown,
                    "isometricLeftDown"
                },
                { 
                    PresetCameraType.IsometricRightUp,
                    "isometricRightUp"
                },
                { 
                    PresetCameraType.IsometricRightDown,
                    "isometricRightDown"
                },
                { 
                    PresetCameraType.IsometricOffAxis1Left,
                    "isometricOffAxis1Left"
                },
                { 
                    PresetCameraType.IsometricOffAxis1Right,
                    "isometricOffAxis1Right"
                },
                { 
                    PresetCameraType.IsometricOffAxis1Top,
                    "isometricOffAxis1Top"
                },
                { 
                    PresetCameraType.IsometricOffAxis2Left,
                    "isometricOffAxis2Left"
                },
                { 
                    PresetCameraType.IsometricOffAxis2Right,
                    "isometricOffAxis2Right"
                },
                { 
                    PresetCameraType.IsometricOffAxis2Top,
                    "isometricOffAxis2Top"
                },
                { 
                    PresetCameraType.IsometricOffAxis3Left,
                    "isometricOffAxis3Left"
                },
                { 
                    PresetCameraType.IsometricOffAxis3Right,
                    "isometricOffAxis3Right"
                },
                { 
                    PresetCameraType.IsometricOffAxis3Bottom,
                    "isometricOffAxis3Bottom"
                },
                { 
                    PresetCameraType.IsometricOffAxis4Left,
                    "isometricOffAxis4Left"
                },
                { 
                    PresetCameraType.IsometricOffAxis4Right,
                    "isometricOffAxis4Right"
                },
                { 
                    PresetCameraType.IsometricOffAxis4Bottom,
                    "isometricOffAxis4Bottom"
                },
                { 
                    PresetCameraType.ObliqueTopLeft,
                    "obliqueTopLeft"
                },
                { 
                    PresetCameraType.ObliqueTop,
                    "obliqueTop"
                },
                { 
                    PresetCameraType.ObliqueTopRight,
                    "obliqueTopRight"
                },
                { 
                    PresetCameraType.ObliqueLeft,
                    "obliqueLeft"
                },
                { 
                    PresetCameraType.ObliqueRight,
                    "obliqueRight"
                },
                { 
                    PresetCameraType.ObliqueBottomLeft,
                    "obliqueBottomLeft"
                },
                { 
                    PresetCameraType.ObliqueBottom,
                    "obliqueBottom"
                },
                { 
                    PresetCameraType.ObliqueBottomRight,
                    "obliqueBottomRight"
                },
                { 
                    PresetCameraType.PerspectiveFront,
                    "perspectiveFront"
                },
                { 
                    PresetCameraType.PerspectiveLeft,
                    "perspectiveLeft"
                },
                { 
                    PresetCameraType.PerspectiveRight,
                    "perspectiveRight"
                },
                { 
                    PresetCameraType.PerspectiveAbove,
                    "perspectiveAbove"
                },
                { 
                    PresetCameraType.PerspectiveBelow,
                    "perspectiveBelow"
                },
                { 
                    PresetCameraType.PerspectiveAboveLeftFacing,
                    "perspectiveAboveLeftFacing"
                },
                { 
                    PresetCameraType.PerspectiveAboveRightFacing,
                    "perspectiveAboveRightFacing"
                },
                { 
                    PresetCameraType.PerspectiveContrastingLeftFacing,
                    "perspectiveContrastingLeftFacing"
                },
                { 
                    PresetCameraType.PerspectiveContrastingRightFacing,
                    "perspectiveContrastingRightFacing"
                },
                { 
                    PresetCameraType.PerspectiveHeroicLeftFacing,
                    "perspectiveHeroicLeftFacing"
                },
                { 
                    PresetCameraType.PerspectiveHeroicRightFacing,
                    "perspectiveHeroicRightFacing"
                },
                { 
                    PresetCameraType.PerspectiveHeroicExtremeLeftFacing,
                    "perspectiveHeroicExtremeLeftFacing"
                },
                { 
                    PresetCameraType.PerspectiveHeroicExtremeRightFacing,
                    "perspectiveHeroicExtremeRightFacing"
                },
                { 
                    PresetCameraType.PerspectiveRelaxed,
                    "perspectiveRelaxed"
                },
                { 
                    PresetCameraType.PerspectiveRelaxedModerately,
                    "perspectiveRelaxedModerately"
                }
            };

        private static Dictionary<OutlineDashing, string> CreatePresetDashTable() => 
            new Dictionary<OutlineDashing, string> { 
                { 
                    OutlineDashing.Solid,
                    "solid"
                },
                { 
                    OutlineDashing.Dash,
                    "dash"
                },
                { 
                    OutlineDashing.DashDot,
                    "dashDot"
                },
                { 
                    OutlineDashing.Dot,
                    "dot"
                },
                { 
                    OutlineDashing.LongDash,
                    "lgDash"
                },
                { 
                    OutlineDashing.LongDashDot,
                    "lgDashDot"
                },
                { 
                    OutlineDashing.LongDashDotDot,
                    "lgDashDotDot"
                },
                { 
                    OutlineDashing.SystemDash,
                    "sysDash"
                },
                { 
                    OutlineDashing.SystemDashDot,
                    "sysDashDot"
                },
                { 
                    OutlineDashing.SystemDashDotDot,
                    "sysDashDotDot"
                },
                { 
                    OutlineDashing.SystemDot,
                    "sysDot"
                }
            };

        private static Dictionary<PresetMaterialType, string> CreatePresetMaterialTypeTable() => 
            new Dictionary<PresetMaterialType, string> { 
                { 
                    PresetMaterialType.None,
                    "none"
                },
                { 
                    PresetMaterialType.LegacyMatte,
                    "legacyMatte"
                },
                { 
                    PresetMaterialType.LegacyPlastic,
                    "legacyPlastic"
                },
                { 
                    PresetMaterialType.LegacyMetal,
                    "legacyMetal"
                },
                { 
                    PresetMaterialType.LegacyWireframe,
                    "legacyWireframe"
                },
                { 
                    PresetMaterialType.Matte,
                    "matte"
                },
                { 
                    PresetMaterialType.Plastic,
                    "plastic"
                },
                { 
                    PresetMaterialType.Metal,
                    "metal"
                },
                { 
                    PresetMaterialType.WarmMatte,
                    "warmMatte"
                },
                { 
                    PresetMaterialType.TranslucentPowder,
                    "translucentPowder"
                },
                { 
                    PresetMaterialType.Powder,
                    "powder"
                },
                { 
                    PresetMaterialType.DarkEdge,
                    "dkEdge"
                },
                { 
                    PresetMaterialType.SoftEdge,
                    "softEdge"
                },
                { 
                    PresetMaterialType.Clear,
                    "clear"
                },
                { 
                    PresetMaterialType.Flat,
                    "flat"
                },
                { 
                    PresetMaterialType.SoftMetal,
                    "softmetal"
                }
            };

        private static Dictionary<PresetShadowType, string> CreatePresetShadowTypeTable() => 
            new Dictionary<PresetShadowType, string> { 
                { 
                    PresetShadowType.TopLeftDrop,
                    "shdw1"
                },
                { 
                    PresetShadowType.TopRightDrop,
                    "shdw2"
                },
                { 
                    PresetShadowType.BackLeftPerspective,
                    "shdw3"
                },
                { 
                    PresetShadowType.BackRightPerspective,
                    "shdw4"
                },
                { 
                    PresetShadowType.BottomLeftDrop,
                    "shdw5"
                },
                { 
                    PresetShadowType.BottomRightDrop,
                    "shdw6"
                },
                { 
                    PresetShadowType.FrontLeftPerspective,
                    "shdw7"
                },
                { 
                    PresetShadowType.FrontRightPerspective,
                    "shdw8"
                },
                { 
                    PresetShadowType.TopLeftSmallDrop,
                    "shdw9"
                },
                { 
                    PresetShadowType.TopLeftLargeDrop,
                    "shdw10"
                },
                { 
                    PresetShadowType.BackLeftLongPerspective,
                    "shdw11"
                },
                { 
                    PresetShadowType.BackRightLongPerspective,
                    "shdw12"
                },
                { 
                    PresetShadowType.TopLeftDoubleDrop,
                    "shdw13"
                },
                { 
                    PresetShadowType.BottomRightSmallDrop,
                    "shdw14"
                },
                { 
                    PresetShadowType.FrontLeftLongPerspective,
                    "shdw15"
                },
                { 
                    PresetShadowType.FrontRightLongPerspective,
                    "shdw16"
                },
                { 
                    PresetShadowType.OuterBox3d,
                    "shdw17"
                },
                { 
                    PresetShadowType.InnerBox3d,
                    "shdw18"
                },
                { 
                    PresetShadowType.BackCenterPerspective,
                    "shdw19"
                },
                { 
                    PresetShadowType.FrontBottomShadow,
                    "shdw20"
                }
            };

        private static Dictionary<DrawingPresetTextWarp, string> CreatePresetTextWarpTable() => 
            new Dictionary<DrawingPresetTextWarp, string> { 
                { 
                    DrawingPresetTextWarp.ArchDown,
                    "textArchDown"
                },
                { 
                    DrawingPresetTextWarp.ArchDownPour,
                    "textArchDownPour"
                },
                { 
                    DrawingPresetTextWarp.ArchUp,
                    "textArchUp"
                },
                { 
                    DrawingPresetTextWarp.ArchUpPour,
                    "textArchUpPour"
                },
                { 
                    DrawingPresetTextWarp.Button,
                    "textButton"
                },
                { 
                    DrawingPresetTextWarp.ButtonPour,
                    "textButtonPour"
                },
                { 
                    DrawingPresetTextWarp.CanDown,
                    "textCanDown"
                },
                { 
                    DrawingPresetTextWarp.CanUp,
                    "textCanUp"
                },
                { 
                    DrawingPresetTextWarp.CascadeDown,
                    "textCascadeDown"
                },
                { 
                    DrawingPresetTextWarp.CascadeUp,
                    "textCascadeUp"
                },
                { 
                    DrawingPresetTextWarp.Chevron,
                    "textChevron"
                },
                { 
                    DrawingPresetTextWarp.ChevronInverted,
                    "textChevronInverted"
                },
                { 
                    DrawingPresetTextWarp.Circle,
                    "textCircle"
                },
                { 
                    DrawingPresetTextWarp.CirclePour,
                    "textCirclePour"
                },
                { 
                    DrawingPresetTextWarp.CurveDown,
                    "textCurveDown"
                },
                { 
                    DrawingPresetTextWarp.CurveUp,
                    "textCurveUp"
                },
                { 
                    DrawingPresetTextWarp.Deflate,
                    "textDeflate"
                },
                { 
                    DrawingPresetTextWarp.DeflateBottom,
                    "textDeflateBottom"
                },
                { 
                    DrawingPresetTextWarp.DeflateInflate,
                    "textDeflateInflate"
                },
                { 
                    DrawingPresetTextWarp.InflateDeflate,
                    "textDeflateInflateDeflate"
                },
                { 
                    DrawingPresetTextWarp.DeflateTop,
                    "textDeflateTop"
                },
                { 
                    DrawingPresetTextWarp.DoubleWave1,
                    "textDoubleWave1"
                },
                { 
                    DrawingPresetTextWarp.FadeDown,
                    "textFadeDown"
                },
                { 
                    DrawingPresetTextWarp.FadeLeft,
                    "textFadeLeft"
                },
                { 
                    DrawingPresetTextWarp.FadeRight,
                    "textFadeRight"
                },
                { 
                    DrawingPresetTextWarp.FadeUp,
                    "textFadeUp"
                },
                { 
                    DrawingPresetTextWarp.Inflate,
                    "textInflate"
                },
                { 
                    DrawingPresetTextWarp.InflateBottom,
                    "textInflateBottom"
                },
                { 
                    DrawingPresetTextWarp.InflateTop,
                    "textInflateTop"
                },
                { 
                    DrawingPresetTextWarp.Plain,
                    "textPlain"
                },
                { 
                    DrawingPresetTextWarp.RingInside,
                    "textRingInside"
                },
                { 
                    DrawingPresetTextWarp.RingOutside,
                    "textRingOutside"
                },
                { 
                    DrawingPresetTextWarp.SlantDown,
                    "textSlantDown"
                },
                { 
                    DrawingPresetTextWarp.SlantUp,
                    "textSlantUp"
                },
                { 
                    DrawingPresetTextWarp.Stop,
                    "textStop"
                },
                { 
                    DrawingPresetTextWarp.Triangle,
                    "textTriangle"
                },
                { 
                    DrawingPresetTextWarp.TriangleInverted,
                    "textTriangleInverted"
                },
                { 
                    DrawingPresetTextWarp.Wave1,
                    "textWave1"
                },
                { 
                    DrawingPresetTextWarp.Wave2,
                    "textWave2"
                },
                { 
                    DrawingPresetTextWarp.Wave4,
                    "textWave4"
                },
                { 
                    DrawingPresetTextWarp.NoShape,
                    "textNoShape"
                }
            };

        private static Dictionary<RectangleAlignType, string> CreateRectangleAlignTypeTable() => 
            new Dictionary<RectangleAlignType, string> { 
                { 
                    RectangleAlignType.TopLeft,
                    "tl"
                },
                { 
                    RectangleAlignType.Top,
                    "t"
                },
                { 
                    RectangleAlignType.TopRight,
                    "tr"
                },
                { 
                    RectangleAlignType.Left,
                    "l"
                },
                { 
                    RectangleAlignType.Center,
                    "ctr"
                },
                { 
                    RectangleAlignType.Right,
                    "r"
                },
                { 
                    RectangleAlignType.BottomLeft,
                    "bl"
                },
                { 
                    RectangleAlignType.Bottom,
                    "b"
                },
                { 
                    RectangleAlignType.BottomRight,
                    "br"
                }
            };

        private static Dictionary<OutlineStrokeAlignment, string> CreateStrokeAlignmentTable() => 
            new Dictionary<OutlineStrokeAlignment, string> { 
                { 
                    OutlineStrokeAlignment.None,
                    "none"
                },
                { 
                    OutlineStrokeAlignment.Center,
                    "ctr"
                },
                { 
                    OutlineStrokeAlignment.Inset,
                    "in"
                }
            };

        private static Dictionary<DrawingTextWrappingType, string> CreateTextWrappingTypeTable() => 
            new Dictionary<DrawingTextWrappingType, string> { 
                { 
                    DrawingTextWrappingType.None,
                    "none"
                },
                { 
                    DrawingTextWrappingType.Square,
                    "square"
                }
            };

        private static Dictionary<TileFlipType, string> CreateTileFlipTypeTable() => 
            new Dictionary<TileFlipType, string> { 
                { 
                    TileFlipType.None,
                    "none"
                },
                { 
                    TileFlipType.Horizontal,
                    "x"
                },
                { 
                    TileFlipType.Vertical,
                    "y"
                },
                { 
                    TileFlipType.Both,
                    "xy"
                }
            };

        private static Dictionary<DrawingTextVerticalOverflowType, string> CreateVerticalOverflowTypeTable() => 
            new Dictionary<DrawingTextVerticalOverflowType, string> { 
                { 
                    DrawingTextVerticalOverflowType.Clip,
                    "clip"
                },
                { 
                    DrawingTextVerticalOverflowType.Overflow,
                    "overflow"
                },
                { 
                    DrawingTextVerticalOverflowType.Ellipsis,
                    "ellipsis"
                }
            };

        private static Dictionary<DrawingTextVerticalTextType, string> CreateVerticalTextTypeTable() => 
            new Dictionary<DrawingTextVerticalTextType, string> { 
                { 
                    DrawingTextVerticalTextType.Horizontal,
                    "horz"
                },
                { 
                    DrawingTextVerticalTextType.Vertical,
                    "vert"
                },
                { 
                    DrawingTextVerticalTextType.Vertical270,
                    "vert270"
                },
                { 
                    DrawingTextVerticalTextType.WordArtVertical,
                    "wordArtVert"
                },
                { 
                    DrawingTextVerticalTextType.EastAsianVertical,
                    "eaVert"
                },
                { 
                    DrawingTextVerticalTextType.MongolianVertical,
                    "mongolianVert"
                },
                { 
                    DrawingTextVerticalTextType.VerticalWordArtRightToLeft,
                    "wordArtVertRtl"
                }
            };

        private static Dictionary<VmlBlackAndWhiteMode, string> CreateVmlBlackAndWhiteModeTable() => 
            new Dictionary<VmlBlackAndWhiteMode, string> { 
                { 
                    VmlBlackAndWhiteMode.Auto,
                    "auto"
                },
                { 
                    VmlBlackAndWhiteMode.Black,
                    "black"
                },
                { 
                    VmlBlackAndWhiteMode.BlackTextAndLines,
                    "blackTextAndLines"
                },
                { 
                    VmlBlackAndWhiteMode.Color,
                    "color"
                },
                { 
                    VmlBlackAndWhiteMode.GrayOutline,
                    "grayOutline"
                },
                { 
                    VmlBlackAndWhiteMode.GrayScale,
                    "grayScale"
                },
                { 
                    VmlBlackAndWhiteMode.Hide,
                    "hide"
                },
                { 
                    VmlBlackAndWhiteMode.HighContrast,
                    "highContrast"
                },
                { 
                    VmlBlackAndWhiteMode.InverseGray,
                    "inverseGray"
                },
                { 
                    VmlBlackAndWhiteMode.LightGrayscale,
                    "lightGrayscale"
                },
                { 
                    VmlBlackAndWhiteMode.Undrawn,
                    "undrawn"
                },
                { 
                    VmlBlackAndWhiteMode.White,
                    "white"
                }
            };

        private static Dictionary<VmlConnectType, string> CreateVmlConnectTypeTable() => 
            new Dictionary<VmlConnectType, string> { 
                { 
                    VmlConnectType.Custom,
                    "custom"
                },
                { 
                    VmlConnectType.None,
                    "none"
                },
                { 
                    VmlConnectType.Rect,
                    "rect"
                },
                { 
                    VmlConnectType.Segments,
                    "segments"
                }
            };

        private static Dictionary<VmlDashStyle, string> CreateVmlDashStyleTable() => 
            new Dictionary<VmlDashStyle, string> { 
                { 
                    VmlDashStyle.Solid,
                    "solid"
                },
                { 
                    VmlDashStyle.ShortDash,
                    "shortdash"
                },
                { 
                    VmlDashStyle.ShortDot,
                    "shortdot"
                },
                { 
                    VmlDashStyle.ShortDashDot,
                    "shortdashdot"
                },
                { 
                    VmlDashStyle.ShortDashDotDot,
                    "shortdashdotdot"
                },
                { 
                    VmlDashStyle.Dot,
                    "dot"
                },
                { 
                    VmlDashStyle.Dash,
                    "dash"
                },
                { 
                    VmlDashStyle.LongDash,
                    "longdash"
                },
                { 
                    VmlDashStyle.DashDot,
                    "dashdot"
                },
                { 
                    VmlDashStyle.LongDashDot,
                    "longdashdot"
                },
                { 
                    VmlDashStyle.LongDashDotDot,
                    "longdashdotdot"
                }
            };

        private static Dictionary<VmlExtensionHandlingBehavior, string> CreateVmlExtensionHandlingBehaviorTable() => 
            new Dictionary<VmlExtensionHandlingBehavior, string> { 
                { 
                    VmlExtensionHandlingBehavior.View,
                    "view"
                },
                { 
                    VmlExtensionHandlingBehavior.Edit,
                    "edit"
                },
                { 
                    VmlExtensionHandlingBehavior.BackwardCompatible,
                    "backwardCompatible"
                }
            };

        private static Dictionary<VmlFillMethod, string> CreateVmlFillMethodTable() => 
            new Dictionary<VmlFillMethod, string> { 
                { 
                    VmlFillMethod.Any,
                    "any"
                },
                { 
                    VmlFillMethod.Linear,
                    "linear"
                },
                { 
                    VmlFillMethod.LinearSigma,
                    "linear sigma"
                },
                { 
                    VmlFillMethod.None,
                    "none"
                },
                { 
                    VmlFillMethod.Sigma,
                    "sigma"
                }
            };

        private static Dictionary<VmlFillType, string> CreateVmlFillTypeTable() => 
            new Dictionary<VmlFillType, string> { 
                { 
                    VmlFillType.Frame,
                    "frame"
                },
                { 
                    VmlFillType.Gradient,
                    "gradient"
                },
                { 
                    VmlFillType.GradientRadial,
                    "gradientRadial"
                },
                { 
                    VmlFillType.Pattern,
                    "pattern"
                },
                { 
                    VmlFillType.Solid,
                    "solid"
                },
                { 
                    VmlFillType.Tile,
                    "tile"
                }
            };

        private static Dictionary<VmlGroupEditAs, string> CreateVmlGroupEditAsTable() => 
            new Dictionary<VmlGroupEditAs, string> { 
                { 
                    VmlGroupEditAs.Bullseye,
                    "bullseye"
                },
                { 
                    VmlGroupEditAs.Canvas,
                    "canvas"
                },
                { 
                    VmlGroupEditAs.Cycle,
                    "cycle"
                },
                { 
                    VmlGroupEditAs.OrganizationChart,
                    "orgchart"
                },
                { 
                    VmlGroupEditAs.Radial,
                    "radial"
                },
                { 
                    VmlGroupEditAs.Stacked,
                    "stacked"
                },
                { 
                    VmlGroupEditAs.Venn,
                    "venn"
                }
            };

        private static Dictionary<VmlImageAspect, string> CreateVmlImageAspectTable() => 
            new Dictionary<VmlImageAspect, string> { 
                { 
                    VmlImageAspect.AtLeast,
                    "atLeast"
                },
                { 
                    VmlImageAspect.AtMost,
                    "atMost"
                },
                { 
                    VmlImageAspect.Ignore,
                    "ignore"
                }
            };

        private static Dictionary<VmlInsetMode, string> CreateVmlInsetModeTable() => 
            new Dictionary<VmlInsetMode, string> { 
                { 
                    VmlInsetMode.Auto,
                    "auto"
                },
                { 
                    VmlInsetMode.Custom,
                    "custom"
                }
            };

        private static Dictionary<VmlLineStyle, string> CreateVmlLineStyleTable() => 
            new Dictionary<VmlLineStyle, string> { 
                { 
                    VmlLineStyle.Single,
                    "single"
                },
                { 
                    VmlLineStyle.ThinThin,
                    "thinThin"
                },
                { 
                    VmlLineStyle.ThinThick,
                    "thinThick"
                },
                { 
                    VmlLineStyle.ThickThin,
                    "thickThin"
                },
                { 
                    VmlLineStyle.ThickBetweenThin,
                    "thickBetweenThin"
                }
            };

        private static Dictionary<VmlShadowType, string> CreateVmlShadowTypeTable() => 
            new Dictionary<VmlShadowType, string> { 
                { 
                    VmlShadowType.Single,
                    "single"
                },
                { 
                    VmlShadowType.Double,
                    "double"
                },
                { 
                    VmlShadowType.Perspective,
                    "perspective"
                },
                { 
                    VmlShadowType.ShapeRelative,
                    "shaperelative"
                },
                { 
                    VmlShadowType.DrawingRelative,
                    "drawingrelative"
                },
                { 
                    VmlShadowType.Emboss,
                    "emboss"
                }
            };

        private static Dictionary<VMLStrokeArrowLength, string> CreateVMLStrokeArrowLengthTable() => 
            new Dictionary<VMLStrokeArrowLength, string> { 
                { 
                    VMLStrokeArrowLength.Short,
                    "short"
                },
                { 
                    VMLStrokeArrowLength.Medium,
                    "medium"
                },
                { 
                    VMLStrokeArrowLength.Long,
                    "long"
                }
            };

        private static Dictionary<VMLStrokeArrowType, string> CreateVMLStrokeArrowTypeTable() => 
            new Dictionary<VMLStrokeArrowType, string> { 
                { 
                    VMLStrokeArrowType.None,
                    "none"
                },
                { 
                    VMLStrokeArrowType.Block,
                    "block"
                },
                { 
                    VMLStrokeArrowType.Classic,
                    "classic"
                },
                { 
                    VMLStrokeArrowType.Diamond,
                    "diamond"
                },
                { 
                    VMLStrokeArrowType.Open,
                    "open"
                },
                { 
                    VMLStrokeArrowType.Oval,
                    "oval"
                }
            };

        private static Dictionary<VMLStrokeArrowWidth, string> CreateVMLStrokeArrowWidthTable() => 
            new Dictionary<VMLStrokeArrowWidth, string> { 
                { 
                    VMLStrokeArrowWidth.Narrow,
                    "narrow"
                },
                { 
                    VMLStrokeArrowWidth.Medium,
                    "medium"
                },
                { 
                    VMLStrokeArrowWidth.Wide,
                    "wide"
                }
            };

        private static Dictionary<VmlStrokeJoinStyle, string> CreateVmlStrokeJoinStyleTable() => 
            new Dictionary<VmlStrokeJoinStyle, string> { 
                { 
                    VmlStrokeJoinStyle.Bevel,
                    "bevel"
                },
                { 
                    VmlStrokeJoinStyle.Miter,
                    "miter"
                },
                { 
                    VmlStrokeJoinStyle.Round,
                    "round"
                }
            };

        private static Dictionary<BlendMode, string> GetBlendModeTable() => 
            new Dictionary<BlendMode, string> { 
                { 
                    BlendMode.Darken,
                    "darken"
                },
                { 
                    BlendMode.Lighten,
                    "lighten"
                },
                { 
                    BlendMode.Multiply,
                    "mult"
                },
                { 
                    BlendMode.Overlay,
                    "over"
                },
                { 
                    BlendMode.Screen,
                    "screen"
                }
            };

        private static Dictionary<DrawingEffectContainerType, string> GetDrawingEffectContainerTypeTable() => 
            new Dictionary<DrawingEffectContainerType, string> { 
                { 
                    DrawingEffectContainerType.Sibling,
                    "sib"
                },
                { 
                    DrawingEffectContainerType.Tree,
                    "tree"
                }
            };

        private static Dictionary<XlFontSchemeStyles, string> GetFontCollectionIndexTable() => 
            new Dictionary<XlFontSchemeStyles, string> { 
                { 
                    XlFontSchemeStyles.Major,
                    "major"
                },
                { 
                    XlFontSchemeStyles.Minor,
                    "minor"
                },
                { 
                    XlFontSchemeStyles.None,
                    "none"
                }
            };

        public static Dictionary<DrawingTextAnchoringType, string> AnchoringTypeTable
        {
            get
            {
                if (anchoringTypeTable == null)
                {
                    object syncAnchoringTypeTable = OpenXmlExporterBase.syncAnchoringTypeTable;
                    lock (syncAnchoringTypeTable)
                    {
                        anchoringTypeTable ??= CreateAnchoringTypeTable();
                    }
                }
                return anchoringTypeTable;
            }
        }

        public static Dictionary<DrawingTextHorizontalOverflowType, string> HorizontalOverflowTypeTable
        {
            get
            {
                if (horizontalOverflowTypeTable == null)
                {
                    object syncHorizontalOverflowTypeTable = OpenXmlExporterBase.syncHorizontalOverflowTypeTable;
                    lock (syncHorizontalOverflowTypeTable)
                    {
                        horizontalOverflowTypeTable ??= CreateHorizontalOverflowTypeTable();
                    }
                }
                return horizontalOverflowTypeTable;
            }
        }

        public static Dictionary<DrawingTextVerticalOverflowType, string> VerticalOverflowTypeTable
        {
            get
            {
                if (verticalOverflowTypeTable == null)
                {
                    object syncVerticalOverflowTypeTable = OpenXmlExporterBase.syncVerticalOverflowTypeTable;
                    lock (syncVerticalOverflowTypeTable)
                    {
                        verticalOverflowTypeTable ??= CreateVerticalOverflowTypeTable();
                    }
                }
                return verticalOverflowTypeTable;
            }
        }

        public static Dictionary<DrawingTextVerticalTextType, string> VerticalTextTypeTable
        {
            get
            {
                if (verticalTextTypeTable == null)
                {
                    object syncVerticalTextTypeTable = OpenXmlExporterBase.syncVerticalTextTypeTable;
                    lock (syncVerticalTextTypeTable)
                    {
                        verticalTextTypeTable ??= CreateVerticalTextTypeTable();
                    }
                }
                return verticalTextTypeTable;
            }
        }

        public static Dictionary<DrawingTextWrappingType, string> TextWrappingTypeTable
        {
            get
            {
                if (textWrappingTypeTable == null)
                {
                    object syncTextWrappingTypeTable = OpenXmlExporterBase.syncTextWrappingTypeTable;
                    lock (syncTextWrappingTypeTable)
                    {
                        textWrappingTypeTable ??= CreateTextWrappingTypeTable();
                    }
                }
                return textWrappingTypeTable;
            }
        }

        public static Dictionary<DrawingPresetTextWarp, string> PresetTextWarpTable
        {
            get
            {
                if (presetTextWarpTable == null)
                {
                    object syncPresetTextWarpTable = OpenXmlExporterBase.syncPresetTextWarpTable;
                    lock (syncPresetTextWarpTable)
                    {
                        presetTextWarpTable ??= CreatePresetTextWarpTable();
                    }
                }
                return presetTextWarpTable;
            }
        }

        public static Dictionary<TileFlipType, string> TileFlipTypeTable
        {
            get
            {
                if (tileFlipTypeTable == null)
                {
                    object syncTileFlipTypeTable = OpenXmlExporterBase.syncTileFlipTypeTable;
                    lock (syncTileFlipTypeTable)
                    {
                        tileFlipTypeTable ??= CreateTileFlipTypeTable();
                    }
                }
                return tileFlipTypeTable;
            }
        }

        public static Dictionary<GradientType, string> GradientTypeTable
        {
            get
            {
                if (gradientTypeTable == null)
                {
                    object syncGradientTypeTable = OpenXmlExporterBase.syncGradientTypeTable;
                    lock (syncGradientTypeTable)
                    {
                        gradientTypeTable ??= CreateGradientTypeTable();
                    }
                }
                return gradientTypeTable;
            }
        }

        public static Dictionary<DrawingPatternType, string> DrawingPatternTypeTable
        {
            get
            {
                if (drawingPatternTypeTable == null)
                {
                    object syncDrawingPatternTypeTable = OpenXmlExporterBase.syncDrawingPatternTypeTable;
                    lock (syncDrawingPatternTypeTable)
                    {
                        drawingPatternTypeTable ??= CreateDrawingPatternTypeTable();
                    }
                }
                return drawingPatternTypeTable;
            }
        }

        public static Dictionary<RectangleAlignType, string> RectangleAlignTypeTable
        {
            get
            {
                if (rectangleAlignTypeTable == null)
                {
                    object syncRectangleAlignTypeTable = OpenXmlExporterBase.syncRectangleAlignTypeTable;
                    lock (syncRectangleAlignTypeTable)
                    {
                        rectangleAlignTypeTable ??= CreateRectangleAlignTypeTable();
                    }
                }
                return rectangleAlignTypeTable;
            }
        }

        public static Dictionary<CompressionState, string> CompressionStateTable
        {
            get
            {
                if (compressionStateTable == null)
                {
                    object syncCompressionStateTable = OpenXmlExporterBase.syncCompressionStateTable;
                    lock (syncCompressionStateTable)
                    {
                        compressionStateTable ??= CreateCompressionStateTable();
                    }
                }
                return compressionStateTable;
            }
        }

        public static Dictionary<BlendMode, string> BlendModeTable
        {
            get
            {
                if (blendModeTable == null)
                {
                    object syncBlendModeTable = OpenXmlExporterBase.syncBlendModeTable;
                    lock (syncBlendModeTable)
                    {
                        blendModeTable ??= GetBlendModeTable();
                    }
                }
                return blendModeTable;
            }
        }

        public static Dictionary<PresetShadowType, string> PresetShadowTypeTable
        {
            get
            {
                if (presetShadowTypeTable == null)
                {
                    object syncPresetShadowTypeTable = OpenXmlExporterBase.syncPresetShadowTypeTable;
                    lock (syncPresetShadowTypeTable)
                    {
                        presetShadowTypeTable ??= CreatePresetShadowTypeTable();
                    }
                }
                return presetShadowTypeTable;
            }
        }

        public static Dictionary<DrawingEffectContainerType, string> DrawingEffectContainerTypeTable
        {
            get
            {
                if (drawingEffectContainerTypeTable == null)
                {
                    object syncDrawingEffectContainerTypeTable = OpenXmlExporterBase.syncDrawingEffectContainerTypeTable;
                    lock (syncDrawingEffectContainerTypeTable)
                    {
                        drawingEffectContainerTypeTable ??= GetDrawingEffectContainerTypeTable();
                    }
                }
                return drawingEffectContainerTypeTable;
            }
        }

        public static Dictionary<OutlineStrokeAlignment, string> StrokeAlignmentTable
        {
            get
            {
                if (strokeAlignmentTable == null)
                {
                    object syncStrokeAlignmentTable = OpenXmlExporterBase.syncStrokeAlignmentTable;
                    lock (syncStrokeAlignmentTable)
                    {
                        strokeAlignmentTable ??= CreateStrokeAlignmentTable();
                    }
                }
                return strokeAlignmentTable;
            }
        }

        public static Dictionary<OutlineEndCapStyle, string> EndCapStyleTable
        {
            get
            {
                if (endCapStyleTable == null)
                {
                    object syncEndCapStyleTable = OpenXmlExporterBase.syncEndCapStyleTable;
                    lock (syncEndCapStyleTable)
                    {
                        endCapStyleTable ??= CreateEndCapStyleTable();
                    }
                }
                return endCapStyleTable;
            }
        }

        public static Dictionary<OutlineCompoundType, string> CompoundTypeTable
        {
            get
            {
                if (compoundTypeTable == null)
                {
                    object syncCompoundTypeTable = OpenXmlExporterBase.syncCompoundTypeTable;
                    lock (syncCompoundTypeTable)
                    {
                        compoundTypeTable ??= CreateCompoundTypeTable();
                    }
                }
                return compoundTypeTable;
            }
        }

        public static Dictionary<OutlineDashing, string> PresetDashTable
        {
            get
            {
                if (presetDashTable == null)
                {
                    object syncPresetDashTable = OpenXmlExporterBase.syncPresetDashTable;
                    lock (syncPresetDashTable)
                    {
                        presetDashTable ??= CreatePresetDashTable();
                    }
                }
                return presetDashTable;
            }
        }

        public static Dictionary<OutlineHeadTailSize, string> HeadTailSizeTable
        {
            get
            {
                if (headTailSizeTable == null)
                {
                    object syncHeadTailSizeTable = OpenXmlExporterBase.syncHeadTailSizeTable;
                    lock (syncHeadTailSizeTable)
                    {
                        headTailSizeTable ??= CreateHeadTailSizeTable();
                    }
                }
                return headTailSizeTable;
            }
        }

        public static Dictionary<VMLStrokeArrowType, string> VmlStrokeArrowTypeTable
        {
            get
            {
                if (vmlStrokeArrowTypeTable == null)
                {
                    object syncVmlStrokeArrowTypeTable = OpenXmlExporterBase.syncVmlStrokeArrowTypeTable;
                    lock (syncVmlStrokeArrowTypeTable)
                    {
                        vmlStrokeArrowTypeTable ??= CreateVMLStrokeArrowTypeTable();
                    }
                }
                return vmlStrokeArrowTypeTable;
            }
        }

        public static Dictionary<VMLStrokeArrowLength, string> VmlStrokeArrowLengthTable
        {
            get
            {
                if (vmlStrokeArrowLengthTable == null)
                {
                    object syncVmlStrokeArrowLengthTable = OpenXmlExporterBase.syncVmlStrokeArrowLengthTable;
                    lock (syncVmlStrokeArrowLengthTable)
                    {
                        vmlStrokeArrowLengthTable ??= CreateVMLStrokeArrowLengthTable();
                    }
                }
                return vmlStrokeArrowLengthTable;
            }
        }

        public static Dictionary<VMLStrokeArrowWidth, string> VmlStrokeArrowWidthTable
        {
            get
            {
                if (vmlStrokeArrowWidthTable == null)
                {
                    object syncVmlStrokeArrowWidthTable = OpenXmlExporterBase.syncVmlStrokeArrowWidthTable;
                    lock (syncVmlStrokeArrowWidthTable)
                    {
                        vmlStrokeArrowWidthTable ??= CreateVMLStrokeArrowWidthTable();
                    }
                }
                return vmlStrokeArrowWidthTable;
            }
        }

        public static Dictionary<OutlineHeadTailType, string> HeadTailTypeTable
        {
            get
            {
                if (headTailTypeTable == null)
                {
                    object syncHeadTailTypeTable = OpenXmlExporterBase.syncHeadTailTypeTable;
                    lock (syncHeadTailTypeTable)
                    {
                        headTailTypeTable ??= CreateHeadTailTypeTable();
                    }
                }
                return headTailTypeTable;
            }
        }

        public static Dictionary<PresetCameraType, string> PresetCameraTypeTable
        {
            get
            {
                if (presetCameraTypeTable == null)
                {
                    object syncPresetCameraTypeTable = OpenXmlExporterBase.syncPresetCameraTypeTable;
                    lock (syncPresetCameraTypeTable)
                    {
                        presetCameraTypeTable ??= CreatePresetCameraTypeTable();
                    }
                }
                return presetCameraTypeTable;
            }
        }

        public static Dictionary<LightRigDirection, string> LightRigDirectionTable
        {
            get
            {
                if (lightRigDirectionTable == null)
                {
                    object syncLightRigDirectionTable = OpenXmlExporterBase.syncLightRigDirectionTable;
                    lock (syncLightRigDirectionTable)
                    {
                        lightRigDirectionTable ??= CreateLightRigDirectionTable();
                    }
                }
                return lightRigDirectionTable;
            }
        }

        public static Dictionary<LightRigPreset, string> LightRigPresetTable
        {
            get
            {
                if (lightRigPresetTable == null)
                {
                    object syncLightRigPresetTable = OpenXmlExporterBase.syncLightRigPresetTable;
                    lock (syncLightRigPresetTable)
                    {
                        lightRigPresetTable ??= CreateLightRigPresetTable();
                    }
                }
                return lightRigPresetTable;
            }
        }

        public static Dictionary<PresetMaterialType, string> PresetMaterialTypeTable
        {
            get
            {
                if (presetMaterialTypeTable == null)
                {
                    object syncPresetMaterialTypeTable = OpenXmlExporterBase.syncPresetMaterialTypeTable;
                    lock (syncPresetMaterialTypeTable)
                    {
                        presetMaterialTypeTable ??= CreatePresetMaterialTypeTable();
                    }
                }
                return presetMaterialTypeTable;
            }
        }

        public static Dictionary<PresetBevelType, string> PresetBevelTypeTable
        {
            get
            {
                if (presetBevelTypeTable == null)
                {
                    object syncPresetBevelTypeTable = OpenXmlExporterBase.syncPresetBevelTypeTable;
                    lock (syncPresetBevelTypeTable)
                    {
                        presetBevelTypeTable ??= CreatePresetBevelTypeTable();
                    }
                }
                return presetBevelTypeTable;
            }
        }

        public static Dictionary<VmlStrokeJoinStyle, string> VmlStrokeJoinStyleTable
        {
            get
            {
                if (vmlStrokeJoinStyleTable == null)
                {
                    object syncVmlStrokeJoinStyleTable = OpenXmlExporterBase.syncVmlStrokeJoinStyleTable;
                    lock (syncVmlStrokeJoinStyleTable)
                    {
                        vmlStrokeJoinStyleTable ??= CreateVmlStrokeJoinStyleTable();
                    }
                }
                return vmlStrokeJoinStyleTable;
            }
        }

        public static Dictionary<VmlFillType, string> VmlFillTypeTable
        {
            get
            {
                if (vmlFillTypeTable == null)
                {
                    object syncVmlFillTypeTable = OpenXmlExporterBase.syncVmlFillTypeTable;
                    lock (syncVmlFillTypeTable)
                    {
                        vmlFillTypeTable ??= CreateVmlFillTypeTable();
                    }
                }
                return vmlFillTypeTable;
            }
        }

        public static Dictionary<VmlLineStyle, string> VmlLineStyleTable
        {
            get
            {
                if (vmlLineStyleTable == null)
                {
                    object syncVmlLineStyleTable = OpenXmlExporterBase.syncVmlLineStyleTable;
                    lock (syncVmlLineStyleTable)
                    {
                        vmlLineStyleTable ??= CreateVmlLineStyleTable();
                    }
                }
                return vmlLineStyleTable;
            }
        }

        public static Dictionary<VmlDashStyle, string> VmlDashStyleTable
        {
            get
            {
                if (vmlDashStyleTable == null)
                {
                    object syncVmlDashStyleTable = OpenXmlExporterBase.syncVmlDashStyleTable;
                    lock (syncVmlDashStyleTable)
                    {
                        vmlDashStyleTable ??= CreateVmlDashStyleTable();
                    }
                }
                return vmlDashStyleTable;
            }
        }

        public static Dictionary<VmlConnectType, string> VmlConnectTypeTable
        {
            get
            {
                if (vmlConnectTypeTable == null)
                {
                    object syncVmlConnectTypeTable = OpenXmlExporterBase.syncVmlConnectTypeTable;
                    lock (syncVmlConnectTypeTable)
                    {
                        vmlConnectTypeTable ??= CreateVmlConnectTypeTable();
                    }
                }
                return vmlConnectTypeTable;
            }
        }

        public static Dictionary<VmlExtensionHandlingBehavior, string> VmlExtensionHandlingBehaviorTable
        {
            get
            {
                if (vmlExtensionHandlingBehaviorTable == null)
                {
                    object syncVmlExtensionHandlingBehaviorTable = OpenXmlExporterBase.syncVmlExtensionHandlingBehaviorTable;
                    lock (syncVmlExtensionHandlingBehaviorTable)
                    {
                        vmlExtensionHandlingBehaviorTable ??= CreateVmlExtensionHandlingBehaviorTable();
                    }
                }
                return vmlExtensionHandlingBehaviorTable;
            }
        }

        public static Dictionary<VmlInsetMode, string> VmlInsetModeTable
        {
            get
            {
                if (vmlInsetModeTable == null)
                {
                    object syncVmlInsetModeTable = OpenXmlExporterBase.syncVmlInsetModeTable;
                    lock (syncVmlInsetModeTable)
                    {
                        vmlInsetModeTable ??= CreateVmlInsetModeTable();
                    }
                }
                return vmlInsetModeTable;
            }
        }

        public static Dictionary<VmlFillMethod, string> VmlFillMethodTable
        {
            get
            {
                if (vmlFillMethodTable == null)
                {
                    object syncVmlFillMethodTable = OpenXmlExporterBase.syncVmlFillMethodTable;
                    lock (syncVmlFillMethodTable)
                    {
                        vmlFillMethodTable ??= CreateVmlFillMethodTable();
                    }
                }
                return vmlFillMethodTable;
            }
        }

        public static Dictionary<VmlImageAspect, string> VmlImageAspectTable
        {
            get
            {
                if (vmlImageAspectTable == null)
                {
                    object syncVmlImageAspectTable = OpenXmlExporterBase.syncVmlImageAspectTable;
                    lock (syncVmlImageAspectTable)
                    {
                        vmlImageAspectTable ??= CreateVmlImageAspectTable();
                    }
                }
                return vmlImageAspectTable;
            }
        }

        public static Dictionary<VmlBlackAndWhiteMode, string> VmlBlackAndWhiteModeTable
        {
            get
            {
                if (vmlBlackAndWhiteModeTable == null)
                {
                    object syncVmlBlackAndWhiteModeTable = OpenXmlExporterBase.syncVmlBlackAndWhiteModeTable;
                    lock (syncVmlBlackAndWhiteModeTable)
                    {
                        vmlBlackAndWhiteModeTable ??= CreateVmlBlackAndWhiteModeTable();
                    }
                }
                return vmlBlackAndWhiteModeTable;
            }
        }

        public static Dictionary<VmlGroupEditAs, string> VmlGroupEditAsTable
        {
            get
            {
                if (vmlGroupEditAsTable == null)
                {
                    object syncVmlGroupEditAsTable = OpenXmlExporterBase.syncVmlGroupEditAsTable;
                    lock (syncVmlGroupEditAsTable)
                    {
                        vmlGroupEditAsTable ??= CreateVmlGroupEditAsTable();
                    }
                }
                return vmlGroupEditAsTable;
            }
        }

        public static Dictionary<VmlShadowType, string> VmlShadowTypeTable
        {
            get
            {
                if (vmlShadowTypeTable == null)
                {
                    object syncVmlShadowTypeTable = OpenXmlExporterBase.syncVmlShadowTypeTable;
                    lock (syncVmlShadowTypeTable)
                    {
                        vmlShadowTypeTable ??= CreateVmlShadowTypeTable();
                    }
                }
                return vmlShadowTypeTable;
            }
        }
    }
}

