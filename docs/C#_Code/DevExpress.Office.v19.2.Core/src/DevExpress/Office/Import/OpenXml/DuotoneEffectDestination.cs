namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class DuotoneEffectDestination : DrawingColorEffectDestinationBase
    {
        private readonly DrawingColor secondColor;
        private bool hasSecondColor;

        public DuotoneEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
            this.secondColor = new DrawingColor(effects.DocumentModel);
        }

        protected override void CheckPropertyValues()
        {
            if (!base.HasColor || !this.hasSecondColor)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override IDrawingEffect CreateEffect() => 
            new DuotoneEffect(base.Color, this.secondColor);

        protected override DrawingColor Color =>
            this.IsFirstColor ? base.Color : this.secondColor;

        protected override bool HasColor
        {
            get => 
                this.IsFirstColor ? base.HasColor : this.hasSecondColor;
            set
            {
                if (this.IsFirstColor)
                {
                    base.HasColor = value;
                }
                else
                {
                    this.hasSecondColor = value;
                }
            }
        }

        private bool IsFirstColor =>
            base.Color.Info.IsEmpty;
    }
}

