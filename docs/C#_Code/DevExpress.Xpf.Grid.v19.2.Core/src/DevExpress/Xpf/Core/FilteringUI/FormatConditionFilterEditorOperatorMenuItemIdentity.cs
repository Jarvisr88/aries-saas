namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class FormatConditionFilterEditorOperatorMenuItemIdentity : IFilterEditorOperatorMenuItemIdentity
    {
        public FormatConditionFilterEditorOperatorMenuItemIdentity(FormatConditionFilter filter)
        {
            this.<Source>k__BackingField = filter.Source as FormatConditionBase;
            this.<Name>k__BackingField = filter.DisplayExpression;
            this.<Info>k__BackingField = filter.Info;
            this.<ApplyToRow>k__BackingField = filter.ApplyToRow;
        }

        private bool Equals(FormatConditionFilterEditorOperatorMenuItemIdentity other) => 
            string.Equals(this.Name, other.Name) && (Equals(this.Info, other.Info) && (this.ApplyToRow == other.ApplyToRow));

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? ((obj is FormatConditionFilterEditorOperatorMenuItemIdentity) && this.Equals((FormatConditionFilterEditorOperatorMenuItemIdentity) obj)) : true) : false;

        public override int GetHashCode() => 
            (((((-1533203491 * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Name)) * -1521134295) + EqualityComparer<FormatConditionFilterInfo>.Default.GetHashCode(this.Info)) * -1521134295) + this.ApplyToRow.GetHashCode();

        internal FormatConditionBase Source { get; }

        public string Name { get; }

        public FormatConditionFilterInfo Info { get; }

        public bool ApplyToRow { get; }
    }
}

