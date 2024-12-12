namespace DevExpress.XtraPrinting.BrickExporters
{
    using System;
    using System.Collections.Generic;

    public class ExportersFactory
    {
        private Dictionary<Type, BrickBaseExporter> brickExporters;

        public ExportersFactory();
        public void AssignExporter(Type brickType, BrickBaseExporter exporter);
        public static BrickBaseExporter CreateExporter(object brick);
        private static BrickBaseExporter CreateExporter(Type brickType);
        public BrickBaseExporter GetExporter(object brick);
        public static Type GetExporterType(Type brickType);
        public static void SetExporter(Type brickType, Type exporterType);
    }
}

