namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class SuperTip : FrameworkContentElement
    {
        private SuperTipItemsCollection items;

        protected internal void AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        protected virtual SuperTipItemsCollection CreateItems() => 
            new SuperTipItemsCollection(this);

        protected internal void RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        [Description("Gets the collection of tooltip items displayed by the current SuperTip object.")]
        public SuperTipItemsCollection Items
        {
            get
            {
                this.items ??= this.CreateItems();
                return this.items;
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                IEnumerator logicalChildren = base.LogicalChildren;
                if (logicalChildren != null)
                {
                    logicalChildren.Reset();
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                foreach (SuperTipItemBase base2 in this.Items)
                {
                    list.Add(base2);
                }
                return list.GetEnumerator();
            }
        }
    }
}

