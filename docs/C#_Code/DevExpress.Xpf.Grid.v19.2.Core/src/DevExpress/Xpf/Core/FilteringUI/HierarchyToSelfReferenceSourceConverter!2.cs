namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class HierarchyToSelfReferenceSourceConverter<HR, SR>
    {
        private const int RootID = -1;
        private readonly Func<HR, int, int, SR> createSelfReferenceItem;
        private readonly Func<HR, IList<HR>> getChildren;
        private int currentID;

        internal HierarchyToSelfReferenceSourceConverter(Func<HR, int, int, SR> createSelfReferenceItem, Func<HR, IList<HR>> getChildren)
        {
            Guard.ArgumentNotNull(createSelfReferenceItem, "createSelfReferenceItem");
            Guard.ArgumentNotNull(getChildren, "getChildren");
            this.createSelfReferenceItem = createSelfReferenceItem;
            this.getChildren = getChildren;
        }

        public SR[] Convert(IEnumerable<HR> hierarchicalSouce)
        {
            this.currentID = -1;
            return this.ConvertCore(hierarchicalSouce).ToArray<SR>();
        }

        private IList<SR> ConvertCore(IEnumerable<HR> hierarchicalSouce)
        {
            if (hierarchicalSouce == null)
            {
                return new SR[0];
            }
            int currentID = this.currentID;
            List<SR> list = new List<SR>();
            foreach (HR local in hierarchicalSouce)
            {
                this.currentID++;
                list.Add(this.createSelfReferenceItem(local, this.currentID, currentID));
                list.AddRange(this.ConvertCore(this.getChildren(local)));
            }
            return list;
        }
    }
}

