namespace DevExpress.Office.Utils
{
    using System;
    using System.Globalization;
    using System.Text;

    public class DXVmlUnit : DXUnitBase
    {
        public DXVmlUnit() : base(0f, DXUnitType.Emu)
        {
        }

        public DXVmlUnit(string value) : base(value, -2.147484E+09f, 2.147484E+09f)
        {
        }

        public DXVmlUnit(float value, DXUnitType type) : base(value, type, -2.147484E+09f, 2.147484E+09f)
        {
        }

        public DXVmlUnit(float value, DXUnitType type, float minValue, float maxValue) : base(value, type, minValue, maxValue)
        {
        }

        private string GetSuffix()
        {
            switch (base.Type)
            {
                case DXUnitType.Pixel:
                    return "px";

                case DXUnitType.Point:
                    return "pt";

                case DXUnitType.Pica:
                    return "pc";

                case DXUnitType.Inch:
                    return "in";

                case DXUnitType.Mm:
                    return "mm";

                case DXUnitType.Cm:
                    return "cm";
            }
            return string.Empty;
        }

        protected override DXUnitBase Parse(string value, float minValue, float maxValue) => 
            new StringUnitValueParser().GetVmlUnitType(value, minValue, maxValue);

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if ((base.Type != DXUnitType.Emu) && (base.Type != DXUnitType.Pixel))
            {
                builder.Append(base.Value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                builder.Append((int) base.Value);
            }
            builder.Append(this.GetSuffix());
            return builder.ToString();
        }
    }
}

