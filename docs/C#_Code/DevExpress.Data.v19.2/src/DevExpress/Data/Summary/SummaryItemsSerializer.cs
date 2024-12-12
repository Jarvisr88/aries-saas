namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class SummaryItemsSerializer
    {
        private const char EscapeCharacter = '\\';
        public const char SummaryItemDelimiter = ';';
        public const char SummaryTypeDelimiter = ':';
        public const char SummaryDisplayFormatDelimiter = ',';
        private static readonly string EscapedEscapeCharacter;
        private static readonly string EscapedSummaryItemDelimeter;
        private ISummaryItemsOwner itemsOwner;

        static SummaryItemsSerializer();
        public SummaryItemsSerializer(ISummaryItemsOwner itemsOwner);
        private string DecodeItem(string toDecode);
        public virtual void Deserialize(string data);
        protected ISummaryItem DeserializeSummaryItem(string data);
        protected virtual void DeserializeSummaryItem(string data, out string fieldName, out SummaryItemType summaryType, out string displayFormat);
        private string EncodeItem(string toEncode);
        protected string GetFieldName(string data);
        protected bool ItemsOwnerNotContainsField(string fieldName);
        public virtual string Serialize();
        protected string[] SplitAndDecodeData(string data);
        private string SplitData(ref string data, char splitter);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static char SummaryItemDelimeter { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static char SummaryTypeDelimeter { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static char SummaryDisplayFormatDelimeter { get; }

        protected ISummaryItemsOwner ItemsOwner { get; }
    }
}

