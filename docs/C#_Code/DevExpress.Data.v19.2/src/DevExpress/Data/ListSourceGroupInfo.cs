namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ListSourceGroupInfo
    {
        private int level;
        private object groupValue;
        private int childDataRowCount;

        public ListSourceGroupInfo();

        public int Level { get; set; }

        public int ChildDataRowCount { get; set; }

        public object GroupValue { get; set; }

        public object AuxValue { get; set; }

        public virtual List<object> Summary { get; }
    }
}

