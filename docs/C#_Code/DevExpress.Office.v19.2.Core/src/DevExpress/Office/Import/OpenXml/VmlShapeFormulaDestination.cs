namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class VmlShapeFormulaDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlSingleFormula formula;

        public VmlShapeFormulaDestination(DestinationAndXmlBasedImporter importer, VmlSingleFormula formula) : base(importer)
        {
            this.formula = formula;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.formula.Equation = this.Importer.ReadAttribute(reader, "eqn");
        }
    }
}

