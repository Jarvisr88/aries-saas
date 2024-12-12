namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;

    [StructLayout(LayoutKind.Explicit)]
    internal sealed class PropVariant : IDisposable
    {
        private static Dictionary<Type, Action<PropVariant, Array, uint>> _vectorActions = null;
        private static Dictionary<Type, Func<object, PropVariant>> _cache = new Dictionary<Type, Func<object, PropVariant>>();
        private static object _padlock = new object();
        [FieldOffset(0)]
        private decimal _decimal;
        [FieldOffset(0)]
        private ushort _valueType;
        [FieldOffset(12)]
        private IntPtr _ptr2;
        [FieldOffset(8)]
        private IntPtr _ptr;
        [FieldOffset(8)]
        private int _int32;
        [FieldOffset(8)]
        private uint _uint32;
        [FieldOffset(8)]
        private byte _byte;
        [FieldOffset(8)]
        private sbyte _sbyte;
        [FieldOffset(8)]
        private short _short;
        [FieldOffset(8)]
        private ushort _ushort;
        [FieldOffset(8)]
        private long _long;
        [FieldOffset(8)]
        private ulong _ulong;
        [FieldOffset(8)]
        private double _double;
        [FieldOffset(8)]
        private float _float;

        public PropVariant()
        {
        }

        public PropVariant(bool[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromBooleanVector(value, (uint) value.Length, this);
        }

        public PropVariant(DateTime[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            System.Runtime.InteropServices.ComTypes.FILETIME[] prgft = new System.Runtime.InteropServices.ComTypes.FILETIME[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                prgft[i] = DateTimeToFileTime(value[i]);
            }
            PropVariantNativeMethods.InitPropVariantFromFileTimeVector(prgft, (uint) prgft.Length, this);
        }

        public PropVariant(double[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromDoubleVector(value, (uint) value.Length, this);
        }

        public PropVariant(short[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromInt16Vector(value, (uint) value.Length, this);
        }

        public PropVariant(int[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromInt32Vector(value, (uint) value.Length, this);
        }

        public PropVariant(long[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromInt64Vector(value, (uint) value.Length, this);
        }

        public PropVariant(string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromStringVector(value, (uint) value.Length, this);
        }

        public PropVariant(ushort[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromUInt16Vector(value, (uint) value.Length, this);
        }

        public PropVariant(uint[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromUInt32Vector(value, (uint) value.Length, this);
        }

        public PropVariant(ulong[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            PropVariantNativeMethods.InitPropVariantFromUInt64Vector(value, (uint) value.Length, this);
        }

        public PropVariant(bool value)
        {
            this._valueType = 11;
            this._int32 = value ? -1 : 0;
        }

        public PropVariant(byte value)
        {
            this._valueType = 0x11;
            this._byte = value;
        }

        public PropVariant(DateTime value)
        {
            this._valueType = 0x40;
            PropVariantNativeMethods.InitPropVariantFromFileTime(ref DateTimeToFileTime(value), this);
        }

        public PropVariant(decimal value)
        {
            this._decimal = value;
            this._valueType = 14;
        }

        public PropVariant(decimal[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this._valueType = 0x100e;
            this._int32 = value.Length;
            this._ptr2 = Marshal.AllocCoTaskMem(value.Length * 0x10);
            for (int i = 0; i < value.Length; i++)
            {
                int[] bits = decimal.GetBits(value[i]);
                Marshal.Copy(bits, 0, this._ptr2, bits.Length);
            }
        }

        public PropVariant(float[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this._valueType = 0x1004;
            this._int32 = value.Length;
            this._ptr2 = Marshal.AllocCoTaskMem(value.Length * 4);
            Marshal.Copy(value, 0, this._ptr2, value.Length);
        }

        public PropVariant(double value)
        {
            this._valueType = 5;
            this._double = value;
        }

        [SecuritySafeCritical]
        public PropVariant(Guid value)
        {
            this._valueType = 0x48;
            byte[] source = value.ToByteArray();
            this._ptr = Marshal.AllocCoTaskMem(source.Length);
            Marshal.Copy(source, 0, this._ptr, source.Length);
        }

        public PropVariant(short value)
        {
            this._valueType = 2;
            this._short = value;
        }

        public PropVariant(int value)
        {
            this._valueType = 3;
            this._int32 = value;
        }

        public PropVariant(long value)
        {
            this._long = value;
            this._valueType = 20;
        }

        public PropVariant(sbyte value)
        {
            this._valueType = 0x10;
            this._sbyte = value;
        }

        public PropVariant(float value)
        {
            this._valueType = 4;
            this._float = value;
        }

        [SecuritySafeCritical]
        public PropVariant(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this._valueType = 0x1f;
            this._ptr = Marshal.StringToCoTaskMemUni(value);
        }

        public PropVariant(ushort value)
        {
            this._valueType = 0x12;
            this._ushort = value;
        }

        public PropVariant(uint value)
        {
            this._valueType = 0x13;
            this._uint32 = value;
        }

        public PropVariant(ulong value)
        {
            this._valueType = 0x15;
            this._ulong = value;
        }

        private static Array CrackSingleDimSafeArray(IntPtr psa)
        {
            if (PropVariantNativeMethods.SafeArrayGetDim(psa) != 1)
            {
                throw new ArgumentException("psa");
            }
            int num2 = PropVariantNativeMethods.SafeArrayGetLBound(psa, 1);
            int num3 = PropVariantNativeMethods.SafeArrayGetUBound(psa, 1);
            object[] objArray = new object[(num3 - num2) + 1];
            for (int i = num2; i <= num3; i++)
            {
                objArray[i] = PropVariantNativeMethods.SafeArrayGetElement(psa, ref i);
            }
            return objArray;
        }

        private static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime value)
        {
            long num = value.ToFileTime();
            return new System.Runtime.InteropServices.ComTypes.FILETIME { 
                dwLowDateTime = (int) (((ulong) num) & 0xffffffffUL),
                dwHighDateTime = (int) (num >> 0x20)
            };
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            PropVariantNativeMethods.PropVariantClear(this);
            GC.SuppressFinalize(this);
        }

        ~PropVariant()
        {
            this.Dispose();
        }

        public static PropVariant FromObject(object value) => 
            (value != null) ? GetDynamicConstructor(value.GetType())(value) : new PropVariant();

        private static Dictionary<Type, Action<PropVariant, Array, uint>> GenerateVectorActions()
        {
            Dictionary<Type, Action<PropVariant, Array, uint>> dictionary = new Dictionary<Type, Action<PropVariant, Array, uint>>();
            Action<PropVariant, Array, uint> action1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Action<PropVariant, Array, uint> local1 = <>c.<>9__1_0;
                action1 = <>c.<>9__1_0 = delegate (PropVariant pv, Array array, uint i) {
                    short num;
                    PropVariantNativeMethods.PropVariantGetInt16Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(short), action1);
            Action<PropVariant, Array, uint> action2 = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Action<PropVariant, Array, uint> local2 = <>c.<>9__1_1;
                action2 = <>c.<>9__1_1 = delegate (PropVariant pv, Array array, uint i) {
                    ushort num;
                    PropVariantNativeMethods.PropVariantGetUInt16Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(ushort), action2);
            Action<PropVariant, Array, uint> action3 = <>c.<>9__1_2;
            if (<>c.<>9__1_2 == null)
            {
                Action<PropVariant, Array, uint> local3 = <>c.<>9__1_2;
                action3 = <>c.<>9__1_2 = delegate (PropVariant pv, Array array, uint i) {
                    int num;
                    PropVariantNativeMethods.PropVariantGetInt32Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(int), action3);
            Action<PropVariant, Array, uint> action4 = <>c.<>9__1_3;
            if (<>c.<>9__1_3 == null)
            {
                Action<PropVariant, Array, uint> local4 = <>c.<>9__1_3;
                action4 = <>c.<>9__1_3 = delegate (PropVariant pv, Array array, uint i) {
                    uint num;
                    PropVariantNativeMethods.PropVariantGetUInt32Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(uint), action4);
            Action<PropVariant, Array, uint> action5 = <>c.<>9__1_4;
            if (<>c.<>9__1_4 == null)
            {
                Action<PropVariant, Array, uint> local5 = <>c.<>9__1_4;
                action5 = <>c.<>9__1_4 = delegate (PropVariant pv, Array array, uint i) {
                    long num;
                    PropVariantNativeMethods.PropVariantGetInt64Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(long), action5);
            Action<PropVariant, Array, uint> action6 = <>c.<>9__1_5;
            if (<>c.<>9__1_5 == null)
            {
                Action<PropVariant, Array, uint> local6 = <>c.<>9__1_5;
                action6 = <>c.<>9__1_5 = delegate (PropVariant pv, Array array, uint i) {
                    ulong num;
                    PropVariantNativeMethods.PropVariantGetUInt64Elem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(ulong), action6);
            Action<PropVariant, Array, uint> action7 = <>c.<>9__1_6;
            if (<>c.<>9__1_6 == null)
            {
                Action<PropVariant, Array, uint> local7 = <>c.<>9__1_6;
                action7 = <>c.<>9__1_6 = delegate (PropVariant pv, Array array, uint i) {
                    System.Runtime.InteropServices.ComTypes.FILETIME filetime;
                    PropVariantNativeMethods.PropVariantGetFileTimeElem(pv, i, out filetime);
                    long fileTimeAsLong = GetFileTimeAsLong(ref filetime);
                    array.SetValue(DateTime.FromFileTime(fileTimeAsLong), (long) i);
                };
            }
            dictionary.Add(typeof(DateTime), action7);
            Action<PropVariant, Array, uint> action8 = <>c.<>9__1_7;
            if (<>c.<>9__1_7 == null)
            {
                Action<PropVariant, Array, uint> local8 = <>c.<>9__1_7;
                action8 = <>c.<>9__1_7 = delegate (PropVariant pv, Array array, uint i) {
                    bool flag;
                    PropVariantNativeMethods.PropVariantGetBooleanElem(pv, i, out flag);
                    array.SetValue(flag, (long) i);
                };
            }
            dictionary.Add(typeof(bool), action8);
            Action<PropVariant, Array, uint> action9 = <>c.<>9__1_8;
            if (<>c.<>9__1_8 == null)
            {
                Action<PropVariant, Array, uint> local9 = <>c.<>9__1_8;
                action9 = <>c.<>9__1_8 = delegate (PropVariant pv, Array array, uint i) {
                    double num;
                    PropVariantNativeMethods.PropVariantGetDoubleElem(pv, i, out num);
                    array.SetValue(num, (long) i);
                };
            }
            dictionary.Add(typeof(double), action9);
            Action<PropVariant, Array, uint> action10 = <>c.<>9__1_9;
            if (<>c.<>9__1_9 == null)
            {
                Action<PropVariant, Array, uint> local10 = <>c.<>9__1_9;
                action10 = <>c.<>9__1_9 = delegate (PropVariant pv, Array array, uint i) {
                    float[] destination = new float[1];
                    Marshal.Copy(pv._ptr2, destination, (int) i, 1);
                    array.SetValue(destination[0], (int) i);
                };
            }
            dictionary.Add(typeof(float), action10);
            Action<PropVariant, Array, uint> action11 = <>c.<>9__1_10;
            if (<>c.<>9__1_10 == null)
            {
                Action<PropVariant, Array, uint> local11 = <>c.<>9__1_10;
                action11 = <>c.<>9__1_10 = delegate (PropVariant pv, Array array, uint i) {
                    int[] bits = new int[4];
                    for (int j = 0; j < bits.Length; j++)
                    {
                        bits[j] = Marshal.ReadInt32(pv._ptr2, ((int) (i * 0x10)) + (j * 4));
                    }
                    array.SetValue(new decimal(bits), (long) i);
                };
            }
            dictionary.Add(typeof(decimal), action11);
            Action<PropVariant, Array, uint> action12 = <>c.<>9__1_11;
            if (<>c.<>9__1_11 == null)
            {
                Action<PropVariant, Array, uint> local12 = <>c.<>9__1_11;
                action12 = <>c.<>9__1_11 = delegate (PropVariant pv, Array array, uint i) {
                    string ppszVal = string.Empty;
                    PropVariantNativeMethods.PropVariantGetStringElem(pv, i, ref ppszVal);
                    array.SetValue(ppszVal, (long) i);
                };
            }
            dictionary.Add(typeof(string), action12);
            Action<PropVariant, Array, uint> action13 = <>c.<>9__1_12;
            if (<>c.<>9__1_12 == null)
            {
                Action<PropVariant, Array, uint> local13 = <>c.<>9__1_12;
                action13 = <>c.<>9__1_12 = delegate (PropVariant pv, Array array, uint i) {
                    byte[] destination = new byte[0x10];
                    Marshal.Copy(pv._ptr, destination, 0, 0x10);
                    array.SetValue(new Guid(destination), (long) i);
                };
            }
            dictionary.Add(typeof(Guid), action13);
            return dictionary;
        }

        private object GetBlobData()
        {
            byte[] destination = new byte[this._int32];
            Marshal.Copy(this._ptr2, destination, 0, this._int32);
            return destination;
        }

        private static Func<object, PropVariant> GetDynamicConstructor(Type type)
        {
            object obj2 = _padlock;
            lock (obj2)
            {
                Func<object, PropVariant> func;
                if (!_cache.TryGetValue(type, out func))
                {
                    Type[] types = new Type[] { type };
                    ConstructorInfo constructor = typeof(PropVariant).GetConstructor(types);
                    if (constructor == null)
                    {
                        throw new NotSupportedException();
                    }
                    ParameterExpression expression = Expression.Parameter(typeof(object), "arg");
                    Expression[] arguments = new Expression[] { Expression.Convert(expression, type) };
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    func = Expression.Lambda<Func<object, PropVariant>>(Expression.New(constructor, arguments), parameters).Compile();
                    _cache.Add(type, func);
                }
                return func;
            }
        }

        private static long GetFileTimeAsLong(ref System.Runtime.InteropServices.ComTypes.FILETIME val) => 
            (val.dwHighDateTime << 0x20) + val.dwLowDateTime;

        private Array GetVector<T>()
        {
            Action<PropVariant, Array, uint> action;
            int num = PropVariantNativeMethods.PropVariantGetElementCount(this);
            if (num <= 0)
            {
                return null;
            }
            object obj2 = _padlock;
            lock (obj2)
            {
                _vectorActions ??= GenerateVectorActions();
            }
            if (!_vectorActions.TryGetValue(typeof(T), out action))
            {
                throw new InvalidCastException();
            }
            Array array = new T[num];
            for (uint i = 0; i < num; i++)
            {
                action(this, array, i);
            }
            return array;
        }

        internal void SetIUnknown(object value)
        {
            this._valueType = 13;
            this._ptr = Marshal.GetIUnknownForObject(value);
        }

        internal void SetSafeArray(Array array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            IntPtr psa = PropVariantNativeMethods.SafeArrayCreateVector(13, 0, (uint) array.Length);
            IntPtr ptr = PropVariantNativeMethods.SafeArrayAccessData(psa);
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    object o = array.GetValue(i);
                    IntPtr val = (o != null) ? Marshal.GetIUnknownForObject(o) : IntPtr.Zero;
                    Marshal.WriteIntPtr(ptr, i * IntPtr.Size, val);
                }
            }
            finally
            {
                PropVariantNativeMethods.SafeArrayUnaccessData(psa);
            }
            this._valueType = 0x200d;
            this._ptr = psa;
        }

        public VarEnum VarType
        {
            get => 
                (VarEnum) this._valueType;
            set => 
                this._valueType = (ushort) value;
        }

        public bool IsNullOrEmpty =>
            (this._valueType == 0) || (this._valueType == 1);

        public object Value
        {
            get
            {
                VarEnum enum2 = (VarEnum) this._valueType;
                if (enum2 > VarEnum.VT_CLSID)
                {
                    if (enum2 > (VarEnum.VT_VECTOR | VarEnum.VT_UI8))
                    {
                        if (enum2 == (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR))
                        {
                            return this.GetVector<string>();
                        }
                        if (enum2 == (VarEnum.VT_VECTOR | VarEnum.VT_FILETIME))
                        {
                            return this.GetVector<DateTime>();
                        }
                        if (enum2 == (VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN))
                        {
                            return CrackSingleDimSafeArray(this._ptr);
                        }
                    }
                    else
                    {
                        switch (enum2)
                        {
                            case (VarEnum.VT_VECTOR | VarEnum.VT_I2):
                                return this.GetVector<short>();

                            case (VarEnum.VT_VECTOR | VarEnum.VT_I4):
                                return this.GetVector<int>();

                            case (VarEnum.VT_VECTOR | VarEnum.VT_R4):
                                return this.GetVector<float>();

                            case (VarEnum.VT_VECTOR | VarEnum.VT_R8):
                                return this.GetVector<double>();

                            default:
                                switch (enum2)
                                {
                                    case (VarEnum.VT_VECTOR | VarEnum.VT_BOOL):
                                        return this.GetVector<bool>();

                                    case (VarEnum.VT_VECTOR | VarEnum.VT_DECIMAL):
                                        return this.GetVector<decimal>();

                                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI2):
                                        return this.GetVector<ushort>();

                                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI4):
                                        return this.GetVector<uint>();

                                    case (VarEnum.VT_VECTOR | VarEnum.VT_I8):
                                        return this.GetVector<long>();

                                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI8):
                                        return this.GetVector<ulong>();

                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                }
                else if (enum2 > VarEnum.VT_FILETIME)
                {
                    if (enum2 == VarEnum.VT_BLOB)
                    {
                        return this.GetBlobData();
                    }
                    if (enum2 == VarEnum.VT_CLSID)
                    {
                        return this.GetVector<Guid>();
                    }
                }
                else
                {
                    switch (enum2)
                    {
                        case VarEnum.VT_I2:
                            return this._short;

                        case VarEnum.VT_I4:
                        case VarEnum.VT_INT:
                            return this._int32;

                        case VarEnum.VT_R4:
                            return this._float;

                        case VarEnum.VT_R8:
                            return this._double;

                        case VarEnum.VT_CY:
                            return this._decimal;

                        case VarEnum.VT_DATE:
                            return DateTime.FromOADate(this._double);

                        case VarEnum.VT_BSTR:
                            return Marshal.PtrToStringBSTR(this._ptr);

                        case VarEnum.VT_DISPATCH:
                            return Marshal.GetObjectForIUnknown(this._ptr);

                        case VarEnum.VT_ERROR:
                            return this._long;

                        case VarEnum.VT_BOOL:
                            return (this._int32 == -1);

                        case VarEnum.VT_VARIANT:
                        case (VarEnum.VT_DECIMAL | VarEnum.VT_NULL):
                        case VarEnum.VT_VOID:
                        case VarEnum.VT_HRESULT:
                        case VarEnum.VT_PTR:
                        case VarEnum.VT_SAFEARRAY:
                        case VarEnum.VT_CARRAY:
                        case VarEnum.VT_USERDEFINED:
                            break;

                        case VarEnum.VT_UNKNOWN:
                            return Marshal.GetObjectForIUnknown(this._ptr);

                        case VarEnum.VT_DECIMAL:
                            return this._decimal;

                        case VarEnum.VT_I1:
                            return this._sbyte;

                        case VarEnum.VT_UI1:
                            return this._byte;

                        case VarEnum.VT_UI2:
                            return this._ushort;

                        case VarEnum.VT_UI4:
                        case VarEnum.VT_UINT:
                            return this._uint32;

                        case VarEnum.VT_I8:
                            return this._long;

                        case VarEnum.VT_UI8:
                            return this._ulong;

                        case VarEnum.VT_LPSTR:
                            return Marshal.PtrToStringAnsi(this._ptr);

                        case VarEnum.VT_LPWSTR:
                            return Marshal.PtrToStringUni(this._ptr);

                        default:
                            if (enum2 != VarEnum.VT_FILETIME)
                            {
                                break;
                            }
                            return DateTime.FromFileTime(this._long);
                    }
                }
                return null;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropVariant.<>c <>9 = new PropVariant.<>c();
            public static Action<PropVariant, Array, uint> <>9__1_0;
            public static Action<PropVariant, Array, uint> <>9__1_1;
            public static Action<PropVariant, Array, uint> <>9__1_2;
            public static Action<PropVariant, Array, uint> <>9__1_3;
            public static Action<PropVariant, Array, uint> <>9__1_4;
            public static Action<PropVariant, Array, uint> <>9__1_5;
            public static Action<PropVariant, Array, uint> <>9__1_6;
            public static Action<PropVariant, Array, uint> <>9__1_7;
            public static Action<PropVariant, Array, uint> <>9__1_8;
            public static Action<PropVariant, Array, uint> <>9__1_9;
            public static Action<PropVariant, Array, uint> <>9__1_10;
            public static Action<PropVariant, Array, uint> <>9__1_11;
            public static Action<PropVariant, Array, uint> <>9__1_12;

            internal void <GenerateVectorActions>b__1_0(PropVariant pv, Array array, uint i)
            {
                short num;
                PropVariantNativeMethods.PropVariantGetInt16Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_1(PropVariant pv, Array array, uint i)
            {
                ushort num;
                PropVariantNativeMethods.PropVariantGetUInt16Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_10(PropVariant pv, Array array, uint i)
            {
                int[] bits = new int[4];
                for (int j = 0; j < bits.Length; j++)
                {
                    bits[j] = Marshal.ReadInt32(pv._ptr2, ((int) (i * 0x10)) + (j * 4));
                }
                array.SetValue(new decimal(bits), (long) i);
            }

            internal void <GenerateVectorActions>b__1_11(PropVariant pv, Array array, uint i)
            {
                string ppszVal = string.Empty;
                PropVariantNativeMethods.PropVariantGetStringElem(pv, i, ref ppszVal);
                array.SetValue(ppszVal, (long) i);
            }

            internal void <GenerateVectorActions>b__1_12(PropVariant pv, Array array, uint i)
            {
                byte[] destination = new byte[0x10];
                Marshal.Copy(pv._ptr, destination, 0, 0x10);
                array.SetValue(new Guid(destination), (long) i);
            }

            internal void <GenerateVectorActions>b__1_2(PropVariant pv, Array array, uint i)
            {
                int num;
                PropVariantNativeMethods.PropVariantGetInt32Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_3(PropVariant pv, Array array, uint i)
            {
                uint num;
                PropVariantNativeMethods.PropVariantGetUInt32Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_4(PropVariant pv, Array array, uint i)
            {
                long num;
                PropVariantNativeMethods.PropVariantGetInt64Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_5(PropVariant pv, Array array, uint i)
            {
                ulong num;
                PropVariantNativeMethods.PropVariantGetUInt64Elem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_6(PropVariant pv, Array array, uint i)
            {
                System.Runtime.InteropServices.ComTypes.FILETIME filetime;
                PropVariantNativeMethods.PropVariantGetFileTimeElem(pv, i, out filetime);
                long fileTimeAsLong = PropVariant.GetFileTimeAsLong(ref filetime);
                array.SetValue(DateTime.FromFileTime(fileTimeAsLong), (long) i);
            }

            internal void <GenerateVectorActions>b__1_7(PropVariant pv, Array array, uint i)
            {
                bool flag;
                PropVariantNativeMethods.PropVariantGetBooleanElem(pv, i, out flag);
                array.SetValue(flag, (long) i);
            }

            internal void <GenerateVectorActions>b__1_8(PropVariant pv, Array array, uint i)
            {
                double num;
                PropVariantNativeMethods.PropVariantGetDoubleElem(pv, i, out num);
                array.SetValue(num, (long) i);
            }

            internal void <GenerateVectorActions>b__1_9(PropVariant pv, Array array, uint i)
            {
                float[] destination = new float[1];
                Marshal.Copy(pv._ptr2, destination, (int) i, 1);
                array.SetValue(destination[0], (int) i);
            }
        }
    }
}

