namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceSummaryItemKey
    {
        private readonly string fieldName;
        private readonly DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType? customServiceSummaryItemType;
        private readonly DevExpress.Data.SummaryItemType summaryItemType;
        public string FieldName =>
            this.fieldName;
        public DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType? CustomServiceSummaryItemType =>
            this.customServiceSummaryItemType;
        public DevExpress.Data.SummaryItemType SummaryItemType =>
            this.summaryItemType;
        public ServiceSummaryItemKey(string fieldName, DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType? customServiceSummaryItemType, DevExpress.Data.SummaryItemType summaryItemType)
        {
            this.fieldName = fieldName;
            this.customServiceSummaryItemType = customServiceSummaryItemType;
            this.summaryItemType = summaryItemType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ServiceSummaryItemKey))
            {
                return false;
            }
            ServiceSummaryItemKey key = (ServiceSummaryItemKey) obj;
            return ((this.fieldName == key.fieldName) && (EqualityComparer<DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType?>.Default.Equals(this.customServiceSummaryItemType, key.customServiceSummaryItemType) && (this.summaryItemType == key.summaryItemType)));
        }

        public override int GetHashCode() => 
            (((((((0x2c49c29c * -1521134295) + this.GetHashCode()) * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.fieldName)) * -1521134295) + EqualityComparer<DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType?>.Default.GetHashCode(this.customServiceSummaryItemType)) * -1521134295) + this.summaryItemType.GetHashCode();
    }
}

