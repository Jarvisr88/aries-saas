namespace DevExpress.Xpf.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class SummaryDefinition
    {
        internal static readonly SummaryDefinition Count = new SummaryDefinition(null, DevExpress.Xpf.Data.SummaryType.Count);

        public SummaryDefinition(string propertyName, DevExpress.Xpf.Data.SummaryType summaryType)
        {
            if (summaryType != DevExpress.Xpf.Data.SummaryType.Count)
            {
                this.PropertyName = propertyName;
            }
            this.SummaryType = summaryType;
        }

        public override bool Equals(object obj)
        {
            SummaryDefinition definition = obj as SummaryDefinition;
            return ((definition != null) && ((this.PropertyName == definition.PropertyName) && (this.SummaryType == definition.SummaryType)));
        }

        public override int GetHashCode() => 
            (((-975136626 * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.PropertyName)) * -1521134295) + this.SummaryType.GetHashCode();

        public string PropertyName { get; private set; }

        public DevExpress.Xpf.Data.SummaryType SummaryType { get; private set; }
    }
}

