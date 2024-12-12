namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("style")]
    public class SvgStyleItem : SvgElement
    {
        public SvgStyleItem()
        {
            this.Type = "";
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgStyleItem>(updateStyle);

        [SvgPropertyNameAlias("type"), DefaultValue("")]
        public string Type
        {
            get => 
                this.GetValueCore<string>("Type", false);
            internal set => 
                this.SetValueCore<string>("Type", value);
        }
    }
}

