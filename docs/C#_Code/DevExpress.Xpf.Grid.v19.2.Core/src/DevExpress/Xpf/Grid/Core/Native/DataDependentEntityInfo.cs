namespace DevExpress.Xpf.Grid.Core.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class DataDependentEntityInfo
    {
        private static readonly DevExpress.Xpf.Data.DataDependentEntity EmptyDataDependentEntity = new DevExpress.Xpf.Data.DataDependentEntity();

        public DataDependentEntityInfo(IEnumerable<string> path) : this(path, EmptyDataDependentEntity, false)
        {
        }

        public DataDependentEntityInfo(IEnumerable<string> path, DevExpress.Xpf.Data.DataDependentEntity dataDependentEntity, bool isFixed)
        {
            Guard.ArgumentNotNull(path, "path");
            Guard.ArgumentNotNull(dataDependentEntity, "dataDependentEntity");
            this.<Path>k__BackingField = path;
            this.<DataDependentEntity>k__BackingField = dataDependentEntity;
            this.<IsFixed>k__BackingField = isFixed;
        }

        public IEnumerable<string> Path { get; }

        public DevExpress.Xpf.Data.DataDependentEntity DataDependentEntity { get; }

        public bool IsFixed { get; }
    }
}

