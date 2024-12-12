namespace DevExpress.Data.Native
{
    using System;
    using System.ComponentModel.Design;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class VS2005ConnectionStringHelper : IDisposable
    {
        private const string dataDirectoryTag = "|DataDirectory|";
        private string connectionString;
        private object dataAdapter;
        private IDesignerHost designerHost;

        public VS2005ConnectionStringHelper();
        public VS2005ConnectionStringHelper(IDesignerHost designerHost);
        private static PropertyInfo FindConnection(object obj, out object connection);
        private static string GetDataConnectionString(object dataAdapter);
        public string GetPatchedConnectionString(IDesignerHost designerHost, string connectionString);
        private static object GetPropertyValue(Type type, object obj, string propertyName);
        private static Type GetType(string assemblyName, string typeName);
        private bool IsWebApplication(object containingProject);
        private bool IsWebProject(object containingProject);
        public void PatchConnectionString(object dataAdapter);
        private static void SetDataConnectionString(object obj, string connectionString);
        private bool ShouldPatchConnectionString(object dataAdapter);
        void IDisposable.Dispose();
    }
}

