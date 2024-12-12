namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal static class UniqueValues
    {
        public static readonly object[] Empty = new object[0];
        public static readonly object[] NotLoaded = new object[] { AsyncServerModeDataController.NoValue };

        public static object[] Aggregate(Type type, object[] uniqueValues)
        {
            if ((uniqueValues == null) || (uniqueValues.Length == 0))
            {
                return new object[3];
            }
            if (AreNotLoaded(uniqueValues))
            {
                object[] objArray1 = new object[3];
                objArray1[0] = uniqueValues[0];
                objArray1[1] = uniqueValues[0];
                return objArray1;
            }
            Type conversionType = TypeHelper.GetConversionType(type);
            object min = GetFirstNotNull(uniqueValues, conversionType, 0);
            object max = GetLastNotNull(uniqueValues, conversionType, 0);
            object uniqueValue = GetAvg(uniqueValues, type, min, max);
            return new object[] { min, max, CheckIsNaN_or_IsInfinity(uniqueValue, conversionType) };
        }

        public static bool AreNotLoaded(object[] uniqueValues) => 
            (uniqueValues == NotLoaded) || ((uniqueValues.Length != 0) && (uniqueValues[0] is NotLoadedObject));

        private static object CheckDBNull(object uniqueValue) => 
            Equals(uniqueValue, DBNull.Value) ? null : uniqueValue;

        private static object CheckDBNullOrNaN(object uniqueValue, Type type) => 
            Equals(uniqueValue, DBNull.Value) ? null : CheckIsNaN_or_IsInfinity(uniqueValue, type);

        private static object CheckIsNaN_or_IsInfinity(object uniqueValue, Type type) => 
            (!(type == typeof(double)) || !(uniqueValue is double)) ? ((!(type == typeof(float)) || !(uniqueValue is float)) ? uniqueValue : ((float.IsNaN((float) uniqueValue) || float.IsInfinity((float) uniqueValue)) ? null : uniqueValue)) : ((double.IsNaN((double) uniqueValue) || double.IsInfinity((double) uniqueValue)) ? null : uniqueValue);

        public static object EnsureNullSafety(object value) => 
            !Equals(value, null) ? value : BaseRowsKeeper.NullObject;

        public static object[] EnsureNullSafety(object[] uniqueValues)
        {
            if ((uniqueValues == null) || (uniqueValues.Length == 0))
            {
                return Empty;
            }
            if (!AreNotLoaded(uniqueValues))
            {
                SkipNullObject(uniqueValues, DBNull.Value, BaseRowsKeeper.NullObject);
                SkipNullObject(uniqueValues, null, BaseRowsKeeper.NullObject);
            }
            return uniqueValues;
        }

        public static object[] Get(IDictionary<string, object[]> cache, string path)
        {
            object[] objArray;
            return (cache.TryGetValue(path, out objArray) ? objArray : Empty);
        }

        private static Type GetActualType(Type type, object min, object max)
        {
            if ((min != null) && ((max != null) && ((min.GetType() == max.GetType()) && (min.GetType() != type))))
            {
                type = min.GetType();
            }
            return type;
        }

        private static object GetAvg(object[] uniqueValues, Type type, object min, object max)
        {
            Type type2;
            Type nullable = TypeHelper.GetNullable(GetActualType(type, min, max), out type2);
            if (type2 == typeof(DateTime))
            {
                return null;
            }
            SkipNullObject(uniqueValues, DBNull.Value, null);
            return SummaryValueExpressiveCalculator.Calculate(SummaryItemType.Average, uniqueValues.ApplyCast(typeof(object), nullable, null), nullable, true, null, null);
        }

        private static object GetFirstNotNull(object[] uniqueValues, Type type, int pos = 0)
        {
            object obj1 = CheckDBNullOrNaN(uniqueValues[pos], type);
            object obj2 = obj1;
            if (obj1 == null)
            {
                object local1 = obj1;
                if (uniqueValues.Length <= (pos + 1))
                {
                    return null;
                }
                obj2 = GetFirstNotNull(uniqueValues, type, pos + 1);
            }
            return obj2;
        }

        private static object GetLastNotNull(object[] uniqueValues, Type type, int pos = 0)
        {
            object obj1 = CheckDBNullOrNaN(uniqueValues[(uniqueValues.Length - 1) - pos], type);
            object obj2 = obj1;
            if (obj1 == null)
            {
                object local1 = obj1;
                if (uniqueValues.Length <= (pos + 1))
                {
                    return null;
                }
                obj2 = GetLastNotNull(uniqueValues, type, pos + 1);
            }
            return obj2;
        }

        public static bool HasNulls(object[] uniqueValues)
        {
            if ((uniqueValues != null) && (uniqueValues.Length != 0))
            {
                if (AreNotLoaded(uniqueValues))
                {
                    return false;
                }
                for (int i = 0; i < uniqueValues.Length; i++)
                {
                    if (IsNullOrDBNull(uniqueValues[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Initialize(IDictionary<string, object[]> cache, string path, object[] uniqueValues)
        {
            if (!cache.ContainsKey(path))
            {
                cache.Add(path, uniqueValues);
            }
            else
            {
                cache[path] = uniqueValues;
            }
        }

        public static bool IsNotLoaded(object value) => 
            value is NotLoadedObject;

        public static bool IsNullOrDBNull(object value) => 
            Equals(value, null) || Equals(value, DBNull.Value);

        public static object[] RestoreNulls(object[] uniqueValues, object nullValue = null)
        {
            if ((uniqueValues == null) || (uniqueValues.Length == 0))
            {
                return Empty;
            }
            object[] objArray = new object[uniqueValues.Length];
            for (int i = 0; i < objArray.Length; i++)
            {
                object obj2;
                objArray[i] = obj2 = uniqueValues[i];
                if (Equals(obj2, BaseRowsKeeper.NullObject))
                {
                    objArray[i] = nullValue;
                }
            }
            return objArray;
        }

        private static void SkipNullObject(object[] uniqueValues, object nullObject, object nullValue = null)
        {
            if (Equals(uniqueValues[0], nullObject))
            {
                uniqueValues[0] = nullValue;
            }
            if (Equals(uniqueValues[uniqueValues.Length - 1], nullObject))
            {
                uniqueValues[uniqueValues.Length - 1] = nullValue;
            }
        }
    }
}

