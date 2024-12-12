namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.About;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=false), NonCategorized]
    public class DXToolboxBrowsableAttribute : Attribute
    {
        public DXToolboxBrowsableAttribute() : this(true)
        {
        }

        public DXToolboxBrowsableAttribute(bool browsable) : this(browsable ? DXToolboxItemKind.Regular : DXToolboxItemKind.Hidden)
        {
        }

        public DXToolboxBrowsableAttribute(DXToolboxItemKind kind)
        {
            if ((kind == DXToolboxItemKind.Regular) && (this.Check() == 1))
            {
                this.Browsable = false;
            }
            else
            {
                this.Browsable = kind != DXToolboxItemKind.Hidden;
            }
        }

        protected virtual int Check() => 
            Utility.IsOnlyWpf();

        public bool Browsable { get; private set; }
    }
}

