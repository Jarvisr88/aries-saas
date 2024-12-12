namespace DevExpress.Utils.Localization.Internal
{
    using DevExpress.Utils.Localization;
    using System;

    public abstract class ActiveLocalizerProvider<T> where T: struct
    {
        private readonly XtraLocalizer<T> defaultLocalizer;

        protected ActiveLocalizerProvider(XtraLocalizer<T> defaultLocalizer)
        {
            this.defaultLocalizer = defaultLocalizer;
            this.SetActiveLocalizerCore(defaultLocalizer);
        }

        public XtraLocalizer<T> GetActiveLocalizer()
        {
            XtraLocalizer<T> activeLocalizerCore = this.GetActiveLocalizerCore();
            if (activeLocalizerCore != null)
            {
                return activeLocalizerCore;
            }
            this.SetActiveLocalizerCore(this.DefaultLocalizer);
            return this.DefaultLocalizer;
        }

        protected internal abstract XtraLocalizer<T> GetActiveLocalizerCore();
        public void SetActiveLocalizer(XtraLocalizer<T> localizer)
        {
            this.SetActiveLocalizerCore(localizer);
        }

        protected internal abstract void SetActiveLocalizerCore(XtraLocalizer<T> localizer);

        protected internal XtraLocalizer<T> DefaultLocalizer =>
            this.defaultLocalizer;
    }
}

