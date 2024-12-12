namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    internal abstract class ConditionalClientAppearanceUpdaterBase
    {
        private VersionedFormatInfoProvider animationTemporaryFormatInfoProvider;

        protected ConditionalClientAppearanceUpdaterBase()
        {
        }

        protected virtual bool CanAccessAnimationProvider() => 
            true;

        private DataUpdate GetAnimationDataUpdate()
        {
            VersionedFormatInfoProvider provider = this.animationTemporaryFormatInfoProvider ?? this.GetProvider();
            if (provider == null)
            {
                return null;
            }
            FormatInfoSnapshot previousVersion = provider.GetPreviousVersion();
            SnapshotValidationService service = new SnapshotValidationService();
            service.RegisterClient(previousVersion);
            DataUpdate update1 = new DataUpdate(previousVersion, provider.GetCurrentVersion());
            update1.ValidationService = service;
            return update1;
        }

        private IList<IList<AnimationTimeline>> GetAnimations()
        {
            DataViewBase view = this.GetView();
            return (((view == null) || (!view.ViewBehavior.HasFormatConditions || view.ScrollAnimationLocker.IsLocked)) ? null : (this.CanAccessAnimationProvider() ? view.DataUpdateAnimationProvider.GetAnimations(new Func<DataUpdate>(this.GetAnimationDataUpdate), this.GetConditionalFormattingClient()) : null));
        }

        protected abstract IConditionalFormattingClientBase GetConditionalFormattingClient();
        protected abstract VersionedFormatInfoProvider GetProvider();
        protected abstract DataViewBase GetView();
        private void InvalidateFormatCache()
        {
            VersionedFormatInfoProvider provider = this.GetProvider();
            if (provider != null)
            {
                IConditionalFormattingClientBase conditionalFormattingClient = this.GetConditionalFormattingClient();
                if (conditionalFormattingClient != null)
                {
                    CacheInvalidator invalidator1 = new CacheInvalidator();
                    invalidator1.RelatedConditions = conditionalFormattingClient.GetRelatedConditions();
                    CacheInvalidator invalidator = invalidator1;
                    this.animationTemporaryFormatInfoProvider = new VersionedFormatInfoProvider(provider.GetCurrentVersion());
                    invalidator.FormatInfoProvider = this.animationTemporaryFormatInfoProvider;
                    invalidator.FormattingHelper.UpdateConditionalAppearance();
                }
            }
        }

        public void OnDataChanged()
        {
            IList<IList<AnimationTimeline>> animations = this.GetAnimations();
            if (animations == null)
            {
                this.UpdateClientAppearance();
            }
            else
            {
                this.InvalidateFormatCache();
                this.StartAnimation(animations);
            }
        }

        public void ResetAnimationFormatInfoProvider()
        {
            this.animationTemporaryFormatInfoProvider = null;
        }

        protected abstract void StartAnimation(IList<IList<AnimationTimeline>> animations);
        protected abstract void UpdateClientAppearance();
    }
}

