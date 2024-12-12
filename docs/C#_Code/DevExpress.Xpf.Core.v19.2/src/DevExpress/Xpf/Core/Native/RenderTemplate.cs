namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;

    [ContentProperty("RenderTree")]
    public class RenderTemplate : FreezableRenderObject
    {
        public static readonly RenderTemplate Default;
        private FrameworkRenderElement renderTree;
        private RenderTriggersCollection triggers;
        private RenderStylesCollection styles;
        private RenderTriggersCollection cachedTriggers;
        private RenderTemplate basedOn;

        static RenderTemplate();
        public RenderTemplate();
        public FrameworkRenderElementContext CreateContext(Namescope namescopeHolder, object dataContext);
        protected RenderTriggersCollection DistinctTriggers(IEnumerable<RenderTriggerBase> triggers);
        protected override void FreezeOverride();
        protected RenderTriggersCollection GetCachedTriggers();
        protected virtual FrameworkRenderElement GetRenderTree();
        protected IEnumerable<RenderTriggerBase> GetTriggers();
        public void InitializeTemplate(FrameworkRenderElementContext context, Namescope namescope);
        public void ReleaseTemplate(FrameworkRenderElementContext context, Namescope namescope);

        public FrameworkRenderElement RenderTree { get; set; }

        public RenderTriggersCollection Triggers { get; set; }

        public RenderStylesCollection Styles { get; set; }

        public RenderTemplate BasedOn { get; set; }
    }
}

