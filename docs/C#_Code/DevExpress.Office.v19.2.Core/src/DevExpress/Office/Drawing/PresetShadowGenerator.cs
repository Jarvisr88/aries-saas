namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class PresetShadowGenerator
    {
        private static Dictionary<PresetShadowType, PresetShadowGeneratorDelegate> generators;

        private static Dictionary<PresetShadowType, PresetShadowGeneratorDelegate> CreateGenerators() => 
            new Dictionary<PresetShadowType, PresetShadowGeneratorDelegate> { 
                { 
                    PresetShadowType.TopLeftDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateTopLeftDrop)
                },
                { 
                    PresetShadowType.TopRightDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateTopRightDrop)
                },
                { 
                    PresetShadowType.BackLeftPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBackLeftPerspective)
                },
                { 
                    PresetShadowType.BackRightPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBackRightPerspective)
                },
                { 
                    PresetShadowType.BottomLeftDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBottomLeftDrop)
                },
                { 
                    PresetShadowType.BottomRightDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBottomRightDrop)
                },
                { 
                    PresetShadowType.FrontLeftPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateFrontLeftPerspective)
                },
                { 
                    PresetShadowType.FrontRightPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateFrontRightPerspective)
                },
                { 
                    PresetShadowType.TopLeftSmallDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateTopLeftSmallDrop)
                },
                { 
                    PresetShadowType.TopLeftLargeDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateTopLeftLargeDrop)
                },
                { 
                    PresetShadowType.BackLeftLongPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBackLeftLongPerspective)
                },
                { 
                    PresetShadowType.BackRightLongPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBackRightLongPerspective)
                },
                { 
                    PresetShadowType.TopLeftDoubleDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateTopLeftDoubleDrop)
                },
                { 
                    PresetShadowType.BottomRightSmallDrop,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBottomRightSmallDrop)
                },
                { 
                    PresetShadowType.FrontLeftLongPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateFrontLeftLongPerspective)
                },
                { 
                    PresetShadowType.FrontRightLongPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateFrontRightLongPerspective)
                },
                { 
                    PresetShadowType.OuterBox3d,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateOuterBox3D)
                },
                { 
                    PresetShadowType.InnerBox3d,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateInnerBox3D)
                },
                { 
                    PresetShadowType.BackCenterPerspective,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateBackCenterPerspective)
                },
                { 
                    PresetShadowType.FrontBottomShadow,
                    new PresetShadowGeneratorDelegate(PresetShadowGenerator.GenerateFrontBottomShadow)
                }
            };

        private static IDrawingEffect[] GenerateBackCenterPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0xc350, 0, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBackLeftLongPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0xc350, 0x256f98, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBackLeftPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0xc350, 0x256f98, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBackRightLongPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0xc350, -2453400, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBackRightPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0xc350, -2453400, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBottomLeftDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0x186a0, 0, 0, RectangleAlignType.BottomLeft, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBottomRightDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0x186a0, 0, 0, RectangleAlignType.BottomRight, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateBottomRightSmallDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0x186a0, 0, 0, RectangleAlignType.BottomRight, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateFrontBottomShadow(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, -100000, 0, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateFrontLeftLongPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, -50000, 0x256f98, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateFrontLeftPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, -50000, 0x256f98, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateFrontRightLongPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, -50000, -2453400, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateFrontRightPerspective(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, -50000, -2453400, 0, RectangleAlignType.Bottom, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateInnerBox3D(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            int direction = presetShadow.OffsetShadow.Direction;
            int sx = 0x186a0;
            int sy = 0x186a0;
            int kx = 0;
            int ky = 0;
            RectangleAlignType topLeft = RectangleAlignType.TopLeft;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, direction, sx, sy, kx, ky, topLeft, false));
            OuterShadowEffect effect2 = new OuterShadowEffect(DrawingColor.Create(FakeDocumentModel.Instance, TransformColor(presetShadow.Color.FinalColor)), OuterShadowEffectInfo.Create((long) num, distance, direction + 0xa4cb80, sx, sy, kx, ky, topLeft, false));
            return new IDrawingEffect[] { effect, effect2 };
        }

        private static IDrawingEffect[] GenerateOuterBox3D(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            int direction = presetShadow.OffsetShadow.Direction;
            int sx = 0x186a0;
            int sy = 0x186a0;
            int kx = 0;
            int ky = 0;
            RectangleAlignType topLeft = RectangleAlignType.TopLeft;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, direction, sx, sy, kx, ky, topLeft, false));
            OuterShadowEffect effect2 = new OuterShadowEffect(DrawingColor.Create(FakeDocumentModel.Instance, TransformColor(presetShadow.Color.FinalColor)), OuterShadowEffectInfo.Create((long) num, distance, direction + 0xa4cb80, sx, sy, kx, ky, topLeft, false));
            return new IDrawingEffect[] { effect, effect2 };
        }

        public static IDrawingEffect[] GenerateShadowEffect(PresetShadowEffect presetShadow)
        {
            PresetShadowGeneratorDelegate delegate2;
            return (Generators.TryGetValue(presetShadow.Type, out delegate2) ? delegate2(presetShadow) : new IDrawingEffect[0]);
        }

        private static IDrawingEffect[] GenerateTopLeftDoubleDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            int direction = presetShadow.OffsetShadow.Direction;
            int sx = 0x186a0;
            int sy = 0x186a0;
            int kx = 0;
            int ky = 0;
            RectangleAlignType topLeft = RectangleAlignType.TopLeft;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, direction, sx, sy, kx, ky, topLeft, false));
            distance *= 2L;
            OuterShadowEffect effect2 = new OuterShadowEffect(DrawingColor.Create(FakeDocumentModel.Instance, TransformColor(presetShadow.Color.FinalColor)), OuterShadowEffectInfo.Create((long) num, distance, direction, sx, sy, kx, ky, topLeft, false));
            return new IDrawingEffect[] { effect, effect2 };
        }

        private static IDrawingEffect[] GenerateTopLeftDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0x186a0, 0, 0, RectangleAlignType.TopLeft, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateTopLeftLargeDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, (int) Math.Round((double) 125000.0), (int) Math.Round((double) 125000.0), 0, 0, RectangleAlignType.BottomRight, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateTopLeftSmallDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, (int) Math.Round((double) 75000.0), (int) Math.Round((double) 75000.0), 0, 0, RectangleAlignType.TopLeft, false));
            return new IDrawingEffect[] { effect };
        }

        private static IDrawingEffect[] GenerateTopRightDrop(PresetShadowEffect presetShadow)
        {
            int num = 0;
            long distance = presetShadow.OffsetShadow.Distance;
            OuterShadowEffect effect = new OuterShadowEffect(presetShadow.Color, OuterShadowEffectInfo.Create((long) num, distance, presetShadow.OffsetShadow.Direction, 0x186a0, 0x186a0, 0, 0, RectangleAlignType.TopRight, false));
            return new IDrawingEffect[] { effect };
        }

        private static Color TransformColor(Color source)
        {
            Color color = DXColor.Blend(source, Color.White);
            int num = (int) Math.Round((double) (((float) (0x66 * source.A)) / 255f));
            return Color.FromArgb(Math.Min(0xff, color.R + num), Math.Min(0xff, color.G + num), Math.Min(0xff, color.B + num));
        }

        private static Dictionary<PresetShadowType, PresetShadowGeneratorDelegate> Generators =>
            generators ??= CreateGenerators();
    }
}

