namespace System.ComponentModel
{
    using DevExpress.Utils.Design;
    using System;
    using System.Threading;

    public sealed class DXDisplayNameAttribute : ResDisplayNameAttribute
    {
        public const string DefaultResourceFile = "PropertyNamesRes";
        private static bool useResourceManager;
        private Type resourceFinder;
        private string resourceName;
        private string resourceFile;
        private string defaultDisplayName;
        private string displayName;
        private string cultureName;

        public DXDisplayNameAttribute(Type resourceFinder, string resourceName) : this(resourceFinder, "PropertyNamesRes", resourceName)
        {
        }

        public DXDisplayNameAttribute(Type resourceFinder, string resourceFile, string resourceName) : this(resourceFinder, resourceFile, resourceName, GetDisplayName(resourceName))
        {
        }

        public DXDisplayNameAttribute(Type resourceFinder, string resourceFile, string resourceName, string defaultDisplayName)
        {
            this.resourceFile = resourceFile;
            this.resourceFinder = resourceFinder;
            this.resourceName = resourceName;
            this.defaultDisplayName = defaultDisplayName;
        }

        private void EnsureLocalizedDisplayName()
        {
            string name = Thread.CurrentThread.CurrentUICulture.Name;
            DXDisplayNameAttribute attribute = this;
            lock (attribute)
            {
                if ((this.displayName == null) || (this.cultureName != name))
                {
                    this.cultureName = name;
                    this.displayName = GetResourceString(this.resourceFinder, this.resourceFile, this.resourceName, this.defaultDisplayName);
                }
            }
        }

        public string GetLocalizedDisplayName()
        {
            this.EnsureLocalizedDisplayName();
            return this.displayName;
        }

        public static bool UseResourceManager
        {
            get => 
                useResourceManager;
            set
            {
                if (useResourceManager != value)
                {
                    useResourceManager = value;
                    EnumTypeConverter.Refresh();
                }
            }
        }

        public string ResourceFile =>
            this.resourceFile;

        public string ResourceName =>
            this.resourceName;

        public Type ResourceFinder =>
            this.resourceFinder;

        public override string DisplayName
        {
            get
            {
                if (!UseResourceManager)
                {
                    return this.defaultDisplayName;
                }
                this.EnsureLocalizedDisplayName();
                return this.displayName;
            }
        }
    }
}

