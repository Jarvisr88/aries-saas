namespace DevExpress.Utils.Localization
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Resources;
    using System.Xml;

    public abstract class XtraResXLocalizer<T> : XtraLocalizer<T> where T: struct
    {
        private readonly XtraLocalizer<T> embeddedLocalizer;
        private ResourceManager manager;

        protected XtraResXLocalizer(XtraLocalizer<T> embeddedLocalizer)
        {
            Guard.ArgumentNotNull(embeddedLocalizer, "embeddedLocalizer");
            this.embeddedLocalizer = embeddedLocalizer;
            this.CreateResourceManager();
        }

        protected internal virtual void CreateResourceManager()
        {
            if (this.manager != null)
            {
                this.manager.ReleaseAllResources();
            }
            this.manager = this.CreateResourceManagerCore();
        }

        protected abstract ResourceManager CreateResourceManagerCore();
        public override XtraLocalizer<T> CreateResXLocalizer() => 
            this.embeddedLocalizer.CreateResXLocalizer();

        public override XmlDocument CreateXmlDocument() => 
            this.embeddedLocalizer.CreateXmlDocument();

        public string GetInvariantString(T id) => 
            this.EmbeddedLocalizer.GetLocalizedString(id);

        public override string GetLocalizedString(T id)
        {
            string localizedString = base.GetLocalizedString(id);
            if (string.IsNullOrEmpty(localizedString))
            {
                lock (localizer)
                {
                    localizedString = base.GetLocalizedString(id);
                    if (string.IsNullOrEmpty(localizedString))
                    {
                        localizedString = this.GetLocalizedStringCore(id);
                        this.AddString(id, localizedString);
                    }
                }
            }
            return localizedString;
        }

        protected internal virtual string GetLocalizedStringCore(T id)
        {
            string localizedStringFromResources = this.GetLocalizedStringFromResources(id);
            return this.embeddedLocalizer.GetLocalizedString(id);
        }

        protected string GetLocalizedStringFromResources(T id)
        {
            string name = $"{this.GetEnumTypeName()}.{id.ToString()}";
            return this.Manager.GetString(name);
        }

        protected override void PopulateStringTable()
        {
        }

        protected internal virtual ResourceManager Manager =>
            this.manager;

        public override string Language =>
            CultureInfo.CurrentUICulture.Name;

        internal XtraLocalizer<T> EmbeddedLocalizer =>
            this.embeddedLocalizer;
    }
}

