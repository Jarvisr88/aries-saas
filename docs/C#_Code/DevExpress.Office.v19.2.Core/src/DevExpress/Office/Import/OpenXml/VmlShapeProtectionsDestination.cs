namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class VmlShapeProtectionsDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlShapeProtections shapeProtections;

        public VmlShapeProtectionsDestination(DestinationAndXmlBasedImporter importer, VmlShapeProtections shapeProtections) : base(importer)
        {
            this.shapeProtections = shapeProtections;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.shapeProtections.Ext = this.Importer.GetWpEnumValue<VmlExtensionHandlingBehavior>(reader, "ext", OpenXmlExporterBase.VmlExtensionHandlingBehaviorTable, VmlExtensionHandlingBehavior.View, "urn:schemas-microsoft-com:vml");
            bool? onOffNullValue = this.Importer.GetOnOffNullValue(reader, "adjusthandles");
            if (onOffNullValue != null)
            {
                this.shapeProtections.AdjustHandles = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "aspectratio");
            if (onOffNullValue != null)
            {
                this.shapeProtections.AspectRatio = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "cropping");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Cropping = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "grouping");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Grouping = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "position");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Position = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "rotation");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Rotation = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "selection");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Selection = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "shapetype");
            if (onOffNullValue != null)
            {
                this.shapeProtections.ShapeType = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "text");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Text = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "ungrouping");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Ungrouping = new bool?(onOffNullValue.Value);
            }
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "verticies");
            if (onOffNullValue != null)
            {
                this.shapeProtections.Vertices = new bool?(onOffNullValue.Value);
            }
        }
    }
}

