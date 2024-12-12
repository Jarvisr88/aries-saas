namespace DevExpress.Office
{
    using System;
    using System.Drawing;

    public abstract class DocumentModelUnitToLayoutUnitConverter
    {
        protected DocumentModelUnitToLayoutUnitConverter()
        {
        }

        public Size ToLayoutUnits(Size value) => 
            new Size(this.ToLayoutUnits(value.Width), this.ToLayoutUnits(value.Height));

        public SizeF ToLayoutUnits(SizeF value) => 
            new SizeF(this.ToLayoutUnits(value.Width), this.ToLayoutUnits(value.Height));

        public abstract int ToLayoutUnits(int value);
        public abstract float ToLayoutUnits(float value);
        public Size ToModelUnits(Size value) => 
            new Size(this.ToModelUnits(value.Width), this.ToModelUnits(value.Height));

        public SizeF ToModelUnits(SizeF value) => 
            new SizeF(this.ToModelUnits(value.Width), this.ToModelUnits(value.Height));

        public abstract int ToModelUnits(int value);
        public abstract float ToModelUnits(float value);
    }
}

