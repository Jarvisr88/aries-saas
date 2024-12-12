namespace DevExpress.Office.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public delegate List<IExporter<TFormat, TResult>> ExportersCalculator<TFormat, TResult>(IExportManagerService<TFormat, TResult> exportManagerService);
}

