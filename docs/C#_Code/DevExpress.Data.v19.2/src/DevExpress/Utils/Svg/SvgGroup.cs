namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("g")]
    public class SvgGroup : SvgElement
    {
        public static SvgGroup Create(SvgElementProperties properties)
        {
            SvgGroup group = new SvgGroup();
            group.Assign(properties);
            return group;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgGroup>(updateStyle);
    }
}

