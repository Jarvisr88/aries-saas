namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;

    public interface IFrameworkRenderElement
    {
        bool ApplySetter(RenderStyleSetter setter);

        string Name { get; set; }

        IEnumerable<IFrameworkRenderElement> Children { get; }
    }
}

