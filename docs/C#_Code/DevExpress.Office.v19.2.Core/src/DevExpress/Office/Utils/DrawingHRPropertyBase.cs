namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingHRPropertyBase : OfficeDrawingIntPropertyBase
    {
        public DrawingHRPropertyBase()
        {
        }

        public DrawingHRPropertyBase(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtTertiaryProperties artProperties = owner as IOfficeArtTertiaryProperties;
            if (artProperties != null)
            {
                this.SetValue(artProperties, base.Value);
            }
        }

        protected abstract void SetValue(IOfficeArtTertiaryProperties artProperties, int value);

        public override bool Complex =>
            false;
    }
}

