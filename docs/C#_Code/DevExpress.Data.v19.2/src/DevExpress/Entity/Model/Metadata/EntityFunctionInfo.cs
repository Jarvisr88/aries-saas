namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Runtime.CompilerServices;

    public class EntityFunctionInfo : IEntityFunctionInfo
    {
        private EdmFunctionInfo functionInfo;

        public EntityFunctionInfo(EdmFunctionInfo functionInfo)
        {
            this.functionInfo = functionInfo;
            this.Name = functionInfo.Name;
            this.Parameters = functionInfo.Parameters;
            this.ResultTypeProperties = functionInfo.ResultTypeProperties;
        }

        public string Name { get; private set; }

        public IFunctionParameterInfo[] Parameters { get; private set; }

        public IEdmComplexTypePropertyInfo[] ResultTypeProperties { get; private set; }
    }
}

