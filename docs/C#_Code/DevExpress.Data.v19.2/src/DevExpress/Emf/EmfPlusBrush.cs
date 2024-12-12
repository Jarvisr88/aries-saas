namespace DevExpress.Emf
{
    using System;

    public class EmfPlusBrush : EmfPlusObject, IDisposable
    {
        private readonly DXBrush brush;

        public EmfPlusBrush(DXBrush brush)
        {
            this.brush = brush;
        }

        public static EmfPlusBrush Create(EmfPlusReader reader)
        {
            reader.ReadInt32();
            switch (reader.ReadInt32())
            {
                case 0:
                    return new EmfPlusBrush(new DXSolidBrush(reader.ReadArgbColor()));

                case 1:
                    return new EmfPlusBrush(new DXHatchBrush((DXHatchStyle) reader.ReadInt32(), reader.ReadArgbColor(), reader.ReadArgbColor()));

                case 2:
                    return CreateTextureBrush(reader);

                case 3:
                    return CreatePathGradienBrush(reader);

                case 4:
                    return CreateLinearGradientBrush(reader);
            }
            return null;
        }

        private static EmfPlusBrush CreateLinearGradientBrush(EmfPlusReader reader)
        {
            EmfPlusBrushData data = (EmfPlusBrushData) reader.ReadInt32();
            DXLinearGradientBrush brush = new DXLinearGradientBrush(reader.ReadDXRectangleF(false), reader.ReadArgbColor(), reader.ReadArgbColor()) {
                WrapMode = (DXWrapMode) reader.ReadInt32()
            };
            reader.ReadBytes(8);
            if (data.HasFlag(EmfPlusBrushData.BrushDataTransform))
            {
                brush.Transform = reader.ReadTransformMatrix();
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataBlendFactors))
            {
                brush.Blend = reader.ReadBlend();
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataPresetColors))
            {
                brush.InterpolationColors = reader.ReadColorBlend();
            }
            return new EmfPlusBrush(brush);
        }

        private static EmfPlusBrush CreatePathGradienBrush(EmfPlusReader reader)
        {
            EmfPlusBrushData data = (EmfPlusBrushData) reader.ReadInt32();
            DXWrapMode mode = (DXWrapMode) reader.ReadInt32();
            ARGBColor color = reader.ReadArgbColor();
            DXPointF tf = reader.ReadDxPointF();
            int num = reader.ReadInt32();
            ARGBColor[] colorArray = new ARGBColor[num];
            for (int i = 0; i < num; i++)
            {
                colorArray[i] = reader.ReadArgbColor();
            }
            DXPathGradientBrush brush = null;
            if (data.HasFlag(EmfPlusBrushData.BrushDataPath))
            {
                reader.ReadInt32();
                brush = new DXPathGradientBrush(new EmfPlusPath(reader).PathData);
            }
            else
            {
                int num3 = reader.ReadInt32();
                DXPointF[] points = new DXPointF[num3];
                int index = 0;
                while (true)
                {
                    if (index >= num3)
                    {
                        DXPathPointTypes[] types = new DXPathPointTypes[] { DXPathPointTypes.StartSubPathPoint };
                        int num5 = 1;
                        while (true)
                        {
                            if (num5 >= num3)
                            {
                                brush = new DXPathGradientBrush(new DXGraphicsPathData(points, types, false));
                                break;
                            }
                            types[num5] = DXPathPointTypes.LineEndPoint;
                            num5++;
                        }
                        break;
                    }
                    points[index] = reader.ReadDxPointF();
                    index++;
                }
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataTransform))
            {
                brush.Transform = reader.ReadTransformMatrix();
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataBlendFactors))
            {
                brush.Blend = reader.ReadBlend();
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataPresetColors))
            {
                brush.InterpolationColors = reader.ReadColorBlend();
            }
            if (data.HasFlag(EmfPlusBrushData.BrushDataFocusScales))
            {
                reader.ReadInt32();
                brush.FocusScales = reader.ReadDxPointF();
            }
            brush.WrapMode = mode;
            brush.SurroundColors = colorArray;
            brush.CenterColor = color;
            brush.CenterPoint = new DXPointF?(tf);
            return new EmfPlusBrush(brush);
        }

        private static EmfPlusBrush CreateTextureBrush(EmfPlusReader reader)
        {
            DXWrapMode wrapMode = (DXWrapMode) reader.ReadInt32();
            DXTransformationMatrix matrix = !((EmfPlusBrushData) reader.ReadInt32()).HasFlag(EmfPlusBrushData.BrushDataTransform) ? new DXTransformationMatrix() : reader.ReadTransformMatrix();
            return new EmfPlusBrush(new DXTextureBrush(new EmfPlusImage(reader).Image, wrapMode) { Transform = matrix });
        }

        public void Dispose()
        {
            if (this.brush != null)
            {
                this.brush.Dispose();
            }
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            this.brush.Write(writer);
        }

        public DXBrush Brush =>
            this.brush;

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypeBrush;

        public override int Size =>
            4 + this.brush.DataSize;
    }
}

