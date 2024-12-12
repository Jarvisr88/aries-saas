namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RuntimeStringIdInfo
    {
        public RuntimeStringIdInfo()
        {
        }

        public RuntimeStringIdInfo(GridControlRuntimeStringId id, string value)
        {
            this.Id = id;
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            RuntimeStringIdInfo info = obj as RuntimeStringIdInfo;
            return ((info != null) ? (this.Id == info.Id) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        [Description("")]
        public string Value { get; set; }

        [Description("")]
        public GridControlRuntimeStringId Id { get; set; }
    }
}

