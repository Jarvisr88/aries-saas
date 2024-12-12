namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    public class SvgTransformGroup : SvgGroup
    {
        public static SvgTransformGroup Create(SvgElementProperties properties)
        {
            SvgTransformGroup group = new SvgTransformGroup();
            group.Assign(properties);
            return group;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgTransformGroup>(updateStyle);
    }
}

