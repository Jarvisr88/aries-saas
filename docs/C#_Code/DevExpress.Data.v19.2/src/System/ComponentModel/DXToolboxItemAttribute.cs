namespace System.ComponentModel
{
    using DevExpress.Utils.About;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false), ClassInterface(ClassInterfaceType.None)]
    public class DXToolboxItemAttribute : ToolboxItemAttribute
    {
        public DXToolboxItemAttribute(bool defaultType) : this(defaultType ? DXToolboxItemKind.Regular : DXToolboxItemKind.Hidden)
        {
        }

        public DXToolboxItemAttribute(DXToolboxItemKind kind) : base(kind != DXToolboxItemKind.Hidden)
        {
            if ((kind == DXToolboxItemKind.Regular) && (this.Check() == 1))
            {
                this.Disable();
            }
        }

        protected DXToolboxItemAttribute(DXToolboxItemKind kind, string toolboxTypeName) : base(toolboxTypeName)
        {
            if ((kind == DXToolboxItemKind.Regular) && (this.Check() == 1))
            {
                this.Disable();
            }
        }

        protected virtual int Check() => 
            Utility.IsOnlyWin();

        private void Disable()
        {
            try
            {
                typeof(ToolboxItemAttribute).GetField("toolboxItemTypeName", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, null);
            }
            catch
            {
            }
        }

        public override object TypeId =>
            ToolboxItemAttribute.Default.TypeId;
    }
}

