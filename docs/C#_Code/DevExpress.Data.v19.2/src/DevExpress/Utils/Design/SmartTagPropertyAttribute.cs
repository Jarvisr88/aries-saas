namespace DevExpress.Utils.Design
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property)]
    public class SmartTagPropertyAttribute : Attribute
    {
        public static readonly int DefaultSortOrder = -1;

        public SmartTagPropertyAttribute() : this(string.Empty, string.Empty)
        {
        }

        public SmartTagPropertyAttribute(string displayName, string category) : this(displayName, category, DefaultSortOrder)
        {
        }

        public SmartTagPropertyAttribute(string displayName, string category, SmartTagActionType finalAction) : this(displayName, category, DefaultSortOrder, finalAction)
        {
        }

        public SmartTagPropertyAttribute(string displayName, string category, int sortOrder) : this(displayName, category, sortOrder, SmartTagActionType.None)
        {
        }

        public SmartTagPropertyAttribute(string displayName, string category, int sortOrder, SmartTagActionType finalAction)
        {
            this.DisplayName = displayName;
            this.Category = category;
            this.SortOrder = sortOrder;
            this.FinalAction = finalAction;
        }

        public string DisplayName { get; set; }

        public string Category { get; set; }

        public int SortOrder { get; set; }

        public SmartTagActionType FinalAction { get; set; }
    }
}

