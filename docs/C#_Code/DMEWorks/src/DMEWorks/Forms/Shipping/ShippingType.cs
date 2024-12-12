namespace DMEWorks.Forms.Shipping
{
    using System;

    public class ShippingType
    {
        public const string None = "None";
        public const string Ups = "Ups";

        private ShippingType()
        {
        }

        public static ShippingTypeEnum ToEnum(string value) => 
            (string.Compare(value, "Ups", true) != 0) ? ShippingTypeEnum.None : ShippingTypeEnum.Ups;

        public static string ToString(ShippingTypeEnum value)
        {
            ShippingTypeEnum enum2 = value;
            return ((enum2 == ShippingTypeEnum.None) ? "None" : ((enum2 == ShippingTypeEnum.Ups) ? "Ups" : value.ToString()));
        }
    }
}

