namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCompatibilityCommandGroup : PdfCommandGroup
    {
        protected override IEnumerable<object> GetPrefix(PdfResources resources) => 
            new object[] { new PdfToken("BX") };

        protected internal override bool ShouldIgnoreUnknownCommands =>
            true;

        protected override string Suffix =>
            "EX";
    }
}

