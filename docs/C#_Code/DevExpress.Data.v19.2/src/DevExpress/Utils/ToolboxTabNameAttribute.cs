namespace DevExpress.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, Inherited=false, AllowMultiple=false)]
    public class ToolboxTabNameAttribute : Attribute
    {
        private string tabName;

        public ToolboxTabNameAttribute(string tabName)
        {
            this.tabName = tabName;
        }

        public string TabName =>
            this.tabName;
    }
}

