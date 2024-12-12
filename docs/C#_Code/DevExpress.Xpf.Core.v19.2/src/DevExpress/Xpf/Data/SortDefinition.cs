namespace DevExpress.Xpf.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public sealed class SortDefinition
    {
        public SortDefinition(string propertyName, ListSortDirection direction)
        {
            this.PropertyName = propertyName;
            this.Direction = direction;
        }

        public override bool Equals(object obj)
        {
            SortDefinition definition = obj as SortDefinition;
            return ((definition != null) && ((this.PropertyName == definition.PropertyName) && (this.Direction == definition.Direction)));
        }

        public override int GetHashCode() => 
            (((0x7f13e82d * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.PropertyName)) * -1521134295) + this.Direction.GetHashCode();

        public string PropertyName { get; private set; }

        public ListSortDirection Direction { get; private set; }
    }
}

