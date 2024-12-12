namespace DevExpress.Office.NumberConverters
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct LanguageId : IConvertToInt<LanguageId>
    {
        public static readonly LanguageId English;
        public static readonly LanguageId French;
        public static readonly LanguageId German;
        public static readonly LanguageId Italian;
        public static readonly LanguageId Russian;
        public static readonly LanguageId Swedish;
        public static readonly LanguageId Turkish;
        public static readonly LanguageId Greek;
        public static readonly LanguageId Spanish;
        public static readonly LanguageId Portuguese;
        public static readonly LanguageId Ukrainian;
        public static readonly LanguageId Hindi;
        private readonly int m_value;
        internal LanguageId(int value)
        {
            this.m_value = value;
        }

        public override bool Equals(object obj) => 
            (obj is LanguageId) && (this.m_value == ((LanguageId) obj).m_value);

        public override int GetHashCode() => 
            this.m_value.GetHashCode();

        public override string ToString() => 
            this.m_value.ToString();

        public static bool operator ==(LanguageId id1, LanguageId id2) => 
            id1.m_value == id2.m_value;

        public static bool operator !=(LanguageId id1, LanguageId id2) => 
            id1.m_value != id2.m_value;

        int IConvertToInt<LanguageId>.ToInt() => 
            this.m_value;

        LanguageId IConvertToInt<LanguageId>.FromInt(int value) => 
            new LanguageId(value);

        static LanguageId()
        {
            English = new LanguageId(0x409);
            French = new LanguageId(0x40c);
            German = new LanguageId(0x407);
            Italian = new LanguageId(0x410);
            Russian = new LanguageId(0x419);
            Swedish = new LanguageId(0x41d);
            Turkish = new LanguageId(0x41f);
            Greek = new LanguageId(0x421);
            Spanish = new LanguageId(0x423);
            Portuguese = new LanguageId(0x425);
            Ukrainian = new LanguageId(0x427);
            Hindi = new LanguageId(0x429);
        }
    }
}

