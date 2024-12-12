namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemInfoBase
    {
        public ItemInfoBase(ItemInfoBase other);
        public ItemInfoBase(string category);
        public override string ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}

