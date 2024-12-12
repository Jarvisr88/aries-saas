namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Drawing.Design;
    using System.Reflection;

    [Serializable]
    public abstract class DataSetToolboxItem : ToolboxItem
    {
        protected DataSetToolboxItem()
        {
        }

        protected DataSetToolboxItem(Type type) : base(type)
        {
        }

        protected override IComponent[] CreateComponentsCore(IDesignerHost host)
        {
            object method;
            Type type = Assembly.LoadWithPartialName("Devart.Data.Design")?.GetType("Devart.Common.Design.DataSetToolboxItemDialog");
            if (type == null)
            {
                method = null;
            }
            else
            {
                Type[] types = new Type[] { typeof(IDesignerHost), typeof(string), typeof(string), typeof(string), typeof(DataSet) };
                method = type.GetMethod("CreateComponent", types);
            }
            MethodInfo info = (MethodInfo) method;
            if (info == null)
            {
                return new IComponent[] { this.CreateDataSet() };
            }
            object[] parameters = new object[] { host, this.ProviderPrefix, this.ProviderName, this.ProviderRegKey, this.CreateDataSet() };
            return (IComponent[]) info.Invoke(null, parameters);
        }

        protected abstract DataSet CreateDataSet();

        protected abstract string ProviderPrefix { get; }

        protected abstract string ProviderName { get; }

        protected abstract string ProviderRegKey { get; }
    }
}

