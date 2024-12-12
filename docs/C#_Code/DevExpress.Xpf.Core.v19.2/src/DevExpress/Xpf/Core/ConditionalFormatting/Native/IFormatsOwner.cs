namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;

    public interface IFormatsOwner
    {
        FormatInfoCollection PredefinedFormats { get; set; }

        FormatInfoCollection PredefinedColorScaleFormats { get; set; }

        FormatInfoCollection PredefinedDataBarFormats { get; set; }

        FormatInfoCollection PredefinedIconSetFormats { get; set; }
    }
}

