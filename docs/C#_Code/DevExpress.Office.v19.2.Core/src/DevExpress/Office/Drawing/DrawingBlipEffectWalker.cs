namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DrawingBlipEffectWalker : IDrawingEffectVisitor
    {
        private float[][] colorMatrixElements;
        private List<DrawingColorMapItem> colorMapItems = new List<DrawingColorMapItem>();

        public virtual void AlphaCeilingEffectVisit()
        {
        }

        public virtual void AlphaFloorEffectVisit()
        {
        }

        private void ApplyEffect(IDrawingEffect effect)
        {
            effect.Visit(this);
        }

        public ColorMap[] GetColorMap()
        {
            if (!this.HasColorMap)
            {
                return null;
            }
            Func<DrawingColorMapItem, ColorMap> selector = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<DrawingColorMapItem, ColorMap> local1 = <>c.<>9__41_0;
                selector = <>c.<>9__41_0 = delegate (DrawingColorMapItem x) {
                    ColorMap map1 = new ColorMap();
                    map1.OldColor = x.From;
                    map1.NewColor = x.To;
                    return map1;
                };
            }
            return this.colorMapItems.Select<DrawingColorMapItem, ColorMap>(selector).ToArray<ColorMap>();
        }

        public virtual void GrayScaleEffectVisit()
        {
            this.ColorMatrixElements[0][0] = 0.3f;
            this.ColorMatrixElements[0][1] = 0.3f;
            this.ColorMatrixElements[0][2] = 0.3f;
            this.ColorMatrixElements[1][0] = 0.59f;
            this.ColorMatrixElements[1][1] = 0.59f;
            this.ColorMatrixElements[1][2] = 0.59f;
            this.ColorMatrixElements[2][0] = 0.11f;
            this.ColorMatrixElements[2][1] = 0.11f;
            this.ColorMatrixElements[2][2] = 0.11f;
        }

        private void InitColorMatrix()
        {
            this.colorMatrixElements = new float[5][];
            float[] singleArray1 = new float[5];
            singleArray1[0] = 1f;
            this.colorMatrixElements[0] = singleArray1;
            float[] singleArray2 = new float[5];
            singleArray2[1] = 1f;
            this.colorMatrixElements[1] = singleArray2;
            float[] singleArray3 = new float[5];
            singleArray3[2] = 1f;
            this.colorMatrixElements[2] = singleArray3;
            float[] singleArray4 = new float[5];
            singleArray4[3] = 1f;
            this.colorMatrixElements[3] = singleArray4;
            float[] singleArray5 = new float[5];
            singleArray5[4] = 1f;
            this.colorMatrixElements[4] = singleArray5;
        }

        public virtual void Visit(AlphaBiLevelEffect drawingEffect)
        {
        }

        public virtual void Visit(AlphaInverseEffect drawingEffect)
        {
        }

        public virtual void Visit(AlphaModulateEffect drawingEffect)
        {
        }

        public virtual void Visit(AlphaModulateFixedEffect drawingEffect)
        {
            float num = (float) DrawingValueConverter.FromPercentage(drawingEffect.Amount);
            if (num != 1f)
            {
                this.ColorMatrixElements[3][3] = num;
            }
        }

        public virtual void Visit(AlphaOutsetEffect drawingEffect)
        {
        }

        public virtual void Visit(AlphaReplaceEffect drawingEffect)
        {
        }

        public virtual void Visit(BiLevelEffect drawingEffect)
        {
        }

        public virtual void Visit(BlendEffect drawingEffect)
        {
        }

        public virtual void Visit(BlurEffect drawingEffect)
        {
        }

        public virtual void Visit(ColorChangeEffect drawingEffect)
        {
            Color finalColor = drawingEffect.ColorFrom.FinalColor;
            Color color2 = drawingEffect.ColorTo.FinalColor;
            if (!drawingEffect.UseAlpha)
            {
                finalColor = Color.FromArgb(finalColor.R, finalColor.G, finalColor.B);
                color2 = Color.FromArgb(color2.R, color2.G, color2.B);
            }
            DrawingColorMapItem item = new DrawingColorMapItem();
            item.From = finalColor;
            item.To = color2;
            this.colorMapItems.Add(item);
        }

        public virtual void Visit(ContainerEffect drawingEffect)
        {
        }

        public virtual void Visit(DuotoneEffect drawingEffect)
        {
        }

        public virtual void Visit(Effect drawingEffect)
        {
        }

        public virtual void Visit(FillEffect drawingEffect)
        {
        }

        public virtual void Visit(FillOverlayEffect drawingEffect)
        {
        }

        public virtual void Visit(GlowEffect drawingEffect)
        {
        }

        public virtual void Visit(HSLEffect drawingEffect)
        {
        }

        public virtual void Visit(InnerShadowEffect drawingEffect)
        {
        }

        public virtual void Visit(LuminanceEffect drawingEffect)
        {
            double num = Math.Min(Math.Max((double) ((((double) drawingEffect.Bright) / 100000.0) * 0.5), (double) -0.5), 0.5);
            double num4 = Math.Min(Math.Max((double) (1.0 / (1.0 - Math.Min((double) (((double) drawingEffect.Contrast) / 100000.0), (double) 0.99999))), (double) 0.0), 100000.0);
            float num5 = (float) num4;
            float num6 = (float) ((((num - 0.5) * num4) + num) + 0.5);
            for (int i = 0; i < 3; i++)
            {
                this.ColorMatrixElements[i][i] = num5;
                this.ColorMatrixElements[4][i] = num6;
            }
        }

        public virtual void Visit(OuterShadowEffect drawingEffect)
        {
        }

        public virtual void Visit(PresetShadowEffect drawingEffect)
        {
        }

        public virtual void Visit(ReflectionEffect drawingEffect)
        {
        }

        public virtual void Visit(RelativeOffsetEffect drawingEffect)
        {
        }

        public virtual void Visit(SoftEdgeEffect drawingEffect)
        {
        }

        public virtual void Visit(SolidColorReplacementEffect drawingEffect)
        {
        }

        public virtual void Visit(TintEffect drawingEffect)
        {
        }

        public virtual void Visit(TransformEffect drawingEffect)
        {
        }

        public void Walk(DrawingEffectCollection effects)
        {
            effects.ForEach(new Action<IDrawingEffect>(this.ApplyEffect));
        }

        public float[][] ColorMatrixElements
        {
            get
            {
                if (this.colorMatrixElements == null)
                {
                    this.InitColorMatrix();
                }
                return this.colorMatrixElements;
            }
        }

        public bool HasColorMatrix =>
            this.colorMatrixElements != null;

        public bool HasColorMap =>
            this.colorMapItems.Count > 0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DrawingBlipEffectWalker.<>c <>9 = new DrawingBlipEffectWalker.<>c();
            public static Func<DrawingColorMapItem, ColorMap> <>9__41_0;

            internal ColorMap <GetColorMap>b__41_0(DrawingColorMapItem x)
            {
                ColorMap map1 = new ColorMap();
                map1.OldColor = x.From;
                map1.NewColor = x.To;
                return map1;
            }
        }
    }
}

