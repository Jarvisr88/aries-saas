namespace DevExpress.Mvvm.Native
{
    using System;

    public static class DesignTimeValuesProvider
    {
        public static readonly DateTime Today = DateTime.Today;
        public static object[] Types = new object[] { typeof(string), typeof(DateTime), typeof(int), typeof(decimal), typeof(byte), typeof(long), typeof(double), typeof(bool) };
        private static readonly object[][] distinctValues = CreateDistinctValues();

        public static object[][] CreateDistinctValues()
        {
            object[] objArray1 = new object[8];
            objArray1[0] = "string1";
            objArray1[1] = Today;
            objArray1[2] = 0x7b;
            objArray1[3] = 0x7b;
            objArray1[4] = (byte) 0x7b;
            objArray1[5] = 0x7b;
            objArray1[6] = 0x7b;
            object[][] objArrayArray1 = new object[3][];
            objArrayArray1[0] = objArray1;
            objArrayArray1[1] = new object[] { "string2", Today.AddDays(1.0), 0x1c8, 0x1c8, (byte) 0x7c, 0x1c8, 0x1c8, true };
            objArrayArray1[2] = new object[] { "string3", Today.AddDays(2.0), 0x315, 0x315, (byte) 0x7d, 0x315, 0x315, false };
            return objArrayArray1;
        }

        public static object[] CreateValues()
        {
            object[] objArray1 = new object[8];
            objArray1[0] = "string";
            objArray1[1] = Today;
            objArray1[2] = 0x7b;
            objArray1[3] = 0x7b;
            objArray1[4] = 0x7b;
            objArray1[5] = 0x7b;
            objArray1[6] = 0x7b;
            return objArray1;
        }

        public static object GetDesignTimeValue(Type propertyType, int index) => 
            GetDesignTimeValue(propertyType, index, null, distinctValues, true);

        public static object GetDesignTimeValue(Type propertyType, object component, object[] values, object[][] distinctValues, bool useDistinctValues)
        {
            Type underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType == null)
            {
                underlyingType = propertyType;
            }
            int index = Array.IndexOf<object>(Types, underlyingType);
            if (index == -1)
            {
                return (!underlyingType.IsValueType ? null : Activator.CreateInstance(underlyingType));
            }
            object obj2 = GetValues((int) component, values, distinctValues, useDistinctValues)[index];
            return ((obj2 != null) ? Convert.ChangeType(obj2, underlyingType, null) : null);
        }

        private static object[] GetValues(int index, object[] values, object[][] distinctValues, bool useDistinctValues) => 
            useDistinctValues ? distinctValues[index % distinctValues.Length] : values;
    }
}

