namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;

    public interface ITableCell
    {
        bool ShouldApplyPadding { get; }

        string FormatString { get; }

        string XlsxFormatString { get; }

        object TextValue { get; }

        BrickModifier Modifier { get; }

        DefaultBoolean XlsExportNativeFormat { get; }

        bool HasCrossReference { get; }

        string Url { get; }
    }
}

