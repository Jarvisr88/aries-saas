namespace DevExpress.Utils.Localization.Internal
{
    using DevExpress.Utils.Localization;
    using System;

    public class DefaultActiveLocalizerProvider<T> : ActiveLocalizerProvider<T> where T: struct
    {
        [ThreadStatic]
        private static XtraLocalizer<T> threadLocalizer;

        public DefaultActiveLocalizerProvider(XtraLocalizer<T> defaultLocalizer) : base(defaultLocalizer)
        {
        }

        protected internal override XtraLocalizer<T> GetActiveLocalizerCore() => 
            DefaultActiveLocalizerProvider<T>.threadLocalizer;

        protected internal override void SetActiveLocalizerCore(XtraLocalizer<T> localizer)
        {
            DefaultActiveLocalizerProvider<T>.threadLocalizer = localizer;
        }
    }
}

