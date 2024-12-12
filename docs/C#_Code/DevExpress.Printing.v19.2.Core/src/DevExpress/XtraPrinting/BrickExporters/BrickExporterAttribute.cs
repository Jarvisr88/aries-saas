namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Native;
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public sealed class BrickExporterAttribute : TypeProviderAttribute
    {
        public BrickExporterAttribute(Type exporterType);

        public Type ExporterType { get; }
    }
}

