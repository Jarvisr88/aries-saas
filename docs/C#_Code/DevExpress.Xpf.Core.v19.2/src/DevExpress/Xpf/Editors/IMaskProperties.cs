namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;

    public interface IMaskProperties
    {
        bool MaskAllowNullInput { get; }

        bool MaskSaveLiteral { get; }

        bool MaskShowPlaceHolders { get; }

        char MaskPlaceHolder { get; }

        string Mask { get; }

        DevExpress.Xpf.Editors.MaskType MaskType { get; }

        bool MaskIgnoreBlank { get; }

        bool MaskUseAsDisplayFormat { get; }

        bool MaskBeepOnError { get; }

        AutoCompleteType MaskAutoComplete { get; }

        CultureInfo MaskCulture { get; }

        DevExpress.Xpf.Editors.MaskType[] SupportedMaskTypes { get; }
    }
}

