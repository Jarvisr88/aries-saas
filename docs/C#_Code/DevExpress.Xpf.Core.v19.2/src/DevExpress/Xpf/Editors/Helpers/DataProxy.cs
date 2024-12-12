namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class DataProxy
    {
        public int f_visibleIndex;
        public object f_component;
        public object f_RowKey;

        protected DataProxy()
        {
        }

        public override string ToString() => 
            (this.f_component != null) ? this.f_component.ToString() : ((this.f_RowKey == null) ? string.Empty : this.f_RowKey.ToString());

        private object Tag { get; set; }
    }
}

