namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.Interaction;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class BrickPaintServiceBase : IBrickPaintService, IDisposable
    {
        private static readonly Color highlightColor = Color.FromArgb(0xc0, 0xca, 0xde, 0xff);
        private bool editingFieldsHighlighted;
        private EditingFieldCollection editingFields;
        private IServiceProvider provider;
        private IInteractionService interactServ;
        private Dictionary<VisualBrick, EditingField> fieldHash = new Dictionary<VisualBrick, EditingField>();
        private System.Drawing.Image imageAsc;
        private System.Drawing.Image imageDesc;
        private System.Drawing.Image imageLightAsc;
        private System.Drawing.Image imageLightDesc;

        protected BrickPaintServiceBase(EditingFieldCollection editingFields, IServiceProvider provider, float dpi = -1f)
        {
            this.editingFields = editingFields;
            this.provider = provider;
            this.Dpi = dpi;
        }

        protected abstract System.Drawing.Image CreateImage(bool isAscending);
        private static System.Drawing.Image CreateResultImage(System.Drawing.Image image)
        {
            if (image is Bitmap)
            {
                return new Bitmap(image.Width, image.Height);
            }
            using (Graphics graphics = Graphics.FromHwndInternal(IntPtr.Zero))
            {
                return graphics.CreateMetafile(new Rectangle(0, 0, image.Width, image.Height), MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly);
            }
        }

        bool IBrickPaintService.TryDrawBackground(Action drawBackground, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect)
        {
            EditingField field;
            if (!this.editingFields.TryGetEditingField(brick, out field))
            {
                return false;
            }
            this.UpdateAdditionalHash(brick, field);
            if (field.ReadOnly || (brick is ICheckBoxBrick))
            {
                return false;
            }
            if (!this.IsFocused(field))
            {
                if (brick is ImageBrick)
                {
                    return false;
                }
                if (!this.editingFieldsHighlighted)
                {
                    return false;
                }
                using (BrickStyle style = new BrickStyle(brick.Style))
                {
                    style.BackColor = DXColor.Blend(highlightColor, style.BackColor);
                    brickPaint.ExecUsingStyle(drawBackground, style);
                }
            }
            return true;
        }

        bool IBrickPaintService.TryDrawContent(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect) => 
            this.TryDrawEditingField(drawContent, brick, brickPaint, gr, rect) || this.TryDrawInteractiveBrick(drawContent, brick as TextBrickBase, brickPaint, gr, rect);

        private System.Drawing.Image GetImage(bool ascending, Color backColor) => 
            (backColor.GetBrightness() >= 0.6) ? (ascending ? this.ImageAsc : this.ImageDesc) : (ascending ? this.ImageLightAsc : this.ImageLightDesc);

        private static Color GuessBackColor(TextBrickBase brick)
        {
            Color backColor = brick.BackColor;
            return ((backColor.A < 10) ? Color.White : backColor);
        }

        private bool IsFocused(EditingField editingField) => 
            ReferenceEquals(this.FocusedField, editingField);

        private static System.Drawing.Image MakeLight(System.Drawing.Image image)
        {
            System.Drawing.Image image2 = CreateResultImage(image);
            using (Graphics graphics = Graphics.FromImage(image2))
            {
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    float[] singleArray1 = new float[5];
                    singleArray1[0] = -1f;
                    float[][] newColorMatrix = new float[5][];
                    newColorMatrix[0] = singleArray1;
                    float[] singleArray2 = new float[5];
                    singleArray2[1] = -1f;
                    newColorMatrix[1] = singleArray2;
                    float[] singleArray3 = new float[5];
                    singleArray3[2] = -1f;
                    newColorMatrix[2] = singleArray3;
                    float[] singleArray4 = new float[5];
                    singleArray4[3] = 1f;
                    newColorMatrix[3] = singleArray4;
                    newColorMatrix[4] = new float[] { 1f, 1f, 1f, 0f, 1f };
                    ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Brush);
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Pen);
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return image2;
        }

        private static PointF SnapPoint(IGraphics gr, PointF point) => 
            GraphicsUnitConverter.Convert((PointF) GraphicsUnitConverter.Round(GraphicsUnitConverter.Convert(point, (float) 300f, gr.Dpi)), gr.Dpi, (float) 300f);

        void IDisposable.Dispose()
        {
            if (this.imageAsc != null)
            {
                this.imageAsc.Dispose();
                this.imageAsc = null;
            }
            if (this.imageDesc != null)
            {
                this.imageDesc.Dispose();
                this.imageDesc = null;
            }
            if (this.imageLightAsc != null)
            {
                this.imageLightAsc.Dispose();
                this.imageLightAsc = null;
            }
            if (this.imageLightDesc != null)
            {
                this.imageLightDesc.Dispose();
                this.imageLightDesc = null;
            }
        }

        protected virtual bool TryDrawCharactercombEditingFieldBrick(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect, bool editingFieldsHighlighted)
        {
            if (!editingFieldsHighlighted)
            {
                return false;
            }
            using (BrickStyle style = new BrickStyle(brick.Style))
            {
                style.BackColor = DXColor.Blend(highlightColor, style.BackColor);
                brickPaint.ExecUsingStyle(delegate {
                    drawContent(gr, rect);
                }, style);
            }
            return true;
        }

        protected virtual bool TryDrawCheckBoxEditingFieldBrick(Action<IGraphics, RectangleF> drawContent, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect, EditingField editingField) => 
            new CheckBrickPainter(this.editingFieldsHighlighted, this.IsFocused(editingField)).TryDrawContent(delegate {
                drawContent(gr, rect);
            }, brickPaint, gr, rect);

        protected virtual bool TryDrawEditingField(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect)
        {
            EditingField field;
            return (this.TryGetEditingField(brick, out field) && (!field.ReadOnly && (!(brick is CheckBoxBrick) ? (!(brick is ICheckBoxBrick) ? (!this.IsFocused(field) ? (!(brick is CharacterCombBrick) ? (!(brick is LabelBrick) ? ((brick is ImageBrick) && this.TryDrawImageEditingFieldBrick(drawContent, brick, brickPaint, gr, rect, this.editingFieldsHighlighted)) : this.TryDrawLabelEditingFieldBrick(drawContent, brick, brickPaint, gr, rect, this.editingFieldsHighlighted)) : this.TryDrawCharactercombEditingFieldBrick(drawContent, brick, brickPaint, gr, rect, this.editingFieldsHighlighted)) : true) : false) : this.TryDrawCheckBoxEditingFieldBrick(drawContent, brickPaint, gr, rect, field))));
        }

        protected virtual bool TryDrawImageEditingFieldBrick(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect, bool editingFieldsHighlighted)
        {
            if (!editingFieldsHighlighted)
            {
                return false;
            }
            drawContent(gr, rect);
            gr.FillRectangle(new SolidBrush(highlightColor), rect);
            return true;
        }

        private unsafe bool TryDrawInteractiveBrick(Action<IGraphics, RectangleF> drawContent, TextBrickBase brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect)
        {
            ColumnSortOrder order;
            if ((brick == null) || ((this.InteractServ == null) || (!this.InteractServ.TryGetAppliedSorting(brick.SortData, out order) || (order == ColumnSortOrder.None))))
            {
                return false;
            }
            Color backColor = GuessBackColor(brick);
            System.Drawing.Image image = this.GetImage(order == ColumnSortOrder.Ascending, backColor);
            SizeF size = GraphicsUnitConverter.Convert((SizeF) image.Size, this.UseDpi() ? this.Dpi : 96f, (float) 300f);
            float num = GraphicsUnitConverter.Convert((float) 1f, (float) 96f, (float) 300f);
            RectangleF* efPtr1 = &rect;
            efPtr1.Width -= size.Width + (6f * num);
            brickPaint.ExecUsingStyle(delegate {
                drawContent(gr, rect);
            }, brick.Style);
            float num2 = (rect.Height - size.Height) / 2f;
            PointF point = new PointF(rect.Right + (3f * num), rect.Top + num2);
            gr.DrawImage(image, new RectangleF(this.UseDpi() ? SnapPoint(gr, point) : point, size));
            return true;
        }

        protected virtual bool TryDrawLabelEditingFieldBrick(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect, bool editingFieldsHighlighted)
        {
            if (!editingFieldsHighlighted)
            {
                return false;
            }
            using (BrickStyle style = new BrickStyle(brick.Style))
            {
                style.BackColor = DXColor.Blend(highlightColor, style.BackColor);
                brickPaint.ExecUsingStyle(delegate {
                    drawContent(gr, rect);
                }, style);
            }
            return true;
        }

        protected bool TryGetEditingField(VisualBrick brick, out EditingField value) => 
            this.editingFields.TryGetEditingField(brick, out value) || this.fieldHash.TryGetValue(brick, out value);

        private void UpdateAdditionalHash(VisualBrick brick, EditingField editingField)
        {
            if (brick is CheckBoxTextBrick)
            {
                this.fieldHash[((CheckBoxTextBrick) brick).CheckBoxBrick] = editingField;
            }
        }

        protected bool UseDpi() => 
            this.Dpi > 0f;

        private System.Drawing.Image ImageDesc
        {
            get
            {
                this.imageDesc ??= this.CreateImage(false);
                return this.imageDesc;
            }
        }

        private System.Drawing.Image ImageAsc
        {
            get
            {
                this.imageAsc ??= this.CreateImage(true);
                return this.imageAsc;
            }
        }

        private System.Drawing.Image ImageLightDesc
        {
            get
            {
                this.imageLightDesc ??= MakeLight(this.ImageDesc);
                return this.imageLightDesc;
            }
        }

        private System.Drawing.Image ImageLightAsc
        {
            get
            {
                this.imageLightAsc ??= MakeLight(this.ImageAsc);
                return this.imageLightAsc;
            }
        }

        protected float Dpi { get; private set; }

        private IInteractionService InteractServ
        {
            get
            {
                this.interactServ ??= this.provider.GetService<IInteractionService>();
                return this.interactServ;
            }
        }

        private EditingField FocusedField
        {
            get
            {
                IEditBrickServiceBase service = this.provider.GetService<IEditBrickServiceBase>();
                return service?.EditingField;
            }
        }

        bool IBrickPaintService.EditingFieldsHighlighted
        {
            get => 
                this.editingFieldsHighlighted;
            set => 
                this.editingFieldsHighlighted = value;
        }

        private class CheckBrickPainter : BrickPaintServiceBase.CheckBrickPainterBase
        {
            public CheckBrickPainter(bool highlighted, bool focused) : base(highlighted, focused)
            {
            }

            public override bool TryDrawContent(Action drawContent, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect)
            {
                float sourceDpi = GraphicsDpi.UnitToDpiI(gr.PageUnit);
                if (base.focused)
                {
                    base.ExecWithSmoothMode(delegate {
                        drawContent();
                        float delta = GraphicsUnitConverter.Convert((float) 1f, gr.Dpi, sourceDpi);
                        gr.ExecUsingClipBounds(() => gr.DrawRectangle(brickPaint.GetPen(this.focusFrameColor, delta), rect), RectangleF.Inflate(rect, 2f * delta, 2f * delta), false);
                    }, gr);
                    return true;
                }
                if (!base.highlighted)
                {
                    return false;
                }
                drawContent();
                gr.FillRectangle(new SolidBrush(BrickPaintServiceBase.highlightColor), rect);
                return true;
            }
        }

        private abstract class CheckBrickPainterBase
        {
            protected bool highlighted;
            protected bool focused;
            protected Color focusFrameColor = Color.Gray;

            public CheckBrickPainterBase(bool highlighted, bool focused)
            {
                this.highlighted = highlighted;
                this.focused = focused;
            }

            protected void ExecWithSmoothMode(Action action, IGraphics gr)
            {
                SmoothingMode smoothingMode = gr.SmoothingMode;
                try
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    action();
                }
                finally
                {
                    gr.SmoothingMode = smoothingMode;
                }
            }

            public abstract bool TryDrawContent(Action drawContent, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect);
        }

        private class RadioBrickPainter : BrickPaintServiceBase.CheckBrickPainterBase
        {
            public RadioBrickPainter(bool highlighted, bool focused) : base(highlighted, focused)
            {
            }

            public override bool TryDrawContent(Action drawContent, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect)
            {
                float sourceDpi = GraphicsDpi.UnitToDpiI(gr.PageUnit);
                if (base.focused)
                {
                    base.ExecWithSmoothMode(delegate {
                        drawContent();
                        float delta = GraphicsUnitConverter.Convert((float) 1f, gr.Dpi, sourceDpi);
                        gr.ExecUsingClipBounds(() => gr.DrawEllipse(brickPaint.GetPen(this.focusFrameColor, delta), rect), RectangleF.Inflate(rect, 2f * delta, 2f * delta), false);
                    }, gr);
                    return true;
                }
                if (!base.highlighted)
                {
                    return false;
                }
                drawContent();
                gr.FillEllipse(new SolidBrush(BrickPaintServiceBase.highlightColor), rect);
                return true;
            }
        }
    }
}

