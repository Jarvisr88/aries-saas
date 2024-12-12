namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class HashCodeHelper
    {
        private const int DefaultSeed = 0x1ac0c0d7;
        private const int DefaultNullHash = 0x3b2b5ff;
        private const string HowToMoreMessage = "Ask SAY for more overloads if REALLY needed";

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode(params int[] values)
        {
            int length = values.Length;
            int num2 = 0;
            for (int i = 0; i < length; i++)
            {
                num2 ^= RotateValue(values[i], length);
            }
            return num2;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode(params object[] values)
        {
            int num = 0;
            foreach (object obj2 in values)
            {
                if (obj2 != null)
                {
                    num ^= obj2.GetHashCode();
                }
            }
            return num;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode2(params int[] array)
        {
            int num = 0x15051505;
            int num2 = num;
            int index = 0;
            int num4 = array.Length * 2;
            while (true)
            {
                if (num4 > 0)
                {
                    num = (((num << 5) + num) + (num >> 0x1b)) ^ array[index];
                    if (num4 > 2)
                    {
                        num2 = (((num2 << 5) + num2) + (num2 >> 0x1b)) ^ array[index + 1];
                        num4 -= 4;
                        index += 2;
                        continue;
                    }
                }
                return (num + (num2 * 0x5d588b65));
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2) => 
            RotateValue(h1, 2) ^ RotateValue(h2, 2);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2, int h3) => 
            (RotateValue(h1, 3) ^ RotateValue(h2, 3)) ^ RotateValue(h3, 3);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2, int h3, int h4) => 
            CalcHashCode32(CalcHashCode32(h1, h2), CalcHashCode32(h3, h4));

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4), h5);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5, int h6) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4, h5), h6);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4, h5, h6), h7);

        public static int Calculate(int h1) => 
            Finish(Combine(0x1ac0c0d7, h1));

        public static int Calculate(int h1, int h2) => 
            Finish(Combine(0x1ac0c0d7, h1, h2));

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int Calculate(int h1, int h2, params int[] hhh) => 
            Finish(Start(h1, h2, hhh));

        public static int Calculate(int h1, int h2, int h3) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3));

        public static int Calculate(int h1, int h2, int h3, int h4) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23));

        public static int Calculate(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23, int h24) => 
            Finish(Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23, h24));

        public static int CalculateByteList(IEnumerable<byte> enumerableOfOctets) => 
            Finish(StartByteList(enumerableOfOctets));

        public static int CalculateByteList(IList<byte> listOfOctets) => 
            Finish(StartByteList(listOfOctets));

        public static int CalculateByteList(byte[] listOfOctets) => 
            Finish(StartByteList(listOfOctets));

        public static int CalculateByteList(IList<byte> listOfOctets, int startIndex, int count) => 
            Finish(StartByteList(listOfOctets, startIndex, count));

        public static int CalculateByteList(byte[] listOfOctets, int startIndex, int count) => 
            Finish(StartByteList(listOfOctets, startIndex, count));

        public static int CalculateCharList(IEnumerable<char> enumerableOfChars) => 
            Finish(StartCharList(enumerableOfChars));

        public static int CalculateCharList(IList<char> listOfChars) => 
            Finish(StartCharList(listOfChars));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1>(T1 o1) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2>(T1 o1, T2 o2) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode());

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int CalculateGeneric<T1, T2>(T1 o1, T2 o2, params object[] ooo) => 
            Finish(StartGeneric<T1, T2>(o1, o2, ooo));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3>(T1 o1, T2 o2, T3 o3) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4>(T1 o1, T2 o2, T3 o3, T4 o4) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23, T24 o24) => 
            Calculate((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode(), (o24 == null) ? 0x3b2b5ff : o24.GetHashCode());

        public static int CalculateGenericList<T>(IEnumerable<T> objects) => 
            Finish(StartGenericList<T>(objects));

        public static int CalculateGenericList<T>(IList<T> listOfObjects) => 
            Finish(StartGenericList<T>(listOfObjects));

        public static int CalculateInt16List(IEnumerable<short> enumerableOfInt16) => 
            Finish(StartInt16List(enumerableOfInt16));

        public static int CalculateInt16List(IList<short> listOfInt16) => 
            Finish(StartInt16List(listOfInt16));

        public static int CalculateInt32List(IEnumerable<int> enumerableOfInt32) => 
            Finish(StartInt32List(enumerableOfInt32));

        public static int CalculateInt32List(IList<int> listOfInt32) => 
            Finish(StartInt32List(listOfInt32));

        public static int Combine(int hashState, int h1) => 
            Primitives.Murmur.CompressionFunc2(hashState, h1);

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int Combine(int hashState, int h1, params int[] hhh) => 
            CombineInt32ListCore(Combine(hashState, h1), hhh);

        public static int Combine(int hashState, int h1, int h2) => 
            Combine(Combine(hashState, h1), h2);

        public static int Combine(int hashState, int h1, int h2, int h3) => 
            Combine(Combine(Combine(hashState, h1), h2), h3);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4) => 
            Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5) => 
            Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6) => 
            Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19), h20);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19), h20), h21);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19), h20), h21), h22);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19), h20), h21), h22), h23);

        public static int Combine(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23, int h24) => 
            Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(Combine(hashState, h1), h2), h3), h4), h5), h6), h7), h8), h9), h10), h11), h12), h13), h14), h15), h16), h17), h18), h19), h20), h21), h22), h23), h24);

        public static int CombineByteList(int hashState, IEnumerable<byte> enumerableOfOctets)
        {
            IList<byte> listOfOctets = enumerableOfOctets as IList<byte>;
            if (listOfOctets != null)
            {
                return CombineByteList(hashState, listOfOctets);
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            foreach (byte num4 in enumerableOfOctets)
            {
                num++;
                int num5 = num4;
                if ((num3 & 3) != 0)
                {
                    num2 |= num5 << ((num3 * 8) & 0x1f);
                    num3++;
                    continue;
                }
                if (num3 == 4)
                {
                    hashState = Combine(hashState, num2);
                }
                num3 = 1;
                num2 = num5;
            }
            if (num3 > 0)
            {
                hashState = Combine(hashState, num2);
            }
            return (hashState ^ num);
        }

        public static int CombineByteList(int hashState, IList<byte> listOfOctets)
        {
            int count = listOfOctets.Count;
            return (CombineByteListCore(hashState, listOfOctets, 0, count) ^ count);
        }

        public static int CombineByteList(int hashState, byte[] listOfOctets)
        {
            int length = listOfOctets.Length;
            return (CombineByteListCore(hashState, listOfOctets, 0, length) ^ length);
        }

        public static int CombineByteList(int hashState, IList<byte> listOfOctets, int startIndex, int count) => 
            CombineByteListCore(hashState, listOfOctets, startIndex, count) ^ count;

        public static int CombineByteList(int hashState, byte[] listOfOctets, int startIndex, int count) => 
            CombineByteListCore(hashState, listOfOctets, startIndex, count) ^ count;

        private static int CombineByteListCore(int hashState, IList<byte> listOfOctets, int start, int count)
        {
            int num = start + count;
            int num2 = num - 4;
            int num3 = start;
            while (num3 <= num2)
            {
                int num4 = listOfOctets[num3];
                int num5 = listOfOctets[num3 + 1];
                int num6 = listOfOctets[num3 + 2];
                int num7 = listOfOctets[num3 + 3];
                int next = ((num4 | (num5 << 8)) | (num6 << 0x10)) | (num7 << 0x18);
                hashState = Primitives.Murmur.CompressionFunc2(hashState, next);
                num3 += 4;
            }
            if (num3 < num)
            {
                int num9 = listOfOctets[num3];
                num3++;
                int num10 = 8;
                while (true)
                {
                    if (num3 >= num)
                    {
                        hashState = Combine(hashState, num9);
                        break;
                    }
                    num9 |= listOfOctets[num3] << (num10 & 0x1f);
                    num3++;
                    num10 += 8;
                }
            }
            return hashState;
        }

        private static int CombineByteListCore(int hashState, byte[] listOfOctets, int start, int count)
        {
            int num = start + count;
            int num2 = num - 4;
            int index = start;
            while (index <= num2)
            {
                int num4 = listOfOctets[index];
                int num5 = listOfOctets[index + 1];
                int num6 = listOfOctets[index + 2];
                int num7 = listOfOctets[index + 3];
                int next = ((num4 | (num5 << 8)) | (num6 << 0x10)) | (num7 << 0x18);
                hashState = Primitives.Murmur.CompressionFunc2(hashState, next);
                index += 4;
            }
            if (index < num)
            {
                int num9 = listOfOctets[index];
                index++;
                int num10 = 8;
                while (true)
                {
                    if (index >= num)
                    {
                        hashState = Combine(hashState, num9);
                        break;
                    }
                    num9 |= listOfOctets[index] << (num10 & 0x1f);
                    index++;
                    num10 += 8;
                }
            }
            return hashState;
        }

        public static int CombineCharList(int hashState, IEnumerable<char> enumerableOfChars)
        {
            IList<char> listOfChars = enumerableOfChars as IList<char>;
            if (listOfChars != null)
            {
                return CombineCharList(hashState, listOfChars);
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            foreach (char ch in enumerableOfChars)
            {
                num++;
                int num4 = ch;
                if (num3 == 1)
                {
                    num2 |= num4 << 0x10;
                    num3 = 2;
                    continue;
                }
                if (num3 == 2)
                {
                    hashState = Combine(hashState, num2);
                }
                num2 = num4;
                num3 = 1;
            }
            if (num3 > 0)
            {
                hashState = Combine(hashState, num2);
            }
            return (hashState ^ (num * 2));
        }

        public static int CombineCharList(int hashState, IList<char> listOfChars) => 
            CombineCharListCore(hashState, listOfChars) ^ (listOfChars.Count * 2);

        private static int CombineCharListCore(int hashState, IList<char> listOfChars)
        {
            int count = listOfChars.Count;
            int num2 = count - 2;
            int num3 = 0;
            while (num3 <= num2)
            {
                int num4 = listOfChars[num3];
                int num5 = listOfChars[num3 + 1];
                int next = num4 | (num5 << 0x10);
                hashState = Primitives.Murmur.CompressionFunc2(hashState, next);
                num3 += 2;
            }
            if (num3 < count)
            {
                hashState = Combine(hashState, listOfChars[num3]);
            }
            return hashState;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1>(int hashState, T1 o1) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode());

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int CombineGeneric<T1>(int hashState, T1 o1, params object[] ooo) => 
            CombineGenericListCore<object>(CombineGeneric<T1>(hashState, o1), ooo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2>(int hashState, T1 o1, T2 o2) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3>(int hashState, T1 o1, T2 o2, T3 o3) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CombineGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23, T24 o24) => 
            Combine(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode(), (o24 == null) ? 0x3b2b5ff : o24.GetHashCode());

        public static int CombineGenericList<T>(int hashState, IEnumerable<T> objects)
        {
            IList<T> listOfObjects = objects as IList<T>;
            if (listOfObjects != null)
            {
                return CombineGenericList<T>(hashState, listOfObjects);
            }
            int num = 0;
            foreach (T local in objects)
            {
                num++;
                hashState = CombineGeneric<T>(hashState, local);
            }
            return (hashState ^ (num * 4));
        }

        public static int CombineGenericList<T>(int hashState, IList<T> listOfObjects) => 
            CombineGenericListCore<T>(hashState, listOfObjects) ^ (listOfObjects.Count * 4);

        private static int CombineGenericListCore<T>(int hashState, IList<T> listOfObjects)
        {
            int count = listOfObjects.Count;
            for (int i = 0; i < count; i++)
            {
                hashState = CombineGeneric<T>(hashState, listOfObjects[i]);
            }
            return hashState;
        }

        public static int CombineInt16List(int hashState, IEnumerable<short> enumerableOfInt16)
        {
            IList<short> list = enumerableOfInt16 as IList<short>;
            if (list != null)
            {
                return CombineInt16List(hashState, list);
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            foreach (short num4 in enumerableOfInt16)
            {
                num++;
                int num5 = (ushort) num4;
                if (num3 == 1)
                {
                    num2 |= num5 << 0x10;
                    num3 = 2;
                    continue;
                }
                if (num3 == 2)
                {
                    hashState = Combine(hashState, num2);
                }
                num2 = num5;
                num3 = 1;
            }
            if (num3 > 0)
            {
                hashState = Combine(hashState, num2);
            }
            return (hashState ^ (num * 2));
        }

        public static int CombineInt16List(int hashState, IList<short> listOfInt16s) => 
            CombineInt16ListCore(hashState, listOfInt16s) ^ (listOfInt16s.Count * 2);

        private static int CombineInt16ListCore(int hashState, IList<short> listOfInt16s)
        {
            int count = listOfInt16s.Count;
            int num2 = count - 2;
            int num3 = 0;
            while (num3 <= num2)
            {
                int num4 = listOfInt16s[num3];
                int num5 = listOfInt16s[num3 + 1];
                int next = num4 + (num5 << 0x10);
                hashState = Primitives.Murmur.CompressionFunc2(hashState, next);
                num3 += 2;
            }
            if (num3 < count)
            {
                hashState = Combine(hashState, listOfInt16s[num3]);
            }
            return hashState;
        }

        public static int CombineInt32List(int hashState, IEnumerable<int> enumerableOfInt32)
        {
            IList<int> list = enumerableOfInt32 as IList<int>;
            if (list != null)
            {
                return CombineInt32List(hashState, list);
            }
            int num = 0;
            foreach (int num2 in enumerableOfInt32)
            {
                num++;
                hashState = Combine(hashState, num2);
            }
            return (hashState ^ (num * 4));
        }

        public static int CombineInt32List(int hashState, IList<int> listOfInt32s) => 
            CombineInt32ListCore(hashState, listOfInt32s) ^ (listOfInt32s.Count * 4);

        private static int CombineInt32ListCore(int hashState, IList<int> listOfInt32s)
        {
            int count = listOfInt32s.Count;
            for (int i = 0; i < count; i++)
            {
                hashState = Primitives.Murmur.CompressionFunc2(hashState, listOfInt32s[i]);
            }
            return hashState;
        }

        public static int Finish(int hashState) => 
            Primitives.Murmur.FinalisationFunc2(hashState);

        public static int Finish(int hashState, int h1) => 
            Finish(Combine(hashState, h1));

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int Finish(int hashState, int h1, params int[] hhh) => 
            Finish(Combine(hashState, h1, hhh));

        public static int Finish(int hashState, int h1, int h2) => 
            Finish(Combine(hashState, h1, h2));

        public static int Finish(int hashState, int h1, int h2, int h3) => 
            Finish(Combine(hashState, h1, h2, h3));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4) => 
            Finish(Combine(hashState, h1, h2, h3, h4));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23));

        public static int Finish(int hashState, int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23, int h24) => 
            Finish(Combine(hashState, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23, h24));

        public static int FinishByteList(int hashState, IEnumerable<byte> enumerableOfOctets) => 
            Finish(CombineByteList(hashState, enumerableOfOctets));

        public static int FinishByteList(int hashState, IList<byte> listOfOctets) => 
            Finish(CombineByteList(hashState, listOfOctets));

        public static int FinishByteList(int hashState, byte[] listOfOctets) => 
            Finish(CombineByteList(hashState, listOfOctets));

        public static int FinishByteList(int hashState, IList<byte> listOfOctets, int startIndex, int count) => 
            Finish(CombineByteList(hashState, listOfOctets, startIndex, count));

        public static int FinishByteList(int hashState, byte[] listOfOctets, int startIndex, int count) => 
            Finish(CombineByteList(hashState, listOfOctets, startIndex, count));

        public static int FinishCharList(int hashState, IEnumerable<char> enumerableOfChars) => 
            Finish(CombineCharList(hashState, enumerableOfChars));

        public static int FinishCharList(int hashState, IList<char> listOfChars) => 
            Finish(CombineCharList(hashState, listOfChars));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1>(int hashState, T1 o1) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode());

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int FinishGeneric<T1>(int hashState, T1 o1, params object[] ooo) => 
            Finish(CombineGeneric<T1>(hashState, o1, ooo));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2>(int hashState, T1 o1, T2 o2) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3>(int hashState, T1 o1, T2 o2, T3 o3) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FinishGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(int hashState, T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23, T24 o24) => 
            Finish(hashState, (o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode(), (o24 == null) ? 0x3b2b5ff : o24.GetHashCode());

        public static int FinishGenericList<T>(int hashState, IEnumerable<T> objects) => 
            Finish(CombineGenericList<T>(hashState, objects));

        public static int FinishGenericList<T>(int hashState, IList<T> listOfObjects) => 
            Finish(CombineGenericList<T>(hashState, listOfObjects));

        public static int FinishInt16List(int hashState, IEnumerable<short> enumerableOfInt16) => 
            Finish(CombineInt16List(hashState, enumerableOfInt16));

        public static int FinishInt16List(int hashState, IList<short> listOfInt16) => 
            Finish(CombineInt16List(hashState, listOfInt16));

        public static int FinishInt32List(int hashState, IEnumerable<int> enumerableOfInt32) => 
            Finish(CombineInt32List(hashState, enumerableOfInt32));

        public static int FinishInt32List(int hashState, IList<int> listOfInt32) => 
            Finish(CombineInt32List(hashState, listOfInt32));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode<T>(T obj) => 
            (obj != null) ? obj.GetHashCode() : 0x3b2b5ff;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNullHash() => 
            0x3b2b5ff;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static int RotateValue(int val, int count)
        {
            int num = (13 * count) & 0x1f;
            return ((val << (num & 0x1f)) | (val >> ((0x20 - num) & 0x1f)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Start() => 
            0x1ac0c0d7;

        public static int Start(int h1) => 
            Combine(0x1ac0c0d7, h1);

        public static int Start(int h1, int h2) => 
            Combine(0x1ac0c0d7, h1, h2);

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int Start(int h1, int h2, params int[] hhh) => 
            Combine(Start(h1), h2, hhh);

        public static int Start(int h1, int h2, int h3) => 
            Combine(0x1ac0c0d7, h1, h2, h3);

        public static int Start(int h1, int h2, int h3, int h4) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4);

        public static int Start(int h1, int h2, int h3, int h4, int h5) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23);

        public static int Start(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15, int h16, int h17, int h18, int h19, int h20, int h21, int h22, int h23, int h24) => 
            Combine(0x1ac0c0d7, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23, h24);

        public static int StartByteList(IEnumerable<byte> enumerableOfOctets) => 
            CombineByteList(Start(), enumerableOfOctets);

        public static int StartByteList(IList<byte> listOfOctets) => 
            CombineByteList(Start(), listOfOctets);

        public static int StartByteList(byte[] listOfOctets) => 
            CombineByteList(Start(), listOfOctets);

        public static int StartByteList(IList<byte> listOfOctets, int startIndex, int count) => 
            CombineByteList(Start(), listOfOctets, startIndex, count);

        public static int StartByteList(byte[] listOfOctets, int startIndex, int count) => 
            CombineByteList(Start(), listOfOctets, startIndex, count);

        public static int StartCharList(IEnumerable<char> enumerableOfChars) => 
            CombineCharList(Start(), enumerableOfChars);

        public static int StartCharList(IList<char> listOfChars) => 
            CombineCharList(Start(), listOfChars);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1>(T1 o1) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2>(T1 o1, T2 o2) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode());

        [Obsolete("Ask SAY for more overloads if REALLY needed"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public static int StartGeneric<T1, T2>(T1 o1, T2 o2, params object[] ooo) => 
            CombineGeneric<T2>(StartGeneric<T1>(o1), o2, ooo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3>(T1 o1, T2 o2, T3 o3) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4>(T1 o1, T2 o2, T3 o3, T4 o4) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StartGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(T1 o1, T2 o2, T3 o3, T4 o4, T5 o5, T6 o6, T7 o7, T8 o8, T9 o9, T10 o10, T11 o11, T12 o12, T13 o13, T14 o14, T15 o15, T16 o16, T17 o17, T18 o18, T19 o19, T20 o20, T21 o21, T22 o22, T23 o23, T24 o24) => 
            Start((o1 == null) ? 0x3b2b5ff : o1.GetHashCode(), (o2 == null) ? 0x3b2b5ff : o2.GetHashCode(), (o3 == null) ? 0x3b2b5ff : o3.GetHashCode(), (o4 == null) ? 0x3b2b5ff : o4.GetHashCode(), (o5 == null) ? 0x3b2b5ff : o5.GetHashCode(), (o6 == null) ? 0x3b2b5ff : o6.GetHashCode(), (o7 == null) ? 0x3b2b5ff : o7.GetHashCode(), (o8 == null) ? 0x3b2b5ff : o8.GetHashCode(), (o9 == null) ? 0x3b2b5ff : o9.GetHashCode(), (o10 == null) ? 0x3b2b5ff : o10.GetHashCode(), (o11 == null) ? 0x3b2b5ff : o11.GetHashCode(), (o12 == null) ? 0x3b2b5ff : o12.GetHashCode(), (o13 == null) ? 0x3b2b5ff : o13.GetHashCode(), (o14 == null) ? 0x3b2b5ff : o14.GetHashCode(), (o15 == null) ? 0x3b2b5ff : o15.GetHashCode(), (o16 == null) ? 0x3b2b5ff : o16.GetHashCode(), (o17 == null) ? 0x3b2b5ff : o17.GetHashCode(), (o18 == null) ? 0x3b2b5ff : o18.GetHashCode(), (o19 == null) ? 0x3b2b5ff : o19.GetHashCode(), (o20 == null) ? 0x3b2b5ff : o20.GetHashCode(), (o21 == null) ? 0x3b2b5ff : o21.GetHashCode(), (o22 == null) ? 0x3b2b5ff : o22.GetHashCode(), (o23 == null) ? 0x3b2b5ff : o23.GetHashCode(), (o24 == null) ? 0x3b2b5ff : o24.GetHashCode());

        public static int StartGenericList<T>(IEnumerable<T> objects) => 
            CombineGenericList<T>(Start(), objects);

        public static int StartGenericList<T>(IList<T> listOfObjects) => 
            CombineGenericList<T>(Start(), listOfObjects);

        public static int StartInt16List(IEnumerable<short> enumerableOfInt16) => 
            CombineInt16List(Start(), enumerableOfInt16);

        public static int StartInt16List(IList<short> listOfInt16) => 
            CombineInt16List(Start(), listOfInt16);

        public static int StartInt32List(IEnumerable<int> enumerableOfInt32) => 
            CombineInt32List(Start(), enumerableOfInt32);

        public static int StartInt32List(IList<int> listOfInt32) => 
            CombineInt32List(Start(), listOfInt32);

        public static int Seed =>
            0x1ac0c0d7;

        public static int NullHash =>
            0x3b2b5ff;

        public static class Blob
        {
            public static Digest CreateDigest(byte[] listOfOctets, int start, int count) => 
                !Environment.Is64BitProcess ? Murmur3_128_32(listOfOctets, start, count) : Murmur3_128_64(listOfOctets, start, count);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static uint FMix32(uint h)
            {
                h ^= h >> 0x10;
                h *= 0x85ebca6b;
                h ^= h >> 13;
                h *= 0xc2b2ae35;
                h ^= h >> 0x10;
                return h;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static ulong FMix64(ulong val)
            {
                ulong num = val;
                num = (num ^ (num >> 0x21)) * 18397679294719823053UL;
                num = (num ^ (num >> 0x21)) * 14181476777654086739UL;
                return (num ^ (num >> 0x21));
            }

            public static Digest Murmur3_128_32(byte[] listOfOctets, int start, int count)
            {
                uint num12;
                uint num13;
                uint num14;
                uint num15;
                int num = start + count;
                int num2 = num - 0x10;
                uint h = 0x1ac0c0d7;
                uint num4 = 0x1ac0c0d7;
                uint num5 = 0x1ac0c0d7;
                uint num6 = 0x1ac0c0d7;
                int index = start;
                while (true)
                {
                    if (index <= num2)
                    {
                        uint num8 = (uint) ((((listOfOctets[index] | (listOfOctets[index + 1] << 8)) | (listOfOctets[index + 2] << 0x10)) | (listOfOctets[index + 3] << 0x18)) * 0x239b961b);
                        h ^= ((num8 << 15) | (num8 >> 0x11)) * 0xab0e9789;
                        h = ((((h << 0x13) | (h >> 13)) + num4) * 5) + 0x561ccd1b;
                        uint num9 = (uint) ((((listOfOctets[index + 4] | (listOfOctets[index + 5] << 8)) | (listOfOctets[index + 6] << 0x10)) | (listOfOctets[index + 7] << 0x18)) * -1425107063);
                        num4 ^= ((num9 << 0x10) | (num9 >> 0x10)) * 0x38b34ae5;
                        num4 = ((((num4 << 0x11) | (num4 >> 15)) + num5) * 5) + 0xbcaa747;
                        uint num10 = (uint) ((((listOfOctets[index + 8] | (listOfOctets[index + 9] << 8)) | (listOfOctets[index + 10] << 0x10)) | (listOfOctets[index + 11] << 0x18)) * 0x38b34ae5);
                        num5 ^= ((num10 << 0x11) | (num10 >> 15)) * 0xa1e38b93;
                        num5 = ((((num5 << 15) | (num5 >> 0x11)) + num6) * 5) + 0x96cd1c35;
                        uint num11 = (uint) ((((listOfOctets[index + 12] | (listOfOctets[index + 13] << 8)) | (listOfOctets[index + 14] << 0x10)) | (listOfOctets[index + 15] << 0x18)) * -1578923117);
                        num6 ^= ((num11 << 0x12) | (num11 >> 14)) * 0x239b961b;
                        num6 = ((((num6 << 13) | (num6 >> 0x13)) + h) * 5) + 0x32ac3b17;
                        index += 0x10;
                        continue;
                    }
                    num12 = 0;
                    num13 = 0;
                    num14 = 0;
                    num15 = 0;
                    switch ((num - index))
                    {
                        case 0:
                            goto TR_0001;

                        case 1:
                            goto TR_0002;

                        case 2:
                            goto TR_0003;

                        case 3:
                            goto TR_0004;

                        case 4:
                            goto TR_0005;

                        case 5:
                            goto TR_0006;

                        case 6:
                            goto TR_0007;

                        case 7:
                            goto TR_0008;

                        case 8:
                            goto TR_0009;

                        case 9:
                            goto TR_000A;

                        case 10:
                            goto TR_000B;

                        case 11:
                            goto TR_000C;

                        case 12:
                            goto TR_000D;

                        case 13:
                            goto TR_000E;

                        case 14:
                            break;

                        case 15:
                            num15 |= (uint) (listOfOctets[index + 14] << 0x10);
                            break;

                        default:
                            throw new InvalidOperationException($"invalid firstIndBeyond({num}) - i({index}) = {num - index}");
                    }
                    num15 |= (uint) (listOfOctets[index + 13] << 8);
                    break;
                }
                goto TR_000E;
            TR_0001:
                num4 ^= (uint) count;
                num5 ^= (uint) count;
                num6 ^= (uint) count;
                h = (((h ^ ((uint) count)) + num4) + num5) + num6;
                num4 = FMix32(num4 + h);
                num5 = FMix32(num5 + h);
                num6 = FMix32(num6 + h);
                h = ((FMix32(h) + num4) + num5) + num6;
                num5 += h;
                num6 += h;
                return new Digest(((num4 + h) << 0x20) | h, (num6 << 0x20) | num5);
            TR_0002:
                num12 = (num12 | listOfOctets[index]) * 0x239b961b;
                h ^= ((num12 << 15) | (num12 >> 0x11)) * 0xab0e9789;
                goto TR_0001;
            TR_0003:
                num12 |= (uint) (listOfOctets[index + 1] << 8);
                goto TR_0002;
            TR_0004:
                num12 |= (uint) (listOfOctets[index + 2] << 0x10);
                goto TR_0003;
            TR_0005:
                num12 |= (uint) (listOfOctets[index + 3] << 0x18);
                goto TR_0004;
            TR_0006:
                num13 = (num13 | listOfOctets[index + 4]) * 0xab0e9789;
                num4 ^= ((num13 << 0x10) | (num13 >> 0x10)) * 0x38b34ae5;
                goto TR_0005;
            TR_0007:
                num13 |= (uint) (listOfOctets[index + 5] << 8);
                goto TR_0006;
            TR_0008:
                num13 |= (uint) (listOfOctets[index + 6] << 0x10);
                goto TR_0007;
            TR_0009:
                num13 |= (uint) (listOfOctets[index + 7] << 0x18);
                goto TR_0008;
            TR_000A:
                num14 = (num14 | listOfOctets[index + 8]) * 0x38b34ae5;
                num5 ^= ((num14 << 0x11) | (num14 >> 15)) * 0xa1e38b93;
                goto TR_0009;
            TR_000B:
                num14 |= (uint) (listOfOctets[index + 9] << 8);
                goto TR_000A;
            TR_000C:
                num14 |= (uint) (listOfOctets[index + 10] << 0x10);
                goto TR_000B;
            TR_000D:
                num14 |= (uint) (listOfOctets[index + 11] << 0x18);
                goto TR_000C;
            TR_000E:
                num15 = (num15 | listOfOctets[index + 12]) * 0xa1e38b93;
                num6 ^= ((num15 << 0x12) | (num15 >> 14)) * 0x239b961b;
                goto TR_000D;
            }

            public static Digest Murmur3_128_64(byte[] listOfOctets, int start, int count)
            {
                ulong num8;
                ulong num9;
                int num = start + count;
                int num2 = num - 0x10;
                ulong val = 0x1ac0c0d7UL;
                ulong num4 = 0x1ac0c0d7UL;
                int index = start;
                while (true)
                {
                    if (index <= num2)
                    {
                        ulong num6 = (ulong) ((((((((listOfOctets[index] | (listOfOctets[index + 1] << 8)) | (listOfOctets[index + 2] << 0x10)) | (listOfOctets[index + 3] << 0x18)) | (listOfOctets[index + 4] << 0x20)) | (listOfOctets[index + 5] << 40)) | (listOfOctets[index + 6] << 0x30)) | (listOfOctets[index + 7] << 0x38)) * 9782798678568883157UL);
                        val ^= ((num6 << 0x1f) | (num6 >> 0x21)) * ((ulong) 0x4cf5ad432745937fL);
                        val = ((((val << 0x1b) | (val >> 0x25)) + num4) * ((ulong) 5L)) + ((ulong) 0x52dce729L);
                        ulong num7 = (ulong) ((((((((listOfOctets[index + 8] | (listOfOctets[index + 9] << 8)) | (listOfOctets[index + 10] << 0x10)) | (listOfOctets[index + 11] << 0x18)) | (listOfOctets[index + 12] << 0x20)) | (listOfOctets[index + 13] << 40)) | (listOfOctets[index + 14] << 0x30)) | (listOfOctets[index + 15] << 0x38)) * ((ulong) 0x4cf5ad432745937fL));
                        num4 ^= ((num7 << 0x21) | (num7 >> 0x1f)) * 9782798678568883157UL;
                        num4 = ((((num4 << 0x1f) | (num4 >> 0x21)) + val) * ((ulong) 5L)) + ((ulong) 0x38495ab5L);
                        index += 0x10;
                        continue;
                    }
                    num8 = 0UL;
                    num9 = 0UL;
                    switch ((num - index))
                    {
                        case 0:
                            goto TR_0001;

                        case 1:
                            goto TR_0002;

                        case 2:
                            goto TR_0003;

                        case 3:
                            goto TR_0004;

                        case 4:
                            goto TR_0005;

                        case 5:
                            goto TR_0006;

                        case 6:
                            goto TR_0007;

                        case 7:
                            goto TR_0008;

                        case 8:
                            goto TR_0009;

                        case 9:
                            goto TR_000A;

                        case 10:
                            goto TR_000B;

                        case 11:
                            goto TR_000C;

                        case 12:
                            goto TR_000D;

                        case 13:
                            goto TR_000E;

                        case 14:
                            break;

                        case 15:
                            num9 |= listOfOctets[index + 14] << 0x30;
                            break;

                        default:
                            throw new InvalidOperationException($"invalid firstIndBeyond({num}) - i({index}) = {num - index}");
                    }
                    num9 |= listOfOctets[index + 13] << 40;
                    break;
                }
                goto TR_000E;
            TR_0001:
                num4 ^= count;
                val = (val ^ count) + num4;
                num4 = FMix64(num4 + val);
                val = FMix64(val) + num4;
                return new Digest(val, num4 + val);
            TR_0002:
                num8 = (num8 | listOfOctets[index]) * 9782798678568883157UL;
                val ^= ((num8 << 0x1f) | (num8 >> 0x21)) * ((ulong) 0x4cf5ad432745937fL);
                goto TR_0001;
            TR_0003:
                num8 |= listOfOctets[index + 1] << 8;
                goto TR_0002;
            TR_0004:
                num8 |= listOfOctets[index + 2] << 0x10;
                goto TR_0003;
            TR_0005:
                num8 |= listOfOctets[index + 3] << 0x18;
                goto TR_0004;
            TR_0006:
                num8 |= listOfOctets[index + 4] << 0x20;
                goto TR_0005;
            TR_0007:
                num8 |= listOfOctets[index + 5] << 40;
                goto TR_0006;
            TR_0008:
                num8 |= listOfOctets[index + 6] << 0x30;
                goto TR_0007;
            TR_0009:
                num8 |= listOfOctets[index + 7] << 0x30;
                goto TR_0008;
            TR_000A:
                num9 = (num9 | listOfOctets[index + 8]) * ((ulong) 0x4cf5ad432745937fL);
                num4 ^= ((num9 << 0x21) | (num9 >> 0x1f)) * 9782798678568883157UL;
                goto TR_0009;
            TR_000B:
                num9 |= listOfOctets[index + 9] << 8;
                goto TR_000A;
            TR_000C:
                num9 |= listOfOctets[index + 10] << 0x10;
                goto TR_000B;
            TR_000D:
                num9 |= listOfOctets[index + 11] << 0x18;
                goto TR_000C;
            TR_000E:
                num9 |= listOfOctets[index + 12] << 0x20;
                goto TR_000D;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct Digest : IEquatable<HashCodeHelper.Blob.Digest>
            {
                private ulong H1;
                private ulong H2;
                internal Digest(ulong h1, ulong h2)
                {
                    this.H1 = h1;
                    this.H2 = h2;
                }

                public bool Equals(HashCodeHelper.Blob.Digest other) => 
                    (this.H1 == other.H1) && (this.H2 == other.H2);

                public override bool Equals(object obj) => 
                    (obj is HashCodeHelper.Blob.Digest) && this.Equals((HashCodeHelper.Blob.Digest) obj);

                public override int GetHashCode() => 
                    this.H1.GetHashCode() ^ this.H2.GetHashCode();

                public override string ToString() => 
                    $"{this.H2:X16}{this.H1:X16}";
            }
        }

        public static class OrderInsensitive
        {
            public static int CalculateGenericList<T>(IEnumerable<T> objects) => 
                CombineGenericList<T>(HashCodeHelper.Start(), objects);

            public static int CalculateGenericList<T>(IList<T> listOfObjects) => 
                CombineGenericList<T>(HashCodeHelper.Start(), listOfObjects);

            public static int CalculateInt32List(IEnumerable<int> enumerableOfInt32) => 
                CombineInt32List(HashCodeHelper.Start(), enumerableOfInt32);

            public static int CalculateInt32List(IList<int> listOfInt32s) => 
                CombineInt32List(HashCodeHelper.Start(), listOfInt32s);

            public static int Combine(int hashState, int h1) => 
                hashState + HashCodeHelper.Primitives.Murmur.FinalisationFunc2(h1);

            public static int CombineGeneric<T1>(int hashState, T1 o1) => 
                Combine(hashState, HashCodeHelper.GetHashCode<T1>(o1));

            public static int CombineGenericList<T>(int hashState, IEnumerable<T> objects)
            {
                IList<T> listOfObjects = objects as IList<T>;
                if (listOfObjects != null)
                {
                    return CombineGenericList<T>(hashState, listOfObjects);
                }
                int num = 0;
                foreach (T local in objects)
                {
                    num++;
                    hashState = CombineGeneric<T>(hashState, local);
                }
                return (hashState ^ num);
            }

            public static int CombineGenericList<T>(int hashState, IList<T> listOfObjects)
            {
                int count = listOfObjects.Count;
                for (int i = 0; i < count; i++)
                {
                    hashState = CombineGeneric<T>(hashState, listOfObjects[i]);
                }
                return (hashState ^ count);
            }

            public static int CombineInt32(int hashState, int i) => 
                Combine(hashState, i);

            public static int CombineInt32List(int hashState, IEnumerable<int> enumerableOfInt32)
            {
                IList<int> list = enumerableOfInt32 as IList<int>;
                if (list != null)
                {
                    return CombineInt32List(hashState, list);
                }
                int num = 0;
                foreach (int num2 in enumerableOfInt32)
                {
                    num++;
                    hashState = CombineInt32(hashState, num2);
                }
                return (hashState ^ num);
            }

            public static int CombineInt32List(int hashState, IList<int> listOfInt32s)
            {
                int count = listOfInt32s.Count;
                for (int i = 0; i < count; i++)
                {
                    hashState = CombineInt32(hashState, listOfInt32s[i]);
                }
                return (hashState ^ count);
            }
        }

        private static class Packing
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Pack(int seed, bool value) => 
                Pack(seed, Start(value), 2);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Pack(int seed, bool? value) => 
                Pack(seed, Start(value), 3);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Pack(int seed, int value, int range) => 
                (seed * range) + value;

            public static int Pack(int seed, int? value, int underlyingRange)
            {
                if (value == null)
                {
                    return ((seed * (underlyingRange + 1)) + underlyingRange);
                }
                int num = value.Value;
                return ((seed * (underlyingRange + 1)) + num);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Start(bool value) => 
                value ? 1 : 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Start(int value) => 
                value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Start(bool? value) => 
                (value == null) ? 2 : (value.Value ? 1 : 0);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Start(int value, int range) => 
                value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Start(int? value, int underlyingRange) => 
                (value == null) ? underlyingRange : value.Value;
        }

        public static class Primitives
        {
            public const int FastPrime = -1521134295;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int FastMixer(int hashState, int nextHash) => 
                (hashState ^ nextHash) * -1521134295;

            public static class FNV1a
            {
                private const uint UInt32Prime = 0x1000193;
                private const uint UInt32Offset = 0x811c9dc5;
                private const ulong UInt64Prime = 0x100000001b3UL;
                private const ulong UInt64Offset = 14695981039346656037UL;
                public const int Int32Prime = 0x1000193;
                public const int Int32Offset = -2128831035;
                public const long Int64Prime = 0x100000001b3L;
                public const long Int64Offset = -3750763034362895579L;

                public static int Combine16(int hashState, short nextInt16) => 
                    Combine8(Combine8(hashState, nextInt16 & 0xff), (nextInt16 >> 8) & 0xff);

                internal static int Combine16(int hashState, ushort nextUInt16) => 
                    Combine8(Combine8(hashState, nextUInt16 & 0xff), nextUInt16 >> 8);

                public static long Combine16(long hashState, short nextInt16) => 
                    Combine8(Combine8(hashState, (long) (nextInt16 & 0xff)), (long) ((nextInt16 >> 8) & 0xff));

                internal static long Combine16(long hashState, ushort nextUInt16) => 
                    Combine8(Combine8(hashState, (long) (nextUInt16 & 0xff)), (long) (nextUInt16 >> 8));

                public static int Combine32(int hashState, int nextInt32) => 
                    Combine8(Combine8(Combine8(Combine8(hashState, nextInt32 & 0xff), (nextInt32 >> 8) & 0xff), (nextInt32 >> 0x10) & 0xff), (nextInt32 >> 0x18) & 0xff);

                public static long Combine32(long hashState, int nextInt32) => 
                    Combine8(Combine8(Combine8(Combine8(hashState, (long) (nextInt32 & 0xff)), (long) ((nextInt32 >> 8) & 0xff)), (long) ((nextInt32 >> 0x10) & 0xff)), (long) ((nextInt32 >> 0x18) & 0xff));

                public static int Combine8(int hashState, int nextOctet) => 
                    (hashState ^ nextOctet) * 0x1000193;

                public static long Combine8(long hashState, long nextOctet) => 
                    (hashState ^ nextOctet) * 0x100000001b3L;
            }

            public static class Murmur
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int CompressionFunc2(int prev, int next)
                {
                    uint num2 = (uint) (next * 0x5bd1e995);
                    return ((prev * 0x5bd1e995) ^ ((int) ((num2 ^ (num2 >> 0x18)) * 0x5bd1e995)));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int CompressionFunc3(int prev, int next)
                {
                    uint num2 = (uint) (next * -862048943);
                    uint num = ((uint) prev) ^ (((num2 << 15) | (num2 >> 0x11)) * 0x1b873593);
                    return (((int) (((num << 13) | (num >> 0x13)) * 5)) + -430675100);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int FinalisationFunc2(int hashState)
                {
                    uint num = (uint) hashState;
                    num = (num ^ (num >> 13)) * 0x5bd1e995;
                    return (int) (num ^ (num >> 15));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int FinalisationFunc3(int hashState)
                {
                    uint num = (uint) hashState;
                    num = (num ^ (num >> 0x10)) * 0x85ebca6b;
                    num = (num ^ (num >> 13)) * 0xc2b2ae35;
                    return (int) (num ^ (num >> 0x10));
                }
            }
        }
    }
}

