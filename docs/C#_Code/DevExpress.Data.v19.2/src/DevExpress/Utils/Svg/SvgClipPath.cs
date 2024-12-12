namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("clipPath")]
    public class SvgClipPath : SvgElement
    {
        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgClipPath>(updateStyle);
    }
}

