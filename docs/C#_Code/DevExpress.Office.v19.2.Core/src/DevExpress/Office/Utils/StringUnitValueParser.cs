namespace DevExpress.Office.Utils
{
    using System;

    public class StringUnitValueParser
    {
        protected internal DXRotationUnit GetRotationUnitType(string inputValue, float minValue, float maxValue)
        {
            ValueInfo info = StringValueParser.TryParse(inputValue);
            if (info.IsValidNumber)
            {
                return new DXRotationUnit(info.Value, GetRotationUnitTypeFromString(info.Unit), minValue, maxValue);
            }
            ArgumentException exception1 = new ArgumentException("UnitValue", inputValue);
            return new DXRotationUnit();
        }

        private static DXUnitType GetRotationUnitTypeFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DXUnitType.Deg;
            }
            if (value.ToUpperInvariant() != "FD")
            {
                throw new ArgumentException("UnitType", value);
            }
            return DXUnitType.Fd;
        }

        private static DXUnitType GetTypeFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DXUnitType.Pixel;
            }
            string s = value.ToUpperInvariant();
            uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
            if (num <= 0x2e000424)
            {
                if (num <= 0x200c87a0)
                {
                    if (num != 0x1fcc12b7)
                    {
                        if ((num == 0x200c87a0) && (s == "%"))
                        {
                            return DXUnitType.Percentage;
                        }
                    }
                    else if (s == "EM")
                    {
                        return DXUnitType.Em;
                    }
                }
                else if (num != 0x2acc2408)
                {
                    if ((num == 0x2e000424) && (s == "PC"))
                    {
                        return DXUnitType.Pica;
                    }
                }
                else if (s == "EX")
                {
                    return DXUnitType.Ex;
                }
            }
            else if (num <= 0x3d001bc1)
            {
                if (num != 0x39001575)
                {
                    if ((num == 0x3d001bc1) && (s == "PT"))
                    {
                        return DXUnitType.Point;
                    }
                }
                else if (s == "PX")
                {
                    return DXUnitType.Pixel;
                }
            }
            else if (num == 0x3fdfcccf)
            {
                if (s == "MM")
                {
                    return DXUnitType.Mm;
                }
            }
            else if (num != 0x60e8fb1e)
            {
                if ((num == 0x83da27ad) && (s == "CM"))
                {
                    return DXUnitType.Cm;
                }
            }
            else if (s == "IN")
            {
                return DXUnitType.Inch;
            }
            throw new ArgumentException("UnitType", value);
        }

        public DXUnit GetUnit(string inputValue) => 
            this.GetUnit(inputValue, -32768f, 32767f);

        protected internal DXUnit GetUnit(string inputValue, float minValue, float maxValue)
        {
            ValueInfo info = StringValueParser.TryParse(inputValue);
            if (info.IsValidNumber)
            {
                return new DXUnit(info.Value, GetTypeFromString(info.Unit), minValue, maxValue);
            }
            ArgumentException exception1 = new ArgumentException("UnitValue", inputValue);
            return new DXUnit();
        }

        protected internal DXVmlUnit GetVmlUnitType(string inputValue, float minValue, float maxValue)
        {
            ValueInfo info = StringValueParser.TryParse(inputValue);
            if (info.IsValidNumber)
            {
                return new DXVmlUnit(info.Value, GetVmlUnitTypeFromString(info.Unit), minValue, maxValue);
            }
            ArgumentException exception1 = new ArgumentException("UnitValue", inputValue);
            return new DXVmlUnit();
        }

        private static DXUnitType GetVmlUnitTypeFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DXUnitType.Emu;
            }
            string str = value.ToUpperInvariant();
            if (str == "PX")
            {
                return DXUnitType.Pixel;
            }
            if (str == "PT")
            {
                return DXUnitType.Point;
            }
            if (str == "PC")
            {
                return DXUnitType.Pica;
            }
            if (str == "IN")
            {
                return DXUnitType.Inch;
            }
            if (str == "MM")
            {
                return DXUnitType.Mm;
            }
            if (str != "CM")
            {
                throw new ArgumentException("UnitType", value);
            }
            return DXUnitType.Cm;
        }
    }
}

