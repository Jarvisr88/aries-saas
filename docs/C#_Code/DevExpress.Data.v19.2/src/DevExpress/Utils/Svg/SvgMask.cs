namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("mask")]
    public class SvgMask : SvgElement
    {
        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgMask>(updateStyle);
    }
}

