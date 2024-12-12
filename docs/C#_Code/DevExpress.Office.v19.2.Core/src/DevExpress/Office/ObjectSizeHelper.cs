namespace DevExpress.Office
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public static class ObjectSizeHelper
    {
        private static Dictionary<Type, int> primitiveTypesSizeOfTable = CreatePrimitiveTypesSizeofTable();

        public static int CalculateApproxObjectSize32(object obj) => 
            CalculateApproxObjectSize32(obj, true);

        public static int CalculateApproxObjectSize32(object obj, bool ignoreStringSize) => 
            !(obj is ValueType) ? CalculateApproxReferenceObjectSize32Core(obj, ignoreStringSize) : CalculateApproxObjectSize32Core(obj, ignoreStringSize);

        private static int CalculateApproxObjectSize32Core(object obj, bool ignoreStringSize) => 
            (obj != null) ? (!(obj is ValueType) ? CalculateApproxReferenceObjectSize32Core(obj, ignoreStringSize) : (!(obj is Enum) ? CalculateApproxValueObjectSize32(obj.GetType()) : CalculateApproxValueObjectSize32(Enum.GetUnderlyingType(obj.GetType())))) : 4;

        private static int CalculateApproxReferenceObjectSize32Core(object obj, bool ignoreStringSize)
        {
            if (obj is IDocumentModel)
            {
                return 0;
            }
            string str = obj as string;
            if (str != null)
            {
                return (!ignoreStringSize ? (4 + (str.Length * 2)) : 4);
            }
            int num = 0;
            foreach (FieldInfo info in obj.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object obj2 = info.GetValue(obj);
                ISupportsSizeOf of = obj2 as ISupportsSizeOf;
                if (of != null)
                {
                    return of.SizeOf();
                }
                num += CalculateApproxObjectSize32Core(obj2, ignoreStringSize);
            }
            return num;
        }

        private static int CalculateApproxValueObjectSize32(Type type)
        {
            int num;
            return (primitiveTypesSizeOfTable.TryGetValue(type, out num) ? num : Marshal.SizeOf(type));
        }

        private static int CalculateCount(object obj)
        {
            PropertyInfo property = obj.GetType().GetProperty("Count");
            if (property != null)
            {
                return (int) property.GetValue(obj, null);
            }
            ICollection is2 = obj as ICollection;
            return ((is2 == null) ? -1 : is2.Count);
        }

        public static List<SizeOfInfo> CalculateSizeOfInfo(object obj)
        {
            List<SizeOfInfo> list = new List<SizeOfInfo>();
            foreach (FieldInfo info in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object obj2 = info.GetValue(obj);
                ISupportsSizeOf of = obj2 as ISupportsSizeOf;
                if (of != null)
                {
                    list.Add(new SizeOfInfo(info.Name, of.SizeOf(), CalculateCount(obj2)));
                }
                else
                {
                    list.Add(new SizeOfInfo(info.Name, CalculateApproxObjectSize32(obj2), CalculateCount(obj2)));
                }
            }
            return list;
        }

        public static SizeOfInfo CalculateTotalSizeOfInfo(List<SizeOfInfo> list, string displayName)
        {
            int sizeOf = 0;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                sizeOf += list[i].SizeOf;
            }
            return new SizeOfInfo(displayName, sizeOf, -1);
        }

        private static Dictionary<Type, int> CreatePrimitiveTypesSizeofTable() => 
            new Dictionary<Type, int> { 
                { 
                    typeof(bool),
                    1
                },
                { 
                    typeof(byte),
                    1
                },
                { 
                    typeof(sbyte),
                    1
                },
                { 
                    typeof(char),
                    2
                },
                { 
                    typeof(short),
                    2
                },
                { 
                    typeof(ushort),
                    2
                },
                { 
                    typeof(int),
                    4
                },
                { 
                    typeof(uint),
                    4
                },
                { 
                    typeof(long),
                    8
                },
                { 
                    typeof(ulong),
                    8
                },
                { 
                    typeof(decimal),
                    0x10
                },
                { 
                    typeof(float),
                    4
                },
                { 
                    typeof(double),
                    8
                }
            };
    }
}

