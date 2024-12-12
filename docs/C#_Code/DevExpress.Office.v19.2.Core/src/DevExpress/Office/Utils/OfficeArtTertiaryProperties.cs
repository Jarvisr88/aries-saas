namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class OfficeArtTertiaryProperties : OfficeArtPropertiesBase, IOfficeArtTertiaryProperties, IOfficeArtPropertiesBase
    {
        private const int defaultFlags = 0;
        private bool writeAlways;

        public OfficeArtTertiaryProperties()
        {
            this.PosRelH = DrawingGroupShapePosRelH.Msoprh.msoprhText;
            this.PosRelV = DrawingGroupShapePosRelV.Msoprv.msoprvText;
        }

        public OfficeArtTertiaryProperties(bool writeAlways) : this()
        {
            this.writeAlways = writeAlways;
        }

        public override void CreateProperties()
        {
            this.SetGroupShapePosHProperties();
            this.SetGroupShapePosRelHProperties();
            this.SetGroupShapePosVProperties();
            this.SetGroupShapePosRelVProperties();
            this.SetGroupShape2PctHorizProperties();
            this.SetGroupShape2PctVertProperties();
            this.SetGroupShape2SizeRelHProperties();
            this.SetGroupShape2SizeRelVProperties();
            this.SetGroupShapePctPosProperties();
            this.SetGroupShapeBooleanProperties();
            this.SetHRProperties();
            this.SetDiagramBooleanProperties();
            this.SetDiagramTypeBooleanProperties();
        }

        public static OfficeArtTertiaryProperties FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtTertiaryProperties properties = new OfficeArtTertiaryProperties();
            properties.Read(reader, header);
            return properties;
        }

        protected internal override void HandleGroupShapeBooleanProperties(DrawingGroupShapeBooleanProperties properties)
        {
            base.HandleGroupShapeBooleanProperties(properties);
            this.HorizRule = properties.HorizRule;
            this.UseHorizRule = properties.UseHorizRule;
            this.StandardHR = properties.StandardHR;
            this.UseStandardHR = properties.UseStandardHR;
            this.NoShadeHR = properties.NoShadeHR;
            this.UseNoShadeHR = properties.UseNoShadeHR;
        }

        private void SetAlignHRProperties()
        {
            base.Properties.Add(new DrawingAlignHR(this.AlignHR));
        }

        private void SetDiagramBooleanProperties()
        {
            DiagramBooleanProperties item = new DiagramBooleanProperties();
            item.PseudoInline = this.PseudoInline;
            base.Properties.Add(item);
        }

        private void SetDiagramTypeBooleanProperties()
        {
            if (this.IsCanvas)
            {
                DiagramTypeBooleanProperties item = new DiagramTypeBooleanProperties();
                item.IsCanvas = true;
                base.Properties.Add(item);
            }
        }

        private void SetDxHeightHRProperties()
        {
            base.Properties.Add(new DrawingDxHeightHR(this.DxHeightHR));
        }

        private void SetDxWidthHRProperties()
        {
            base.Properties.Add(new DrawingDxWidthHR(this.DxWidthHR));
        }

        private void SetGroupShape2PctHorizProperties()
        {
            if (this.UseRelativeWidth)
            {
                base.Properties.Add(new DrawingGroupShape2PctHoriz(this.PctHoriz));
            }
        }

        private void SetGroupShape2PctVertProperties()
        {
            if (this.UseRelativeHeight)
            {
                base.Properties.Add(new DrawingGroupShape2PctVert(this.PctVert));
            }
        }

        private void SetGroupShape2SizeRelHProperties()
        {
            if (this.UseRelativeWidth)
            {
                base.Properties.Add(new DrawingGroupShape2SizeRelH(this.SizeRelH));
            }
        }

        private void SetGroupShape2SizeRelVProperties()
        {
            if (this.UseRelativeHeight)
            {
                base.Properties.Add(new DrawingGroupShape2SizeRelV(this.SizeRelV));
            }
        }

        private void SetGroupShapeBooleanProperties()
        {
            DrawingGroupShapeBooleanProperties item = new DrawingGroupShapeBooleanProperties {
                Value = 0,
                HorizRule = this.HorizRule,
                UseHorizRule = this.UseHorizRule,
                StandardHR = this.StandardHR,
                UseStandardHR = this.UseStandardHR,
                NoShadeHR = this.NoShadeHR,
                UseNoShadeHR = this.UseNoShadeHR
            };
            base.Properties.Add(item);
        }

        private void SetGroupShapePctPosProperties()
        {
            if ((this.PctHorizPos != 0) || (this.PctVertPos != 0))
            {
                if (this.PctHorizPos != 0)
                {
                    base.Properties.Add(new DrawingGroupShape2PctHorizPos(this.PctHorizPos));
                }
                else
                {
                    base.Properties.Add(new DrawingGroupShape2PctHorizPos());
                }
                if (this.PctVertPos != 0)
                {
                    base.Properties.Add(new DrawingGroupShape2PctVertPos(this.PctVertPos));
                }
                else
                {
                    base.Properties.Add(new DrawingGroupShape2PctVertPos());
                }
            }
        }

        private void SetGroupShapePosHProperties()
        {
            if (this.UsePosH)
            {
                base.Properties.Add(new DrawingGroupShapePosH(this.PosH));
            }
        }

        private void SetGroupShapePosRelHProperties()
        {
            if (this.UsePosH)
            {
                base.Properties.Add(new DrawingGroupShapePosRelH(this.PosRelH));
            }
        }

        private void SetGroupShapePosRelVProperties()
        {
            if (this.UsePosV)
            {
                base.Properties.Add(new DrawingGroupShapePosRelV(this.PosRelV));
            }
        }

        private void SetGroupShapePosVProperties()
        {
            if (this.UsePosV)
            {
                base.Properties.Add(new DrawingGroupShapePosV(this.PosV));
            }
        }

        private void SetHRProperties()
        {
            if (this.UseHorizRule && this.HorizRule)
            {
                this.SetAlignHRProperties();
                this.SetPctHRProperties();
                this.SetDxHeightHRProperties();
                this.SetDxWidthHRProperties();
            }
        }

        private void SetPctHRProperties()
        {
            base.Properties.Add(new DrawingPctHR(this.PctHR));
        }

        protected internal override bool ShouldWrite() => 
            this.writeAlways || (base.Properties.Count > 0);

        public override int HeaderTypeCode =>
            0xf122;

        public bool IsBehindDoc { get; set; }

        public bool UseIsBehindDoc { get; set; }

        public bool UseRelativeWidth { get; set; }

        public bool UseRelativeHeight { get; set; }

        public bool UsePosH { get; set; }

        public bool UsePosV { get; set; }

        public bool IsCanvas { get; set; }

        public bool Filled { get; set; }

        public bool UseFilled { get; set; }

        public bool LayoutInCell { get; set; }

        public bool UseLayoutInCell { get; set; }

        public int PctHoriz { get; set; }

        public int PctVert { get; set; }

        public int PctHorizPos { get; set; }

        public int PctVertPos { get; set; }

        public bool PseudoInline { get; set; }

        public bool HorizRule { get; set; }

        public bool UseHorizRule { get; set; }

        public int AlignHR { get; set; }

        public int PctHR { get; set; }

        public int DxHeightHR { get; set; }

        public int DxWidthHR { get; set; }

        public bool StandardHR { get; set; }

        public bool UseStandardHR { get; set; }

        public bool NoShadeHR { get; set; }

        public bool UseNoShadeHR { get; set; }

        public DrawingGroupShape2SizeRelH.RelativeFrom SizeRelH { get; set; }

        public DrawingGroupShape2SizeRelV.RelativeFrom SizeRelV { get; set; }

        public DrawingGroupShapePosH.Msoph PosH { get; set; }

        public DrawingGroupShapePosV.Msopv PosV { get; set; }

        public DrawingGroupShapePosRelH.Msoprh PosRelH { get; set; }

        public DrawingGroupShapePosRelV.Msoprv PosRelV { get; set; }

        public bool PctHorizPosValid =>
            this.PctHorizPos != -10001;

        public bool PctVertPosValid =>
            this.PctVertPos != -10001;
    }
}

