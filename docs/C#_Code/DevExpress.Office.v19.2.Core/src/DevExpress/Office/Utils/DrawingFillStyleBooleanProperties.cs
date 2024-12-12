namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class DrawingFillStyleBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private FillStyle fillStyle = (FillStyle.UseFilled | FillStyle.Filled);

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtPropertiesBase base2 = (IOfficeArtPropertiesBase) owner;
            base2.Filled = this.Filled;
            base2.UseFilled = this.UseFilled;
        }

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);
            this.fillStyle = (FillStyle) base.Value;
        }

        public override void Write(BinaryWriter writer)
        {
            base.Value = (int) this.fillStyle;
            base.Write(writer);
        }

        public bool NoFillHitTest
        {
            get => 
                (this.fillStyle & FillStyle.NoFillHitTest) == FillStyle.NoFillHitTest;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.NoFillHitTest;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.NoFillHitTest;
                }
            }
        }

        public bool UseNoFillHitTest
        {
            get => 
                (this.fillStyle & FillStyle.UseNoFillHitTest) == FillStyle.UseNoFillHitTest;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseNoFillHitTest;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseNoFillHitTest;
                }
            }
        }

        public bool FillUseRect
        {
            get => 
                (this.fillStyle & FillStyle.FillUseRect) == FillStyle.FillUseRect;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.FillUseRect;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.FillUseRect;
                }
            }
        }

        public bool UseFillUseRect
        {
            get => 
                (this.fillStyle & FillStyle.UseFillUseRect) == FillStyle.UseFillUseRect;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseFillUseRect;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseFillUseRect;
                }
            }
        }

        public bool FillShape
        {
            get => 
                (this.fillStyle & FillStyle.FillShape) == FillStyle.FillShape;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.FillShape;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.FillShape;
                }
            }
        }

        public bool UseFillShape
        {
            get => 
                (this.fillStyle & FillStyle.UseFillShape) == FillStyle.UseFillShape;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseFillShape;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseFillShape;
                }
            }
        }

        public bool HitTestFill
        {
            get => 
                (this.fillStyle & FillStyle.HitTestFill) == FillStyle.HitTestFill;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.HitTestFill;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.HitTestFill;
                }
            }
        }

        public bool UseHitTestFill
        {
            get => 
                (this.fillStyle & FillStyle.UseHitTestFill) == FillStyle.UseHitTestFill;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseHitTestFill;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseHitTestFill;
                }
            }
        }

        public bool Filled
        {
            get => 
                (this.fillStyle & FillStyle.Filled) == FillStyle.Filled;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.Filled;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.Filled;
                }
            }
        }

        public bool UseFilled
        {
            get => 
                (this.fillStyle & FillStyle.UseFilled) == FillStyle.UseFilled;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseFilled;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseFilled;
                }
            }
        }

        public bool ShapeAnchor
        {
            get => 
                (this.fillStyle & FillStyle.ShapeAnchor) == FillStyle.ShapeAnchor;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.ShapeAnchor;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.ShapeAnchor;
                }
            }
        }

        public bool UseShapeAnchor
        {
            get => 
                (this.fillStyle & FillStyle.UseShapeAnchor) == FillStyle.UseShapeAnchor;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseShapeAnchor;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseShapeAnchor;
                }
            }
        }

        public bool RecolorFillAsPicture
        {
            get => 
                (this.fillStyle & FillStyle.RecolorFillAsPicture) == FillStyle.RecolorFillAsPicture;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.RecolorFillAsPicture;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.RecolorFillAsPicture;
                }
            }
        }

        public bool UseRecolorFillAsPicture
        {
            get => 
                (this.fillStyle & FillStyle.UseRecolorFillAsPicture) == FillStyle.UseRecolorFillAsPicture;
            set
            {
                if (value)
                {
                    this.fillStyle |= FillStyle.UseRecolorFillAsPicture;
                }
                else
                {
                    this.fillStyle &= ~FillStyle.UseRecolorFillAsPicture;
                }
            }
        }

        public override bool Complex =>
            false;

        [Flags]
        public enum FillStyle
        {
            NoFillHitTest = 1,
            FillUseRect = 2,
            FillShape = 4,
            HitTestFill = 8,
            Filled = 0x10,
            ShapeAnchor = 0x20,
            RecolorFillAsPicture = 0x40,
            UseNoFillHitTest = 0x10000,
            UseFillUseRect = 0x20000,
            UseFillShape = 0x40000,
            UseHitTestFill = 0x80000,
            UseFilled = 0x100000,
            UseShapeAnchor = 0x200000,
            UseRecolorFillAsPicture = 0x400000
        }
    }
}

