namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HyperlinkPropertyAccessor
    {
        private readonly IHyperlinkPropertyOwner owner;
        private const string Delimiter = ";";
        private string[] navigatePaths;
        private string[] displayPaths;
        private readonly DataAccessor accessor = new DataAccessor();

        public HyperlinkPropertyAccessor(IHyperlinkPropertyOwner owner)
        {
            this.owner = owner;
            this.Initialize();
            this.InitializeDataAccessor();
        }

        private string[] CalcPaths(string member)
        {
            if (string.IsNullOrEmpty(member))
            {
                return new string[0];
            }
            string[] separator = new string[] { ";" };
            return member.Split(separator, StringSplitOptions.None).ToArray<string>();
        }

        private IEnumerable<string> GetDisplayProperties()
        {
            if (this.displayPaths.Any<string>())
            {
                return this.displayPaths;
            }
            return new string[] { "ValueColumn" };
        }

        public object[] GetDisplayValues(DataProxy proxy) => 
            (from x in this.GetDisplayProperties() select this.GetValue(proxy, x)).ToArray<object>();

        private IEnumerable<string> GetNavigateProperties()
        {
            if (this.navigatePaths.Any<string>())
            {
                return this.navigatePaths;
            }
            return new string[] { "ValueColumn" };
        }

        public object[] GetNavigateValues(DataProxy proxy) => 
            (from x in this.GetNavigateProperties() select this.GetValue(proxy, x)).ToArray<object>();

        public DataProxy GetProxy(object value) => 
            this.accessor.CreateProxy(value, -1);

        public object GetValue(DataProxy proxy, string name) => 
            (name != null) ? this.accessor.GetPropertyValue(proxy, name) : null;

        private void Initialize()
        {
            this.displayPaths = this.CalcPaths(this.owner.DisplayMember);
            this.navigatePaths = this.CalcPaths(this.owner.NavigationUrlMember);
        }

        private void InitializeDataAccessor()
        {
            this.accessor.BeginInit();
            this.accessor.GenerateDefaultDescriptors("", "", null);
            this.navigatePaths.ForEach<string>(x => this.accessor.Fetch(x));
            this.displayPaths.ForEach<string>(x => this.accessor.Fetch(x));
            this.accessor.EndInit();
        }
    }
}

