namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class CacheInvalidator : FrameworkElement, IConditionalFormattingClient<CacheInvalidator>, IConditionalFormattingClientBase
    {
        private ConditionalFormattingHelper<CacheInvalidator> helper;
        private DevExpress.Xpf.Core.Locker locker;

        public CacheInvalidator()
        {
            this.helper = new ConditionalFormattingHelper<CacheInvalidator>(this, null);
            this.locker = new DevExpress.Xpf.Core.Locker();
        }

        public IList<FormatConditionBaseInfo> GetRelatedConditions() => 
            this.RelatedConditions;

        public FormatValueProvider? GetValueProvider(string fieldName) => 
            new FormatValueProvider?(this.FormatInfoProvider.GetValueProvider(fieldName));

        public void UpdateBackground()
        {
            this.helper.CoerceBackground(new SolidColorBrush());
        }

        public void UpdateCustomAppearance(CustomAppearanceEventArgs args)
        {
        }

        public void UpdateDataBarFormatInfo(DataBarFormatInfo info)
        {
        }

        public IList<FormatConditionBaseInfo> RelatedConditions { get; set; }

        public IFormatInfoProvider FormatInfoProvider { get; set; }

        public ConditionalFormattingHelper<CacheInvalidator> FormattingHelper =>
            this.helper;

        public bool HasCustomAppearance =>
            false;

        public bool IsReady =>
            true;

        public bool IsSelected =>
            false;

        public DevExpress.Xpf.Core.Locker Locker =>
            this.locker;
    }
}

