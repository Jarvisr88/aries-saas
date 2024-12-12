namespace DevExpress.Office.Utils
{
    using System;

    public class DiagramTypeBooleanProperties : OfficeDrawingIntPropertyBase
    {
        private const int defaultFlags = 0xfff;
        private const int msodgmtCanvas = 0;

        public DiagramTypeBooleanProperties()
        {
            base.Value = 0xfff;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.IsCanvas = this.IsCanvas;
            }
        }

        public bool IsCanvas
        {
            get => 
                base.Value == 0;
            set
            {
                if (value)
                {
                    base.Value = 0;
                }
            }
        }
    }
}

