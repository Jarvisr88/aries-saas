namespace DevExpress.Office.Utils
{
    using System;

    public class DiagramBooleanProperties : OfficeDrawingIntPropertyBase
    {
        private const int defaultFlags = 0x10000;
        private const int fPseudoInline = 0x10001;

        public DiagramBooleanProperties()
        {
            base.Value = 0x10000;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PseudoInline = this.PseudoInline;
            }
        }

        public bool PseudoInline
        {
            get => 
                base.Value == 0x10001;
            set
            {
                if (value)
                {
                    base.Value = 0x10001;
                }
            }
        }
    }
}

