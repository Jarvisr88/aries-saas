namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class UIUnitConverter
    {
        private readonly UnitPrecisionDictionary unitPrecisionDictionary;

        public UIUnitConverter(UnitPrecisionDictionary unitPrecisionDictionary)
        {
            Guard.ArgumentNotNull(unitPrecisionDictionary, "unitPrecisionDictionary");
            this.unitPrecisionDictionary = unitPrecisionDictionary;
        }

        public UIUnit CreateUIUnit(string text, DocumentUnit defaultUnitType) => 
            this.CreateUIUnit(text, defaultUnitType, false);

        internal UIUnit CreateUIUnit(string text, DocumentUnit defaultUnitType, bool isValueInPercent) => 
            UIUnit.Create(text, defaultUnitType, this.unitPrecisionDictionary, isValueInPercent);

        internal int ToInt(double value) => 
            (value <= 2147483647.0) ? ((value >= -2147483648.0) ? ((int) value) : -2147483648) : 0x7fffffff;

        internal int ToTwipsUnit(UIUnit value) => 
            this.ToTwipsUnit(value, false);

        public int ToTwipsUnit(UIUnit value, bool isValueInPercent)
        {
            float num = this.ToTwipsUnitF(value, isValueInPercent);
            return ((num >= 0f) ? this.ToInt(Math.Ceiling((double) num)) : this.ToInt(Math.Floor((double) num)));
        }

        public float ToTwipsUnitF(UIUnit value) => 
            this.ToTwipsUnitF(value, false);

        internal float ToTwipsUnitF(UIUnit value, bool isValueInPercent)
        {
            if (isValueInPercent)
            {
                return value.Value;
            }
            switch (value.UnitType)
            {
                case DocumentUnit.Inch:
                    return Units.InchesToTwipsF(value.Value);

                case DocumentUnit.Millimeter:
                    return Units.MillimetersToTwipsF(value.Value);

                case DocumentUnit.Centimeter:
                    return Units.CentimetersToTwipsF(value.Value);

                case DocumentUnit.Point:
                    return Units.PointsToTwipsF(value.Value);
            }
            return 0f;
        }

        public UIUnit ToUIUnit(int value, DocumentUnit type) => 
            this.ToUIUnit(value, type, false);

        internal UIUnit ToUIUnit(int value, DocumentUnit type, bool isValueInPercent) => 
            this.ToUIUnitF((float) value, type, isValueInPercent);

        public UIUnit ToUIUnitF(float value, DocumentUnit type) => 
            this.ToUIUnitF(value, type, false);

        public UIUnit ToUIUnitF(float value, DocumentUnit type, bool isValueInPercent)
        {
            if (isValueInPercent)
            {
                return new UIUnit(value, type, this.unitPrecisionDictionary) { IsValueInPercent = true };
            }
            switch (type)
            {
                case DocumentUnit.Inch:
                    return new UIUnit(Units.TwipsToInchesF(value), DocumentUnit.Inch, this.unitPrecisionDictionary);

                case DocumentUnit.Millimeter:
                    return new UIUnit(Units.TwipsToMillimetersF(value), DocumentUnit.Millimeter, this.unitPrecisionDictionary);

                case DocumentUnit.Centimeter:
                    return new UIUnit(Units.TwipsToCentimetersF(value), DocumentUnit.Centimeter, this.unitPrecisionDictionary);

                case DocumentUnit.Point:
                    return new UIUnit(Units.TwipsToPointsF(value), DocumentUnit.Point, this.unitPrecisionDictionary);
            }
            return null;
        }
    }
}

