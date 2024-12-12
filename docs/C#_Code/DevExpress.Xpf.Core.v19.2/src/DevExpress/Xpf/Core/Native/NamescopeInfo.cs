namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class NamescopeInfo
    {
        public NamescopeInfo(INamescope namescope, string name);

        public INamescope Namescope { get; private set; }

        public string Name { get; private set; }
    }
}

