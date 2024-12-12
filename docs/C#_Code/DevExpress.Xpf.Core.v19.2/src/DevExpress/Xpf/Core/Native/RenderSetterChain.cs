namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderSetterChain
    {
        private readonly string name;
        private readonly string property;
        private readonly Namescope namescope;
        private FrameworkRenderElementContext context;
        private RenderSetterChain.RenderSetterChainEntry head;
        private RenderSetterChain.RenderSetterChainEntry tail;
        private RenderSetterChain.RenderSetterChainEntry activeEntry;
        private int count;

        public RenderSetterChain(string name, string property, Namescope namescope);
        public void Add(RenderSetterContext context);
        private RenderSetterChain.RenderSetterChainEntry GetActiveEntry();
        private FrameworkRenderElementContext GetElement(INamescope namescope);
        public void UpdateValue();

        private class RenderSetterChainEntry
        {
            public readonly RenderSetterContext source;
            public readonly RenderTriggerContextBase owner;
            public int index;
            public RenderSetterChain.RenderSetterChainEntry prev;
            public RenderSetterChain.RenderSetterChainEntry next;

            public RenderSetterChainEntry(RenderSetterContext source);
        }
    }
}

