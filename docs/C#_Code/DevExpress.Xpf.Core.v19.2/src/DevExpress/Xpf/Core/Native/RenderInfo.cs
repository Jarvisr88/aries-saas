namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("Elements")]
    public class RenderInfo
    {
        public RenderInfo();
        public virtual bool Contains(string stateName);

        public Dictionary<string, IRenderInfo> Elements { get; set; }

        public IRenderInfo this[string state] { get; }
    }
}

