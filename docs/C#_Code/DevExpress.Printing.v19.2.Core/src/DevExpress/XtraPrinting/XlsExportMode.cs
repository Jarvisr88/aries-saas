﻿namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum XlsExportMode
    {
        SingleFile,
        SingleFilePageByPage,
        DifferentFiles
    }
}

