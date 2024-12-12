namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public abstract class UnitConverter
    {
        private readonly DocumentModelUnitConverter unitConverter;

        protected UnitConverter(DocumentModelUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public abstract float FromUnits(float value);
        public abstract float ToUnits(float value);

        public DocumentModelUnitConverter Converter =>
            this.unitConverter;
    }
}

