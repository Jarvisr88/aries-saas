namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public static class BrickXamlExporterFactory
    {
        private static readonly Dictionary<string, Type> exportersHashTable = new Dictionary<string, Type>();

        static BrickXamlExporterFactory()
        {
            RegisterDefaultExporters();
        }

        public static BrickXamlExporterBase CreateExporter(BrickBase brick) => 
            CreateExporter(brick.GetType());

        public static BrickXamlExporterBase CreateExporter(Type brickType)
        {
            Type type;
            if (exportersHashTable.TryGetValue(brickType.FullName, out type))
            {
                BrickXamlExporterBase base2 = Activator.CreateInstance(type) as BrickXamlExporterBase;
                if (base2 != null)
                {
                    return base2;
                }
            }
            return ((brickType.GetBaseType() == null) ? null : CreateExporter(brickType.GetBaseType()));
        }

        private static void RegisterDefaultExporters()
        {
            RegisterExporter<TextBrick, TextBrickXamlExporter>();
            RegisterExporter<BrickContainerBase, BrickContainerBaseXamlExporter>();
            RegisterExporter<BrickContainer, BrickContainerXamlExporter>();
            RegisterExporter<CompositeBrick, CompositeBrickXamlExporter>();
            RegisterExporter<PanelBrick, PanelBrickXamlExporter>();
            RegisterExporter<Page, PageXamlExporter>();
            RegisterExporter<LineBrick, LineBrickXamlExporter>();
            RegisterExporter<CheckBoxBrick, CheckBoxBrickXamlExporter>();
            RegisterExporter<PageInfoTextBrick, PageInfoTextBrickXamlExporter>();
            RegisterExporter<ImageBrick, ImageBrickXamlExporter>();
            RegisterExporter<VisualBrick, BrickImageXamlExporter>();
            RegisterExporter("DevExpress.XtraPrinting.RichTextBrickBase", typeof(RichTextBrickXamlExporter));
            RegisterExporter<SeparableBrick, PanelBrickXamlExporter>();
        }

        private static void RegisterExporter<T, V>()
        {
            RegisterExporter(typeof(T), typeof(V));
        }

        public static void RegisterExporter(string brickTypeName, Type exporterType)
        {
            exportersHashTable[brickTypeName] = exporterType;
        }

        public static void RegisterExporter(Type brickType, Type exporterType)
        {
            RegisterExporter(brickType.FullName, exporterType);
        }
    }
}

