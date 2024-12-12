namespace DevExpress.Office.Printing
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(TransformationBrickExporter))]
    public class TransformationBrick : PanelBrick
    {
        private bool isHitTestAllowed;

        public TransformationBrick()
        {
            base.BackColor = DXColor.Transparent;
            base.BorderWidth = 0f;
            this.NoClip = true;
            this.SeparableHorz = false;
            this.SeparableVert = false;
        }

        protected override bool AllowHitTest =>
            this.isHitTestAllowed;

        public bool IsHitTestAllowed
        {
            get => 
                this.isHitTestAllowed;
            set => 
                this.isHitTestAllowed = value;
        }
    }
}

