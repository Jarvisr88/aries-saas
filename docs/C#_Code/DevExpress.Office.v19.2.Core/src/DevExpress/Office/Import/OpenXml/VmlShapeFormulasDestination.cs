namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class VmlShapeFormulasDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlSingleFormulasCollection formulas;
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public VmlShapeFormulasDestination(DestinationAndXmlBasedImporter importer, VmlSingleFormulasCollection formulas) : base(importer)
        {
            this.formulas = formulas;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("f", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeFormulasDestination.OnFormula));
            return table;
        }

        private static VmlShapeFormulasDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (VmlShapeFormulasDestination) importer.PeekDestination();

        private static Destination OnFormula(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlSingleFormula item = new VmlSingleFormula();
            GetThis(importer).formulas.Add(item);
            return new VmlShapeFormulaDestination(importer, item);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

