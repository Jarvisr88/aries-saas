namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public class UnitsConverter
    {
        private readonly DocumentModelUnitConverter unitConverter;

        public UnitsConverter(DocumentModelUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public float ValueUnitToModelUnitsF(ValueInfo info)
        {
            if (!string.IsNullOrEmpty(info.Unit))
            {
                string s = info.Unit.ToLower(CultureInfo.InvariantCulture);
                uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
                if (num > 0x4e4e5564)
                {
                    if (num > 0x5d4e6d01)
                    {
                        if (num == 0x602e1e0f)
                        {
                            if (s == "mm")
                            {
                                return this.unitConverter.MillimetersToModelUnitsF(info.Value);
                            }
                        }
                        else if (num != 0x6429a72d)
                        {
                            if ((num == 0xe80c2f78) && (s == "m"))
                            {
                                return this.unitConverter.CentimetersToModelUnitsF(info.Value * 100f);
                            }
                        }
                        else if (s == "cm")
                        {
                            return this.unitConverter.CentimetersToModelUnitsF(info.Value);
                        }
                    }
                    else if (num == 0x5922da17)
                    {
                        if (s == "ft")
                        {
                            return this.unitConverter.InchesToModelUnitsF(info.Value * 12f);
                        }
                    }
                    else if (num == 0x5c2e17c3)
                    {
                        if (s == "mi")
                        {
                            goto TR_0001;
                        }
                    }
                    else if ((num == 0x5d4e6d01) && (s == "pt"))
                    {
                        return this.unitConverter.PointsToModelUnitsF(info.Value);
                    }
                }
                else if (num > 0x200c87a0)
                {
                    if (num == 0x41387a9e)
                    {
                        if (s == "in")
                        {
                            return this.unitConverter.InchesToModelUnitsF(info.Value);
                        }
                    }
                    else if (num != 0x443cfc85)
                    {
                        if ((num == 0x4e4e5564) && (s == "pc"))
                        {
                            return this.unitConverter.PicasToModelUnitsF(info.Value);
                        }
                    }
                    else if (s == "km")
                    {
                        return this.unitConverter.CentimetersToModelUnitsF(info.Value * 100000f);
                    }
                }
                else if (num == 0x16c0ddfd)
                {
                    if (s == "inch")
                    {
                        return this.unitConverter.InchesToModelUnitsF(info.Value);
                    }
                }
                else if ((num == 0x200c87a0) && (s == "%"))
                {
                    goto TR_0001;
                }
            }
            return info.Value;
        TR_0001:
            return (info.Value / 100f);
        }
    }
}

