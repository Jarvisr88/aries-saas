namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    public class SvgContent : SvgElement
    {
        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgContent>(updateStyle);

        public string Content
        {
            get => 
                this.GetValueCore<string>("Content", false);
            internal set => 
                this.SetValueCore<string>("Content", value);
        }
    }
}

