namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("clipPath")]
    public class SvgClippingDefinition : SvgDefinition
    {
        public override bool IgnoreChildren { get; }
    }
}

