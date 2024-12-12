namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class OfficePropertiesFactory
    {
        private static readonly ConstructorInfo Empty = BlipFactory.GetConstructor(typeof(DrawingEmpty), new Type[0]);
        private static List<OfficeDrawingPropertyInfo> infos = new List<OfficeDrawingPropertyInfo>();
        private static Dictionary<short, ConstructorInfo> propertyTypes = new Dictionary<short, ConstructorInfo>();
        private static Dictionary<Type, short> propertyIdentifiers = new Dictionary<Type, short>();

        static OfficePropertiesFactory()
        {
            infos.Add(new OfficeDrawingPropertyInfo(0x7f, typeof(DrawingBooleanProtectionProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x100, typeof(DrawingCropFromTop)));
            infos.Add(new OfficeDrawingPropertyInfo(0x101, typeof(DrawingCropFromBottom)));
            infos.Add(new OfficeDrawingPropertyInfo(0x102, typeof(DrawingCropFromLeft)));
            infos.Add(new OfficeDrawingPropertyInfo(0x103, typeof(DrawingCropFromRight)));
            infos.Add(new OfficeDrawingPropertyInfo(-16123, typeof(DrawingBlipName)));
            infos.Add(new OfficeDrawingPropertyInfo(0x106, typeof(DrawingBlipFlags)));
            infos.Add(new OfficeDrawingPropertyInfo(0x108, typeof(DrawingBlipContrast)));
            infos.Add(new OfficeDrawingPropertyInfo(0x109, typeof(DrawingBlipBrightness)));
            infos.Add(new OfficeDrawingPropertyInfo(0x10b, typeof(DrawingPictureIdentifier)));
            infos.Add(new OfficeDrawingPropertyInfo(0x13f, typeof(DrawingBlipBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1bf, typeof(DrawingFillStyleBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1cb, typeof(DrawingLineWidth)));
            infos.Add(new OfficeDrawingPropertyInfo(460, typeof(DrawingLineMiterLimit)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1cd, typeof(DrawingLineCompoundType)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1ce, typeof(DrawingLineDashing)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d0, typeof(DrawingLineStartArrowhead)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d1, typeof(DrawingLineEndArrowhead)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d2, typeof(DrawingLineStartArrowWidth)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d3, typeof(DrawingLineStartArrowLength)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d4, typeof(DrawingLineEndArrowWidth)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d5, typeof(DrawingLineEndArrowLength)));
            infos.Add(new OfficeDrawingPropertyInfo(470, typeof(DrawingLineJoinStyle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1d7, typeof(DrawingLineCapStyle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1ff, typeof(DrawingLineStyleBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x304, typeof(DrawingBlackWhiteMode)));
            infos.Add(new OfficeDrawingPropertyInfo(0x33f, typeof(DrawingShapeBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x4104, typeof(DrawingBlipIdentifier)));
            infos.Add(new OfficeDrawingPropertyInfo(0x80, typeof(DrawingTextIdentifier)));
            infos.Add(new OfficeDrawingPropertyInfo(-15488, typeof(DrawingShapeName)));
            infos.Add(new OfficeDrawingPropertyInfo(-15487, typeof(DrawingShapeDescription)));
            infos.Add(new OfficeDrawingPropertyInfo(-15486, typeof(DrawingShapeHyperlink)));
            infos.Add(new OfficeDrawingPropertyInfo(-15475, typeof(DrawingShapeTooltip)));
            infos.Add(new OfficeDrawingPropertyInfo(0x3bf, typeof(DrawingGroupShapeBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x500, typeof(DiagramTypeBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x53f, typeof(DiagramBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x38f, typeof(DrawingGroupShapePosH)));
            infos.Add(new OfficeDrawingPropertyInfo(0x390, typeof(DrawingGroupShapePosRelH)));
            infos.Add(new OfficeDrawingPropertyInfo(0x391, typeof(DrawingGroupShapePosV)));
            infos.Add(new OfficeDrawingPropertyInfo(0x392, typeof(DrawingGroupShapePosRelV)));
            infos.Add(new OfficeDrawingPropertyInfo(0x393, typeof(DrawingPctHR)));
            infos.Add(new OfficeDrawingPropertyInfo(0x394, typeof(DrawingAlignHR)));
            infos.Add(new OfficeDrawingPropertyInfo(0x395, typeof(DrawingDxHeightHR)));
            infos.Add(new OfficeDrawingPropertyInfo(0x396, typeof(DrawingDxWidthHR)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c0, typeof(DrawingGroupShape2PctHoriz)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c1, typeof(DrawingGroupShape2PctVert)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c2, typeof(DrawingGroupShape2PctHorizPos)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c3, typeof(DrawingGroupShape2PctVertPos)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c4, typeof(DrawingGroupShape2SizeRelH)));
            infos.Add(new OfficeDrawingPropertyInfo(0x7c5, typeof(DrawingGroupShape2SizeRelV)));
            infos.Add(new OfficeDrawingPropertyInfo(4, typeof(DrawingRotation)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1c0, typeof(DrawingLineColor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1c1, typeof(DrawingLineOpacity)));
            infos.Add(new OfficeDrawingPropertyInfo(0x81, typeof(DrawingTextLeft)));
            infos.Add(new OfficeDrawingPropertyInfo(130, typeof(DrawingTextTop)));
            infos.Add(new OfficeDrawingPropertyInfo(0x83, typeof(DrawingTextRight)));
            infos.Add(new OfficeDrawingPropertyInfo(0x84, typeof(DrawingTextBottom)));
            infos.Add(new OfficeDrawingPropertyInfo(900, typeof(DrawingWrapLeftDistance)));
            infos.Add(new OfficeDrawingPropertyInfo(0x385, typeof(DrawingWrapTopDistance)));
            infos.Add(new OfficeDrawingPropertyInfo(0x386, typeof(DrawingWrapRightDistance)));
            infos.Add(new OfficeDrawingPropertyInfo(0x387, typeof(DrawingWrapBottomDistance)));
            infos.Add(new OfficeDrawingPropertyInfo(0x4186, typeof(DrawingFillBlipIdentifier)));
            infos.Add(new OfficeDrawingPropertyInfo(0x200, typeof(DrawingShadowType)));
            infos.Add(new OfficeDrawingPropertyInfo(0x201, typeof(DrawingShadowColor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x204, typeof(DrawingShadowOpacity)));
            infos.Add(new OfficeDrawingPropertyInfo(0x205, typeof(DrawingShadowOffsetX)));
            infos.Add(new OfficeDrawingPropertyInfo(0x206, typeof(DrawingShadowOffsetY)));
            infos.Add(new OfficeDrawingPropertyInfo(0x209, typeof(DrawingShadowHorizontalScalingFactor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x20a, typeof(DrawingShadowHorizontalSkewAngle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x20b, typeof(DrawingShadowVerticalSkewAngle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x20c, typeof(DrawingShadowVerticalScalingFactor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x210, typeof(DrawingShadowOriginX)));
            infos.Add(new OfficeDrawingPropertyInfo(0x211, typeof(DrawingShadowOriginY)));
            infos.Add(new OfficeDrawingPropertyInfo(540, typeof(DrawingShadowSoftness)));
            infos.Add(new OfficeDrawingPropertyInfo(0x23f, typeof(DrawingShadowStyleBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x158, typeof(DrawingConnectionPointsType)));
            infos.Add(new OfficeDrawingPropertyInfo(0xbf, typeof(DrawingTextBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(0x8b, typeof(DrawingTextDirection)));
            infos.Add(new OfficeDrawingPropertyInfo(0x180, typeof(OfficeDrawingFillType)));
            infos.Add(new OfficeDrawingPropertyInfo(0x181, typeof(DrawingFillColor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x182, typeof(DrawingFillOpacity)));
            infos.Add(new OfficeDrawingPropertyInfo(0x183, typeof(DrawingFillBackColor)));
            infos.Add(new OfficeDrawingPropertyInfo(0x184, typeof(DrawingFillBackOpacity)));
            infos.Add(new OfficeDrawingPropertyInfo(0x185, typeof(DrawingFillBWColor)));
            infos.Add(new OfficeDrawingPropertyInfo(-15994, typeof(DrawingFillBlip)));
            infos.Add(new OfficeDrawingPropertyInfo(-15993, typeof(DrawingFillBlipName)));
            infos.Add(new OfficeDrawingPropertyInfo(0x188, typeof(DrawingFillBlipFlags)));
            infos.Add(new OfficeDrawingPropertyInfo(0x189, typeof(DrawingFillWidth)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18a, typeof(DrawingFillHeight)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18b, typeof(DrawingFillAngle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18c, typeof(DrawingFillFocus)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18d, typeof(DrawingFillToLeft)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18e, typeof(DrawingFillToTop)));
            infos.Add(new OfficeDrawingPropertyInfo(0x18f, typeof(DrawingFillToRight)));
            infos.Add(new OfficeDrawingPropertyInfo(400, typeof(DrawingFillToBottom)));
            infos.Add(new OfficeDrawingPropertyInfo(0x191, typeof(DrawingFillRectLeft)));
            infos.Add(new OfficeDrawingPropertyInfo(0x192, typeof(DrawingFillRectTop)));
            infos.Add(new OfficeDrawingPropertyInfo(0x193, typeof(DrawingFillRectRight)));
            infos.Add(new OfficeDrawingPropertyInfo(0x194, typeof(DrawingFillRectBottom)));
            infos.Add(new OfficeDrawingPropertyInfo(0x195, typeof(DrawingFillDzType)));
            infos.Add(new OfficeDrawingPropertyInfo(0x196, typeof(DrawingFillShadePreset)));
            infos.Add(new OfficeDrawingPropertyInfo(-15977, typeof(DrawingFillShadeColors)));
            infos.Add(new OfficeDrawingPropertyInfo(0x198, typeof(DrawingFillOriginX)));
            infos.Add(new OfficeDrawingPropertyInfo(0x199, typeof(DrawingFillOriginY)));
            infos.Add(new OfficeDrawingPropertyInfo(410, typeof(DrawingFillShapeOriginX)));
            infos.Add(new OfficeDrawingPropertyInfo(0x19b, typeof(DrawingFillShapeOriginY)));
            infos.Add(new OfficeDrawingPropertyInfo(0x19c, typeof(DrawingFillShadeType)));
            infos.Add(new OfficeDrawingPropertyInfo(0x19e, typeof(DrawingFillColorExt)));
            infos.Add(new OfficeDrawingPropertyInfo(0x19f, typeof(DrawingFillReserved415)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1a0, typeof(DrawingFillTintShade)));
            infos.Add(new OfficeDrawingPropertyInfo(-15967, typeof(DrawingFillReserved417)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1a2, typeof(DrawingFillBackColorExt)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1a3, typeof(DrawingFillReserved419)));
            infos.Add(new OfficeDrawingPropertyInfo(420, typeof(DrawingFillBackTintShade)));
            infos.Add(new OfficeDrawingPropertyInfo(-15963, typeof(DrawingFillReserved421)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1a6, typeof(DrawingFillReserved422)));
            infos.Add(new OfficeDrawingPropertyInfo(0x1a7, typeof(DrawingFillReserved423)));
            infos.Add(new OfficeDrawingPropertyInfo(-16059, typeof(DrawingGeometryVertices)));
            infos.Add(new OfficeDrawingPropertyInfo(-16047, typeof(DrawingGeometryConnectionSites)));
            infos.Add(new OfficeDrawingPropertyInfo(320, typeof(DrawingGeometryLeft)));
            infos.Add(new OfficeDrawingPropertyInfo(0x141, typeof(DrawingGeometryTop)));
            infos.Add(new OfficeDrawingPropertyInfo(0x142, typeof(DrawingGeometryRight)));
            infos.Add(new OfficeDrawingPropertyInfo(0x143, typeof(DrawingGeometryBottom)));
            infos.Add(new OfficeDrawingPropertyInfo(0x144, typeof(DrawingGeometryShapePath)));
            infos.Add(new OfficeDrawingPropertyInfo(-16058, typeof(DrawingGeometrySegmentInfo)));
            infos.Add(new OfficeDrawingPropertyInfo(0x147, typeof(DrawingGeometryAdjustValue1)));
            infos.Add(new OfficeDrawingPropertyInfo(0x148, typeof(DrawingGeometryAdjustValue2)));
            infos.Add(new OfficeDrawingPropertyInfo(0x149, typeof(DrawingGeometryAdjustValue3)));
            infos.Add(new OfficeDrawingPropertyInfo(330, typeof(DrawingGeometryAdjustValue4)));
            infos.Add(new OfficeDrawingPropertyInfo(0x14b, typeof(DrawingGeometryAdjustValue5)));
            infos.Add(new OfficeDrawingPropertyInfo(0x14c, typeof(DrawingGeometryAdjustValue6)));
            infos.Add(new OfficeDrawingPropertyInfo(0x14d, typeof(DrawingGeometryAdjustValue7)));
            infos.Add(new OfficeDrawingPropertyInfo(0x14e, typeof(DrawingGeometryAdjustValue8)));
            infos.Add(new OfficeDrawingPropertyInfo(-16046, typeof(DrawingGeometryConnectionSitesDir)));
            infos.Add(new OfficeDrawingPropertyInfo(0x153, typeof(DrawingGeometryLimoX)));
            infos.Add(new OfficeDrawingPropertyInfo(340, typeof(DrawingGeometryLimoY)));
            infos.Add(new OfficeDrawingPropertyInfo(-16042, typeof(DrawingGeometryGuides)));
            infos.Add(new OfficeDrawingPropertyInfo(0x17f, typeof(DrawingGeometryBooleanProperties)));
            infos.Add(new OfficeDrawingPropertyInfo(-15447, typeof(DrawingMetroBlob)));
            infos.Add(new OfficeDrawingPropertyInfo(0x303, typeof(DrawingCxStyle)));
            infos.Add(new OfficeDrawingPropertyInfo(0x85, typeof(DrawingShapeWrapText)));
            infos.Add(new OfficeDrawingPropertyInfo(0x87, typeof(DrawingShapeAnchorText)));
            infos.Add(new OfficeDrawingPropertyInfo(0x88, typeof(DrawingShapeTextFlow)));
            infos.Add(new OfficeDrawingPropertyInfo(0x89, typeof(DrawingShapeFontDirection)));
            infos.Add(new OfficeDrawingPropertyInfo(-16192, typeof(DrawingGeometryText)));
            infos.Add(new OfficeDrawingPropertyInfo(0xc2, typeof(DrawingGeometryTextAlign)));
            infos.Add(new OfficeDrawingPropertyInfo(0xc3, typeof(DrawingGeometryTextSize)));
            infos.Add(new OfficeDrawingPropertyInfo(0xc4, typeof(DrawingGeometryTextSpacing)));
            infos.Add(new OfficeDrawingPropertyInfo(-16187, typeof(DrawingGeometryTextFontName)));
            infos.Add(new OfficeDrawingPropertyInfo(-16186, typeof(DrawingGeometryTextCSSFont)));
            infos.Add(new OfficeDrawingPropertyInfo(0xff, typeof(DrawingGeometryTextBooleanProperties)));
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                propertyTypes.Add(infos[i].Identifier, BlipFactory.GetConstructor(infos[i].Type, new Type[0]));
                propertyIdentifiers.Add(infos[i].Type, infos[i].Identifier);
            }
        }

        public static IOfficeDrawingProperty CreateProperty(BinaryReader reader)
        {
            ConstructorInfo empty;
            ushort num = reader.ReadUInt16();
            short key = (short) num;
            if (!propertyTypes.TryGetValue(key, out empty) && !propertyTypes.TryGetValue((short) (key | 0x4000), out empty))
            {
                empty = Empty;
            }
            OfficeDrawingPropertyBase base2 = empty.Invoke(new object[0]) as OfficeDrawingPropertyBase;
            base2.SetComplex((num & 0x8000) != 0);
            base2.Read(reader);
            return base2;
        }

        public static short GetOpcodeByType(Type propertyType)
        {
            short num;
            if (!propertyIdentifiers.TryGetValue(propertyType, out num))
            {
                num = 0;
            }
            return num;
        }
    }
}

