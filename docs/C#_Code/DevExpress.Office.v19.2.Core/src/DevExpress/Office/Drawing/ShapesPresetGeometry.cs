namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public static class ShapesPresetGeometry
    {
        private static readonly Dictionary<ShapePreset, ModelShapeCustomGeometry> calculatedGeometries = new Dictionary<ShapePreset, ModelShapeCustomGeometry>();
        private static readonly Dictionary<ShapePreset, Func<ModelShapeCustomGeometry>> geometrysGenerators = InitializeGenerators();

        private static ModelShapeCustomGeometry GenerateAccentBorderCallout1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -38333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateAccentBorderCallout2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -46667"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateAccentBorderCallout3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 100000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj7", "val 112963"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj8", "val -8333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj7 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w adj8 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj8", "-2147483647", "2147483647", "adj7", "-2147483647", "2147483647", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateAccentCallout1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -38333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateAccentCallout2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -46667"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateAccentCallout3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 100000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj7", "val 112963"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj8", "val -8333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj7 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w adj8 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj8", "-2147483647", "2147483647", "adj7", "-2147483647", "2147483647", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathLine("x1", "b"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonBackPrevious()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g11", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonBeginning()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "+- g11 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "+- g11 g15 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g17", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g16", "g9"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathLine("g16", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g17", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g16", "g9"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathLine("g16", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g17", "vc"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g16", "g9"));
            item.Instructions.Add(new PathLine("g16", "g10"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonBlank()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonDocument()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss 9 32"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 16"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "+- g12 0 g13"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "+- g9 g13 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g11", "g9"));
            item.Instructions.Add(new PathLine("g14", "g9"));
            item.Instructions.Add(new PathLine("g12", "g15"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g9"));
            item.Instructions.Add(new PathLine("g14", "g9"));
            item.Instructions.Add(new PathLine("g14", "g15"));
            item.Instructions.Add(new PathLine("g12", "g15"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g14", "g9"));
            item.Instructions.Add(new PathLine("g14", "g15"));
            item.Instructions.Add(new PathLine("g12", "g15"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g9"));
            item.Instructions.Add(new PathLine("g14", "g9"));
            item.Instructions.Add(new PathLine("g12", "g15"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g12", "g15"));
            item.Instructions.Add(new PathLine("g14", "g15"));
            item.Instructions.Add(new PathLine("g14", "g9"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonEnd()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 7 8"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "+- g11 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "+- g11 g15 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g16", "vc"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g17", "g9"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g17", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g16", "vc"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g17", "g9"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g17", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g16", "vc"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g17", "g9"));
            item.Instructions.Add(new PathLine("g12", "g9"));
            item.Instructions.Add(new PathLine("g12", "g10"));
            item.Instructions.Add(new PathLine("g17", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonForwardNext()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g12", "vc"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g12", "vc"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g12", "vc"));
            item.Instructions.Add(new PathLine("g11", "g10"));
            item.Instructions.Add(new PathLine("g11", "g9"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonHelp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1 7"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 3 14"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "*/ g13 2 7"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g13 3 7"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "*/ g13 4 7"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "*/ g13 17 28"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "*/ g13 21 28"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "*/ g13 11 14"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "+- g9 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "+- g9 g21 0"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "+- g9 g23 0"));
            geometry.Guides.Add(new ModelShapeGuide("g31", "+- g9 g24 0"));
            geometry.Guides.Add(new ModelShapeGuide("g33", "+- g11 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g36", "+- g11 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g37", "+- g11 g20 0"));
            geometry.Guides.Add(new ModelShapeGuide("g41", "*/ g13 1 14"));
            geometry.Guides.Add(new ModelShapeGuide("g42", "*/ g13 3 28"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g33", "g27"));
            item.Instructions.Add(new PathArc("g16", "g16", "cd2", "cd2"));
            item.Instructions.Add(new PathArc("g14", "g15", "0", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g36", "g30"));
            item.Instructions.Add(new PathLine("g36", "g29"));
            item.Instructions.Add(new PathArc("g14", "g15", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("g14", "g14", "0", "-10800000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g31"));
            item.Instructions.Add(new PathArc("g42", "g42", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g33", "g27"));
            item.Instructions.Add(new PathArc("g16", "g16", "cd2", "cd2"));
            item.Instructions.Add(new PathArc("g14", "g15", "0", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g36", "g30"));
            item.Instructions.Add(new PathLine("g36", "g29"));
            item.Instructions.Add(new PathArc("g14", "g15", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("g14", "g14", "0", "-10800000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g31"));
            item.Instructions.Add(new PathArc("g42", "g42", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g33", "g27"));
            item.Instructions.Add(new PathArc("g16", "g16", "cd2", "cd2"));
            item.Instructions.Add(new PathArc("g14", "g15", "0", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g36", "g30"));
            item.Instructions.Add(new PathLine("g36", "g29"));
            item.Instructions.Add(new PathArc("g14", "g15", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("g41", "g42", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("g14", "g14", "0", "-10800000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g31"));
            item.Instructions.Add(new PathArc("g42", "g42", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonHome()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1 16"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "*/ g13 3 16"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "*/ g13 5 16"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ g13 7 16"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g13 9 16"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "*/ g13 11 16"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "*/ g13 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "*/ g13 13 16"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "*/ g13 7 8"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "+- g9 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "+- g9 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g26", "+- g9 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "+- g9 g21 0"));
            geometry.Guides.Add(new ModelShapeGuide("g28", "+- g11 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "+- g11 g18 0"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "+- g11 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g31", "+- g11 g20 0"));
            geometry.Guides.Add(new ModelShapeGuide("g32", "+- g11 g22 0"));
            geometry.Guides.Add(new ModelShapeGuide("g33", "+- g11 g23 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathLine("g11", "vc"));
            item.Instructions.Add(new PathLine("g28", "vc"));
            item.Instructions.Add(new PathLine("g28", "g10"));
            item.Instructions.Add(new PathLine("g33", "g10"));
            item.Instructions.Add(new PathLine("g33", "vc"));
            item.Instructions.Add(new PathLine("g12", "vc"));
            item.Instructions.Add(new PathLine("g32", "g26"));
            item.Instructions.Add(new PathLine("g32", "g24"));
            item.Instructions.Add(new PathLine("g31", "g24"));
            item.Instructions.Add(new PathLine("g31", "g25"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g32", "g26"));
            item.Instructions.Add(new PathLine("g32", "g24"));
            item.Instructions.Add(new PathLine("g31", "g24"));
            item.Instructions.Add(new PathLine("g31", "g25"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g28", "vc"));
            item.Instructions.Add(new PathLine("g28", "g10"));
            item.Instructions.Add(new PathLine("g29", "g10"));
            item.Instructions.Add(new PathLine("g29", "g27"));
            item.Instructions.Add(new PathLine("g30", "g27"));
            item.Instructions.Add(new PathLine("g30", "g10"));
            item.Instructions.Add(new PathLine("g33", "g10"));
            item.Instructions.Add(new PathLine("g33", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathLine("g11", "vc"));
            item.Instructions.Add(new PathLine("g12", "vc"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g29", "g27"));
            item.Instructions.Add(new PathLine("g30", "g27"));
            item.Instructions.Add(new PathLine("g30", "g10"));
            item.Instructions.Add(new PathLine("g29", "g10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathLine("g31", "g25"));
            item.Instructions.Add(new PathLine("g31", "g24"));
            item.Instructions.Add(new PathLine("g32", "g24"));
            item.Instructions.Add(new PathLine("g32", "g26"));
            item.Instructions.Add(new PathLine("g12", "vc"));
            item.Instructions.Add(new PathLine("g33", "vc"));
            item.Instructions.Add(new PathLine("g33", "g10"));
            item.Instructions.Add(new PathLine("g28", "g10"));
            item.Instructions.Add(new PathLine("g28", "vc"));
            item.Instructions.Add(new PathLine("g11", "vc"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g31", "g25"));
            item.Instructions.Add(new PathLine("g32", "g26"));
            item.Instructions.Add(new PathMove("g33", "vc"));
            item.Instructions.Add(new PathLine("g28", "vc"));
            item.Instructions.Add(new PathMove("g29", "g10"));
            item.Instructions.Add(new PathLine("g29", "g27"));
            item.Instructions.Add(new PathLine("g30", "g27"));
            item.Instructions.Add(new PathLine("g30", "g10"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonInformation()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1 32"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "*/ g13 5 16"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ g13 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g13 13 32"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "*/ g13 19 32"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "*/ g13 11 16"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "*/ g13 13 16"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "*/ g13 7 8"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "+- g9 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g28", "+- g9 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "+- g9 g18 0"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "+- g9 g23 0"));
            geometry.Guides.Add(new ModelShapeGuide("g31", "+- g9 g24 0"));
            geometry.Guides.Add(new ModelShapeGuide("g32", "+- g11 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g34", "+- g11 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g35", "+- g11 g20 0"));
            geometry.Guides.Add(new ModelShapeGuide("g37", "+- g11 g22 0"));
            geometry.Guides.Add(new ModelShapeGuide("g38", "*/ g13 3 32"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathArc("dx2", "dx2", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathArc("dx2", "dx2", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g25"));
            item.Instructions.Add(new PathArc("g38", "g38", "3cd4", "21600000"));
            item.Instructions.Add(new PathMove("g32", "g28"));
            item.Instructions.Add(new PathLine("g32", "g29"));
            item.Instructions.Add(new PathLine("g34", "g29"));
            item.Instructions.Add(new PathLine("g34", "g30"));
            item.Instructions.Add(new PathLine("g32", "g30"));
            item.Instructions.Add(new PathLine("g32", "g31"));
            item.Instructions.Add(new PathLine("g37", "g31"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g35", "g30"));
            item.Instructions.Add(new PathLine("g35", "g28"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Lighten,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "g25"));
            item.Instructions.Add(new PathArc("g38", "g38", "3cd4", "21600000"));
            item.Instructions.Add(new PathMove("g32", "g28"));
            item.Instructions.Add(new PathLine("g35", "g28"));
            item.Instructions.Add(new PathLine("g35", "g30"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g37", "g31"));
            item.Instructions.Add(new PathLine("g32", "g31"));
            item.Instructions.Add(new PathLine("g32", "g30"));
            item.Instructions.Add(new PathLine("g34", "g30"));
            item.Instructions.Add(new PathLine("g34", "g29"));
            item.Instructions.Add(new PathLine("g32", "g29"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "g9"));
            item.Instructions.Add(new PathArc("dx2", "dx2", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "g25"));
            item.Instructions.Add(new PathArc("g38", "g38", "3cd4", "21600000"));
            item.Instructions.Add(new PathMove("g32", "g28"));
            item.Instructions.Add(new PathLine("g35", "g28"));
            item.Instructions.Add(new PathLine("g35", "g30"));
            item.Instructions.Add(new PathLine("g37", "g30"));
            item.Instructions.Add(new PathLine("g37", "g31"));
            item.Instructions.Add(new PathLine("g32", "g31"));
            item.Instructions.Add(new PathLine("g32", "g30"));
            item.Instructions.Add(new PathLine("g34", "g30"));
            item.Instructions.Add(new PathLine("g34", "g29"));
            item.Instructions.Add(new PathLine("g32", "g29"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonMovie()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1455 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 1905 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "*/ g13 2325 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "*/ g13 16155 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ g13 17010 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g13 19335 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "*/ g13 19725 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "*/ g13 20595 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "*/ g13 5280 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "*/ g13 5730 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "*/ g13 6630 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "*/ g13 7492 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g26", "*/ g13 9067 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "*/ g13 9555 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g28", "*/ g13 13342 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "*/ g13 14580 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "*/ g13 15592 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g31", "+- g11 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g32", "+- g11 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g33", "+- g11 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g34", "+- g11 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g35", "+- g11 g18 0"));
            geometry.Guides.Add(new ModelShapeGuide("g36", "+- g11 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g37", "+- g11 g20 0"));
            geometry.Guides.Add(new ModelShapeGuide("g38", "+- g11 g21 0"));
            geometry.Guides.Add(new ModelShapeGuide("g39", "+- g9 g22 0"));
            geometry.Guides.Add(new ModelShapeGuide("g40", "+- g9 g23 0"));
            geometry.Guides.Add(new ModelShapeGuide("g41", "+- g9 g24 0"));
            geometry.Guides.Add(new ModelShapeGuide("g42", "+- g9 g25 0"));
            geometry.Guides.Add(new ModelShapeGuide("g43", "+- g9 g26 0"));
            geometry.Guides.Add(new ModelShapeGuide("g44", "+- g9 g27 0"));
            geometry.Guides.Add(new ModelShapeGuide("g45", "+- g9 g28 0"));
            geometry.Guides.Add(new ModelShapeGuide("g46", "+- g9 g29 0"));
            geometry.Guides.Add(new ModelShapeGuide("g47", "+- g9 g30 0"));
            geometry.Guides.Add(new ModelShapeGuide("g48", "+- g9 g31 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g11", "g39"));
            item.Instructions.Add(new PathLine("g11", "g44"));
            item.Instructions.Add(new PathLine("g31", "g44"));
            item.Instructions.Add(new PathLine("g32", "g43"));
            item.Instructions.Add(new PathLine("g33", "g43"));
            item.Instructions.Add(new PathLine("g33", "g47"));
            item.Instructions.Add(new PathLine("g35", "g47"));
            item.Instructions.Add(new PathLine("g35", "g45"));
            item.Instructions.Add(new PathLine("g36", "g45"));
            item.Instructions.Add(new PathLine("g38", "g46"));
            item.Instructions.Add(new PathLine("g12", "g46"));
            item.Instructions.Add(new PathLine("g12", "g41"));
            item.Instructions.Add(new PathLine("g38", "g41"));
            item.Instructions.Add(new PathLine("g37", "g42"));
            item.Instructions.Add(new PathLine("g35", "g42"));
            item.Instructions.Add(new PathLine("g35", "g41"));
            item.Instructions.Add(new PathLine("g34", "g40"));
            item.Instructions.Add(new PathLine("g32", "g40"));
            item.Instructions.Add(new PathLine("g31", "g39"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g39"));
            item.Instructions.Add(new PathLine("g11", "g44"));
            item.Instructions.Add(new PathLine("g31", "g44"));
            item.Instructions.Add(new PathLine("g32", "g43"));
            item.Instructions.Add(new PathLine("g33", "g43"));
            item.Instructions.Add(new PathLine("g33", "g47"));
            item.Instructions.Add(new PathLine("g35", "g47"));
            item.Instructions.Add(new PathLine("g35", "g45"));
            item.Instructions.Add(new PathLine("g36", "g45"));
            item.Instructions.Add(new PathLine("g38", "g46"));
            item.Instructions.Add(new PathLine("g12", "g46"));
            item.Instructions.Add(new PathLine("g12", "g41"));
            item.Instructions.Add(new PathLine("g38", "g41"));
            item.Instructions.Add(new PathLine("g37", "g42"));
            item.Instructions.Add(new PathLine("g35", "g42"));
            item.Instructions.Add(new PathLine("g35", "g41"));
            item.Instructions.Add(new PathLine("g34", "g40"));
            item.Instructions.Add(new PathLine("g32", "g40"));
            item.Instructions.Add(new PathLine("g31", "g39"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g39"));
            item.Instructions.Add(new PathLine("g31", "g39"));
            item.Instructions.Add(new PathLine("g32", "g40"));
            item.Instructions.Add(new PathLine("g34", "g40"));
            item.Instructions.Add(new PathLine("g35", "g41"));
            item.Instructions.Add(new PathLine("g35", "g42"));
            item.Instructions.Add(new PathLine("g37", "g42"));
            item.Instructions.Add(new PathLine("g38", "g41"));
            item.Instructions.Add(new PathLine("g12", "g41"));
            item.Instructions.Add(new PathLine("g12", "g46"));
            item.Instructions.Add(new PathLine("g38", "g46"));
            item.Instructions.Add(new PathLine("g36", "g45"));
            item.Instructions.Add(new PathLine("g35", "g45"));
            item.Instructions.Add(new PathLine("g35", "g47"));
            item.Instructions.Add(new PathLine("g33", "g47"));
            item.Instructions.Add(new PathLine("g33", "g43"));
            item.Instructions.Add(new PathLine("g32", "g43"));
            item.Instructions.Add(new PathLine("g31", "g44"));
            item.Instructions.Add(new PathLine("g11", "g44"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonReturn()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 7 8"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "*/ g13 5 8"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "*/ g13 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ g13 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "+- g9 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "+- g9 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "+- g9 g18 0"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "+- g11 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "+- g11 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "+- g11 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "+- g11 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g26", "+- g11 g18 0"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "*/ g13 1 8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g12", "g21"));
            item.Instructions.Add(new PathLine("g23", "g9"));
            item.Instructions.Add(new PathLine("hc", "g21"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathLine("g24", "g20"));
            item.Instructions.Add(new PathArc("g27", "g27", "0", "cd4"));
            item.Instructions.Add(new PathLine("g25", "g19"));
            item.Instructions.Add(new PathArc("g27", "g27", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("g26", "g21"));
            item.Instructions.Add(new PathLine("g11", "g21"));
            item.Instructions.Add(new PathLine("g11", "g20"));
            item.Instructions.Add(new PathArc("g17", "g17", "cd2", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "g10"));
            item.Instructions.Add(new PathArc("g17", "g17", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g22", "g21"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g12", "g21"));
            item.Instructions.Add(new PathLine("g23", "g9"));
            item.Instructions.Add(new PathLine("hc", "g21"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathLine("g24", "g20"));
            item.Instructions.Add(new PathArc("g27", "g27", "0", "cd4"));
            item.Instructions.Add(new PathLine("g25", "g19"));
            item.Instructions.Add(new PathArc("g27", "g27", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("g26", "g21"));
            item.Instructions.Add(new PathLine("g11", "g21"));
            item.Instructions.Add(new PathLine("g11", "g20"));
            item.Instructions.Add(new PathArc("g17", "g17", "cd2", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "g10"));
            item.Instructions.Add(new PathArc("g17", "g17", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g22", "g21"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g12", "g21"));
            item.Instructions.Add(new PathLine("g22", "g21"));
            item.Instructions.Add(new PathLine("g22", "g20"));
            item.Instructions.Add(new PathArc("g17", "g17", "0", "cd4"));
            item.Instructions.Add(new PathLine("g25", "g10"));
            item.Instructions.Add(new PathArc("g17", "g17", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("g11", "g21"));
            item.Instructions.Add(new PathLine("g26", "g21"));
            item.Instructions.Add(new PathLine("g26", "g20"));
            item.Instructions.Add(new PathArc("g27", "g27", "cd2", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "g19"));
            item.Instructions.Add(new PathArc("g27", "g27", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathLine("hc", "g21"));
            item.Instructions.Add(new PathLine("g23", "g9"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateActionButtonSound()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "*/ ss 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 5 16"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "*/ g13 5 8"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "*/ g13 11 16"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ g13 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g13 7 8"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "+- g9 g14 0"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "+- g9 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "+- g9 g17 0"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "+- g9 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "+- g11 g15 0"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "+- g11 g16 0"));
            geometry.Guides.Add(new ModelShapeGuide("g26", "+- g11 g18 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g11", "g21"));
            item.Instructions.Add(new PathLine("g11", "g22"));
            item.Instructions.Add(new PathLine("g24", "g22"));
            item.Instructions.Add(new PathLine("g25", "g10"));
            item.Instructions.Add(new PathLine("g25", "g9"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g21"));
            item.Instructions.Add(new PathLine("g11", "g22"));
            item.Instructions.Add(new PathLine("g24", "g22"));
            item.Instructions.Add(new PathLine("g25", "g10"));
            item.Instructions.Add(new PathLine("g25", "g9"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("g11", "g21"));
            item.Instructions.Add(new PathLine("g24", "g21"));
            item.Instructions.Add(new PathLine("g25", "g9"));
            item.Instructions.Add(new PathLine("g25", "g10"));
            item.Instructions.Add(new PathLine("g24", "g22"));
            item.Instructions.Add(new PathLine("g11", "g22"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("g26", "g21"));
            item.Instructions.Add(new PathLine("g12", "g20"));
            item.Instructions.Add(new PathMove("g26", "vc"));
            item.Instructions.Add(new PathLine("g12", "vc"));
            item.Instructions.Add(new PathMove("g26", "g22"));
            item.Instructions.Add(new PathLine("g12", "g23"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateArc()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16200000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 0 adj2 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("sw11", "+- enAng 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw12", "+- sw11 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw11 sw11 sw12"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin wd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos hd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 wd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 hd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sw0", "+- 21600000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("da1", "+- swAng 0 sw0"));
            geometry.Guides.Add(new ModelShapeGuide("g1", "max x1 x2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "?: da1 r g1"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- cd4 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw2", "+- 27000000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw3", "?: sw1 sw1 sw2"));
            geometry.Guides.Add(new ModelShapeGuide("da2", "+- swAng 0 sw3"));
            geometry.Guides.Add(new ModelShapeGuide("g5", "max y1 y2"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "?: da2 b g5"));
            geometry.Guides.Add(new ModelShapeGuide("sw4", "+- cd2 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw5", "+- 32400000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw6", "?: sw4 sw4 sw5"));
            geometry.Guides.Add(new ModelShapeGuide("da3", "+- swAng 0 sw6"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "min x1 x2"));
            geometry.Guides.Add(new ModelShapeGuide("il", "?: da3 l g9"));
            geometry.Guides.Add(new ModelShapeGuide("sw7", "+- 3cd4 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw8", "+- 37800000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw9", "?: sw7 sw7 sw8"));
            geometry.Guides.Add(new ModelShapeGuide("da4", "+- swAng 0 sw9"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "min y1 y2"));
            geometry.Guides.Add(new ModelShapeGuide("it", "?: da4 t g13"));
            geometry.Guides.Add(new ModelShapeGuide("cang1", "+- stAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("cang2", "+- enAng cd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("cang3", "+/ cang1 cang2 2"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "21599999", "", "", "", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang1", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang3", "hc", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang2", "x2", "y2"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            item.Instructions.Add(new PathLine("hc", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 43750"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 50000"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dh2", "+- aw2 0 th2"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bw", "+- r 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("bh", "+- b 0 dh2"));
            geometry.Guides.Add(new ModelShapeGuide("bs", "min bw bh"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "*/ 100000 bs ss"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("bd", "*/ ss a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bd3", "+- bd 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("bd2", "max bd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- th bd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- dh2 th 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 dh2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- dh2 bd 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- y3 bd2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "th", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "50000", "r", "y4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "50000", "", "", "", "x4", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "0", "maxAdj4", "", "", "", "bd", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "th2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "aw2"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("l", "y5"));
            item.Instructions.Add(new PathArc("bd", "bd", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x4", "dh2"));
            item.Instructions.Add(new PathLine("x4", "t"));
            item.Instructions.Add(new PathLine("r", "aw2"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathArc("bd2", "bd2", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("th", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentConnector2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentConnector3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj1 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x1", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentConnector4()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+/ x1 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+/ t y2 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "-2147483647", "2147483647", "x2", "y2"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentConnector5()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+/ x1 x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+/ t y2 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ b y2 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "-2147483647", "2147483647", "", "", "", "x3", "y3"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 50000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- x3 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x3 dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("x0", "*/ x4 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y2 b 2"));
            geometry.Guides.Add(new ModelShapeGuide("y15", "+/ y1 b 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "50000", "l", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "50000", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x0", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y15"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ShapeTextRectangle.FromString("l", "y2", "x4", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBevel()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "x1"));
            geometry.ShapeTextRectangle.FromString("x1", "x1", "x2", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "x1"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.LightenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Lighten,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Darken,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x1", "x1"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathMove("r", "t"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBlockArc()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 10800000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("istAng", "pin 0 adj2 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sw11", "+- istAng 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw12", "+- sw11 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw11 sw11 sw12"));
            geometry.Guides.Add(new ModelShapeGuide("iswAng", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("wt3", "sin wd2 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht3", "cos hd2 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "cat2 wd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "sat2 hd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "+- wd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "+- hd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin iwd2 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos ihd2 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("wt4", "sin iwd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht4", "cos ihd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 iwd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 ihd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dx4", "cat2 iwd2 ht4 wt4"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "sat2 ihd2 ht4 wt4"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sw0", "+- 21600000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("da1", "+- swAng 0 sw0"));
            geometry.Guides.Add(new ModelShapeGuide("g1", "max x1 x2"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "max x3 x4"));
            geometry.Guides.Add(new ModelShapeGuide("g3", "max g1 g2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "?: da1 r g3"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- cd4 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw2", "+- 27000000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw3", "?: sw1 sw1 sw2"));
            geometry.Guides.Add(new ModelShapeGuide("da2", "+- swAng 0 sw3"));
            geometry.Guides.Add(new ModelShapeGuide("g5", "max y1 y2"));
            geometry.Guides.Add(new ModelShapeGuide("g6", "max y3 y4"));
            geometry.Guides.Add(new ModelShapeGuide("g7", "max g5 g6"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "?: da2 b g7"));
            geometry.Guides.Add(new ModelShapeGuide("sw4", "+- cd2 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw5", "+- 32400000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw6", "?: sw4 sw4 sw5"));
            geometry.Guides.Add(new ModelShapeGuide("da3", "+- swAng 0 sw6"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "min x1 x2"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "min x3 x4"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "min g9 g10"));
            geometry.Guides.Add(new ModelShapeGuide("il", "?: da3 l g11"));
            geometry.Guides.Add(new ModelShapeGuide("sw7", "+- 3cd4 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw8", "+- 37800000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw9", "?: sw7 sw7 sw8"));
            geometry.Guides.Add(new ModelShapeGuide("da4", "+- swAng 0 sw9"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "min y1 y2"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "min y3 y4"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "min g13 g14"));
            geometry.Guides.Add(new ModelShapeGuide("it", "?: da4 t g15"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+/ x1 x4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+/ y1 y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+/ x3 x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+/ y3 y2 2"));
            geometry.Guides.Add(new ModelShapeGuide("cang1", "+- stAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("cang2", "+- istAng cd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("cang3", "+/ cang1 cang2 2"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "21599999", "adj3", "0", "50000", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang1", "x5", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang2", "x6", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cang3", "hc", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "istAng", "iswAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBorderCallout1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -38333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBorderCallout2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -46667"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBorderCallout3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 100000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj7", "val 112963"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj8", "val -8333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj7 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w adj8 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj8", "-2147483647", "2147483647", "adj7", "-2147483647", "2147483647", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBracePair()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 8333"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 25000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ ss a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc x1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ x1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- x1 it 0"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 it"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "25000", "l", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x2", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "-5400000"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "-5400000"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("x2", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "-5400000"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathMove("x3", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "-5400000"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBracketPair()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ x1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "l", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("x1", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathMove("x2", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCallout1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -38333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCallout2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 112500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -46667"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCallout3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 100000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj6", "val -16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj7", "val 112963"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj8", "val -8333"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj6 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj7 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w adj8 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-2147483647", "2147483647", "adj1", "-2147483647", "2147483647", "x1", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "-2147483647", "2147483647", "adj3", "-2147483647", "2147483647", "x2", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj6", "-2147483647", "2147483647", "adj5", "-2147483647", "2147483647", "x3", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj8", "-2147483647", "2147483647", "adj7", "-2147483647", "2147483647", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCan()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y1 y1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- b 0 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "maxAdj", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "y2", "r", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "-10800000"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.Lighten,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "cd2"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "y1"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd2"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "cd2"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd2"));
            item.Instructions.Add(new PathLine("l", "y1"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChartPlus()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                Stroke = false
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("0", "10"));
            item.Instructions.Add(new PathLine("10", "10"));
            item.Instructions.Add(new PathLine("10", "0"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("5", "0"));
            item.Instructions.Add(new PathLine("5", "10"));
            item.Instructions.Add(new PathMove("0", "5"));
            item.Instructions.Add(new PathLine("10", "5"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChartStar()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                Stroke = false
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("0", "10"));
            item.Instructions.Add(new PathLine("10", "10"));
            item.Instructions.Add(new PathLine("10", "0"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("10", "10"));
            item.Instructions.Add(new PathMove("0", "10"));
            item.Instructions.Add(new PathLine("10", "0"));
            item.Instructions.Add(new PathMove("5", "0"));
            item.Instructions.Add(new PathLine("5", "10"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChartX()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                Stroke = false
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("0", "10"));
            item.Instructions.Add(new PathLine("10", "10"));
            item.Instructions.Add(new PathLine("10", "0"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10,
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("10", "10"));
            item.Instructions.Add(new PathMove("0", "10"));
            item.Instructions.Add(new PathLine("10", "0"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChevron()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ x2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "+- x2 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "?: dx x1 l"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "?: dx x2 r"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "maxAdj", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "t", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathLine("x1", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChord()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 2700000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 16200000"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 0 adj2 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- enAng 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw2", "+- sw1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw1 sw1 sw2"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin wd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos hd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 wd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 hd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ x1 x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y1 y2 2"));
            geometry.Guides.Add(new ModelShapeGuide("midAng0", "*/ swAng 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("midAng", "+- stAng midAng0 cd2"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "21599999", "", "", "", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("stAng", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("enAng", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("midAng", "x3", "y3"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCircularArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 1142319"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 20457681"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 10800000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a5", "pin 0 adj5 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a5 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 1 adj3 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj4 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("thh", "*/ ss a5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("rw1", "+- wd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rh1", "+- hd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rw2", "+- rw1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rh2", "+- rh1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rw3", "+- rw2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh3", "+- rh2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtH", "sin rw3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("htH", "cos rh3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxH", "cat2 rw3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("dyH", "sat2 rh3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("xH", "+- hc dxH 0"));
            geometry.Guides.Add(new ModelShapeGuide("yH", "+- vc dyH 0"));
            geometry.Guides.Add(new ModelShapeGuide("rI", "min rw2 rh2"));
            geometry.Guides.Add(new ModelShapeGuide("u1", "*/ dxH dxH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u2", "*/ dyH dyH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("u4", "+- u1 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u5", "+- u2 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u6", "*/ u4 u5 u1"));
            geometry.Guides.Add(new ModelShapeGuide("u7", "*/ u6 1 u2"));
            geometry.Guides.Add(new ModelShapeGuide("u8", "+- 1 0 u7"));
            geometry.Guides.Add(new ModelShapeGuide("u9", "sqrt u8"));
            geometry.Guides.Add(new ModelShapeGuide("u10", "*/ u4 1 dxH"));
            geometry.Guides.Add(new ModelShapeGuide("u11", "*/ u10 1 dyH"));
            geometry.Guides.Add(new ModelShapeGuide("u12", "+/ 1 u9 u11"));
            geometry.Guides.Add(new ModelShapeGuide("u13", "at2 1 u12"));
            geometry.Guides.Add(new ModelShapeGuide("u14", "+- u13 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u15", "?: u13 u13 u14"));
            geometry.Guides.Add(new ModelShapeGuide("u16", "+- u15 0 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("u17", "+- u16 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u18", "?: u16 u16 u17"));
            geometry.Guides.Add(new ModelShapeGuide("u19", "+- u18 0 cd2"));
            geometry.Guides.Add(new ModelShapeGuide("u20", "+- u18 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("u21", "?: u19 u20 u18"));
            geometry.Guides.Add(new ModelShapeGuide("maxAng", "abs u21"));
            geometry.Guides.Add(new ModelShapeGuide("aAng", "pin 0 adj2 maxAng"));
            geometry.Guides.Add(new ModelShapeGuide("ptAng", "+- enAng aAng 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtA", "sin rw3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("htA", "cos rh3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxA", "cat2 rw3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("dyA", "sat2 rh3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("xA", "+- hc dxA 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA", "+- vc dyA 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtE", "sin rw1 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htE", "cos rh1 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxE", "cat2 rw1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("dyE", "sat2 rh1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("xE", "+- hc dxE 0"));
            geometry.Guides.Add(new ModelShapeGuide("yE", "+- vc dyE 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxG", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyG", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xG", "+- xH dxG 0"));
            geometry.Guides.Add(new ModelShapeGuide("yG", "+- yH dyG 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxB", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyB", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xB", "+- xH 0 dxB 0"));
            geometry.Guides.Add(new ModelShapeGuide("yB", "+- yH 0 dyB 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- xB 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- yB 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- xG 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- yG 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("rO", "min rw1 rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x1O", "*/ sx1 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y1O", "*/ sy1 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x2O", "*/ sx2 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y2O", "*/ sy2 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("dxO", "+- x2O 0 x1O"));
            geometry.Guides.Add(new ModelShapeGuide("dyO", "+- y2O 0 y1O"));
            geometry.Guides.Add(new ModelShapeGuide("dO", "mod dxO dyO 0"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ x1O y2O 1"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ x2O y1O 1"));
            geometry.Guides.Add(new ModelShapeGuide("DO", "+- q1 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ rO rO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "*/ dO dO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "*/ q3 q4 1"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "*/ DO DO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "+- q5 0 q6"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "max q7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelO", "sqrt q8"));
            geometry.Guides.Add(new ModelShapeGuide("ndyO", "*/ dyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("sdyO", "?: ndyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ sdyO dxO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "*/ q9 sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "*/ DO dyO 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxF1", "+/ q11 q10 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "+- q11 0 q10"));
            geometry.Guides.Add(new ModelShapeGuide("dxF2", "*/ q12 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("adyO", "abs dyO"));
            geometry.Guides.Add(new ModelShapeGuide("q13", "*/ adyO sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q14", "*/ DO dxO -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyF1", "+/ q14 q13 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q15", "+- q14 0 q13"));
            geometry.Guides.Add(new ModelShapeGuide("dyF2", "*/ q15 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q16", "+- x2O 0 dxF1"));
            geometry.Guides.Add(new ModelShapeGuide("q17", "+- x2O 0 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("q18", "+- y2O 0 dyF1"));
            geometry.Guides.Add(new ModelShapeGuide("q19", "+- y2O 0 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("q20", "mod q16 q18 0"));
            geometry.Guides.Add(new ModelShapeGuide("q21", "mod q17 q19 0"));
            geometry.Guides.Add(new ModelShapeGuide("q22", "+- q21 0 q20"));
            geometry.Guides.Add(new ModelShapeGuide("dxF", "?: q22 dxF1 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("dyF", "?: q22 dyF1 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxF", "*/ dxF rw1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("sdyF", "*/ dyF rh1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("xF", "+- hc sdxF 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF", "+- vc sdyF 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1I", "*/ sx1 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y1I", "*/ sy1 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("x2I", "*/ sx2 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y2I", "*/ sy2 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "+- x2I 0 x1I"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "+- y2I 0 y1I"));
            geometry.Guides.Add(new ModelShapeGuide("dI", "mod dxI dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "*/ x1I y2I 1"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "*/ x2I y1I 1"));
            geometry.Guides.Add(new ModelShapeGuide("DI", "+- v1 0 v2"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v4", "*/ dI dI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v5", "*/ v3 v4 1"));
            geometry.Guides.Add(new ModelShapeGuide("v6", "*/ DI DI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v7", "+- v5 0 v6"));
            geometry.Guides.Add(new ModelShapeGuide("v8", "max v7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelI", "sqrt v8"));
            geometry.Guides.Add(new ModelShapeGuide("v9", "*/ sdyO dxI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v10", "*/ v9 sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v11", "*/ DI dyI 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxC1", "+/ v11 v10 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v12", "+- v11 0 v10"));
            geometry.Guides.Add(new ModelShapeGuide("dxC2", "*/ v12 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("adyI", "abs dyI"));
            geometry.Guides.Add(new ModelShapeGuide("v13", "*/ adyI sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v14", "*/ DI dxI -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyC1", "+/ v14 v13 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v15", "+- v14 0 v13"));
            geometry.Guides.Add(new ModelShapeGuide("dyC2", "*/ v15 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v16", "+- x1I 0 dxC1"));
            geometry.Guides.Add(new ModelShapeGuide("v17", "+- x1I 0 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("v18", "+- y1I 0 dyC1"));
            geometry.Guides.Add(new ModelShapeGuide("v19", "+- y1I 0 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("v20", "mod v16 v18 0"));
            geometry.Guides.Add(new ModelShapeGuide("v21", "mod v17 v19 0"));
            geometry.Guides.Add(new ModelShapeGuide("v22", "+- v21 0 v20"));
            geometry.Guides.Add(new ModelShapeGuide("dxC", "?: v22 dxC1 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("dyC", "?: v22 dyC1 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxC", "*/ dxC rw2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("sdyC", "*/ dyC rh2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("xC", "+- hc sdxC 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC", "+- vc sdyC 0"));
            geometry.Guides.Add(new ModelShapeGuide("ist0", "at2 sdxC sdyC"));
            geometry.Guides.Add(new ModelShapeGuide("ist1", "+- ist0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("istAng", "?: ist0 ist0 ist1"));
            geometry.Guides.Add(new ModelShapeGuide("isw1", "+- stAng 0 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("isw2", "+- isw1 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("iswAng", "?: isw1 isw2 isw1"));
            geometry.Guides.Add(new ModelShapeGuide("p1", "+- xF 0 xC"));
            geometry.Guides.Add(new ModelShapeGuide("p2", "+- yF 0 yC"));
            geometry.Guides.Add(new ModelShapeGuide("p3", "mod p1 p2 0"));
            geometry.Guides.Add(new ModelShapeGuide("p4", "*/ p3 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("p5", "+- p4 0 thh"));
            geometry.Guides.Add(new ModelShapeGuide("xGp", "?: p5 xF xG"));
            geometry.Guides.Add(new ModelShapeGuide("yGp", "?: p5 yF yG"));
            geometry.Guides.Add(new ModelShapeGuide("xBp", "?: p5 xC xB"));
            geometry.Guides.Add(new ModelShapeGuide("yBp", "?: p5 yC yB"));
            geometry.Guides.Add(new ModelShapeGuide("en0", "at2 sdxF sdyF"));
            geometry.Guides.Add(new ModelShapeGuide("en1", "+- en0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("en2", "?: en0 en0 en1"));
            geometry.Guides.Add(new ModelShapeGuide("sw0", "+- en2 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- sw0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw0 sw0 sw1"));
            geometry.Guides.Add(new ModelShapeGuide("wtI", "sin rw3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htI", "cos rh3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "cat2 rw3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "sat2 rh3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("xI", "+- hc dxI 0"));
            geometry.Guides.Add(new ModelShapeGuide("yI", "+- vc dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("aI", "+- stAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("aA", "+- ptAng cd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("aB", "+- ptAng cd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos rw1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin rh1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "maxAng", "", "", "", "xA", "yA"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj4", "0", "21599999", "", "", "", "xE", "yE"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj3", "0", "21599999", "adj1", "0", "maxAdj1", "xF", "yF"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("", "", "", "adj5", "0", "25000", "xB", "yB"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aI", "xI", "yI"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("ptAng", "xGp", "yGp"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aA", "xA", "yA"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aB", "xBp", "yBp"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xE", "yE"));
            item.Instructions.Add(new PathArc("rw1", "rh1", "stAng", "swAng"));
            item.Instructions.Add(new PathLine("xGp", "yGp"));
            item.Instructions.Add(new PathLine("xA", "yA"));
            item.Instructions.Add(new PathLine("xBp", "yBp"));
            item.Instructions.Add(new PathLine("xC", "yC"));
            item.Instructions.Add(new PathArc("rw2", "rh2", "istAng", "iswAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCloud()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ w 2977 21600"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h 3262 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 17087 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 17337 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "*/ w 67 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g28", "*/ h 21577 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "*/ w 21582 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "*/ h 1235 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "g29", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "g28"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "g27", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "g30"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0xa8c0L,
                Height = 0xa8c0L
            };
            item.Instructions.Add(new PathMove("3900", "14370"));
            item.Instructions.Add(new PathArc("6753", "9190", "-11429249", "7426832"));
            item.Instructions.Add(new PathArc("5333", "7267", "-8646143", "5396714"));
            item.Instructions.Add(new PathArc("4365", "5945", "-8748475", "5983381"));
            item.Instructions.Add(new PathArc("4857", "6595", "-7859164", "7034504"));
            item.Instructions.Add(new PathArc("5333", "7273", "-4722533", "6541615"));
            item.Instructions.Add(new PathArc("6775", "9220", "-2776035", "7816140"));
            item.Instructions.Add(new PathArc("5785", "7867", "37501", "6842000"));
            item.Instructions.Add(new PathArc("6752", "9215", "1347096", "6910353"));
            item.Instructions.Add(new PathArc("7720", "10543", "3974558", "4542661"));
            item.Instructions.Add(new PathArc("4360", "5918", "-16496525", "8804134"));
            item.Instructions.Add(new PathArc("4345", "5945", "-14809710", "9151131"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0xa8c0L,
                Height = 0xa8c0L,
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("4693", "26177"));
            item.Instructions.Add(new PathArc("4345", "5945", "5204520", "1585770"));
            item.Instructions.Add(new PathMove("6928", "34899"));
            item.Instructions.Add(new PathArc("4360", "5918", "4416628", "686848"));
            item.Instructions.Add(new PathMove("16478", "39090"));
            item.Instructions.Add(new PathArc("6752", "9215", "8257449", "844866"));
            item.Instructions.Add(new PathMove("28827", "34751"));
            item.Instructions.Add(new PathArc("6752", "9215", "387196", "959901"));
            item.Instructions.Add(new PathMove("34129", "22954"));
            item.Instructions.Add(new PathArc("5785", "7867", "-4217541", "4255042"));
            item.Instructions.Add(new PathMove("41798", "15354"));
            item.Instructions.Add(new PathArc("5333", "7273", "1819082", "1665090"));
            item.Instructions.Add(new PathMove("38324", "5426"));
            item.Instructions.Add(new PathArc("4857", "6595", "-824660", "891534"));
            item.Instructions.Add(new PathMove("29078", "3952"));
            item.Instructions.Add(new PathArc("4857", "6595", "-8950887", "1091722"));
            item.Instructions.Add(new PathMove("22141", "4720"));
            item.Instructions.Add(new PathArc("4365", "5945", "-9809656", "1061181"));
            item.Instructions.Add(new PathMove("14000", "5192"));
            item.Instructions.Add(new PathArc("6753", "9190", "-4002417", "739161"));
            item.Instructions.Add(new PathMove("4127", "15789"));
            item.Instructions.Add(new PathArc("6753", "9190", "9459261", "711490"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCloudCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -20833"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 62500"));
            geometry.Guides.Add(new ModelShapeGuide("dxPos", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dyPos", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("xPos", "+- hc dxPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("yPos", "+- vc dyPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("ht", "cat2 hd2 dxPos dyPos"));
            geometry.Guides.Add(new ModelShapeGuide("wt", "sat2 wd2 dxPos dyPos"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "cat2 wd2 ht wt"));
            geometry.Guides.Add(new ModelShapeGuide("g3", "sat2 hd2 ht wt"));
            geometry.Guides.Add(new ModelShapeGuide("g4", "+- hc g2 0"));
            geometry.Guides.Add(new ModelShapeGuide("g5", "+- vc g3 0"));
            geometry.Guides.Add(new ModelShapeGuide("g6", "+- g4 0 xPos"));
            geometry.Guides.Add(new ModelShapeGuide("g7", "+- g5 0 yPos"));
            geometry.Guides.Add(new ModelShapeGuide("g8", "mod g6 g7 0"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "*/ ss 6600 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "+- g8 0 g9"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "*/ g10 1 3"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "*/ ss 1800 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "+- g11 g12 0"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "*/ g13 g6 g8"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "*/ g13 g7 g8"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "+- g14 xPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "+- g15 yPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "*/ ss 4800 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g19", "*/ g11 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("g20", "+- g18 g19 0"));
            geometry.Guides.Add(new ModelShapeGuide("g21", "*/ g20 g6 g8"));
            geometry.Guides.Add(new ModelShapeGuide("g22", "*/ g20 g7 g8"));
            geometry.Guides.Add(new ModelShapeGuide("g23", "+- g21 xPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("g24", "+- g22 yPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("g25", "*/ ss 1200 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g26", "*/ ss 600 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x23", "+- xPos g26 0"));
            geometry.Guides.Add(new ModelShapeGuide("x24", "+- g16 g25 0"));
            geometry.Guides.Add(new ModelShapeGuide("x25", "+- g23 g12 0"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ w 2977 21600"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h 3262 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 17087 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 17337 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g27", "*/ w 67 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g28", "*/ h 21577 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g29", "*/ w 21582 21600"));
            geometry.Guides.Add(new ModelShapeGuide("g30", "*/ h 1235 21600"));
            geometry.Guides.Add(new ModelShapeGuide("pang", "at2 dxPos dyPos"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "adj2", "-2147483647", "2147483647", "xPos", "yPos"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "g27", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "g28"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "g29", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "g30"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("pang", "xPos", "yPos"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0xa8c0L,
                Height = 0xa8c0L
            };
            item.Instructions.Add(new PathMove("3900", "14370"));
            item.Instructions.Add(new PathArc("6753", "9190", "-11429249", "7426832"));
            item.Instructions.Add(new PathArc("5333", "7267", "-8646143", "5396714"));
            item.Instructions.Add(new PathArc("4365", "5945", "-8748475", "5983381"));
            item.Instructions.Add(new PathArc("4857", "6595", "-7859164", "7034504"));
            item.Instructions.Add(new PathArc("5333", "7273", "-4722533", "6541615"));
            item.Instructions.Add(new PathArc("6775", "9220", "-2776035", "7816140"));
            item.Instructions.Add(new PathArc("5785", "7867", "37501", "6842000"));
            item.Instructions.Add(new PathArc("6752", "9215", "1347096", "6910353"));
            item.Instructions.Add(new PathArc("7720", "10543", "3974558", "4542661"));
            item.Instructions.Add(new PathArc("4360", "5918", "-16496525", "8804134"));
            item.Instructions.Add(new PathArc("4345", "5945", "-14809710", "9151131"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x23", "yPos"));
            item.Instructions.Add(new PathArc("g26", "g26", "0", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x24", "g17"));
            item.Instructions.Add(new PathArc("g25", "g25", "0", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x25", "g24"));
            item.Instructions.Add(new PathArc("g12", "g12", "0", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0xa8c0L,
                Height = 0xa8c0L,
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("4693", "26177"));
            item.Instructions.Add(new PathArc("4345", "5945", "5204520", "1585770"));
            item.Instructions.Add(new PathMove("6928", "34899"));
            item.Instructions.Add(new PathArc("4360", "5918", "4416628", "686848"));
            item.Instructions.Add(new PathMove("16478", "39090"));
            item.Instructions.Add(new PathArc("6752", "9215", "8257449", "844866"));
            item.Instructions.Add(new PathMove("28827", "34751"));
            item.Instructions.Add(new PathArc("6752", "9215", "387196", "959901"));
            item.Instructions.Add(new PathMove("34129", "22954"));
            item.Instructions.Add(new PathArc("5785", "7867", "-4217541", "4255042"));
            item.Instructions.Add(new PathMove("41798", "15354"));
            item.Instructions.Add(new PathArc("5333", "7273", "1819082", "1665090"));
            item.Instructions.Add(new PathMove("38324", "5426"));
            item.Instructions.Add(new PathArc("4857", "6595", "-824660", "891534"));
            item.Instructions.Add(new PathMove("29078", "3952"));
            item.Instructions.Add(new PathArc("4857", "6595", "-8950887", "1091722"));
            item.Instructions.Add(new PathMove("22141", "4720"));
            item.Instructions.Add(new PathArc("4365", "5945", "-9809656", "1061181"));
            item.Instructions.Add(new PathMove("14000", "5192"));
            item.Instructions.Add(new PathArc("6753", "9190", "-4002417", "739161"));
            item.Instructions.Add(new PathMove("4127", "15789"));
            item.Instructions.Add(new PathArc("6753", "9190", "9459261", "711490"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCorner()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ 100000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("cx1", "*/ x1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("cy1", "+/ y1 b 2"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- w 0 h"));
            geometry.Guides.Add(new ModelShapeGuide("it", "?: d y1 t"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "?: d r x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "cy1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "cx1", "t"));
            geometry.ShapeTextRectangle.FromString("l", "it", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCornerTabs()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("md", "mod w h 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ 1 md 20"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- 0 b dx"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- 0 r dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "dx", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "dx", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "b"));
            geometry.ShapeTextRectangle.FromString("dx", "dx", "x1", "y1");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("dx", "t"));
            item.Instructions.Add(new PathLine("l", "dx"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("dx", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "dx"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("r", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCube()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ y4 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y1 b 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ x4 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ y1 r 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "100000", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ShapeTextRectangle.FromString("l", "y1", "x4", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x4", "y1"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.LightenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("y1", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("y1", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathMove("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedConnector2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "wd2", "t", "r", "hd2", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedConnector3()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+/ l x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ r x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 3 4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x2", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "t", "x2", "hd4", "x2", "vc"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x2", "y3", "x3", "b", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedConnector4()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+/ l x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ r x2 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+/ x2 x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+/ x3 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+/ t y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+/ t y1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y1 y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+/ b y4 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x2", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "-2147483647", "2147483647", "x3", "y4"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "t", "x2", "y2", "x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x2", "y3", "x4", "y4", "x3", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x5", "y4", "r", "y5", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedConnector5()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "*/ w adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+/ x3 x6 2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+/ l x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+/ x3 x1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+/ x6 x1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+/ x6 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+/ t y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+/ t y1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y1 y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+/ b y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+/ y5 y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+/ y5 b 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "", "", "", "x3", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "-2147483647", "2147483647", "x1", "y4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "-2147483647", "2147483647", "", "", "", "x6", "y5"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x2", "t", "x3", "y2", "x3", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y3", "x4", "y4", "x1", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x5", "y4", "x6", "y6", "x6", "y5"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x6", "y7", "x7", "b", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedDownArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+/ th aw 4"));
            geometry.Guides.Add(new ModelShapeGuide("wR", "+- wd2 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "*/ wR 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ q7 q7 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ th th 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- q8 0 q9"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "sqrt q10"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "*/ q11 h q7"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 idy ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- wR th 0"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ h h 1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ ah ah 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- q2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "sqrt q4"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ q5 wR h"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- wR dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x3 dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- aw 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dh", "*/ q6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x5 0 dh"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- x7 dh 0"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ aw 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "at2 ah dx"));
            geometry.Guides.Add(new ModelShapeGuide("mswAng", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("iy", "+- b 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ix", "+/ wR x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dang2", "at2 idy q12"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "+- 3cd4 swAng 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng2", "+- 3cd4 0 dang2"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- dang2 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- cd4 dang2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "adj2", "", "", "", "x7", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x4", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ix", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "q12", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x6", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x8", "y1"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x6", "b"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng", "mswAng"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathArc("wR", "h", "3cd4", "swAng"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ix", "iy"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng2", "swAng2"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathArc("wR", "h", "cd2", "swAng3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ix", "iy"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng2", "swAng2"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathArc("wR", "h", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathArc("wR", "h", "3cd4", "swAng"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathLine("x6", "b"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng", "mswAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedLeftArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 a2"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+/ th aw 4"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "+- hd2 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "*/ hR 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ q7 q7 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ th th 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- q8 0 q9"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "sqrt q10"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "*/ q11 w q7"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 idx ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- hR th 0"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ w w 1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ ah ah 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- q2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "sqrt q4"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ q5 hR w"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- hR dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- y3 dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- aw 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dh", "*/ q6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y5 0 dh"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- y7 dh 0"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ aw 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l ah 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "at2 ah dy"));
            geometry.Guides.Add(new ModelShapeGuide("mswAng", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("ix", "+- l idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("iy", "+/ hR y3 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dang2", "at2 idx q12"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- dang2 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- swAng dang2 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng3", "+- 0 0 dang2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "a2", "x1", "y5"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "r", "y4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "maxAdj3", "", "", "", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "q12"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd3", "l", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "y8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "iy"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y6"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathArc("w", "hR", "swAng", "swAng2"));
            item.Instructions.Add(new PathArc("w", "hR", "stAng3", "swAng3"));
            item.Instructions.Add(new PathLine("x1", "y8"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "y3"));
            item.Instructions.Add(new PathArc("w", "hR", "0", "-5400000"));
            item.Instructions.Add(new PathLine("l", "t"));
            item.Instructions.Add(new PathArc("w", "hR", "3cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "y3"));
            item.Instructions.Add(new PathArc("w", "hR", "0", "-5400000"));
            item.Instructions.Add(new PathLine("l", "t"));
            item.Instructions.Add(new PathArc("w", "hR", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathArc("w", "hR", "0", "swAng"));
            item.Instructions.Add(new PathLine("x1", "y8"));
            item.Instructions.Add(new PathLine("l", "y6"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathArc("w", "hR", "swAng", "swAng2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 a2"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+/ th aw 4"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "+- hd2 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "*/ hR 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ q7 q7 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ th th 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- q8 0 q9"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "sqrt q10"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "*/ q11 w q7"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 idx ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- hR th 0"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ w w 1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ ah ah 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- q2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "sqrt q4"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ q5 hR w"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- hR dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- y3 dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- aw 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dh", "*/ q6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y5 0 dh"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- y7 dh 0"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ aw 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "at2 ah dy"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "+- cd2 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("mswAng", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("ix", "+- r 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("iy", "+/ hR y3 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dang2", "at2 idx q12"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- dang2 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- cd4 dang2 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng3", "+- cd2 0 dang2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "a2", "x1", "y5"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "r", "y4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "maxAdj3", "", "", "", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "iy"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "y8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x1", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "q12"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "hR"));
            item.Instructions.Add(new PathArc("w", "hR", "cd2", "mswAng"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("r", "y6"));
            item.Instructions.Add(new PathLine("x1", "y8"));
            item.Instructions.Add(new PathLine("x1", "y7"));
            item.Instructions.Add(new PathArc("w", "hR", "stAng", "swAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "th"));
            item.Instructions.Add(new PathArc("w", "hR", "3cd4", "swAng2"));
            item.Instructions.Add(new PathArc("w", "hR", "stAng3", "swAng3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "hR"));
            item.Instructions.Add(new PathArc("w", "hR", "cd2", "mswAng"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("r", "y6"));
            item.Instructions.Add(new PathLine("x1", "y8"));
            item.Instructions.Add(new PathLine("x1", "y7"));
            item.Instructions.Add(new PathArc("w", "hR", "stAng", "swAng"));
            item.Instructions.Add(new PathLine("l", "hR"));
            item.Instructions.Add(new PathArc("w", "hR", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("r", "th"));
            item.Instructions.Add(new PathArc("w", "hR", "3cd4", "swAng2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurvedUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+/ th aw 4"));
            geometry.Guides.Add(new ModelShapeGuide("wR", "+- wd2 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "*/ wR 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ q7 q7 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ th th 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- q8 0 q9"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "sqrt q10"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "*/ q11 h q7"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 idy ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- wR th 0"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ h h 1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ ah ah 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- q2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "sqrt q4"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ q5 wR h"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- wR dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x3 dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- aw 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dh", "*/ q6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x5 0 dh"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- x7 dh 0"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ aw 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t ah 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "at2 ah dx"));
            geometry.Guides.Add(new ModelShapeGuide("mswAng", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("iy", "+- t idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("ix", "+/ wR x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dang2", "at2 idy q12"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- dang2 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("mswAng2", "+- 0 0 swAng2"));
            geometry.Guides.Add(new ModelShapeGuide("stAng3", "+- cd4 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- swAng dang2 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng2", "+- cd4 0 dang2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "a2", "", "", "", "x7", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x4", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x6", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "q12", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ix", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x8", "y1"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x6", "t"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng3", "swAng3"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng2", "swAng2"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("wR", "b"));
            item.Instructions.Add(new PathArc("wR", "h", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("th", "t"));
            item.Instructions.Add(new PathArc("wR", "h", "cd2", "-5400000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ix", "iy"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng2", "swAng2"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x6", "t"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathArc("wR", "h", "stAng3", "swAng"));
            item.Instructions.Add(new PathLine("wR", "b"));
            item.Instructions.Add(new PathArc("wR", "h", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("th", "t"));
            item.Instructions.Add(new PathArc("wR", "h", "cd2", "-5400000"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDecagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 105146"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 2160000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cos wd2 4320000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin shd2 4320000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sin shd2 2160000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "y2", "x4", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDiagStripe()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ x2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ x2 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ y2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y2 b 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "100000", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "hc", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "x3", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDiamond()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd4", "hd4", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDodecagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w 2894 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 7906 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 13694 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 18706 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h 2894 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 7906 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 13694 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h 18706 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x4", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("l", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDonut()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "+- wd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "+- hd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("", "", "", "adj", "0", "50000", "dr", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("dr", "vc"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "cd2", "-5400000"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "0", "-5400000"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "3cd4", "-5400000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDoubleWave()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 6250"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin -10000 adj2 10000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ y1 10 3"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y1 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- y1 dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y4 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- y4 dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs dx1"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "?: of2 0 of2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- l 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dx8", "?: of2 of2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- r 0 dx8"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "+/ dx2 x8 6"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx4", "+/ dx2 x8 3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x2 dx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+/ x2 x8 2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- x5 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+/ x6 x8 2"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- l dx8 0"));
            geometry.Guides.Add(new ModelShapeGuide("x15", "+- r dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- x9 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x11", "+- x9 dx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("x12", "+/ x9 x15 2"));
            geometry.Guides.Add(new ModelShapeGuide("x13", "+- x12 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x14", "+/ x13 x15 2"));
            geometry.Guides.Add(new ModelShapeGuide("x16", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("il", "max x2 x9"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "min x8 x15"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h a1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 it"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "12500", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x12", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x5", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x16", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y2", "x4", "y3", "x5", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x6", "y2", "x7", "y3", "x8", "y1"));
            item.Instructions.Add(new PathLine("x15", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x14", "y6", "x13", "y5", "x12", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x11", "y6", "x10", "y5", "x9", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDownArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ x1 dy1 wd2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y1 dy2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "100000", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "t", "x2", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDownArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 64977"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss h"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- b 0 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ y2 1 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x2", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj4", "0", "maxAdj4", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateEllipse()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateEllipseRibbon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 25000 adj2 75000"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- 100000 0 a1"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "*/ q10 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "+- a1 0 q11"));
            geometry.Guides.Add(new ModelShapeGuide("minAdj3", "max 0 q12"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin minAdj3 adj3 a1"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a2 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 wd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x3"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("f1", "*/ 4 dy1 w"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ x3 x3 w"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "+- x3 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ f1 q2 1"));
            geometry.Guides.Add(new ModelShapeGuide("cx1", "*/ x3 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("cy1", "*/ f1 cx1 1"));
            geometry.Guides.Add(new ModelShapeGuide("cx2", "+- r 0 cx1"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "+- q1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ x2 x2 w"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- x2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "*/ f1 q4 1"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- q5 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- dy1 dy3 y3"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "+- q6 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("cy3", "+- q7 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh", "+- b 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ dy1 14 16"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+/ q8 rh 2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- q5 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- y3 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("cx4", "*/ x2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ f1 cx4 1"));
            geometry.Guides.Add(new ModelShapeGuide("cy4", "+- q9 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("cx5", "+- r 0 cx4"));
            geometry.Guides.Add(new ModelShapeGuide("cy6", "+- cy3 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- y1 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("cy7", "+- q1 q1 y7"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- b 0 dy1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "hc", "q1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "25000", "75000", "", "", "", "x2", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "minAdj3", "a1", "l", "y8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "q1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd8", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y2"));
            geometry.ShapeTextRectangle.FromString("x2", "q1", "x5", "y6");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx1", "cy1", "x3", "y1"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x5", "y3"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx2", "cy1", "r", "t"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "rh"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx5", "cy4", "x5", "y5"));
            item.Instructions.Add(new PathLine("x5", "y6"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy6", "x2", "y6"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx4", "cy4", "l", "rh"));
            item.Instructions.Add(new PathLine("wd8", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x3", "y7"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x5", "y3"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "y7"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy7", "x3", "y7"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx1", "cy1", "x3", "y1"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x5", "y3"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx2", "cy1", "r", "t"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "rh"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx5", "cy4", "x5", "y5"));
            item.Instructions.Add(new PathLine("x5", "y6"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy6", "x2", "y6"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx4", "cy4", "l", "rh"));
            item.Instructions.Add(new PathLine("wd8", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x2", "y5"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathMove("x5", "y3"));
            item.Instructions.Add(new PathLine("x5", "y5"));
            item.Instructions.Add(new PathMove("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "y7"));
            item.Instructions.Add(new PathMove("x4", "y7"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateEllipseRibbon2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 25000 adj2 75000"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "+- 100000 0 a1"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "*/ q10 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "+- a1 0 q11"));
            geometry.Guides.Add(new ModelShapeGuide("minAdj3", "max 0 q12"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin minAdj3 adj3 a1"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a2 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 wd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x3"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("f1", "*/ 4 dy1 w"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ x3 x3 w"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "+- x3 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("u1", "*/ f1 q2 1"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 u1"));
            geometry.Guides.Add(new ModelShapeGuide("cx1", "*/ x3 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("cu1", "*/ f1 cx1 1"));
            geometry.Guides.Add(new ModelShapeGuide("cy1", "+- b 0 cu1"));
            geometry.Guides.Add(new ModelShapeGuide("cx2", "+- r 0 cx1"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "+- q1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ x2 x2 w"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "+- x2 0 q3"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "*/ f1 q4 1"));
            geometry.Guides.Add(new ModelShapeGuide("u3", "+- q5 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- b 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+- dy1 dy3 u3"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "+- q6 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("cu3", "+- q7 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("cy3", "+- b 0 cu3"));
            geometry.Guides.Add(new ModelShapeGuide("rh", "+- b 0 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "*/ dy1 14 16"));
            geometry.Guides.Add(new ModelShapeGuide("u2", "+/ q8 rh 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 u2"));
            geometry.Guides.Add(new ModelShapeGuide("u5", "+- q5 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- b 0 u5"));
            geometry.Guides.Add(new ModelShapeGuide("u6", "+- u3 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 u6"));
            geometry.Guides.Add(new ModelShapeGuide("cx4", "*/ x2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ f1 cx4 1"));
            geometry.Guides.Add(new ModelShapeGuide("cu4", "+- q9 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("cy4", "+- b 0 cu4"));
            geometry.Guides.Add(new ModelShapeGuide("cx5", "+- r 0 cx4"));
            geometry.Guides.Add(new ModelShapeGuide("cu6", "+- cu3 rh 0"));
            geometry.Guides.Add(new ModelShapeGuide("cy6", "+- b 0 cu6"));
            geometry.Guides.Add(new ModelShapeGuide("u7", "+- u1 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- b 0 u7"));
            geometry.Guides.Add(new ModelShapeGuide("cu7", "+- q1 q1 u7"));
            geometry.Guides.Add(new ModelShapeGuide("cy7", "+- b 0 cu7"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "hc", "rh"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "25000", "100000", "", "", "", "x2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "minAdj3", "a1", "l", "dy1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd8", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "rh"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y2"));
            geometry.ShapeTextRectangle.FromString("x2", "y6", "x5", "rh");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx1", "cy1", "x3", "y1"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x5", "y3"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx2", "cy1", "r", "b"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "q1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx5", "cy4", "x5", "y5"));
            item.Instructions.Add(new PathLine("x5", "y6"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy6", "x2", "y6"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx4", "cy4", "l", "q1"));
            item.Instructions.Add(new PathLine("wd8", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x3", "y7"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x5", "y3"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "y7"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy7", "x3", "y7"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("wd8", "y2"));
            item.Instructions.Add(new PathLine("l", "q1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx4", "cy4", "x2", "y5"));
            item.Instructions.Add(new PathLine("x2", "y6"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy6", "x5", "y6"));
            item.Instructions.Add(new PathLine("x5", "y5"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx5", "cy4", "r", "q1"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx2", "cy1", "x4", "y1"));
            item.Instructions.Add(new PathLine("x5", "y3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cy3", "x2", "y3"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "cx1", "cy1", "l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x2", "y3"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathMove("x5", "y5"));
            item.Instructions.Add(new PathLine("x5", "y3"));
            item.Instructions.Add(new PathMove("x3", "y7"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathMove("x4", "y1"));
            item.Instructions.Add(new PathLine("x4", "y7"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartAlternateProcess()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 ssd6"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 ssd6"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ ssd6 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ssd6"));
            item.Instructions.Add(new PathArc("ssd6", "ssd6", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathArc("ssd6", "ssd6", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("ssd6", "ssd6", "0", "cd4"));
            item.Instructions.Add(new PathLine("ssd6", "b"));
            item.Instructions.Add(new PathArc("ssd6", "ssd6", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartCollate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ShapeTextRectangle.FromString("wd4", "hd4", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("2", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("2", "2"));
            item.Instructions.Add(new PathLine("0", "2"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartConnector()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartDecision()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd4", "hd4", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("2", "1"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartDelay()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartDisplay()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 5 6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd6", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("0", "3"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathArc("1", "3", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("1", "6"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartDocument()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h 17322 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 20172 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "y1");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("21600", "0"));
            item.Instructions.Add(new PathLine("21600", "17322"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "10800", "17322", "10800", "23922", "0", "20172"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartExtract()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ShapeTextRectangle.FromString("wd4", "vc", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "2"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("2", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartInputOutput()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 2 5"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 3 5"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "*/ w 4 5"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "*/ w 9 10"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd10", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "vc"));
            geometry.ShapeTextRectangle.FromString("wd5", "t", "x5", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 5L,
                Height = 5L
            };
            item.Instructions.Add(new PathMove("0", "5"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathLine("4", "5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartInternalStorage()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd8", "hd8", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 1L,
                Height = 1L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("0", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 8L,
                Height = 8L
            };
            item.Instructions.Add(new PathMove("1", "0"));
            item.Instructions.Add(new PathLine("1", "8"));
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("8", "1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                Width = 1L,
                Height = 1L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("0", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartMagneticDisk()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 5 6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "hd3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "hd3", "r", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathArc("3", "1", "cd2", "cd2"));
            item.Instructions.Add(new PathLine("6", "5"));
            item.Instructions.Add(new PathArc("3", "1", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("6", "1"));
            item.Instructions.Add(new PathArc("3", "1", "0", "cd2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathArc("3", "1", "cd2", "cd2"));
            item.Instructions.Add(new PathLine("6", "5"));
            item.Instructions.Add(new PathArc("3", "1", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartMagneticDrum()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 2 3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd6", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("1", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathArc("1", "3", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("1", "6"));
            item.Instructions.Add(new PathArc("1", "3", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("5", "6"));
            item.Instructions.Add(new PathArc("1", "3", "cd4", "cd2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("1", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathArc("1", "3", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("1", "6"));
            item.Instructions.Add(new PathArc("1", "3", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartMagneticTape()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("ang1", "at2 w h"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("hc", "b"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "ang1"));
            item.Instructions.Add(new PathLine("r", "ib"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartManualInput()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "hd10"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "hd5", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 5L,
                Height = 5L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathLine("5", "5"));
            item.Instructions.Add(new PathLine("0", "5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartManualOperation()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 4 5"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 9 10"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd10", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "vc"));
            geometry.ShapeTextRectangle.FromString("wd5", "t", "x3", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 5L,
                Height = 5L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathLine("4", "5"));
            item.Instructions.Add(new PathLine("1", "5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartMerge()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ShapeTextRectangle.FromString("wd4", "t", "x2", "vc");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("2", "0"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartMultidocument()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 3675 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "*/ h 20782 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 9298 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 12286 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "*/ w 18595 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "y2", "x5", "y8");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("0", "20782"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "9298", "23542", "9298", "18022", "18595", "18022"));
            item.Instructions.Add(new PathLine("18595", "3675"));
            item.Instructions.Add(new PathLine("0", "3675"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("1532", "3675"));
            item.Instructions.Add(new PathLine("1532", "1815"));
            item.Instructions.Add(new PathLine("20000", "1815"));
            item.Instructions.Add(new PathLine("20000", "16252"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "19298", "16252", "18595", "16352", "18595", "16352"));
            item.Instructions.Add(new PathLine("18595", "3675"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("2972", "1815"));
            item.Instructions.Add(new PathLine("2972", "0"));
            item.Instructions.Add(new PathLine("21600", "0"));
            item.Instructions.Add(new PathLine("21600", "14392"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "20800", "14392", "20000", "14467", "20000", "14467"));
            item.Instructions.Add(new PathLine("20000", "1815"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("0", "3675"));
            item.Instructions.Add(new PathLine("18595", "3675"));
            item.Instructions.Add(new PathLine("18595", "18022"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "9298", "18022", "9298", "23542", "0", "20782"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("1532", "3675"));
            item.Instructions.Add(new PathLine("1532", "1815"));
            item.Instructions.Add(new PathLine("20000", "1815"));
            item.Instructions.Add(new PathLine("20000", "16252"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "19298", "16252", "18595", "16352", "18595", "16352"));
            item.Instructions.Add(new PathMove("2972", "1815"));
            item.Instructions.Add(new PathLine("2972", "0"));
            item.Instructions.Add(new PathLine("21600", "0"));
            item.Instructions.Add(new PathLine("21600", "14392"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "20800", "14392", "20000", "14467", "20000", "14467"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.None,
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("0", "20782"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "9298", "23542", "9298", "18022", "18595", "18022"));
            item.Instructions.Add(new PathLine("18595", "16352"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "18595", "16352", "19298", "16252", "20000", "16252"));
            item.Instructions.Add(new PathLine("20000", "14467"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "20000", "14467", "20800", "14392", "21600", "14392"));
            item.Instructions.Add(new PathLine("21600", "0"));
            item.Instructions.Add(new PathLine("2972", "0"));
            item.Instructions.Add(new PathLine("2972", "1815"));
            item.Instructions.Add(new PathLine("1532", "1815"));
            item.Instructions.Add(new PathLine("1532", "3675"));
            item.Instructions.Add(new PathLine("0", "3675"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartOfflineStorage()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("wd4", "t", "x4", "vc");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("2", "0"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 5L,
                Height = 5L
            };
            item.Instructions.Add(new PathMove("2", "4"));
            item.Instructions.Add(new PathLine("3", "4"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = true,
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("2", "0"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartOffpageConnector()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h 4 5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "y1");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("10", "0"));
            item.Instructions.Add(new PathLine("10", "8"));
            item.Instructions.Add(new PathLine("5", "10"));
            item.Instructions.Add(new PathLine("0", "8"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartOnlineStorage()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 5 6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ShapeTextRectangle.FromString("wd6", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 6L,
                Height = 6L
            };
            item.Instructions.Add(new PathMove("1", "0"));
            item.Instructions.Add(new PathLine("6", "0"));
            item.Instructions.Add(new PathArc("1", "3", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("1", "6"));
            item.Instructions.Add(new PathArc("1", "3", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartOr()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("hc", "t"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("r", "vc"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartPredefinedProcess()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 7 8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd8", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 1L,
                Height = 1L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("0", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 8L,
                Height = 8L
            };
            item.Instructions.Add(new PathMove("1", "0"));
            item.Instructions.Add(new PathLine("1", "8"));
            item.Instructions.Add(new PathMove("7", "0"));
            item.Instructions.Add(new PathLine("7", "8"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                Width = 1L,
                Height = 1L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("0", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartPreparation()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 4 5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd5", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 10,
                Height = 10
            };
            item.Instructions.Add(new PathMove("0", "5"));
            item.Instructions.Add(new PathLine("2", "0"));
            item.Instructions.Add(new PathLine("8", "0"));
            item.Instructions.Add(new PathLine("10", "5"));
            item.Instructions.Add(new PathLine("8", "10"));
            item.Instructions.Add(new PathLine("2", "10"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartProcess()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 1L,
                Height = 1L
            };
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("1", "1"));
            item.Instructions.Add(new PathLine("0", "1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartPunchedCard()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "hd5", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 5L,
                Height = 5L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("5", "0"));
            item.Instructions.Add(new PathLine("5", "5"));
            item.Instructions.Add(new PathLine("0", "5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartPunchedTape()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 9 10"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 4 5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "hd10"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "hd5", "r", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 20,
                Height = 20
            };
            item.Instructions.Add(new PathMove("0", "2"));
            item.Instructions.Add(new PathArc("5", "2", "cd2", "-10800000"));
            item.Instructions.Add(new PathArc("5", "2", "cd2", "cd2"));
            item.Instructions.Add(new PathLine("20", "18"));
            item.Instructions.Add(new PathArc("5", "2", "0", "-10800000"));
            item.Instructions.Add(new PathArc("5", "2", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartSort()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 3 4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("wd4", "hd4", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false,
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("2", "1"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false,
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("2", "1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                Width = 2L,
                Height = 2L
            };
            item.Instructions.Add(new PathMove("0", "1"));
            item.Instructions.Add(new PathLine("1", "0"));
            item.Instructions.Add(new PathLine("2", "1"));
            item.Instructions.Add(new PathLine("1", "2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartSummingJunction()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("il", "it"));
            item.Instructions.Add(new PathLine("ir", "ib"));
            item.Instructions.Add(new PathMove("ir", "it"));
            item.Instructions.Add(new PathLine("il", "ib"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFlowChartTerminator()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ w 1018 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 20582 21600"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h 3163 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 18437 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("3475", "0"));
            item.Instructions.Add(new PathLine("18125", "0"));
            item.Instructions.Add(new PathArc("3475", "10800", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("3475", "21600"));
            item.Instructions.Add(new PathArc("3475", "10800", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFoldedCorner()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ dy2 1 5"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- x1 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 dy1 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "b"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "b"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathLine("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFrame()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x1", "x1", "x4", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x1", "x1"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("x4", "x1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFunnel()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("d", "*/ ss 1 20"));
            geometry.Guides.Add(new ModelShapeGuide("rw2", "+- wd2 0 d"));
            geometry.Guides.Add(new ModelShapeGuide("rh2", "+- hd4 0 d"));
            geometry.Guides.Add(new ModelShapeGuide("t1", "cos wd2 480000"));
            geometry.Guides.Add(new ModelShapeGuide("t2", "sin hd4 480000"));
            geometry.Guides.Add(new ModelShapeGuide("da", "at2 t1 t2"));
            geometry.Guides.Add(new ModelShapeGuide("2da", "*/ da 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("stAng1", "+- cd2 0 da"));
            geometry.Guides.Add(new ModelShapeGuide("swAng1", "+- cd2 2da 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- cd2 0 2da"));
            geometry.Guides.Add(new ModelShapeGuide("rw3", "*/ wd2 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("rh3", "*/ hd4 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("ct1", "cos hd4 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("st1", "sin wd2 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("m1", "mod ct1 st1 0"));
            geometry.Guides.Add(new ModelShapeGuide("n1", "*/ wd2 hd4 m1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos n1 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin n1 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- hd4 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("ct3", "cos rh3 da"));
            geometry.Guides.Add(new ModelShapeGuide("st3", "sin rw3 da"));
            geometry.Guides.Add(new ModelShapeGuide("m3", "mod ct3 st3 0"));
            geometry.Guides.Add(new ModelShapeGuide("n3", "*/ rw3 rh3 m3"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "cos n3 da"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "sin n3 da"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("vc3", "+- b 0 rh3"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc3 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- wd2 0 rw2"));
            geometry.Guides.Add(new ModelShapeGuide("cd", "*/ cd2 2 1"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd4", "stAng1", "swAng1"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathArc("rw3", "rh3", "da", "swAng3"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x2", "hd4"));
            item.Instructions.Add(new PathArc("rw2", "rh2", "cd2", "-21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateGear6()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 15000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 3526"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 20000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 5358"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("lFD", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("l2", "*/ lFD 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("l3", "+- th2 l2 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh", "+- hd2 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rw", "+- wd2 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "+- rw 0 rh"));
            geometry.Guides.Add(new ModelShapeGuide("maxr", "?: dr rh rw"));
            geometry.Guides.Add(new ModelShapeGuide("ha", "at2 maxr l3"));
            geometry.Guides.Add(new ModelShapeGuide("aA1", "+- 19800000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD1", "+- 19800000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta11", "cos rw aA1"));
            geometry.Guides.Add(new ModelShapeGuide("ta12", "sin rh aA1"));
            geometry.Guides.Add(new ModelShapeGuide("bA1", "at2 ta11 ta12"));
            geometry.Guides.Add(new ModelShapeGuide("cta1", "cos rh bA1"));
            geometry.Guides.Add(new ModelShapeGuide("sta1", "sin rw bA1"));
            geometry.Guides.Add(new ModelShapeGuide("ma1", "mod cta1 sta1 0"));
            geometry.Guides.Add(new ModelShapeGuide("na1", "*/ rw rh ma1"));
            geometry.Guides.Add(new ModelShapeGuide("dxa1", "cos na1 bA1"));
            geometry.Guides.Add(new ModelShapeGuide("dya1", "sin na1 bA1"));
            geometry.Guides.Add(new ModelShapeGuide("xA1", "+- hc dxa1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA1", "+- vc dya1 0"));
            geometry.Guides.Add(new ModelShapeGuide("td11", "cos rw aD1"));
            geometry.Guides.Add(new ModelShapeGuide("td12", "sin rh aD1"));
            geometry.Guides.Add(new ModelShapeGuide("bD1", "at2 td11 td12"));
            geometry.Guides.Add(new ModelShapeGuide("ctd1", "cos rh bD1"));
            geometry.Guides.Add(new ModelShapeGuide("std1", "sin rw bD1"));
            geometry.Guides.Add(new ModelShapeGuide("md1", "mod ctd1 std1 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd1", "*/ rw rh md1"));
            geometry.Guides.Add(new ModelShapeGuide("dxd1", "cos nd1 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("dyd1", "sin nd1 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("xD1", "+- hc dxd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD1", "+- vc dyd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAD1", "+- xA1 0 xD1"));
            geometry.Guides.Add(new ModelShapeGuide("yAD1", "+- yA1 0 yD1"));
            geometry.Guides.Add(new ModelShapeGuide("lAD1", "mod xAD1 yAD1 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "at2 yAD1 xAD1"));
            geometry.Guides.Add(new ModelShapeGuide("dxF1", "sin lFD a1"));
            geometry.Guides.Add(new ModelShapeGuide("dyF1", "cos lFD a1"));
            geometry.Guides.Add(new ModelShapeGuide("xF1", "+- xD1 dxF1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF1", "+- yD1 dyF1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE1", "+- xA1 0 dxF1"));
            geometry.Guides.Add(new ModelShapeGuide("yE1", "+- yA1 0 dyF1"));
            geometry.Guides.Add(new ModelShapeGuide("yC1t", "sin th a1"));
            geometry.Guides.Add(new ModelShapeGuide("xC1t", "cos th a1"));
            geometry.Guides.Add(new ModelShapeGuide("yC1", "+- yF1 yC1t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xC1", "+- xF1 0 xC1t"));
            geometry.Guides.Add(new ModelShapeGuide("yB1", "+- yE1 yC1t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB1", "+- xE1 0 xC1t"));
            geometry.Guides.Add(new ModelShapeGuide("aD6", "+- 3cd4 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td61", "cos rw aD6"));
            geometry.Guides.Add(new ModelShapeGuide("td62", "sin rh aD6"));
            geometry.Guides.Add(new ModelShapeGuide("bD6", "at2 td61 td62"));
            geometry.Guides.Add(new ModelShapeGuide("ctd6", "cos rh bD6"));
            geometry.Guides.Add(new ModelShapeGuide("std6", "sin rw bD6"));
            geometry.Guides.Add(new ModelShapeGuide("md6", "mod ctd6 std6 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd6", "*/ rw rh md6"));
            geometry.Guides.Add(new ModelShapeGuide("dxd6", "cos nd6 bD6"));
            geometry.Guides.Add(new ModelShapeGuide("dyd6", "sin nd6 bD6"));
            geometry.Guides.Add(new ModelShapeGuide("xD6", "+- hc dxd6 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD6", "+- vc dyd6 0"));
            geometry.Guides.Add(new ModelShapeGuide("xA6", "+- hc 0 dxd6"));
            geometry.Guides.Add(new ModelShapeGuide("xF6", "+- xD6 0 lFD"));
            geometry.Guides.Add(new ModelShapeGuide("xE6", "+- xA6 lFD 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC6", "+- yD6 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("swAng1", "+- bA1 0 bD6"));
            geometry.Guides.Add(new ModelShapeGuide("aA2", "+- 1800000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD2", "+- 1800000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta21", "cos rw aA2"));
            geometry.Guides.Add(new ModelShapeGuide("ta22", "sin rh aA2"));
            geometry.Guides.Add(new ModelShapeGuide("bA2", "at2 ta21 ta22"));
            geometry.Guides.Add(new ModelShapeGuide("yA2", "+- h 0 yD1"));
            geometry.Guides.Add(new ModelShapeGuide("td21", "cos rw aD2"));
            geometry.Guides.Add(new ModelShapeGuide("td22", "sin rh aD2"));
            geometry.Guides.Add(new ModelShapeGuide("bD2", "at2 td21 td22"));
            geometry.Guides.Add(new ModelShapeGuide("yD2", "+- h 0 yA1"));
            geometry.Guides.Add(new ModelShapeGuide("yC2", "+- h 0 yB1"));
            geometry.Guides.Add(new ModelShapeGuide("yB2", "+- h 0 yC1"));
            geometry.Guides.Add(new ModelShapeGuide("xB2", "val xC1"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- bA2 0 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("aD3", "+- cd4 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td31", "cos rw aD3"));
            geometry.Guides.Add(new ModelShapeGuide("td32", "sin rh aD3"));
            geometry.Guides.Add(new ModelShapeGuide("bD3", "at2 td31 td32"));
            geometry.Guides.Add(new ModelShapeGuide("yD3", "+- h 0 yD6"));
            geometry.Guides.Add(new ModelShapeGuide("yB3", "+- h 0 yC6"));
            geometry.Guides.Add(new ModelShapeGuide("aD4", "+- 9000000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td41", "cos rw aD4"));
            geometry.Guides.Add(new ModelShapeGuide("td42", "sin rh aD4"));
            geometry.Guides.Add(new ModelShapeGuide("bD4", "at2 td41 td42"));
            geometry.Guides.Add(new ModelShapeGuide("xD4", "+- w 0 xD1"));
            geometry.Guides.Add(new ModelShapeGuide("xC4", "+- w 0 xC1"));
            geometry.Guides.Add(new ModelShapeGuide("xB4", "+- w 0 xB1"));
            geometry.Guides.Add(new ModelShapeGuide("aD5", "+- 12600000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td51", "cos rw aD5"));
            geometry.Guides.Add(new ModelShapeGuide("td52", "sin rh aD5"));
            geometry.Guides.Add(new ModelShapeGuide("bD5", "at2 td51 td52"));
            geometry.Guides.Add(new ModelShapeGuide("xD5", "+- w 0 xA1"));
            geometry.Guides.Add(new ModelShapeGuide("xC5", "+- w 0 xB1"));
            geometry.Guides.Add(new ModelShapeGuide("xB5", "+- w 0 xC1"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn1", "+/ xB1 xC1 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn1", "+/ yB1 yC1 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn2", "+- b 0 yCxn1"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn4", "+/ r 0 xCxn1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "20000", "xD6", "yD6"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "5358", "", "", "", "xA6", "yD6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("19800000", "xCxn1", "yCxn1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("1800000", "xCxn1", "yCxn2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "yB3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("9000000", "xCxn4", "yCxn2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("12600000", "xCxn4", "yCxn1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "yC6"));
            geometry.ShapeTextRectangle.FromString("xD5", "yA1", "xA1", "yD2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xA1", "yA1"));
            item.Instructions.Add(new PathLine("xB1", "yB1"));
            item.Instructions.Add(new PathLine("xC1", "yC1"));
            item.Instructions.Add(new PathLine("xD1", "yD1"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD1", "swAng2"));
            item.Instructions.Add(new PathLine("xC1", "yB2"));
            item.Instructions.Add(new PathLine("xB1", "yC2"));
            item.Instructions.Add(new PathLine("xA1", "yD2"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD2", "swAng1"));
            item.Instructions.Add(new PathLine("xF6", "yB3"));
            item.Instructions.Add(new PathLine("xE6", "yB3"));
            item.Instructions.Add(new PathLine("xA6", "yD3"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD3", "swAng1"));
            item.Instructions.Add(new PathLine("xB4", "yC2"));
            item.Instructions.Add(new PathLine("xC4", "yB2"));
            item.Instructions.Add(new PathLine("xD4", "yA2"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD4", "swAng2"));
            item.Instructions.Add(new PathLine("xB5", "yC1"));
            item.Instructions.Add(new PathLine("xC5", "yB1"));
            item.Instructions.Add(new PathLine("xD5", "yA1"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD5", "swAng1"));
            item.Instructions.Add(new PathLine("xE6", "yC6"));
            item.Instructions.Add(new PathLine("xF6", "yC6"));
            item.Instructions.Add(new PathLine("xD6", "yD6"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD6", "swAng1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateGear9()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 10000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 1763"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 20000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 2679"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("lFD", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("l2", "*/ lFD 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("l3", "+- th2 l2 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh", "+- hd2 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rw", "+- wd2 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "+- rw 0 rh"));
            geometry.Guides.Add(new ModelShapeGuide("maxr", "?: dr rh rw"));
            geometry.Guides.Add(new ModelShapeGuide("ha", "at2 maxr l3"));
            geometry.Guides.Add(new ModelShapeGuide("aA1", "+- 18600000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD1", "+- 18600000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta11", "cos rw aA1"));
            geometry.Guides.Add(new ModelShapeGuide("ta12", "sin rh aA1"));
            geometry.Guides.Add(new ModelShapeGuide("bA1", "at2 ta11 ta12"));
            geometry.Guides.Add(new ModelShapeGuide("cta1", "cos rh bA1"));
            geometry.Guides.Add(new ModelShapeGuide("sta1", "sin rw bA1"));
            geometry.Guides.Add(new ModelShapeGuide("ma1", "mod cta1 sta1 0"));
            geometry.Guides.Add(new ModelShapeGuide("na1", "*/ rw rh ma1"));
            geometry.Guides.Add(new ModelShapeGuide("dxa1", "cos na1 bA1"));
            geometry.Guides.Add(new ModelShapeGuide("dya1", "sin na1 bA1"));
            geometry.Guides.Add(new ModelShapeGuide("xA1", "+- hc dxa1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA1", "+- vc dya1 0"));
            geometry.Guides.Add(new ModelShapeGuide("td11", "cos rw aD1"));
            geometry.Guides.Add(new ModelShapeGuide("td12", "sin rh aD1"));
            geometry.Guides.Add(new ModelShapeGuide("bD1", "at2 td11 td12"));
            geometry.Guides.Add(new ModelShapeGuide("ctd1", "cos rh bD1"));
            geometry.Guides.Add(new ModelShapeGuide("std1", "sin rw bD1"));
            geometry.Guides.Add(new ModelShapeGuide("md1", "mod ctd1 std1 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd1", "*/ rw rh md1"));
            geometry.Guides.Add(new ModelShapeGuide("dxd1", "cos nd1 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("dyd1", "sin nd1 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("xD1", "+- hc dxd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD1", "+- vc dyd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAD1", "+- xA1 0 xD1"));
            geometry.Guides.Add(new ModelShapeGuide("yAD1", "+- yA1 0 yD1"));
            geometry.Guides.Add(new ModelShapeGuide("lAD1", "mod xAD1 yAD1 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "at2 yAD1 xAD1"));
            geometry.Guides.Add(new ModelShapeGuide("dxF1", "sin lFD a1"));
            geometry.Guides.Add(new ModelShapeGuide("dyF1", "cos lFD a1"));
            geometry.Guides.Add(new ModelShapeGuide("xF1", "+- xD1 dxF1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF1", "+- yD1 dyF1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE1", "+- xA1 0 dxF1"));
            geometry.Guides.Add(new ModelShapeGuide("yE1", "+- yA1 0 dyF1"));
            geometry.Guides.Add(new ModelShapeGuide("yC1t", "sin th a1"));
            geometry.Guides.Add(new ModelShapeGuide("xC1t", "cos th a1"));
            geometry.Guides.Add(new ModelShapeGuide("yC1", "+- yF1 yC1t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xC1", "+- xF1 0 xC1t"));
            geometry.Guides.Add(new ModelShapeGuide("yB1", "+- yE1 yC1t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB1", "+- xE1 0 xC1t"));
            geometry.Guides.Add(new ModelShapeGuide("aA2", "+- 21000000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD2", "+- 21000000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta21", "cos rw aA2"));
            geometry.Guides.Add(new ModelShapeGuide("ta22", "sin rh aA2"));
            geometry.Guides.Add(new ModelShapeGuide("bA2", "at2 ta21 ta22"));
            geometry.Guides.Add(new ModelShapeGuide("cta2", "cos rh bA2"));
            geometry.Guides.Add(new ModelShapeGuide("sta2", "sin rw bA2"));
            geometry.Guides.Add(new ModelShapeGuide("ma2", "mod cta2 sta2 0"));
            geometry.Guides.Add(new ModelShapeGuide("na2", "*/ rw rh ma2"));
            geometry.Guides.Add(new ModelShapeGuide("dxa2", "cos na2 bA2"));
            geometry.Guides.Add(new ModelShapeGuide("dya2", "sin na2 bA2"));
            geometry.Guides.Add(new ModelShapeGuide("xA2", "+- hc dxa2 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA2", "+- vc dya2 0"));
            geometry.Guides.Add(new ModelShapeGuide("td21", "cos rw aD2"));
            geometry.Guides.Add(new ModelShapeGuide("td22", "sin rh aD2"));
            geometry.Guides.Add(new ModelShapeGuide("bD2", "at2 td21 td22"));
            geometry.Guides.Add(new ModelShapeGuide("ctd2", "cos rh bD2"));
            geometry.Guides.Add(new ModelShapeGuide("std2", "sin rw bD2"));
            geometry.Guides.Add(new ModelShapeGuide("md2", "mod ctd2 std2 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd2", "*/ rw rh md2"));
            geometry.Guides.Add(new ModelShapeGuide("dxd2", "cos nd2 bD2"));
            geometry.Guides.Add(new ModelShapeGuide("dyd2", "sin nd2 bD2"));
            geometry.Guides.Add(new ModelShapeGuide("xD2", "+- hc dxd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD2", "+- vc dyd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAD2", "+- xA2 0 xD2"));
            geometry.Guides.Add(new ModelShapeGuide("yAD2", "+- yA2 0 yD2"));
            geometry.Guides.Add(new ModelShapeGuide("lAD2", "mod xAD2 yAD2 0"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "at2 yAD2 xAD2"));
            geometry.Guides.Add(new ModelShapeGuide("dxF2", "sin lFD a2"));
            geometry.Guides.Add(new ModelShapeGuide("dyF2", "cos lFD a2"));
            geometry.Guides.Add(new ModelShapeGuide("xF2", "+- xD2 dxF2 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF2", "+- yD2 dyF2 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE2", "+- xA2 0 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("yE2", "+- yA2 0 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("yC2t", "sin th a2"));
            geometry.Guides.Add(new ModelShapeGuide("xC2t", "cos th a2"));
            geometry.Guides.Add(new ModelShapeGuide("yC2", "+- yF2 yC2t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xC2", "+- xF2 0 xC2t"));
            geometry.Guides.Add(new ModelShapeGuide("yB2", "+- yE2 yC2t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB2", "+- xE2 0 xC2t"));
            geometry.Guides.Add(new ModelShapeGuide("swAng1", "+- bA2 0 bD1"));
            geometry.Guides.Add(new ModelShapeGuide("aA3", "+- 1800000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD3", "+- 1800000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta31", "cos rw aA3"));
            geometry.Guides.Add(new ModelShapeGuide("ta32", "sin rh aA3"));
            geometry.Guides.Add(new ModelShapeGuide("bA3", "at2 ta31 ta32"));
            geometry.Guides.Add(new ModelShapeGuide("cta3", "cos rh bA3"));
            geometry.Guides.Add(new ModelShapeGuide("sta3", "sin rw bA3"));
            geometry.Guides.Add(new ModelShapeGuide("ma3", "mod cta3 sta3 0"));
            geometry.Guides.Add(new ModelShapeGuide("na3", "*/ rw rh ma3"));
            geometry.Guides.Add(new ModelShapeGuide("dxa3", "cos na3 bA3"));
            geometry.Guides.Add(new ModelShapeGuide("dya3", "sin na3 bA3"));
            geometry.Guides.Add(new ModelShapeGuide("xA3", "+- hc dxa3 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA3", "+- vc dya3 0"));
            geometry.Guides.Add(new ModelShapeGuide("td31", "cos rw aD3"));
            geometry.Guides.Add(new ModelShapeGuide("td32", "sin rh aD3"));
            geometry.Guides.Add(new ModelShapeGuide("bD3", "at2 td31 td32"));
            geometry.Guides.Add(new ModelShapeGuide("ctd3", "cos rh bD3"));
            geometry.Guides.Add(new ModelShapeGuide("std3", "sin rw bD3"));
            geometry.Guides.Add(new ModelShapeGuide("md3", "mod ctd3 std3 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd3", "*/ rw rh md3"));
            geometry.Guides.Add(new ModelShapeGuide("dxd3", "cos nd3 bD3"));
            geometry.Guides.Add(new ModelShapeGuide("dyd3", "sin nd3 bD3"));
            geometry.Guides.Add(new ModelShapeGuide("xD3", "+- hc dxd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD3", "+- vc dyd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAD3", "+- xA3 0 xD3"));
            geometry.Guides.Add(new ModelShapeGuide("yAD3", "+- yA3 0 yD3"));
            geometry.Guides.Add(new ModelShapeGuide("lAD3", "mod xAD3 yAD3 0"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "at2 yAD3 xAD3"));
            geometry.Guides.Add(new ModelShapeGuide("dxF3", "sin lFD a3"));
            geometry.Guides.Add(new ModelShapeGuide("dyF3", "cos lFD a3"));
            geometry.Guides.Add(new ModelShapeGuide("xF3", "+- xD3 dxF3 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF3", "+- yD3 dyF3 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE3", "+- xA3 0 dxF3"));
            geometry.Guides.Add(new ModelShapeGuide("yE3", "+- yA3 0 dyF3"));
            geometry.Guides.Add(new ModelShapeGuide("yC3t", "sin th a3"));
            geometry.Guides.Add(new ModelShapeGuide("xC3t", "cos th a3"));
            geometry.Guides.Add(new ModelShapeGuide("yC3", "+- yF3 yC3t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xC3", "+- xF3 0 xC3t"));
            geometry.Guides.Add(new ModelShapeGuide("yB3", "+- yE3 yC3t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB3", "+- xE3 0 xC3t"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- bA3 0 bD2"));
            geometry.Guides.Add(new ModelShapeGuide("aA4", "+- 4200000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD4", "+- 4200000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta41", "cos rw aA4"));
            geometry.Guides.Add(new ModelShapeGuide("ta42", "sin rh aA4"));
            geometry.Guides.Add(new ModelShapeGuide("bA4", "at2 ta41 ta42"));
            geometry.Guides.Add(new ModelShapeGuide("cta4", "cos rh bA4"));
            geometry.Guides.Add(new ModelShapeGuide("sta4", "sin rw bA4"));
            geometry.Guides.Add(new ModelShapeGuide("ma4", "mod cta4 sta4 0"));
            geometry.Guides.Add(new ModelShapeGuide("na4", "*/ rw rh ma4"));
            geometry.Guides.Add(new ModelShapeGuide("dxa4", "cos na4 bA4"));
            geometry.Guides.Add(new ModelShapeGuide("dya4", "sin na4 bA4"));
            geometry.Guides.Add(new ModelShapeGuide("xA4", "+- hc dxa4 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA4", "+- vc dya4 0"));
            geometry.Guides.Add(new ModelShapeGuide("td41", "cos rw aD4"));
            geometry.Guides.Add(new ModelShapeGuide("td42", "sin rh aD4"));
            geometry.Guides.Add(new ModelShapeGuide("bD4", "at2 td41 td42"));
            geometry.Guides.Add(new ModelShapeGuide("ctd4", "cos rh bD4"));
            geometry.Guides.Add(new ModelShapeGuide("std4", "sin rw bD4"));
            geometry.Guides.Add(new ModelShapeGuide("md4", "mod ctd4 std4 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd4", "*/ rw rh md4"));
            geometry.Guides.Add(new ModelShapeGuide("dxd4", "cos nd4 bD4"));
            geometry.Guides.Add(new ModelShapeGuide("dyd4", "sin nd4 bD4"));
            geometry.Guides.Add(new ModelShapeGuide("xD4", "+- hc dxd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD4", "+- vc dyd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAD4", "+- xA4 0 xD4"));
            geometry.Guides.Add(new ModelShapeGuide("yAD4", "+- yA4 0 yD4"));
            geometry.Guides.Add(new ModelShapeGuide("lAD4", "mod xAD4 yAD4 0"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "at2 yAD4 xAD4"));
            geometry.Guides.Add(new ModelShapeGuide("dxF4", "sin lFD a4"));
            geometry.Guides.Add(new ModelShapeGuide("dyF4", "cos lFD a4"));
            geometry.Guides.Add(new ModelShapeGuide("xF4", "+- xD4 dxF4 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF4", "+- yD4 dyF4 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE4", "+- xA4 0 dxF4"));
            geometry.Guides.Add(new ModelShapeGuide("yE4", "+- yA4 0 dyF4"));
            geometry.Guides.Add(new ModelShapeGuide("yC4t", "sin th a4"));
            geometry.Guides.Add(new ModelShapeGuide("xC4t", "cos th a4"));
            geometry.Guides.Add(new ModelShapeGuide("yC4", "+- yF4 yC4t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xC4", "+- xF4 0 xC4t"));
            geometry.Guides.Add(new ModelShapeGuide("yB4", "+- yE4 yC4t 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB4", "+- xE4 0 xC4t"));
            geometry.Guides.Add(new ModelShapeGuide("swAng3", "+- bA4 0 bD3"));
            geometry.Guides.Add(new ModelShapeGuide("aA5", "+- 6600000 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD5", "+- 6600000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta51", "cos rw aA5"));
            geometry.Guides.Add(new ModelShapeGuide("ta52", "sin rh aA5"));
            geometry.Guides.Add(new ModelShapeGuide("bA5", "at2 ta51 ta52"));
            geometry.Guides.Add(new ModelShapeGuide("td51", "cos rw aD5"));
            geometry.Guides.Add(new ModelShapeGuide("td52", "sin rh aD5"));
            geometry.Guides.Add(new ModelShapeGuide("bD5", "at2 td51 td52"));
            geometry.Guides.Add(new ModelShapeGuide("xD5", "+- w 0 xA4"));
            geometry.Guides.Add(new ModelShapeGuide("xC5", "+- w 0 xB4"));
            geometry.Guides.Add(new ModelShapeGuide("xB5", "+- w 0 xC4"));
            geometry.Guides.Add(new ModelShapeGuide("swAng4", "+- bA5 0 bD4"));
            geometry.Guides.Add(new ModelShapeGuide("aD6", "+- 9000000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td61", "cos rw aD6"));
            geometry.Guides.Add(new ModelShapeGuide("td62", "sin rh aD6"));
            geometry.Guides.Add(new ModelShapeGuide("bD6", "at2 td61 td62"));
            geometry.Guides.Add(new ModelShapeGuide("xD6", "+- w 0 xA3"));
            geometry.Guides.Add(new ModelShapeGuide("xC6", "+- w 0 xB3"));
            geometry.Guides.Add(new ModelShapeGuide("xB6", "+- w 0 xC3"));
            geometry.Guides.Add(new ModelShapeGuide("aD7", "+- 11400000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td71", "cos rw aD7"));
            geometry.Guides.Add(new ModelShapeGuide("td72", "sin rh aD7"));
            geometry.Guides.Add(new ModelShapeGuide("bD7", "at2 td71 td72"));
            geometry.Guides.Add(new ModelShapeGuide("xD7", "+- w 0 xA2"));
            geometry.Guides.Add(new ModelShapeGuide("xC7", "+- w 0 xB2"));
            geometry.Guides.Add(new ModelShapeGuide("xB7", "+- w 0 xC2"));
            geometry.Guides.Add(new ModelShapeGuide("aD8", "+- 13800000 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td81", "cos rw aD8"));
            geometry.Guides.Add(new ModelShapeGuide("td82", "sin rh aD8"));
            geometry.Guides.Add(new ModelShapeGuide("bD8", "at2 td81 td82"));
            geometry.Guides.Add(new ModelShapeGuide("xA8", "+- w 0 xD1"));
            geometry.Guides.Add(new ModelShapeGuide("xD8", "+- w 0 xA1"));
            geometry.Guides.Add(new ModelShapeGuide("xC8", "+- w 0 xB1"));
            geometry.Guides.Add(new ModelShapeGuide("xB8", "+- w 0 xC1"));
            geometry.Guides.Add(new ModelShapeGuide("aA9", "+- 3cd4 0 ha"));
            geometry.Guides.Add(new ModelShapeGuide("aD9", "+- 3cd4 ha 0"));
            geometry.Guides.Add(new ModelShapeGuide("td91", "cos rw aD9"));
            geometry.Guides.Add(new ModelShapeGuide("td92", "sin rh aD9"));
            geometry.Guides.Add(new ModelShapeGuide("bD9", "at2 td91 td92"));
            geometry.Guides.Add(new ModelShapeGuide("ctd9", "cos rh bD9"));
            geometry.Guides.Add(new ModelShapeGuide("std9", "sin rw bD9"));
            geometry.Guides.Add(new ModelShapeGuide("md9", "mod ctd9 std9 0"));
            geometry.Guides.Add(new ModelShapeGuide("nd9", "*/ rw rh md9"));
            geometry.Guides.Add(new ModelShapeGuide("dxd9", "cos nd9 bD9"));
            geometry.Guides.Add(new ModelShapeGuide("dyd9", "sin nd9 bD9"));
            geometry.Guides.Add(new ModelShapeGuide("xD9", "+- hc dxd9 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD9", "+- vc dyd9 0"));
            geometry.Guides.Add(new ModelShapeGuide("ta91", "cos rw aA9"));
            geometry.Guides.Add(new ModelShapeGuide("ta92", "sin rh aA9"));
            geometry.Guides.Add(new ModelShapeGuide("bA9", "at2 ta91 ta92"));
            geometry.Guides.Add(new ModelShapeGuide("xA9", "+- hc 0 dxd9"));
            geometry.Guides.Add(new ModelShapeGuide("xF9", "+- xD9 0 lFD"));
            geometry.Guides.Add(new ModelShapeGuide("xE9", "+- xA9 lFD 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC9", "+- yD9 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("swAng5", "+- bA9 0 bD8"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn1", "+/ xB1 xC1 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn1", "+/ yB1 yC1 2"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn2", "+/ xB2 xC2 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn2", "+/ yB2 yC2 2"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn3", "+/ xB3 xC3 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn3", "+/ yB3 yC3 2"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn4", "+/ xB4 xC4 2"));
            geometry.Guides.Add(new ModelShapeGuide("yCxn4", "+/ yB4 yC4 2"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn5", "+/ r 0 xCxn4"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn6", "+/ r 0 xCxn3"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn7", "+/ r 0 xCxn2"));
            geometry.Guides.Add(new ModelShapeGuide("xCxn8", "+/ r 0 xCxn1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "20000", "xD9", "yD9"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "2679", "", "", "", "xA9", "yD9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("18600000", "xCxn1", "yCxn1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("21000000", "xCxn2", "yCxn2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("1800000", "xCxn3", "yCxn3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("4200000", "xCxn4", "yCxn4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("6600000", "xCxn5", "yCxn4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("9000000", "xCxn6", "yCxn3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("11400000", "xCxn7", "yCxn2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("13800000", "xCxn8", "yCxn1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "yC9"));
            geometry.ShapeTextRectangle.FromString("xA8", "yD1", "xD1", "yD3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xA1", "yA1"));
            item.Instructions.Add(new PathLine("xB1", "yB1"));
            item.Instructions.Add(new PathLine("xC1", "yC1"));
            item.Instructions.Add(new PathLine("xD1", "yD1"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD1", "swAng1"));
            item.Instructions.Add(new PathLine("xB2", "yB2"));
            item.Instructions.Add(new PathLine("xC2", "yC2"));
            item.Instructions.Add(new PathLine("xD2", "yD2"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD2", "swAng2"));
            item.Instructions.Add(new PathLine("xB3", "yB3"));
            item.Instructions.Add(new PathLine("xC3", "yC3"));
            item.Instructions.Add(new PathLine("xD3", "yD3"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD3", "swAng3"));
            item.Instructions.Add(new PathLine("xB4", "yB4"));
            item.Instructions.Add(new PathLine("xC4", "yC4"));
            item.Instructions.Add(new PathLine("xD4", "yD4"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD4", "swAng4"));
            item.Instructions.Add(new PathLine("xB5", "yC4"));
            item.Instructions.Add(new PathLine("xC5", "yB4"));
            item.Instructions.Add(new PathLine("xD5", "yA4"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD5", "swAng3"));
            item.Instructions.Add(new PathLine("xB6", "yC3"));
            item.Instructions.Add(new PathLine("xC6", "yB3"));
            item.Instructions.Add(new PathLine("xD6", "yA3"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD6", "swAng2"));
            item.Instructions.Add(new PathLine("xB7", "yC2"));
            item.Instructions.Add(new PathLine("xC7", "yB2"));
            item.Instructions.Add(new PathLine("xD7", "yA2"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD7", "swAng1"));
            item.Instructions.Add(new PathLine("xB8", "yC1"));
            item.Instructions.Add(new PathLine("xC8", "yB1"));
            item.Instructions.Add(new PathLine("xD8", "yA1"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD8", "swAng5"));
            item.Instructions.Add(new PathLine("xE9", "yC9"));
            item.Instructions.Add(new PathLine("xF9", "yC9"));
            item.Instructions.Add(new PathLine("xD9", "yD9"));
            item.Instructions.Add(new PathArc("rw", "rh", "bD9", "swAng5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHalfFrame()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 33333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 33333"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("g1", "*/ h x1 w"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "+- h 0 g1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ 100000 g2 ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ y1 w h"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ x1 h w"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("cx1", "*/ x1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("cy1", "+/ y2 b 2"));
            geometry.Guides.Add(new ModelShapeGuide("cx2", "+/ x2 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("cy2", "*/ y1 1 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "cx2", "cy2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "cx1", "cy1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHeart()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 49 48"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w 10 48"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t 0 hd3"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ w 1 6"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 5 6"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 2 3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "hd4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ShapeTextRectangle.FromString("il", "hd4", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("hc", "hd4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y1", "x4", "hd4", "hc", "b"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "hd4", "x2", "y1", "hc", "hd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHeptagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 102572"));
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 105210"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("svc", "*/ vc  vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ swd2 97493 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ swd2 78183 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ swd2 43388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ shd2 62349 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ shd2 22252 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ shd2 90097 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- svc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- svc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- svc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x5", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("x2", "y1", "x5", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHexagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 115470"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin shd2 3600000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ maxAdj -1 2"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "+- a q1 0"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "?: q2 4 2"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "?: q2 3 2"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "?: q2 q1 0"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "+/ a q5 q1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "*/ q6 q4 -1"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "+- q3 q7 0"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ w q8 24"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h q8 24"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 it"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "maxAdj", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y1"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHomePlate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+/ x1 r 2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ x1 1 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "maxAdj", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHorizontalScroll()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 25000"));
            geometry.Guides.Add(new ModelShapeGuide("ch", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ch2", "*/ ch 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ch4", "*/ ch 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- ch ch2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- ch ch 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 ch"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- b 0 ch2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y6 0 ch2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 ch"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 ch2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "25000", "", "", "", "ch", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "ch"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("ch", "ch", "x4", "y6");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathLine("x4", "ch2"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "0", "cd2"));
            item.Instructions.Add(new PathLine("x3", "ch"));
            item.Instructions.Add(new PathLine("ch2", "ch"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("l", "y7"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd2", "-10800000"));
            item.Instructions.Add(new PathLine("ch", "y6"));
            item.Instructions.Add(new PathLine("x4", "y6"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ch2", "y4"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "0", "-10800000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ch2", "y4"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "0", "-10800000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x4", "ch"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-16200000"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd2", "-10800000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "y3"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x3", "ch"));
            item.Instructions.Add(new PathLine("x3", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd2", "cd2"));
            item.Instructions.Add(new PathLine("r", "y5"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathLine("ch", "y6"));
            item.Instructions.Add(new PathLine("ch", "y7"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x3", "ch"));
            item.Instructions.Add(new PathLine("x4", "ch"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathMove("x4", "ch"));
            item.Instructions.Add(new PathLine("x4", "ch2"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "0", "cd2"));
            item.Instructions.Add(new PathMove("ch2", "y4"));
            item.Instructions.Add(new PathLine("ch2", "y3"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd2", "cd2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd2"));
            item.Instructions.Add(new PathMove("ch", "y3"));
            item.Instructions.Add(new PathLine("ch", "y6"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateIrregularSeal1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x5", "*/ w 4627 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x12", "*/ w 8485 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x21", "*/ w 16702 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x24", "*/ w 14522 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 6320 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "*/ h 8615 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y9", "*/ h 13937 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y18", "*/ h 13290 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x24", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x12", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y18"));
            geometry.ShapeTextRectangle.FromString("x5", "y3", "x21", "y9");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("10800", "5800"));
            item.Instructions.Add(new PathLine("14522", "0"));
            item.Instructions.Add(new PathLine("14155", "5325"));
            item.Instructions.Add(new PathLine("18380", "4457"));
            item.Instructions.Add(new PathLine("16702", "7315"));
            item.Instructions.Add(new PathLine("21097", "8137"));
            item.Instructions.Add(new PathLine("17607", "10475"));
            item.Instructions.Add(new PathLine("21600", "13290"));
            item.Instructions.Add(new PathLine("16837", "12942"));
            item.Instructions.Add(new PathLine("18145", "18095"));
            item.Instructions.Add(new PathLine("14020", "14457"));
            item.Instructions.Add(new PathLine("13247", "19737"));
            item.Instructions.Add(new PathLine("10532", "14935"));
            item.Instructions.Add(new PathLine("8485", "21600"));
            item.Instructions.Add(new PathLine("7715", "15627"));
            item.Instructions.Add(new PathLine("4762", "17617"));
            item.Instructions.Add(new PathLine("5667", "13937"));
            item.Instructions.Add(new PathLine("135", "14587"));
            item.Instructions.Add(new PathLine("3722", "11775"));
            item.Instructions.Add(new PathLine("0", "8615"));
            item.Instructions.Add(new PathLine("4627", "7617"));
            item.Instructions.Add(new PathLine("370", "2295"));
            item.Instructions.Add(new PathLine("7312", "6320"));
            item.Instructions.Add(new PathLine("8352", "2295"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateIrregularSeal2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 9722 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "*/ w 5372 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x16", "*/ w 11612 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x19", "*/ w 14640 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 1887 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 6382 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "*/ h 12877 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y14", "*/ h 19712 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y16", "*/ h 18842 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y17", "*/ h 15935 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y24", "*/ h 6645 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y8"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x16", "y16"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y24"));
            geometry.ShapeTextRectangle.FromString("x5", "y3", "x19", "y17");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("11462", "4342"));
            item.Instructions.Add(new PathLine("14790", "0"));
            item.Instructions.Add(new PathLine("14525", "5777"));
            item.Instructions.Add(new PathLine("18007", "3172"));
            item.Instructions.Add(new PathLine("16380", "6532"));
            item.Instructions.Add(new PathLine("21600", "6645"));
            item.Instructions.Add(new PathLine("16985", "9402"));
            item.Instructions.Add(new PathLine("18270", "11290"));
            item.Instructions.Add(new PathLine("16380", "12310"));
            item.Instructions.Add(new PathLine("18877", "15632"));
            item.Instructions.Add(new PathLine("14640", "14350"));
            item.Instructions.Add(new PathLine("14942", "17370"));
            item.Instructions.Add(new PathLine("12180", "15935"));
            item.Instructions.Add(new PathLine("11612", "18842"));
            item.Instructions.Add(new PathLine("9872", "17370"));
            item.Instructions.Add(new PathLine("8700", "19712"));
            item.Instructions.Add(new PathLine("7527", "18125"));
            item.Instructions.Add(new PathLine("4917", "21600"));
            item.Instructions.Add(new PathLine("4805", "18240"));
            item.Instructions.Add(new PathLine("1285", "17825"));
            item.Instructions.Add(new PathLine("3330", "15370"));
            item.Instructions.Add(new PathLine("0", "12877"));
            item.Instructions.Add(new PathLine("3935", "11592"));
            item.Instructions.Add(new PathLine("1172", "8270"));
            item.Instructions.Add(new PathLine("5372", "7817"));
            item.Instructions.Add(new PathLine("4502", "3625"));
            item.Instructions.Add(new PathLine("8550", "6382"));
            item.Instructions.Add(new PathLine("9722", "1887"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- l dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ y1 dx2 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- x2  0 dx1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "r", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "r", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 64977"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss w"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+/ x2 r 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "x1", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "maxAdj3", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "0", "maxAdj4", "", "", "", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x2", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftBrace()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+- 100000 0 a2"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "min q1 a2"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ q2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ q3 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 y1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin y1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- y1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b dy1 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "hc", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "100000", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "r", "b"));
            geometry.ShapeTextRectangle.FromString("il", "it", "r", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("hc", "y4"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "-5400000"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "y1"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("hc", "y4"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "-5400000"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "y1"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftBracket()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 8333"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos w 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin y1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- y1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b dy1 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "maxAdj", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "r", "b"));
            geometry.ShapeTextRectangle.FromString("il", "it", "r", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathArc("w", "y1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "y1"));
            item.Instructions.Add(new PathArc("w", "y1", "cd2", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathArc("w", "y1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "y1"));
            item.Instructions.Add(new PathArc("w", "y1", "cd2", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftCircularArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val -1142319"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 1142319"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 10800000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a5", "pin 0 adj5 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a5 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 1 adj3 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj4 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("thh", "*/ ss a5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("rw1", "+- wd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rh1", "+- hd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rw2", "+- rw1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rh2", "+- rh1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rw3", "+- rw2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh3", "+- rh2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtH", "sin rw3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("htH", "cos rh3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxH", "cat2 rw3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("dyH", "sat2 rh3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("xH", "+- hc dxH 0"));
            geometry.Guides.Add(new ModelShapeGuide("yH", "+- vc dyH 0"));
            geometry.Guides.Add(new ModelShapeGuide("rI", "min rw2 rh2"));
            geometry.Guides.Add(new ModelShapeGuide("u1", "*/ dxH dxH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u2", "*/ dyH dyH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("u4", "+- u1 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u5", "+- u2 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u6", "*/ u4 u5 u1"));
            geometry.Guides.Add(new ModelShapeGuide("u7", "*/ u6 1 u2"));
            geometry.Guides.Add(new ModelShapeGuide("u8", "+- 1 0 u7"));
            geometry.Guides.Add(new ModelShapeGuide("u9", "sqrt u8"));
            geometry.Guides.Add(new ModelShapeGuide("u10", "*/ u4 1 dxH"));
            geometry.Guides.Add(new ModelShapeGuide("u11", "*/ u10 1 dyH"));
            geometry.Guides.Add(new ModelShapeGuide("u12", "+/ 1 u9 u11"));
            geometry.Guides.Add(new ModelShapeGuide("u13", "at2 1 u12"));
            geometry.Guides.Add(new ModelShapeGuide("u14", "+- u13 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u15", "?: u13 u13 u14"));
            geometry.Guides.Add(new ModelShapeGuide("u16", "+- u15 0 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("u17", "+- u16 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u18", "?: u16 u16 u17"));
            geometry.Guides.Add(new ModelShapeGuide("u19", "+- u18 0 cd2"));
            geometry.Guides.Add(new ModelShapeGuide("u20", "+- u18 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("u21", "?: u19 u20 u18"));
            geometry.Guides.Add(new ModelShapeGuide("u22", "abs u21"));
            geometry.Guides.Add(new ModelShapeGuide("minAng", "*/ u22 -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("u23", "abs adj2"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "*/ u23 -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("aAng", "pin minAng a2 0"));
            geometry.Guides.Add(new ModelShapeGuide("ptAng", "+- enAng aAng 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtA", "sin rw3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("htA", "cos rh3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxA", "cat2 rw3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("dyA", "sat2 rh3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("xA", "+- hc dxA 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA", "+- vc dyA 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtE", "sin rw1 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htE", "cos rh1 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxE", "cat2 rw1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("dyE", "sat2 rh1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("xE", "+- hc dxE 0"));
            geometry.Guides.Add(new ModelShapeGuide("yE", "+- vc dyE 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtD", "sin rw2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htD", "cos rh2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxD", "cat2 rw2 htD wtD"));
            geometry.Guides.Add(new ModelShapeGuide("dyD", "sat2 rh2 htD wtD"));
            geometry.Guides.Add(new ModelShapeGuide("xD", "+- hc dxD 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD", "+- vc dyD 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxG", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyG", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xG", "+- xH dxG 0"));
            geometry.Guides.Add(new ModelShapeGuide("yG", "+- yH dyG 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxB", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyB", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xB", "+- xH 0 dxB 0"));
            geometry.Guides.Add(new ModelShapeGuide("yB", "+- yH 0 dyB 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- xB 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- yB 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- xG 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- yG 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("rO", "min rw1 rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x1O", "*/ sx1 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y1O", "*/ sy1 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x2O", "*/ sx2 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y2O", "*/ sy2 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("dxO", "+- x2O 0 x1O"));
            geometry.Guides.Add(new ModelShapeGuide("dyO", "+- y2O 0 y1O"));
            geometry.Guides.Add(new ModelShapeGuide("dO", "mod dxO dyO 0"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ x1O y2O 1"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ x2O y1O 1"));
            geometry.Guides.Add(new ModelShapeGuide("DO", "+- q1 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ rO rO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "*/ dO dO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "*/ q3 q4 1"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "*/ DO DO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "+- q5 0 q6"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "max q7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelO", "sqrt q8"));
            geometry.Guides.Add(new ModelShapeGuide("ndyO", "*/ dyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("sdyO", "?: ndyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ sdyO dxO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "*/ q9 sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "*/ DO dyO 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxF1", "+/ q11 q10 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "+- q11 0 q10"));
            geometry.Guides.Add(new ModelShapeGuide("dxF2", "*/ q12 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("adyO", "abs dyO"));
            geometry.Guides.Add(new ModelShapeGuide("q13", "*/ adyO sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q14", "*/ DO dxO -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyF1", "+/ q14 q13 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q15", "+- q14 0 q13"));
            geometry.Guides.Add(new ModelShapeGuide("dyF2", "*/ q15 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q16", "+- x2O 0 dxF1"));
            geometry.Guides.Add(new ModelShapeGuide("q17", "+- x2O 0 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("q18", "+- y2O 0 dyF1"));
            geometry.Guides.Add(new ModelShapeGuide("q19", "+- y2O 0 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("q20", "mod q16 q18 0"));
            geometry.Guides.Add(new ModelShapeGuide("q21", "mod q17 q19 0"));
            geometry.Guides.Add(new ModelShapeGuide("q22", "+- q21 0 q20"));
            geometry.Guides.Add(new ModelShapeGuide("dxF", "?: q22 dxF1 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("dyF", "?: q22 dyF1 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxF", "*/ dxF rw1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("sdyF", "*/ dyF rh1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("xF", "+- hc sdxF 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF", "+- vc sdyF 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1I", "*/ sx1 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y1I", "*/ sy1 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("x2I", "*/ sx2 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y2I", "*/ sy2 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "+- x2I 0 x1I"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "+- y2I 0 y1I"));
            geometry.Guides.Add(new ModelShapeGuide("dI", "mod dxI dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "*/ x1I y2I 1"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "*/ x2I y1I 1"));
            geometry.Guides.Add(new ModelShapeGuide("DI", "+- v1 0 v2"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v4", "*/ dI dI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v5", "*/ v3 v4 1"));
            geometry.Guides.Add(new ModelShapeGuide("v6", "*/ DI DI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v7", "+- v5 0 v6"));
            geometry.Guides.Add(new ModelShapeGuide("v8", "max v7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelI", "sqrt v8"));
            geometry.Guides.Add(new ModelShapeGuide("v9", "*/ sdyO dxI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v10", "*/ v9 sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v11", "*/ DI dyI 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxC1", "+/ v11 v10 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v12", "+- v11 0 v10"));
            geometry.Guides.Add(new ModelShapeGuide("dxC2", "*/ v12 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("adyI", "abs dyI"));
            geometry.Guides.Add(new ModelShapeGuide("v13", "*/ adyI sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v14", "*/ DI dxI -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyC1", "+/ v14 v13 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v15", "+- v14 0 v13"));
            geometry.Guides.Add(new ModelShapeGuide("dyC2", "*/ v15 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v16", "+- x1I 0 dxC1"));
            geometry.Guides.Add(new ModelShapeGuide("v17", "+- x1I 0 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("v18", "+- y1I 0 dyC1"));
            geometry.Guides.Add(new ModelShapeGuide("v19", "+- y1I 0 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("v20", "mod v16 v18 0"));
            geometry.Guides.Add(new ModelShapeGuide("v21", "mod v17 v19 0"));
            geometry.Guides.Add(new ModelShapeGuide("v22", "+- v21 0 v20"));
            geometry.Guides.Add(new ModelShapeGuide("dxC", "?: v22 dxC1 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("dyC", "?: v22 dyC1 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxC", "*/ dxC rw2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("sdyC", "*/ dyC rh2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("xC", "+- hc sdxC 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC", "+- vc sdyC 0"));
            geometry.Guides.Add(new ModelShapeGuide("ist0", "at2 sdxC sdyC"));
            geometry.Guides.Add(new ModelShapeGuide("ist1", "+- ist0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("istAng0", "?: ist0 ist0 ist1"));
            geometry.Guides.Add(new ModelShapeGuide("isw1", "+- stAng 0 istAng0"));
            geometry.Guides.Add(new ModelShapeGuide("isw2", "+- isw1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("iswAng0", "?: isw1 isw1 isw2"));
            geometry.Guides.Add(new ModelShapeGuide("istAng", "+- istAng0 iswAng0 0"));
            geometry.Guides.Add(new ModelShapeGuide("iswAng", "+- 0 0 iswAng0"));
            geometry.Guides.Add(new ModelShapeGuide("p1", "+- xF 0 xC"));
            geometry.Guides.Add(new ModelShapeGuide("p2", "+- yF 0 yC"));
            geometry.Guides.Add(new ModelShapeGuide("p3", "mod p1 p2 0"));
            geometry.Guides.Add(new ModelShapeGuide("p4", "*/ p3 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("p5", "+- p4 0 thh"));
            geometry.Guides.Add(new ModelShapeGuide("xGp", "?: p5 xF xG"));
            geometry.Guides.Add(new ModelShapeGuide("yGp", "?: p5 yF yG"));
            geometry.Guides.Add(new ModelShapeGuide("xBp", "?: p5 xC xB"));
            geometry.Guides.Add(new ModelShapeGuide("yBp", "?: p5 yC yB"));
            geometry.Guides.Add(new ModelShapeGuide("en0", "at2 sdxF sdyF"));
            geometry.Guides.Add(new ModelShapeGuide("en1", "+- en0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("en2", "?: en0 en0 en1"));
            geometry.Guides.Add(new ModelShapeGuide("sw0", "+- en2 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- sw0 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw0 sw1 sw0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng0", "+- stAng swAng 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng0", "+- 0 0 swAng"));
            geometry.Guides.Add(new ModelShapeGuide("wtI", "sin rw3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htI", "cos rh3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "cat2 rw3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "sat2 rh3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("xI", "+- hc dxI 0"));
            geometry.Guides.Add(new ModelShapeGuide("yI", "+- vc dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("aI", "+- stAng cd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("aA", "+- ptAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("aB", "+- ptAng cd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos rw1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin rh1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "minAng", "0", "", "", "", "xA", "yA"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj4", "0", "21599999", "", "", "", "xE", "yE"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj3", "0", "21599999", "adj1", "0", "maxAdj1", "xF", "yF"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("", "", "", "adj5", "0", "25000", "xB", "yB"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aI", "xI", "yI"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("ptAng", "xGp", "yGp"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aA", "xA", "yA"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aB", "xBp", "yBp"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xE", "yE"));
            item.Instructions.Add(new PathLine("xD", "yD"));
            item.Instructions.Add(new PathArc("rw2", "rh2", "istAng", "iswAng"));
            item.Instructions.Add(new PathLine("xBp", "yBp"));
            item.Instructions.Add(new PathLine("xA", "yA"));
            item.Instructions.Add(new PathLine("xGp", "yGp"));
            item.Instructions.Add(new PathLine("xF", "yF"));
            item.Instructions.Add(new PathArc("rw1", "rh1", "stAng0", "swAng0"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ y1 x2 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- x2 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x3 dx1 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "x3", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x4", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 48123"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss wd2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a4 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "x1", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "maxAdj3", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "0", "maxAdj4", "", "", "", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x2", "t", "x3", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightCircularArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 1142319"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 20457681"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 11942319"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a5", "pin 0 adj5 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a5 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 1 adj3 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj4 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("thh", "*/ ss a5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("rw1", "+- wd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rh1", "+- hd2 th2 thh"));
            geometry.Guides.Add(new ModelShapeGuide("rw2", "+- rw1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rh2", "+- rh1 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("rw3", "+- rw2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("rh3", "+- rh2 th2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtH", "sin rw3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("htH", "cos rh3 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxH", "cat2 rw3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("dyH", "sat2 rh3 htH wtH"));
            geometry.Guides.Add(new ModelShapeGuide("xH", "+- hc dxH 0"));
            geometry.Guides.Add(new ModelShapeGuide("yH", "+- vc dyH 0"));
            geometry.Guides.Add(new ModelShapeGuide("rI", "min rw2 rh2"));
            geometry.Guides.Add(new ModelShapeGuide("u1", "*/ dxH dxH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u2", "*/ dyH dyH 1"));
            geometry.Guides.Add(new ModelShapeGuide("u3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("u4", "+- u1 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u5", "+- u2 0 u3"));
            geometry.Guides.Add(new ModelShapeGuide("u6", "*/ u4 u5 u1"));
            geometry.Guides.Add(new ModelShapeGuide("u7", "*/ u6 1 u2"));
            geometry.Guides.Add(new ModelShapeGuide("u8", "+- 1 0 u7"));
            geometry.Guides.Add(new ModelShapeGuide("u9", "sqrt u8"));
            geometry.Guides.Add(new ModelShapeGuide("u10", "*/ u4 1 dxH"));
            geometry.Guides.Add(new ModelShapeGuide("u11", "*/ u10 1 dyH"));
            geometry.Guides.Add(new ModelShapeGuide("u12", "+/ 1 u9 u11"));
            geometry.Guides.Add(new ModelShapeGuide("u13", "at2 1 u12"));
            geometry.Guides.Add(new ModelShapeGuide("u14", "+- u13 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u15", "?: u13 u13 u14"));
            geometry.Guides.Add(new ModelShapeGuide("u16", "+- u15 0 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("u17", "+- u16 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("u18", "?: u16 u16 u17"));
            geometry.Guides.Add(new ModelShapeGuide("u19", "+- u18 0 cd2"));
            geometry.Guides.Add(new ModelShapeGuide("u20", "+- u18 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("u21", "?: u19 u20 u18"));
            geometry.Guides.Add(new ModelShapeGuide("maxAng", "abs u21"));
            geometry.Guides.Add(new ModelShapeGuide("aAng", "pin 0 adj2 maxAng"));
            geometry.Guides.Add(new ModelShapeGuide("ptAng", "+- enAng aAng 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtA", "sin rw3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("htA", "cos rh3 ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxA", "cat2 rw3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("dyA", "sat2 rh3 htA wtA"));
            geometry.Guides.Add(new ModelShapeGuide("xA", "+- hc dxA 0"));
            geometry.Guides.Add(new ModelShapeGuide("yA", "+- vc dyA 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxG", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyG", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xG", "+- xH dxG 0"));
            geometry.Guides.Add(new ModelShapeGuide("yG", "+- yH dyG 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxB", "cos thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyB", "sin thh ptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xB", "+- xH 0 dxB 0"));
            geometry.Guides.Add(new ModelShapeGuide("yB", "+- yH 0 dyB 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- xB 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- yB 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- xG 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- yG 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("rO", "min rw1 rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x1O", "*/ sx1 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y1O", "*/ sy1 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("x2O", "*/ sx2 rO rw1"));
            geometry.Guides.Add(new ModelShapeGuide("y2O", "*/ sy2 rO rh1"));
            geometry.Guides.Add(new ModelShapeGuide("dxO", "+- x2O 0 x1O"));
            geometry.Guides.Add(new ModelShapeGuide("dyO", "+- y2O 0 y1O"));
            geometry.Guides.Add(new ModelShapeGuide("dO", "mod dxO dyO 0"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ x1O y2O 1"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ x2O y1O 1"));
            geometry.Guides.Add(new ModelShapeGuide("DO", "+- q1 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ rO rO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "*/ dO dO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q5", "*/ q3 q4 1"));
            geometry.Guides.Add(new ModelShapeGuide("q6", "*/ DO DO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q7", "+- q5 0 q6"));
            geometry.Guides.Add(new ModelShapeGuide("q8", "max q7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelO", "sqrt q8"));
            geometry.Guides.Add(new ModelShapeGuide("ndyO", "*/ dyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("sdyO", "?: ndyO -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("q9", "*/ sdyO dxO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q10", "*/ q9 sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q11", "*/ DO dyO 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxF1", "+/ q11 q10 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q12", "+- q11 0 q10"));
            geometry.Guides.Add(new ModelShapeGuide("dxF2", "*/ q12 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("adyO", "abs dyO"));
            geometry.Guides.Add(new ModelShapeGuide("q13", "*/ adyO sdelO 1"));
            geometry.Guides.Add(new ModelShapeGuide("q14", "*/ DO dxO -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyF1", "+/ q14 q13 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q15", "+- q14 0 q13"));
            geometry.Guides.Add(new ModelShapeGuide("dyF2", "*/ q15 1 q4"));
            geometry.Guides.Add(new ModelShapeGuide("q16", "+- x2O 0 dxF1"));
            geometry.Guides.Add(new ModelShapeGuide("q17", "+- x2O 0 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("q18", "+- y2O 0 dyF1"));
            geometry.Guides.Add(new ModelShapeGuide("q19", "+- y2O 0 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("q20", "mod q16 q18 0"));
            geometry.Guides.Add(new ModelShapeGuide("q21", "mod q17 q19 0"));
            geometry.Guides.Add(new ModelShapeGuide("q22", "+- q21 0 q20"));
            geometry.Guides.Add(new ModelShapeGuide("dxF", "?: q22 dxF1 dxF2"));
            geometry.Guides.Add(new ModelShapeGuide("dyF", "?: q22 dyF1 dyF2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxF", "*/ dxF rw1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("sdyF", "*/ dyF rh1 rO"));
            geometry.Guides.Add(new ModelShapeGuide("xF", "+- hc sdxF 0"));
            geometry.Guides.Add(new ModelShapeGuide("yF", "+- vc sdyF 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1I", "*/ sx1 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y1I", "*/ sy1 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("x2I", "*/ sx2 rI rw2"));
            geometry.Guides.Add(new ModelShapeGuide("y2I", "*/ sy2 rI rh2"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "+- x2I 0 x1I"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "+- y2I 0 y1I"));
            geometry.Guides.Add(new ModelShapeGuide("dI", "mod dxI dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "*/ x1I y2I 1"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "*/ x2I y1I 1"));
            geometry.Guides.Add(new ModelShapeGuide("DI", "+- v1 0 v2"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "*/ rI rI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v4", "*/ dI dI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v5", "*/ v3 v4 1"));
            geometry.Guides.Add(new ModelShapeGuide("v6", "*/ DI DI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v7", "+- v5 0 v6"));
            geometry.Guides.Add(new ModelShapeGuide("v8", "max v7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdelI", "sqrt v8"));
            geometry.Guides.Add(new ModelShapeGuide("v9", "*/ sdyO dxI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v10", "*/ v9 sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v11", "*/ DI dyI 1"));
            geometry.Guides.Add(new ModelShapeGuide("dxC1", "+/ v11 v10 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v12", "+- v11 0 v10"));
            geometry.Guides.Add(new ModelShapeGuide("dxC2", "*/ v12 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("adyI", "abs dyI"));
            geometry.Guides.Add(new ModelShapeGuide("v13", "*/ adyI sdelI 1"));
            geometry.Guides.Add(new ModelShapeGuide("v14", "*/ DI dxI -1"));
            geometry.Guides.Add(new ModelShapeGuide("dyC1", "+/ v14 v13 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v15", "+- v14 0 v13"));
            geometry.Guides.Add(new ModelShapeGuide("dyC2", "*/ v15 1 v4"));
            geometry.Guides.Add(new ModelShapeGuide("v16", "+- x1I 0 dxC1"));
            geometry.Guides.Add(new ModelShapeGuide("v17", "+- x1I 0 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("v18", "+- y1I 0 dyC1"));
            geometry.Guides.Add(new ModelShapeGuide("v19", "+- y1I 0 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("v20", "mod v16 v18 0"));
            geometry.Guides.Add(new ModelShapeGuide("v21", "mod v17 v19 0"));
            geometry.Guides.Add(new ModelShapeGuide("v22", "+- v21 0 v20"));
            geometry.Guides.Add(new ModelShapeGuide("dxC", "?: v22 dxC1 dxC2"));
            geometry.Guides.Add(new ModelShapeGuide("dyC", "?: v22 dyC1 dyC2"));
            geometry.Guides.Add(new ModelShapeGuide("sdxC", "*/ dxC rw2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("sdyC", "*/ dyC rh2 rI"));
            geometry.Guides.Add(new ModelShapeGuide("xC", "+- hc sdxC 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC", "+- vc sdyC 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtI", "sin rw3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("htI", "cos rh3 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxI", "cat2 rw3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("dyI", "sat2 rh3 htI wtI"));
            geometry.Guides.Add(new ModelShapeGuide("xI", "+- hc dxI 0"));
            geometry.Guides.Add(new ModelShapeGuide("yI", "+- vc dyI 0"));
            geometry.Guides.Add(new ModelShapeGuide("lptAng", "+- stAng 0 aAng"));
            geometry.Guides.Add(new ModelShapeGuide("wtL", "sin rw3 lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("htL", "cos rh3 lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dxL", "cat2 rw3 htL wtL"));
            geometry.Guides.Add(new ModelShapeGuide("dyL", "sat2 rh3 htL wtL"));
            geometry.Guides.Add(new ModelShapeGuide("xL", "+- hc dxL 0"));
            geometry.Guides.Add(new ModelShapeGuide("yL", "+- vc dyL 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxK", "cos thh lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyK", "sin thh lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xK", "+- xI dxK 0"));
            geometry.Guides.Add(new ModelShapeGuide("yK", "+- yI dyK 0"));
            geometry.Guides.Add(new ModelShapeGuide("dxJ", "cos thh lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("dyJ", "sin thh lptAng"));
            geometry.Guides.Add(new ModelShapeGuide("xJ", "+- xI 0 dxJ 0"));
            geometry.Guides.Add(new ModelShapeGuide("yJ", "+- yI 0 dyJ 0"));
            geometry.Guides.Add(new ModelShapeGuide("p1", "+- xF 0 xC"));
            geometry.Guides.Add(new ModelShapeGuide("p2", "+- yF 0 yC"));
            geometry.Guides.Add(new ModelShapeGuide("p3", "mod p1 p2 0"));
            geometry.Guides.Add(new ModelShapeGuide("p4", "*/ p3 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("p5", "+- p4 0 thh"));
            geometry.Guides.Add(new ModelShapeGuide("xGp", "?: p5 xF xG"));
            geometry.Guides.Add(new ModelShapeGuide("yGp", "?: p5 yF yG"));
            geometry.Guides.Add(new ModelShapeGuide("xBp", "?: p5 xC xB"));
            geometry.Guides.Add(new ModelShapeGuide("yBp", "?: p5 yC yB"));
            geometry.Guides.Add(new ModelShapeGuide("en0", "at2 sdxF sdyF"));
            geometry.Guides.Add(new ModelShapeGuide("en1", "+- en0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("en2", "?: en0 en0 en1"));
            geometry.Guides.Add(new ModelShapeGuide("od0", "+- en2 0 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("od1", "+- od0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("od2", "?: od0 od0 od1"));
            geometry.Guides.Add(new ModelShapeGuide("st0", "+- stAng 0 od2"));
            geometry.Guides.Add(new ModelShapeGuide("st1", "+- st0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("st2", "?: st0 st0 st1"));
            geometry.Guides.Add(new ModelShapeGuide("sw0", "+- en2 0 st2"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- sw0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw0 sw0 sw1"));
            geometry.Guides.Add(new ModelShapeGuide("ist0", "at2 sdxC sdyC"));
            geometry.Guides.Add(new ModelShapeGuide("ist1", "+- ist0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("istAng", "?: ist0 ist0 ist1"));
            geometry.Guides.Add(new ModelShapeGuide("id0", "+- istAng 0 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("id1", "+- id0 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("id2", "?: id0 id1 id0"));
            geometry.Guides.Add(new ModelShapeGuide("ien0", "+- stAng 0 id2"));
            geometry.Guides.Add(new ModelShapeGuide("ien1", "+- ien0 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("ien2", "?: ien1 ien1 ien0"));
            geometry.Guides.Add(new ModelShapeGuide("isw1", "+- ien2 0 istAng"));
            geometry.Guides.Add(new ModelShapeGuide("isw2", "+- isw1 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("iswAng", "?: isw1 isw2 isw1"));
            geometry.Guides.Add(new ModelShapeGuide("wtE", "sin rw1 st2"));
            geometry.Guides.Add(new ModelShapeGuide("htE", "cos rh1 st2"));
            geometry.Guides.Add(new ModelShapeGuide("dxE", "cat2 rw1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("dyE", "sat2 rh1 htE wtE"));
            geometry.Guides.Add(new ModelShapeGuide("xE", "+- hc dxE 0"));
            geometry.Guides.Add(new ModelShapeGuide("yE", "+- vc dyE 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtD", "sin rw2 ien2"));
            geometry.Guides.Add(new ModelShapeGuide("htD", "cos rh2 ien2"));
            geometry.Guides.Add(new ModelShapeGuide("dxD", "cat2 rw2 htD wtD"));
            geometry.Guides.Add(new ModelShapeGuide("dyD", "sat2 rh2 htD wtD"));
            geometry.Guides.Add(new ModelShapeGuide("xD", "+- hc dxD 0"));
            geometry.Guides.Add(new ModelShapeGuide("yD", "+- vc dyD 0"));
            geometry.Guides.Add(new ModelShapeGuide("xKp", "?: p5 xE xK"));
            geometry.Guides.Add(new ModelShapeGuide("yKp", "?: p5 yE yK"));
            geometry.Guides.Add(new ModelShapeGuide("xJp", "?: p5 xD xJ"));
            geometry.Guides.Add(new ModelShapeGuide("yJp", "?: p5 yD yJ"));
            geometry.Guides.Add(new ModelShapeGuide("aL", "+- lptAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("aA", "+- ptAng cd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("aB", "+- ptAng cd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("aJ", "+- lptAng cd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos rw1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin rh1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "maxAng", "", "", "", "xA", "yA"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj4", "0", "21599999", "", "", "", "xE", "yE"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj3", "0", "21599999", "adj1", "0", "maxAdj1", "xF", "yF"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("", "", "", "adj5", "0", "25000", "xB", "yB"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aL", "xL", "yL"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("lptAng", "xKp", "yKp"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("ptAng", "xGp", "yGp"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aA", "xA", "yA"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aB", "xBp", "yBp"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("aJ", "xJp", "yJp"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xL", "yL"));
            item.Instructions.Add(new PathLine("xKp", "yKp"));
            item.Instructions.Add(new PathLine("xE", "yE"));
            item.Instructions.Add(new PathArc("rw1", "rh1", "st2", "swAng"));
            item.Instructions.Add(new PathLine("xGp", "yGp"));
            item.Instructions.Add(new PathLine("xA", "yA"));
            item.Instructions.Add(new PathLine("xBp", "yBp"));
            item.Instructions.Add(new PathLine("xC", "yC"));
            item.Instructions.Add(new PathArc("rw2", "rh2", "istAng", "iswAng"));
            item.Instructions.Add(new PathLine("xJp", "yJp"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightRibbon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 33333"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "+- 100000 0 a3"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- wd2 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w1 ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a3 -200000"));
            geometry.Guides.Add(new ModelShapeGuide("ly1", "+- vc dy2 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("ry4", "+- vc dy1 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("ly2", "+- ly1 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("ry3", "+- b 0 ly2"));
            geometry.Guides.Add(new ModelShapeGuide("ly4", "*/ ly2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("ry1", "+- b 0 ly4"));
            geometry.Guides.Add(new ModelShapeGuide("ly3", "+- ly4 0 ly1"));
            geometry.Guides.Add(new ModelShapeGuide("ry2", "+- b 0 ly3"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "*/ a3 ss 400000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc wd32 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- ly1 hR 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- ry2 0 hR"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "x4", "ry2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "33333", "x3", "ry2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "ry3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "ly4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "ly2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "ry1"));
            geometry.ShapeTextRectangle.FromString("x1", "ly1", "x4", "ry4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "ly2"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "ly1"));
            item.Instructions.Add(new PathLine("hc", "ly1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x4", "ry2"));
            item.Instructions.Add(new PathLine("x4", "ry1"));
            item.Instructions.Add(new PathLine("r", "ry3"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathLine("x4", "ry4"));
            item.Instructions.Add(new PathLine("hc", "ry4"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x2", "ly3"));
            item.Instructions.Add(new PathLine("x1", "ly3"));
            item.Instructions.Add(new PathLine("x1", "ly4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x3", "ry2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "ly2"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x1", "ly1"));
            item.Instructions.Add(new PathLine("hc", "ly1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x4", "ry2"));
            item.Instructions.Add(new PathLine("x4", "ry1"));
            item.Instructions.Add(new PathLine("r", "ry3"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathLine("x4", "ry4"));
            item.Instructions.Add(new PathLine("hc", "ry4"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x2", "ly3"));
            item.Instructions.Add(new PathLine("x1", "ly3"));
            item.Instructions.Add(new PathLine("x1", "ly4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "ry2"));
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "ly3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+- 100000 0 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ q1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- y4 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y4 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ dx3 x1 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x3", "x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y4"));
            geometry.ShapeTextRectangle.FromString("il", "y3", "ir", "y5");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y4"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "x1"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x5", "x1"));
            item.Instructions.Add(new PathLine("x4", "x1"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x6", "y3"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x6", "b"));
            item.Instructions.Add(new PathLine("x6", "y5"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "+- 100000 0 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dx4", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 dx4"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 dx4"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x4 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x4 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- y4 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y4 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ dx3 x1 dx4"));
            geometry.Guides.Add(new ModelShapeGuide("cx1", "+/ x1 x5 2"));
            geometry.Guides.Add(new ModelShapeGuide("cy1", "+/ x1 y5 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "x3", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "x3", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x2", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "cx1", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x5", "cy1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "x1"));
            geometry.ShapeTextRectangle.FromString("il", "y3", "x4", "y5");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y4"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "x1"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("x4", "t"));
            item.Instructions.Add(new PathLine("r", "x1"));
            item.Instructions.Add(new PathLine("x5", "x1"));
            item.Instructions.Add(new PathLine("x5", "y5"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLightningBolt()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w 5022 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 8472 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 8757 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "*/ w 10012 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "*/ w 12860 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "*/ w 13917 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x11", "*/ w 16577 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h 3890 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h 6080 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "*/ h 7437 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "*/ h 9705 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "*/ h 12007 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y10", "*/ h 14277 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y11", "*/ h 14915 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x5", "y11"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x11", "y7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x8", "y2"));
            geometry.ShapeTextRectangle.FromString("x4", "y4", "x9", "y10");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("8472", "0"));
            item.Instructions.Add(new PathLine("12860", "6080"));
            item.Instructions.Add(new PathLine("11050", "6797"));
            item.Instructions.Add(new PathLine("16577", "12007"));
            item.Instructions.Add(new PathLine("14767", "12877"));
            item.Instructions.Add(new PathLine("21600", "21600"));
            item.Instructions.Add(new PathLine("10012", "14915"));
            item.Instructions.Add(new PathLine("12222", "13987"));
            item.Instructions.Add(new PathLine("5022", "9705"));
            item.Instructions.Add(new PathLine("7602", "8382"));
            item.Instructions.Add(new PathLine("0", "3890"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLine()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "r", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLineInv()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "r", "t"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathDivide()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 5880"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 11760"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 1000 adj1 36745"));
            geometry.Guides.Add(new ModelShapeGuide("ma1", "+- 0 0 a1"));
            geometry.Guides.Add(new ModelShapeGuide("ma3h", "+/ 73490 ma1 4"));
            geometry.Guides.Add(new ModelShapeGuide("ma3w", "*/ 36745 w h"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "min ma3h ma3w"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 1000 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("m4a3", "*/ -4 a3 1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "+- 73490 m4a3 a1"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("yg", "*/ h a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("rad", "*/ h a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("a", "+- yg rad 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y3 0 a"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 0 rad"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 rad"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "1000", "36745", "l", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "r", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "1000", "maxAdj3", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x3", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "y3", "x3", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("hc", "y1"));
            item.Instructions.Add(new PathArc("rad", "rad", "3cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "y5"));
            item.Instructions.Add(new PathArc("rad", "rad", "cd4", "21600000"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x1", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathEqual()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 11760"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 36745"));
            geometry.Guides.Add(new ModelShapeGuide("2a1", "*/ a1 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("mAdj2", "+- 100000 0 2a1"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 mAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a2 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yC1", "+/ y1 y2 2"));
            geometry.Guides.Add(new ModelShapeGuide("yC2", "+/ y3 y4 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "36745", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "mAdj2", "r", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "yC1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "yC2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "yC1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "yC2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x2", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x1", "y3"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathMinus()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x2", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathMultiply()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 51965"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "at2 w h"));
            geometry.Guides.Add(new ModelShapeGuide("sa", "sin 1 a"));
            geometry.Guides.Add(new ModelShapeGuide("ca", "cos 1 a"));
            geometry.Guides.Add(new ModelShapeGuide("ta", "tan 1 a"));
            geometry.Guides.Add(new ModelShapeGuide("dl", "mod w h 0"));
            geometry.Guides.Add(new ModelShapeGuide("rw", "*/ dl 51965 100000"));
            geometry.Guides.Add(new ModelShapeGuide("lM", "+- dl 0 rw"));
            geometry.Guides.Add(new ModelShapeGuide("xM", "*/ ca lM 2"));
            geometry.Guides.Add(new ModelShapeGuide("yM", "*/ sa lM 2"));
            geometry.Guides.Add(new ModelShapeGuide("dxAM", "*/ sa th 2"));
            geometry.Guides.Add(new ModelShapeGuide("dyAM", "*/ ca th 2"));
            geometry.Guides.Add(new ModelShapeGuide("xA", "+- xM 0 dxAM"));
            geometry.Guides.Add(new ModelShapeGuide("yA", "+- yM dyAM 0"));
            geometry.Guides.Add(new ModelShapeGuide("xB", "+- xM dxAM 0"));
            geometry.Guides.Add(new ModelShapeGuide("yB", "+- yM 0 dyAM"));
            geometry.Guides.Add(new ModelShapeGuide("xBC", "+- hc 0 xB"));
            geometry.Guides.Add(new ModelShapeGuide("yBC", "*/ xBC ta 1"));
            geometry.Guides.Add(new ModelShapeGuide("yC", "+- yBC yB 0"));
            geometry.Guides.Add(new ModelShapeGuide("xD", "+- r 0 xB"));
            geometry.Guides.Add(new ModelShapeGuide("xE", "+- r 0 xA"));
            geometry.Guides.Add(new ModelShapeGuide("yFE", "+- vc 0 yA"));
            geometry.Guides.Add(new ModelShapeGuide("xFE", "*/ yFE 1 ta"));
            geometry.Guides.Add(new ModelShapeGuide("xF", "+- xE 0 xFE"));
            geometry.Guides.Add(new ModelShapeGuide("xL", "+- xA xFE 0"));
            geometry.Guides.Add(new ModelShapeGuide("yG", "+- b 0 yA"));
            geometry.Guides.Add(new ModelShapeGuide("yH", "+- b 0 yB"));
            geometry.Guides.Add(new ModelShapeGuide("yI", "+- b 0 yC"));
            geometry.Guides.Add(new ModelShapeGuide("xC2", "+- r 0 xM"));
            geometry.Guides.Add(new ModelShapeGuide("yC3", "+- b 0 yM"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "51965", "l", "th"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "xM", "yM"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "xC2", "yM"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "xC2", "yC3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xM", "yC3"));
            geometry.ShapeTextRectangle.FromString("xA", "yB", "xE", "yH");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xA", "yA"));
            item.Instructions.Add(new PathLine("xB", "yB"));
            item.Instructions.Add(new PathLine("hc", "yC"));
            item.Instructions.Add(new PathLine("xD", "yB"));
            item.Instructions.Add(new PathLine("xE", "yA"));
            item.Instructions.Add(new PathLine("xF", "vc"));
            item.Instructions.Add(new PathLine("xE", "yG"));
            item.Instructions.Add(new PathLine("xD", "yH"));
            item.Instructions.Add(new PathLine("hc", "yI"));
            item.Instructions.Add(new PathLine("xB", "yH"));
            item.Instructions.Add(new PathLine("xA", "yG"));
            item.Instructions.Add(new PathLine("xL", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathNotEqual()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 6600000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 11760"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("crAng", "pin 4200000 adj2 6600000"));
            geometry.Guides.Add(new ModelShapeGuide("2a1", "*/ a1 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "+- 100000 0 2a1"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a3 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("cadj2", "+- crAng 0 cd4"));
            geometry.Guides.Add(new ModelShapeGuide("xadj2", "tan hd2 cadj2"));
            geometry.Guides.Add(new ModelShapeGuide("len", "mod xadj2 hd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("bhw", "*/ len dy1 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("bhw2", "*/ bhw 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- hc xadj2 bhw2"));
            geometry.Guides.Add(new ModelShapeGuide("dx67", "*/ xadj2 y1 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- x7 0 dx67"));
            geometry.Guides.Add(new ModelShapeGuide("dx57", "*/ xadj2 y2 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x7 0 dx57"));
            geometry.Guides.Add(new ModelShapeGuide("dx47", "*/ xadj2 y3 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x7 0 dx47"));
            geometry.Guides.Add(new ModelShapeGuide("dx37", "*/ xadj2 y4 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x7 0 dx37"));
            geometry.Guides.Add(new ModelShapeGuide("dx27", "*/ xadj2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- x7 0 dx27"));
            geometry.Guides.Add(new ModelShapeGuide("rx7", "+- x7 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("rx6", "+- x6 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("rx5", "+- x5 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("rx4", "+- x4 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("rx3", "+- x3 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("rx2", "+- x2 bhw 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx7", "*/ dy1 hd2 len"));
            geometry.Guides.Add(new ModelShapeGuide("rxt", "+- x7 dx7 0"));
            geometry.Guides.Add(new ModelShapeGuide("lxt", "+- rx7 0 dx7"));
            geometry.Guides.Add(new ModelShapeGuide("rx", "?: cadj2 rxt rx7"));
            geometry.Guides.Add(new ModelShapeGuide("lx", "?: cadj2 x7 lxt"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ dy1 xadj2 len"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "+- 0 0 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("ry", "?: cadj2 dy3 t"));
            geometry.Guides.Add(new ModelShapeGuide("ly", "?: cadj2 t dy4"));
            geometry.Guides.Add(new ModelShapeGuide("dlx", "+- w 0 rx"));
            geometry.Guides.Add(new ModelShapeGuide("drx", "+- w 0 lx"));
            geometry.Guides.Add(new ModelShapeGuide("dly", "+- h 0 ry"));
            geometry.Guides.Add(new ModelShapeGuide("dry", "+- h 0 ly"));
            geometry.Guides.Add(new ModelShapeGuide("xC1", "+/ rx lx 2"));
            geometry.Guides.Add(new ModelShapeGuide("xC2", "+/ drx dlx 2"));
            geometry.Guides.Add(new ModelShapeGuide("yC1", "+/ ry ly 2"));
            geometry.Guides.Add(new ModelShapeGuide("yC2", "+/ y1 y2 2"));
            geometry.Guides.Add(new ModelShapeGuide("yC3", "+/ y3 y4 2"));
            geometry.Guides.Add(new ModelShapeGuide("yC4", "+/ dry dly 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "50000", "l", "y1"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "4200000", "6600000", "", "", "", "lx", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x8", "yC2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x8", "yC3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xC2", "yC4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "yC2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "yC3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "xC1", "yC1"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x8", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("x6", "y1"));
            item.Instructions.Add(new PathLine("lx", "ly"));
            item.Instructions.Add(new PathLine("rx", "ry"));
            item.Instructions.Add(new PathLine("rx6", "y1"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathLine("x8", "y2"));
            item.Instructions.Add(new PathLine("rx5", "y2"));
            item.Instructions.Add(new PathLine("rx4", "y3"));
            item.Instructions.Add(new PathLine("x8", "y3"));
            item.Instructions.Add(new PathLine("x8", "y4"));
            item.Instructions.Add(new PathLine("rx3", "y4"));
            item.Instructions.Add(new PathLine("drx", "dry"));
            item.Instructions.Add(new PathLine("dlx", "dly"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMathPlus()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 23520"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 73490"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h 73490 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "73490", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y1"));
            geometry.ShapeTextRectangle.FromString("x1", "y2", "x4", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateMoon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 87500"));
            geometry.Guides.Add(new ModelShapeGuide("g0", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("g0w", "*/ g0 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("g1", "+- ss 0 g0"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "*/ g0 g0 g1"));
            geometry.Guides.Add(new ModelShapeGuide("g3", "*/ ss ss g1"));
            geometry.Guides.Add(new ModelShapeGuide("g4", "*/ g3 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("g5", "+- g4 0 g2"));
            geometry.Guides.Add(new ModelShapeGuide("g6", "+- g5 0 g0"));
            geometry.Guides.Add(new ModelShapeGuide("g6w", "*/ g6 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("g7", "*/ g5 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("g8", "+- g7 0 g0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ g8 hd2 ss"));
            geometry.Guides.Add(new ModelShapeGuide("g10h", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("g11h", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "*/ g0 9598 32768"));
            geometry.Guides.Add(new ModelShapeGuide("g12w", "*/ g12 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "+- ss 0 g12"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ ss ss 1"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ g13 g13 1"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "+- q1 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("q4", "sqrt q3"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "*/ q4 hd2 ss"));
            geometry.Guides.Add(new ModelShapeGuide("g15h", "+- vc 0 dy4"));
            geometry.Guides.Add(new ModelShapeGuide("g16h", "+- vc dy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("g17w", "+- g6w 0 g0w"));
            geometry.Guides.Add(new ModelShapeGuide("g18w", "*/ g17w 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dx2p", "+- g0w g18w w"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ dx2p -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ hd2 -1 1"));
            geometry.Guides.Add(new ModelShapeGuide("stAng1", "at2 dx2 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("enAngp1", "at2 dx2 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("enAng1", "+- enAngp1 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("swAng1", "+- enAng1 0 stAng1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "87500", "", "", "", "g0w", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "g0w", "vc"));
            geometry.ShapeTextRectangle.FromString("g12w", "g15h", "g0w", "g16h");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("r", "b"));
            item.Instructions.Add(new PathArc("w", "hd2", "cd4", "cd2"));
            item.Instructions.Add(new PathArc("g18w", "dy1", "stAng1", "swAng1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateNonIsoscelesTrapezoid()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+/ r x3 2"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ wd3 a1 maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("adjm", "max a1 a2"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ hd3 adjm maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("irt", "*/ wd3 a2 maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 irt"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj", "", "", "", "x2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj", "", "", "", "x3", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateNoSmoking()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 18750"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "+- wd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "+- hd2 0 dr"));
            geometry.Guides.Add(new ModelShapeGuide("ang", "at2 w h"));
            geometry.Guides.Add(new ModelShapeGuide("ct", "cos ihd2 ang"));
            geometry.Guides.Add(new ModelShapeGuide("st", "sin iwd2 ang"));
            geometry.Guides.Add(new ModelShapeGuide("m", "mod ct st 0"));
            geometry.Guides.Add(new ModelShapeGuide("n", "*/ iwd2 ihd2 m"));
            geometry.Guides.Add(new ModelShapeGuide("drd2", "*/ dr 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dang", "at2 n drd2"));
            geometry.Guides.Add(new ModelShapeGuide("2dang", "*/ dang 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "+- -10800000 2dang 0"));
            geometry.Guides.Add(new ModelShapeGuide("t3", "at2 w h"));
            geometry.Guides.Add(new ModelShapeGuide("stAng1", "+- t3 0 dang"));
            geometry.Guides.Add(new ModelShapeGuide("stAng2", "+- stAng1 0 cd2"));
            geometry.Guides.Add(new ModelShapeGuide("ct1", "cos ihd2 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("st1", "sin iwd2 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("m1", "mod ct1 st1 0"));
            geometry.Guides.Add(new ModelShapeGuide("n1", "*/ iwd2 ihd2 m1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos n1 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin n1 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("", "", "", "adj", "0", "50000", "dr", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "stAng1", "swAng"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "stAng2", "swAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateNotchedRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ dy1 dx2 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "r", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x3", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathLine("x1", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateOctagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 29289"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ x1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "x1"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "x1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateParallelogram()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ x5 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x3"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ wd2 a maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "*/ 5 a maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "+/ 1 q1 12"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ q2 w 1"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ q2 h 1"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 it"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ h hc x2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "pin 0 q3 h"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "maxAdj", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x5", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePentagon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 105146"));
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 110557"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("svc", "*/ vc  vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos swd2 1080000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cos swd2 18360000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin shd2 1080000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sin shd2 18360000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- svc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- svc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ y1 dx2 dx1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y1"));
            geometry.ShapeTextRectangle.FromString("x2", "it", "x3", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePie()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 16200000"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "pin 0 adj2 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("sw1", "+- enAng 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("sw2", "+- sw1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: sw1 sw1 sw2"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin wd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos hd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 wd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 hd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "", "", "", "x1", "y1"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj2", "0", "21599999", "", "", "", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "ir", "it", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            item.Instructions.Add(new PathLine("hc", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePieWedge()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("g1", "cos w 13500000"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "sin h 13500000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r g1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b g2 0"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathArc("w", "h", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePlaque()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ x1 70711 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "-5400000"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "-5400000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePlaqueTabs()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("md", "mod w h 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ 1 md 20"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- 0 b dx"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- 0 r dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "dx", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "dx", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "b"));
            geometry.ShapeTextRectangle.FromString("dx", "dx", "x1", "y1");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("dx", "t"));
            item.Instructions.Add(new PathArc("dx", "dx", "0", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("dx", "dx", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("r", "t"));
            item.Instructions.Add(new PathLine("r", "dx"));
            item.Instructions.Add(new PathArc("dx", "dx", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "b"));
            item.Instructions.Add(new PathArc("dx", "dx", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePlus()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- w 0 h"));
            geometry.Guides.Add(new ModelShapeGuide("il", "?: d l x1"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "?: d r x2"));
            geometry.Guides.Add(new ModelShapeGuide("it", "?: d x1 t"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "?: d y2 b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "x1"));
            item.Instructions.Add(new PathLine("x1", "x1"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("r", "x1"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateQuadArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 22500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 22500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 22500"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+- 100000 0 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ q1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ dx3 x1 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x3", "x1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "y3", "ir", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "x1"));
            item.Instructions.Add(new PathLine("x2", "x1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x5", "x1"));
            item.Instructions.Add(new PathLine("x4", "x1"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("x6", "y3"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x6", "y5"));
            item.Instructions.Add(new PathLine("x6", "y4"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("x4", "y6"));
            item.Instructions.Add(new PathLine("x5", "y6"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("x2", "y6"));
            item.Instructions.Add(new PathLine("x3", "y6"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateQuadArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 18515"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 18515"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 18515"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 48123"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "+- 50000 0 a2"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin a1 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a4 200000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a4 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- r 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- b 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- vc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc dx3 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x4", "ah"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x3", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "ah"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj4", "a1", "maxAdj4", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x2", "y2", "x7", "y7");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("ah", "y3"));
            item.Instructions.Add(new PathLine("ah", "y4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("x4", "ah"));
            item.Instructions.Add(new PathLine("x3", "ah"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x6", "ah"));
            item.Instructions.Add(new PathLine("x5", "ah"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathLine("x7", "y2"));
            item.Instructions.Add(new PathLine("x7", "y4"));
            item.Instructions.Add(new PathLine("x8", "y4"));
            item.Instructions.Add(new PathLine("x8", "y3"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x8", "y6"));
            item.Instructions.Add(new PathLine("x8", "y5"));
            item.Instructions.Add(new PathLine("x7", "y5"));
            item.Instructions.Add(new PathLine("x7", "y7"));
            item.Instructions.Add(new PathLine("x5", "y7"));
            item.Instructions.Add(new PathLine("x5", "y8"));
            item.Instructions.Add(new PathLine("x6", "y8"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("x3", "y8"));
            item.Instructions.Add(new PathLine("x4", "y8"));
            item.Instructions.Add(new PathLine("x4", "y7"));
            item.Instructions.Add(new PathLine("x2", "y7"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathLine("ah", "y5"));
            item.Instructions.Add(new PathLine("ah", "y6"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRibbon()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 25000 adj2 75000"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- r 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a2 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 wd32 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- x9 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x2 wd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- x9 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x5 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x6 wd32 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ y4 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "*/ h a1 400000"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- b 0 hR"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- y2 0 hR"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "33333", "hc", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "25000", "75000", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd8", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x10", "y3"));
            geometry.ShapeTextRectangle.FromString("x2", "y2", "x9", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x4", "t"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x8", "y2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x10", "y3"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("x9", "y5"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "cd4"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("l", "y4"));
            item.Instructions.Add(new PathLine("wd8", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x5", "hR"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "cd4"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x6", "hR"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd2", "-5400000"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x4", "t"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x8", "y2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("x10", "y3"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("x9", "y5"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "cd4"));
            item.Instructions.Add(new PathLine("x3", "b"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("l", "y4"));
            item.Instructions.Add(new PathLine("wd8", "y3"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x5", "hR"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathMove("x6", "y2"));
            item.Instructions.Add(new PathLine("x6", "hR"));
            item.Instructions.Add(new PathMove("x2", "y4"));
            item.Instructions.Add(new PathLine("x2", "y6"));
            item.Instructions.Add(new PathMove("x9", "y6"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRibbon2()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 25000 adj2 75000"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- r 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ w a2 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 wd32 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- x9 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x2 wd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- x9 0 wd8"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x5 0 wd32"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x6 wd32 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- t dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y4 b 2"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "*/ h a1 400000"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- b 0 hR"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- y1 0 hR"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "33333", "hc", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "25000", "75000", "", "", "", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "wd8", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x10", "y3"));
            geometry.ShapeTextRectangle.FromString("x2", "t", "x9", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x4", "b"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("x8", "y2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x10", "y3"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("x9", "hR"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "-5400000"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("l", "y4"));
            item.Instructions.Add(new PathLine("wd8", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x5", "y6"));
            item.Instructions.Add(new PathArc("wd32", "hR", "0", "-5400000"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x6", "y6"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("wd8", "y3"));
            item.Instructions.Add(new PathLine("l", "y4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x2", "hR"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x8", "t"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x10", "y3"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x7", "b"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "-10800000"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathArc("wd32", "hR", "3cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x5", "y2"));
            item.Instructions.Add(new PathLine("x5", "y6"));
            item.Instructions.Add(new PathMove("x6", "y6"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathMove("x2", "y7"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathMove("x9", "y4"));
            item.Instructions.Add(new PathLine("x9", "y7"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ y1 dx1 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- x1 dx2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "y1", "x2", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRightArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 64977"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss w"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ x2 1 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "x3", "y2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "r", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj3", "0", "maxAdj3", "", "", "", "x3", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "0", "maxAdj4", "", "", "", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRightBrace()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 8333"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+- 100000 0 a2"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "min q1 a2"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "*/ q2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ q3 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y3 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin y1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- l dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- y1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b dy1 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "maxAdj1", "hc", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "100000", "r", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "r", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "l", "b"));
            geometry.ShapeTextRectangle.FromString("l", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathArc("wd2", "y1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("hc", "y2"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "-5400000"));
            item.Instructions.Add(new PathArc("wd2", "y1", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "y4"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathArc("wd2", "y1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("hc", "y2"));
            item.Instructions.Add(new PathArc("wd2", "y1", "cd2", "-5400000"));
            item.Instructions.Add(new PathArc("wd2", "y1", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("hc", "y4"));
            item.Instructions.Add(new PathArc("wd2", "y1", "0", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRightBracket()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 8333"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos w 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin y1 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- l dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- y1 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b dy1 y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "maxAdj", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathArc("w", "y1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("w", "y1", "0", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathArc("w", "y1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("w", "y1", "0", "cd4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRound1Rect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "*/ dx1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 idx"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "t", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathArc("dx1", "dx1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRound2DiagRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("a", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 a"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 a"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ x1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ a 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- dx1 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "?: d dx1 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 dx"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 dx"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("dx", "dx", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathArc("a", "a", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            item.Instructions.Add(new PathLine("a", "b"));
            item.Instructions.Add(new PathArc("a", "a", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRound2SameRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("tx1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("tx2", "+- r 0 tx1"));
            geometry.Guides.Add(new ModelShapeGuide("bx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bx2", "+- r 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("by1", "+- b 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- tx1 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("tdx", "*/ tx1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bdx", "*/ bx1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "?: d tdx bdx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 bdx"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "tx2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "bx1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "tdx", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("tx1", "t"));
            item.Instructions.Add(new PathLine("tx2", "t"));
            item.Instructions.Add(new PathArc("tx1", "tx1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "by1"));
            item.Instructions.Add(new PathArc("bx1", "bx1", "0", "cd4"));
            item.Instructions.Add(new PathLine("bx1", "b"));
            item.Instructions.Add(new PathArc("bx1", "bx1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "tx1"));
            item.Instructions.Add(new PathArc("tx1", "tx1", "cd2", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRoundRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ x1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathArc("x1", "x1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathArc("x1", "x1", "0", "cd4"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRtTriangle()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h 7 12"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "*/ w 7 12"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "*/ h 11 12"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "hc", "vc"));
            geometry.ShapeTextRectangle.FromString("wd12", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("l", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSmileyFace()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 4653"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin -4653 adj 4653"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w 4969 21699"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w 6215 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 13135 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ w 16640 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h 7570 21600"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 16515 21600"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y3 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ h a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y4 dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("wR", "*/ w 1125 21600"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "*/ h 1125 21600"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "-4653", "4653", "hc", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathArc("wR", "hR", "cd2", "21600000"));
            item.Instructions.Add(new PathMove("x3", "y1"));
            item.Instructions.Add(new PathArc("wR", "hR", "cd2", "21600000"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x1", "y2"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y5", "x4", "y2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSnip1Rect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ dx1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+/ x1 r 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "50000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("l", "it", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("r", "dx1"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSnip2DiagRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("lx1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("lx2", "+- r 0 lx1"));
            geometry.Guides.Add(new ModelShapeGuide("ly1", "+- b 0 lx1"));
            geometry.Guides.Add(new ModelShapeGuide("rx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("rx2", "+- r 0 rx1"));
            geometry.Guides.Add(new ModelShapeGuide("ry1", "+- b 0 rx1"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- lx1 0 rx1"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "?: d lx1 rx1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ dx 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "lx1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "rx2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("lx1", "t"));
            item.Instructions.Add(new PathLine("rx2", "t"));
            item.Instructions.Add(new PathLine("r", "rx1"));
            item.Instructions.Add(new PathLine("r", "ly1"));
            item.Instructions.Add(new PathLine("lx2", "b"));
            item.Instructions.Add(new PathLine("rx1", "b"));
            item.Instructions.Add(new PathLine("l", "ry1"));
            item.Instructions.Add(new PathLine("l", "lx1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSnip2SameRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("tx1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("tx2", "+- r 0 tx1"));
            geometry.Guides.Add(new ModelShapeGuide("bx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bx2", "+- r 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("by1", "+- b 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("d", "+- tx1 0 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "?: d tx1 bx1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ dx 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ tx1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+/ by1 b 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "tx2", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "bx1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("tx1", "t"));
            item.Instructions.Add(new PathLine("tx2", "t"));
            item.Instructions.Add(new PathLine("r", "tx1"));
            item.Instructions.Add(new PathLine("r", "by1"));
            item.Instructions.Add(new PathLine("bx2", "b"));
            item.Instructions.Add(new PathLine("bx1", "b"));
            item.Instructions.Add(new PathLine("l", "by1"));
            item.Instructions.Add(new PathLine("l", "tx1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSnipRoundRect()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16667"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ x1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+/ x2 r 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "50000", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "50000", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "dx2"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathLine("l", "x1"));
            item.Instructions.Add(new PathArc("x1", "x1", "cd2", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSquareTabs()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("md", "mod w h 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ 1 md 20"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- 0 b dx"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- 0 r dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "dx", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "dx", "x1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "dx", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "dx", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x1", "dx"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x1", "y1"));
            geometry.ShapeTextRectangle.FromString("dx", "dx", "x1", "y1");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("dx", "t"));
            item.Instructions.Add(new PathLine("dx", "dx"));
            item.Instructions.Add(new PathLine("l", "dx"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("dx", "y1"));
            item.Instructions.Add(new PathLine("dx", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "dx"));
            item.Instructions.Add(new PathLine("x1", "dx"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar10()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 42533"));
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 105146"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ swd2 95106 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ swd2 58779 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ hd2 80902 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ hd2 30902 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ swd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 80902 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 30902 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 95106 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 58779 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 iwd2"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc iwd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "y1"));
            geometry.ShapeTextRectangle.FromString("sx2", "sy2", "sx5", "sy3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y2"));
            item.Instructions.Add(new PathLine("sx2", "sy2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx4", "sy1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("sx5", "sy2"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("sx6", "vc"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("sx5", "sy3"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("sx4", "sy4"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx3", "sy4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("sx2", "sy3"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("sx1", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar12()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 37500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 1800000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin hd2 3600000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "*/ w 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "*/ h 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "cos iwd2 900000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "cos iwd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx3", "cos iwd2 4500000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "sin ihd2 4500000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "sin ihd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy3", "sin ihd2 900000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx3"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc sdx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc 0 sdy3"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc sdy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy5", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy6", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "hd4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "wd4", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "hd4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "wd4", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "y1"));
            geometry.ShapeTextRectangle.FromString("sx2", "sy2", "sx5", "sy5");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy3"));
            item.Instructions.Add(new PathLine("x1", "hd4"));
            item.Instructions.Add(new PathLine("sx2", "sy2"));
            item.Instructions.Add(new PathLine("wd4", "y1"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx4", "sy1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("sx5", "sy2"));
            item.Instructions.Add(new PathLine("x4", "hd4"));
            item.Instructions.Add(new PathLine("sx6", "sy3"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx6", "sy4"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("sx5", "sy5"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("sx4", "sy6"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx3", "sy6"));
            item.Instructions.Add(new PathLine("wd4", "y4"));
            item.Instructions.Add(new PathLine("sx2", "sy5"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("sx1", "sy4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar16()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 37500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ wd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ wd2 70711 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ wd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ hd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ hd2 70711 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ hd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 98079 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 83147 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx3", "*/ iwd2 55557 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx4", "*/ iwd2 19509 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 98079 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 83147 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy3", "*/ ihd2 55557 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy4", "*/ ihd2 19509 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx3"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc 0 sdx4"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc sdx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc sdx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx7", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx8", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc 0 sdy3"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc 0 sdy4"));
            geometry.Guides.Add(new ModelShapeGuide("sy5", "+- vc sdy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy6", "+- vc sdy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy7", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy8", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos iwd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin ihd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x5", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x5", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y6"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x2", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x3", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x4", "y1"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy4"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("sx2", "sy3"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("sx3", "sy2"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("sx4", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx5", "sy1"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("sx6", "sy2"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathLine("sx7", "sy3"));
            item.Instructions.Add(new PathLine("x6", "y3"));
            item.Instructions.Add(new PathLine("sx8", "sy4"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx8", "sy5"));
            item.Instructions.Add(new PathLine("x6", "y4"));
            item.Instructions.Add(new PathLine("sx7", "sy6"));
            item.Instructions.Add(new PathLine("x5", "y5"));
            item.Instructions.Add(new PathLine("sx6", "sy7"));
            item.Instructions.Add(new PathLine("x4", "y6"));
            item.Instructions.Add(new PathLine("sx5", "sy8"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx4", "sy8"));
            item.Instructions.Add(new PathLine("x3", "y6"));
            item.Instructions.Add(new PathLine("sx3", "sy7"));
            item.Instructions.Add(new PathLine("x2", "y5"));
            item.Instructions.Add(new PathLine("sx2", "sy6"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("sx1", "sy5"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar24()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 37500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 900000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cos wd2 1800000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dx4", "val wd4"));
            geometry.Guides.Add(new ModelShapeGuide("dx5", "cos wd2 4500000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin hd2 4500000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sin hd2 3600000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "val hd4"));
            geometry.Guides.Add(new ModelShapeGuide("dy5", "sin hd2 900000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc 0 dx4"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc 0 dx5"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- hc dx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc 0 dy4"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc 0 dy5"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- vc dy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- vc dy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- vc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y9", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y10", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 99144 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx3", "*/ iwd2 79335 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx4", "*/ iwd2 60876 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx5", "*/ iwd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx6", "*/ iwd2 13053 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 99144 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy3", "*/ ihd2 79335 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy4", "*/ ihd2 60876 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy5", "*/ ihd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy6", "*/ ihd2 13053 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx3"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc 0 sdx4"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc 0 sdx5"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc 0 sdx6"));
            geometry.Guides.Add(new ModelShapeGuide("sx7", "+- hc sdx6 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx8", "+- hc sdx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx9", "+- hc sdx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx10", "+- hc sdx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx11", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx12", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc 0 sdy3"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc 0 sdy4"));
            geometry.Guides.Add(new ModelShapeGuide("sy5", "+- vc 0 sdy5"));
            geometry.Guides.Add(new ModelShapeGuide("sy6", "+- vc 0 sdy6"));
            geometry.Guides.Add(new ModelShapeGuide("sy7", "+- vc sdy6 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy8", "+- vc sdy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy9", "+- vc sdy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy10", "+- vc sdy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy11", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy12", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos iwd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin ihd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "ssd2", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy6"));
            item.Instructions.Add(new PathLine("x1", "y5"));
            item.Instructions.Add(new PathLine("sx2", "sy5"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("sx3", "sy4"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("sx4", "sy3"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathLine("sx5", "sy2"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathLine("sx6", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx7", "sy1"));
            item.Instructions.Add(new PathLine("x6", "y1"));
            item.Instructions.Add(new PathLine("sx8", "sy2"));
            item.Instructions.Add(new PathLine("x7", "y2"));
            item.Instructions.Add(new PathLine("sx9", "sy3"));
            item.Instructions.Add(new PathLine("x8", "y3"));
            item.Instructions.Add(new PathLine("sx10", "sy4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("sx11", "sy5"));
            item.Instructions.Add(new PathLine("x10", "y5"));
            item.Instructions.Add(new PathLine("sx12", "sy6"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx12", "sy7"));
            item.Instructions.Add(new PathLine("x10", "y6"));
            item.Instructions.Add(new PathLine("sx11", "sy8"));
            item.Instructions.Add(new PathLine("x9", "y7"));
            item.Instructions.Add(new PathLine("sx10", "sy9"));
            item.Instructions.Add(new PathLine("x8", "y8"));
            item.Instructions.Add(new PathLine("sx9", "sy10"));
            item.Instructions.Add(new PathLine("x7", "y9"));
            item.Instructions.Add(new PathLine("sx8", "sy11"));
            item.Instructions.Add(new PathLine("x6", "y10"));
            item.Instructions.Add(new PathLine("sx7", "sy12"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx6", "sy12"));
            item.Instructions.Add(new PathLine("x5", "y10"));
            item.Instructions.Add(new PathLine("sx5", "sy11"));
            item.Instructions.Add(new PathLine("x4", "y9"));
            item.Instructions.Add(new PathLine("sx4", "sy10"));
            item.Instructions.Add(new PathLine("x3", "y8"));
            item.Instructions.Add(new PathLine("sx3", "sy9"));
            item.Instructions.Add(new PathLine("x2", "y7"));
            item.Instructions.Add(new PathLine("sx2", "sy8"));
            item.Instructions.Add(new PathLine("x1", "y6"));
            item.Instructions.Add(new PathLine("sx1", "sy7"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar32()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 37500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ wd2 98079 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ wd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ wd2 83147 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx4", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dx5", "*/ wd2 55557 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx6", "*/ wd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx7", "*/ wd2 19509 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ hd2 98079 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ hd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ hd2 83147 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy5", "*/ hd2 55557 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy6", "*/ hd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy7", "*/ hd2 19509 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc 0 dx4"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc 0 dx5"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc 0 dx6"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- hc 0 dx7"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- hc dx7 0"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- hc dx6 0"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- hc dx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("x11", "+- hc dx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("x12", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x13", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x14", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc 0 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- vc 0 dy4"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc 0 dy5"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- vc 0 dy6"));
            geometry.Guides.Add(new ModelShapeGuide("y7", "+- vc 0 dy7"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "+- vc dy7 0"));
            geometry.Guides.Add(new ModelShapeGuide("y9", "+- vc dy6 0"));
            geometry.Guides.Add(new ModelShapeGuide("y10", "+- vc dy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("y11", "+- vc dy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("y12", "+- vc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y13", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y14", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 99518 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 95694 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx3", "*/ iwd2 88192 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx4", "*/ iwd2 77301 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx5", "*/ iwd2 63439 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx6", "*/ iwd2 47140 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx7", "*/ iwd2 29028 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx8", "*/ iwd2 9802 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 99518 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 95694 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy3", "*/ ihd2 88192 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy4", "*/ ihd2 77301 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy5", "*/ ihd2 63439 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy6", "*/ ihd2 47140 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy7", "*/ ihd2 29028 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy8", "*/ ihd2 9802 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx3"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc 0 sdx4"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc 0 sdx5"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc 0 sdx6"));
            geometry.Guides.Add(new ModelShapeGuide("sx7", "+- hc 0 sdx7"));
            geometry.Guides.Add(new ModelShapeGuide("sx8", "+- hc 0 sdx8"));
            geometry.Guides.Add(new ModelShapeGuide("sx9", "+- hc sdx8 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx10", "+- hc sdx7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx11", "+- hc sdx6 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx12", "+- hc sdx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx13", "+- hc sdx4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx14", "+- hc sdx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx15", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx16", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc 0 sdy3"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc 0 sdy4"));
            geometry.Guides.Add(new ModelShapeGuide("sy5", "+- vc 0 sdy5"));
            geometry.Guides.Add(new ModelShapeGuide("sy6", "+- vc 0 sdy6"));
            geometry.Guides.Add(new ModelShapeGuide("sy7", "+- vc 0 sdy7"));
            geometry.Guides.Add(new ModelShapeGuide("sy8", "+- vc 0 sdy8"));
            geometry.Guides.Add(new ModelShapeGuide("sy9", "+- vc sdy8 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy10", "+- vc sdy7 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy11", "+- vc sdy6 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy12", "+- vc sdy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy13", "+- vc sdy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy14", "+- vc sdy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy15", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy16", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos iwd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin ihd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "ssd2", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy8"));
            item.Instructions.Add(new PathLine("x1", "y7"));
            item.Instructions.Add(new PathLine("sx2", "sy7"));
            item.Instructions.Add(new PathLine("x2", "y6"));
            item.Instructions.Add(new PathLine("sx3", "sy6"));
            item.Instructions.Add(new PathLine("x3", "y5"));
            item.Instructions.Add(new PathLine("sx4", "sy5"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("sx5", "sy4"));
            item.Instructions.Add(new PathLine("x5", "y3"));
            item.Instructions.Add(new PathLine("sx6", "sy3"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("sx7", "sy2"));
            item.Instructions.Add(new PathLine("x7", "y1"));
            item.Instructions.Add(new PathLine("sx8", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx9", "sy1"));
            item.Instructions.Add(new PathLine("x8", "y1"));
            item.Instructions.Add(new PathLine("sx10", "sy2"));
            item.Instructions.Add(new PathLine("x9", "y2"));
            item.Instructions.Add(new PathLine("sx11", "sy3"));
            item.Instructions.Add(new PathLine("x10", "y3"));
            item.Instructions.Add(new PathLine("sx12", "sy4"));
            item.Instructions.Add(new PathLine("x11", "y4"));
            item.Instructions.Add(new PathLine("sx13", "sy5"));
            item.Instructions.Add(new PathLine("x12", "y5"));
            item.Instructions.Add(new PathLine("sx14", "sy6"));
            item.Instructions.Add(new PathLine("x13", "y6"));
            item.Instructions.Add(new PathLine("sx15", "sy7"));
            item.Instructions.Add(new PathLine("x14", "y7"));
            item.Instructions.Add(new PathLine("sx16", "sy8"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx16", "sy9"));
            item.Instructions.Add(new PathLine("x14", "y8"));
            item.Instructions.Add(new PathLine("sx15", "sy10"));
            item.Instructions.Add(new PathLine("x13", "y9"));
            item.Instructions.Add(new PathLine("sx14", "sy11"));
            item.Instructions.Add(new PathLine("x12", "y10"));
            item.Instructions.Add(new PathLine("sx13", "sy12"));
            item.Instructions.Add(new PathLine("x11", "y11"));
            item.Instructions.Add(new PathLine("sx12", "sy13"));
            item.Instructions.Add(new PathLine("x10", "y12"));
            item.Instructions.Add(new PathLine("sx11", "sy14"));
            item.Instructions.Add(new PathLine("x9", "y13"));
            item.Instructions.Add(new PathLine("sx10", "sy15"));
            item.Instructions.Add(new PathLine("x8", "y14"));
            item.Instructions.Add(new PathLine("sx9", "sy16"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx8", "sy16"));
            item.Instructions.Add(new PathLine("x7", "y14"));
            item.Instructions.Add(new PathLine("sx7", "sy15"));
            item.Instructions.Add(new PathLine("x6", "y13"));
            item.Instructions.Add(new PathLine("sx6", "sy14"));
            item.Instructions.Add(new PathLine("x5", "y12"));
            item.Instructions.Add(new PathLine("sx5", "sy13"));
            item.Instructions.Add(new PathLine("x4", "y11"));
            item.Instructions.Add(new PathLine("sx4", "sy12"));
            item.Instructions.Add(new PathLine("x3", "y10"));
            item.Instructions.Add(new PathLine("sx3", "sy11"));
            item.Instructions.Add(new PathLine("x2", "y9"));
            item.Instructions.Add(new PathLine("sx2", "sy10"));
            item.Instructions.Add(new PathLine("x1", "y8"));
            item.Instructions.Add(new PathLine("sx1", "sy9"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar4()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx", "cos iwd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy", "sin ihd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc sdx 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc sdy 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("sx1", "sy1", "sx2", "sy2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx2", "sy1"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx2", "sy2"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx1", "sy2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar5()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 19098"));
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 105146"));
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 110557"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("svc", "*/ vc  vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos swd2 1080000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cos swd2 18360000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin shd2 1080000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sin shd2 18360000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- svc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- svc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ swd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ shd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "cos iwd2 20520000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "cos iwd2 3240000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "sin ihd2 3240000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "sin ihd2 20520000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- svc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- svc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- svc ihd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- svc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "y1"));
            geometry.ShapeTextRectangle.FromString("sx1", "sy1", "sx4", "sy3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathLine("sx2", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("sx4", "sy2"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("hc", "sy3"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("sx1", "sy2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar6()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 28868"));
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 115470"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos swd2 1800000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc hd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ swd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 iwd2"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc iwd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "sin ihd2 3600000"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "hd4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "hd4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("sx1", "sy1", "sx4", "sy2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "hd4"));
            item.Instructions.Add(new PathLine("sx2", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("x2", "hd4"));
            item.Instructions.Add(new PathLine("sx4", "vc"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("sx3", "sy2"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx2", "sy2"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("sx1", "vc"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar7()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 34601"));
            geometry.AdjustValues.Add(new ModelShapeGuide("hf", "val 102572"));
            geometry.AdjustValues.Add(new ModelShapeGuide("vf", "val 105210"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("swd2", "*/ wd2 hf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("shd2", "*/ hd2 vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("svc", "*/ vc  vf 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ swd2 97493 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ swd2 78183 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "*/ swd2 43388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ shd2 62349 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ shd2 22252 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ shd2 90097 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc 0 dx3"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- svc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- svc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- svc dy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ swd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ shd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 97493 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 78183 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx3", "*/ iwd2 43388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc 0 sdx3"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc sdx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx5", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx6", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 90097 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 22252 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy3", "*/ ihd2 62349 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- svc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- svc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- svc sdy3 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- svc ihd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- svc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x5", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x6", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x4", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x3", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("sx2", "sy1", "sx5", "sy3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y2"));
            item.Instructions.Add(new PathLine("sx1", "sy2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx4", "sy1"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathLine("sx6", "sy2"));
            item.Instructions.Add(new PathLine("x6", "y2"));
            item.Instructions.Add(new PathLine("sx5", "sy3"));
            item.Instructions.Add(new PathLine("x4", "y3"));
            item.Instructions.Add(new PathLine("hc", "sy4"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("sx2", "sy3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStar8()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 37500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 a 50000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx1", "*/ iwd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdx2", "*/ iwd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy1", "*/ ihd2 92388 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sdy2", "*/ ihd2 38268 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sx1", "+- hc 0 sdx1"));
            geometry.Guides.Add(new ModelShapeGuide("sx2", "+- hc 0 sdx2"));
            geometry.Guides.Add(new ModelShapeGuide("sx3", "+- hc sdx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sx4", "+- hc sdx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy1", "+- vc 0 sdy1"));
            geometry.Guides.Add(new ModelShapeGuide("sy2", "+- vc 0 sdy2"));
            geometry.Guides.Add(new ModelShapeGuide("sy3", "+- vc sdy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("sy4", "+- vc sdy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("yAdj", "+- vc 0 ihd2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "hc", "yAdj"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x1", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "y1"));
            geometry.ShapeTextRectangle.FromString("sx1", "sy1", "sx4", "sy4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("sx1", "sy2"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("sx2", "sy1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("sx3", "sy1"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("sx4", "sy2"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("sx4", "sy3"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("sx3", "sy4"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("sx2", "sy4"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathLine("sx1", "sy3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStraightConnector1()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None
            };
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStripedRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 84375 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "*/ ss 5 32"));
            geometry.Guides.Add(new ModelShapeGuide("dx5", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- r 0 dx5"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ h a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx6", "*/ dy1 dx5 hd2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 dx6"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "100000", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x5", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x5", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x5", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x4", "y1", "x6", "y2");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("ssd32", "y1"));
            item.Instructions.Add(new PathLine("ssd32", "y2"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ssd16", "y1"));
            item.Instructions.Add(new PathLine("ssd8", "y1"));
            item.Instructions.Add(new PathLine("ssd8", "y2"));
            item.Instructions.Add(new PathLine("ssd16", "y2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x4", "y1"));
            item.Instructions.Add(new PathLine("x5", "y1"));
            item.Instructions.Add(new PathLine("x5", "t"));
            item.Instructions.Add(new PathLine("r", "vc"));
            item.Instructions.Add(new PathLine("x5", "b"));
            item.Instructions.Add(new PathLine("x5", "y2"));
            item.Instructions.Add(new PathLine("x4", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSun()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 12500 adj 46875"));
            geometry.Guides.Add(new ModelShapeGuide("g0", "+- 50000 0 a"));
            geometry.Guides.Add(new ModelShapeGuide("g1", "*/ g0 30274 32768"));
            geometry.Guides.Add(new ModelShapeGuide("g2", "*/ g0 12540 32768"));
            geometry.Guides.Add(new ModelShapeGuide("g3", "+- g1 50000 0"));
            geometry.Guides.Add(new ModelShapeGuide("g4", "+- g2 50000 0"));
            geometry.Guides.Add(new ModelShapeGuide("g5", "+- 50000 0 g1"));
            geometry.Guides.Add(new ModelShapeGuide("g6", "+- 50000 0 g2"));
            geometry.Guides.Add(new ModelShapeGuide("g7", "*/ g0 23170 32768"));
            geometry.Guides.Add(new ModelShapeGuide("g8", "+- 50000 g7 0"));
            geometry.Guides.Add(new ModelShapeGuide("g9", "+- 50000 0 g7"));
            geometry.Guides.Add(new ModelShapeGuide("g10", "*/ g5 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g11", "*/ g6 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("g12", "+- g10 3662 0"));
            geometry.Guides.Add(new ModelShapeGuide("g13", "+- g11 3662 0"));
            geometry.Guides.Add(new ModelShapeGuide("g14", "+- g11 12500 0"));
            geometry.Guides.Add(new ModelShapeGuide("g15", "+- 100000 0 g10"));
            geometry.Guides.Add(new ModelShapeGuide("g16", "+- 100000 0 g12"));
            geometry.Guides.Add(new ModelShapeGuide("g17", "+- 100000 0 g13"));
            geometry.Guides.Add(new ModelShapeGuide("g18", "+- 100000 0 g14"));
            geometry.Guides.Add(new ModelShapeGuide("ox1", "*/ w 18436 21600"));
            geometry.Guides.Add(new ModelShapeGuide("oy1", "*/ h 3163 21600"));
            geometry.Guides.Add(new ModelShapeGuide("ox2", "*/ w 3163 21600"));
            geometry.Guides.Add(new ModelShapeGuide("oy2", "*/ h 18436 21600"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "*/ w g8 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "*/ w g9 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "*/ w g10 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x12", "*/ w g12 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x13", "*/ w g13 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x14", "*/ w g14 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x15", "*/ w g15 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x16", "*/ w g16 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x17", "*/ w g17 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x18", "*/ w g18 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x19", "*/ w a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("wR", "*/ w g0 100000"));
            geometry.Guides.Add(new ModelShapeGuide("hR", "*/ h g0 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y8", "*/ h g8 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y9", "*/ h g9 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y10", "*/ h g10 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y12", "*/ h g12 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y13", "*/ h g13 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y14", "*/ h g14 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y15", "*/ h g15 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y16", "*/ h g16 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y17", "*/ h g17 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y18", "*/ h g18 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "12500", "46875", "", "", "", "x19", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("x9", "y9", "x8", "y8");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("r", "vc"));
            item.Instructions.Add(new PathLine("x15", "y18"));
            item.Instructions.Add(new PathLine("x15", "y14"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ox1", "oy1"));
            item.Instructions.Add(new PathLine("x16", "y13"));
            item.Instructions.Add(new PathLine("x17", "y12"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "t"));
            item.Instructions.Add(new PathLine("x18", "y10"));
            item.Instructions.Add(new PathLine("x14", "y10"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ox2", "oy1"));
            item.Instructions.Add(new PathLine("x13", "y12"));
            item.Instructions.Add(new PathLine("x12", "y13"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("x10", "y14"));
            item.Instructions.Add(new PathLine("x10", "y18"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ox2", "oy2"));
            item.Instructions.Add(new PathLine("x12", "y17"));
            item.Instructions.Add(new PathLine("x13", "y16"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("hc", "b"));
            item.Instructions.Add(new PathLine("x14", "y15"));
            item.Instructions.Add(new PathLine("x18", "y15"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ox1", "oy2"));
            item.Instructions.Add(new PathLine("x17", "y16"));
            item.Instructions.Add(new PathLine("x16", "y17"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x19", "vc"));
            item.Instructions.Add(new PathArc("wR", "hR", "cd2", "21600000"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSwooshArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 1 adj1 75000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 70000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("ad1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ad2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("xB", "+- r 0 ad2"));
            geometry.Guides.Add(new ModelShapeGuide("yB", "+- t ssd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("alfa", "*/ cd4 1 14"));
            geometry.Guides.Add(new ModelShapeGuide("dx0", "tan ssd8 alfa"));
            geometry.Guides.Add(new ModelShapeGuide("xC", "+- xB 0 dx0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "tan ad1 alfa"));
            geometry.Guides.Add(new ModelShapeGuide("yF", "+- yB ad1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xF", "+- xB dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xE", "+- xF dx0 0"));
            geometry.Guides.Add(new ModelShapeGuide("yE", "+- yF ssd8 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "+- yE 0 t"));
            geometry.Guides.Add(new ModelShapeGuide("dy22", "*/ dy2 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ h 1 20"));
            geometry.Guides.Add(new ModelShapeGuide("yD", "+- t dy22 dy3"));
            geometry.Guides.Add(new ModelShapeGuide("dy4", "*/ hd6 1 1"));
            geometry.Guides.Add(new ModelShapeGuide("yP1", "+- hd6 dy4 0"));
            geometry.Guides.Add(new ModelShapeGuide("xP1", "val wd6"));
            geometry.Guides.Add(new ModelShapeGuide("dy5", "*/ hd6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("yP2", "+- yF dy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("xP2", "val wd4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "1", "75000", "xF", "yF"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "xB", "yB"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "xC", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "yD"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xE", "yE"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "xP1", "yP1", "xB", "yB"));
            item.Instructions.Add(new PathLine("xC", "t"));
            item.Instructions.Add(new PathLine("r", "yD"));
            item.Instructions.Add(new PathLine("xE", "yE"));
            item.Instructions.Add(new PathLine("xF", "yF"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "xP2", "yP2", "l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTeardrop()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 200000"));
            geometry.Guides.Add(new ModelShapeGuide("r2", "sqrt 2"));
            geometry.Guides.Add(new ModelShapeGuide("tw", "*/ wd2 r2 1"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ hd2 r2 1"));
            geometry.Guides.Add(new ModelShapeGuide("sw", "*/ tw a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("sh", "*/ th a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos sw 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin sh 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+/ hc x1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+/ vc y1 2"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "200000", "", "", "", "x1", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x1", "y1"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd2", "cd4"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "x2", "t", "x1", "y1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "r", "y2", "r", "vc"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "0", "cd4"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTrapezoid()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ ss a 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- r 0 x2"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ wd3 a maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ hd3 a maxAdj"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "maxAdj", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x4", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("x3", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTriangle()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w a 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x1 wd2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "100000", "", "", "", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "x2", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "l", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x2", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "r", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x3", "vc"));
            geometry.ShapeTextRectangle.FromString("x1", "vc", "x3", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 100000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- t dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ x1 dy2 wd2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 0 dy1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "100000", "", "", "", "x1", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x2", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUpArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 64977"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 100000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss h"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+/ y2 b 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x2", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj4", "0", "maxAdj4", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ShapeTextRectangle.FromString("l", "y2", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUpDownArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 50000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- b 0 y2"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ x1 y2 wd2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- y2 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y3 dy1 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "100000", "", "", "", "x1", "y3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj2", "0", "maxAdj2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x2", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y2"));
            geometry.ShapeTextRectangle.FromString("x1", "y1", "x2", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("l", "y3"));
            item.Instructions.Add(new PathLine("x1", "y3"));
            item.Instructions.Add(new PathLine("x1", "y2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUpDownArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 48123"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj2", "*/ 50000 w ss"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 maxAdj2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ 50000 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a3 ss hd2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "*/ ss a1 200000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ h a4 200000"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy2 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "x2", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "maxAdj2", "", "", "", "x1", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "r", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj4", "0", "maxAdj4", "l", "y2"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ShapeTextRectangle.FromString("l", "y2", "r", "y3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x2", "y2"));
            item.Instructions.Add(new PathLine("x2", "y1"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("x4", "y1"));
            item.Instructions.Add(new PathLine("x3", "y1"));
            item.Instructions.Add(new PathLine("x3", "y2"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "y3"));
            item.Instructions.Add(new PathLine("x3", "y3"));
            item.Instructions.Add(new PathLine("x3", "y4"));
            item.Instructions.Add(new PathLine("x4", "y4"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("x1", "y4"));
            item.Instructions.Add(new PathLine("x2", "y4"));
            item.Instructions.Add(new PathLine("x2", "y3"));
            item.Instructions.Add(new PathLine("l", "y3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUturnArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 25000"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 43750"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj5", "val 75000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin 0 adj2 25000"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj1", "*/ a2 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 maxAdj1"));
            geometry.Guides.Add(new ModelShapeGuide("q2", "*/ a1 ss h"));
            geometry.Guides.Add(new ModelShapeGuide("q3", "+- 100000 0 q2"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj3", "*/ q3 h ss"));
            geometry.Guides.Add(new ModelShapeGuide("a3", "pin 0 adj3 maxAdj3"));
            geometry.Guides.Add(new ModelShapeGuide("q1", "+- a3 a1 0"));
            geometry.Guides.Add(new ModelShapeGuide("minAdj5", "*/ q1 ss h"));
            geometry.Guides.Add(new ModelShapeGuide("a5", "pin minAdj5 adj5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th", "*/ ss a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("aw2", "*/ ss a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("th2", "*/ th 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("dh2", "+- aw2 0 th2"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "*/ h a5 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ah", "*/ ss a3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- y5 0 ah"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- r 0 dh2"));
            geometry.Guides.Add(new ModelShapeGuide("bw", "*/ x9 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("bs", "min bw y4"));
            geometry.Guides.Add(new ModelShapeGuide("maxAdj4", "*/ bs 100000 ss"));
            geometry.Guides.Add(new ModelShapeGuide("a4", "pin 0 adj4 maxAdj4"));
            geometry.Guides.Add(new ModelShapeGuide("bd", "*/ ss a4 100000"));
            geometry.Guides.Add(new ModelShapeGuide("bd3", "+- bd 0 th"));
            geometry.Guides.Add(new ModelShapeGuide("bd2", "max bd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- th bd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+- r 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- x8 0 aw2"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x6 dh2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- x9 0 bd"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x7 0 bd2"));
            geometry.Guides.Add(new ModelShapeGuide("cx", "+/ th x7 2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "0", "maxAdj1", "", "", "", "th", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "0", "25000", "", "", "", "x6", "b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj3", "0", "maxAdj3", "x6", "y4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj4", "0", "maxAdj4", "", "", "", "bd", "t"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj5", "minAdj5", "100000", "r", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x6", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "x8", "y5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "cx", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "th2", "b"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("l", "bd"));
            item.Instructions.Add(new PathArc("bd", "bd", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x4", "t"));
            item.Instructions.Add(new PathArc("bd", "bd", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("x9", "y4"));
            item.Instructions.Add(new PathLine("r", "y4"));
            item.Instructions.Add(new PathLine("x8", "y5"));
            item.Instructions.Add(new PathLine("x6", "y4"));
            item.Instructions.Add(new PathLine("x7", "y4"));
            item.Instructions.Add(new PathLine("x7", "x3"));
            item.Instructions.Add(new PathArc("bd2", "bd2", "0", "-5400000"));
            item.Instructions.Add(new PathLine("x3", "th"));
            item.Instructions.Add(new PathArc("bd2", "bd2", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("th", "b"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateVerticalScroll()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 12500"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 25000"));
            geometry.Guides.Add(new ModelShapeGuide("ch", "*/ ss a 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ch2", "*/ ch 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("ch4", "*/ ch 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- ch ch2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+- ch ch 0"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- r 0 ch"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- r 0 ch2"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- x6 0 ch2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- b 0 ch"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 ch2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "25000", "l", "ch"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "ch", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x6", "vc"));
            geometry.ShapeTextRectangle.FromString("ch", "ch", "x6", "y4");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ch2", "b"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("ch2", "y4"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd4", "-10800000"));
            item.Instructions.Add(new PathLine("ch", "y3"));
            item.Instructions.Add(new PathLine("ch", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x7", "t"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x6", "ch"));
            item.Instructions.Add(new PathLine("x6", "y4"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x4", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.DarkenLess,
                Stroke = false,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("x4", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("ch", "y4"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "3cd4"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "3cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("ch", "y3"));
            item.Instructions.Add(new PathLine("ch", "ch2"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x7", "t"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("x6", "ch"));
            item.Instructions.Add(new PathLine("x6", "y4"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "0", "cd4"));
            item.Instructions.Add(new PathLine("ch2", "b"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "cd2"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("x3", "t"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "3cd4", "cd2"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "cd4", "cd2"));
            item.Instructions.Add(new PathLine("x4", "ch2"));
            item.Instructions.Add(new PathMove("x6", "ch"));
            item.Instructions.Add(new PathLine("x3", "ch"));
            item.Instructions.Add(new PathMove("ch2", "y3"));
            item.Instructions.Add(new PathArc("ch4", "ch4", "3cd4", "cd2"));
            item.Instructions.Add(new PathLine("ch", "y4"));
            item.Instructions.Add(new PathMove("ch2", "b"));
            item.Instructions.Add(new PathArc("ch2", "ch2", "cd4", "-5400000"));
            item.Instructions.Add(new PathLine("ch", "y3"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWave()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("a1", "pin 0 adj1 20000"));
            geometry.Guides.Add(new ModelShapeGuide("a2", "pin -10000 adj2 10000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h a1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "*/ y1 10 3"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- y1 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- y1 dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- b 0 y1"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- y4 0 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- y4 dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs dx1"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "?: of2 0 of2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- l 0 dx2"));
            geometry.Guides.Add(new ModelShapeGuide("dx5", "?: of2 of2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- r 0 dx5"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "+/ dx2 x5 3"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- x2 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x4", "+/ x3 x5 2"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- l dx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("x10", "+- r dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("x7", "+- x6 dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x8", "+/ x7 x10 2"));
            geometry.Guides.Add(new ModelShapeGuide("x9", "+- r 0 x1"));
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("xAdj2", "+- hc 0 dx1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "max x2 x6"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "min x5 x10"));
            geometry.Guides.Add(new ModelShapeGuide("it", "*/ h a1 50000"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 it"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "20000", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xAdj2", "y1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "x1", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "xAdj", "y4"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "x9", "vc"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y2", "x4", "y3", "x5", "y1"));
            item.Instructions.Add(new PathLine("x10", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x8", "y6", "x7", "y5", "x6", "y4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWedgeEllipseCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -20833"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 62500"));
            geometry.Guides.Add(new ModelShapeGuide("dxPos", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dyPos", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("xPos", "+- hc dxPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("yPos", "+- vc dyPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("sdx", "*/ dxPos h 1"));
            geometry.Guides.Add(new ModelShapeGuide("sdy", "*/ dyPos w 1"));
            geometry.Guides.Add(new ModelShapeGuide("pang", "at2 sdx sdy"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "+- pang 660000 0"));
            geometry.Guides.Add(new ModelShapeGuide("enAng", "+- pang 0 660000"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cos wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sin hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cos wd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sin hd2 enAng"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("stAng1", "at2 dx1 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("enAng1", "at2 dx2 dy2"));
            geometry.Guides.Add(new ModelShapeGuide("swAng1", "+- enAng1 0 stAng1"));
            geometry.Guides.Add(new ModelShapeGuide("swAng2", "+- swAng1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: swAng1 swAng1 swAng2"));
            geometry.Guides.Add(new ModelShapeGuide("idx", "cos wd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("idy", "sin hd2 2700000"));
            geometry.Guides.Add(new ModelShapeGuide("il", "+- hc 0 idx"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- hc idx 0"));
            geometry.Guides.Add(new ModelShapeGuide("it", "+- vc 0 idy"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- vc idy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "adj2", "-2147483647", "2147483647", "xPos", "yPos"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "il", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "il", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "ir", "ib"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "ir", "it"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("pang", "xPos", "yPos"));
            geometry.ShapeTextRectangle.FromString("il", "it", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xPos", "yPos"));
            item.Instructions.Add(new PathLine("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng1", "swAng"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWedgeRectCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -20833"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 62500"));
            geometry.Guides.Add(new ModelShapeGuide("dxPos", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dyPos", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("xPos", "+- hc dxPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("yPos", "+- vc dyPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "+- xPos 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "+- yPos 0 vc"));
            geometry.Guides.Add(new ModelShapeGuide("dq", "*/ dxPos h w"));
            geometry.Guides.Add(new ModelShapeGuide("ady", "abs dyPos"));
            geometry.Guides.Add(new ModelShapeGuide("adq", "abs dq"));
            geometry.Guides.Add(new ModelShapeGuide("dz", "+- ady 0 adq"));
            geometry.Guides.Add(new ModelShapeGuide("xg1", "?: dxPos 7 2"));
            geometry.Guides.Add(new ModelShapeGuide("xg2", "?: dxPos 10 5"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w xg1 12"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w xg2 12"));
            geometry.Guides.Add(new ModelShapeGuide("yg1", "?: dyPos 7 2"));
            geometry.Guides.Add(new ModelShapeGuide("yg2", "?: dyPos 10 5"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h yg1 12"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h yg2 12"));
            geometry.Guides.Add(new ModelShapeGuide("t1", "?: dxPos l xPos"));
            geometry.Guides.Add(new ModelShapeGuide("xl", "?: dz l t1"));
            geometry.Guides.Add(new ModelShapeGuide("t2", "?: dyPos x1 xPos"));
            geometry.Guides.Add(new ModelShapeGuide("xt", "?: dz t2 x1"));
            geometry.Guides.Add(new ModelShapeGuide("t3", "?: dxPos xPos r"));
            geometry.Guides.Add(new ModelShapeGuide("xr", "?: dz r t3"));
            geometry.Guides.Add(new ModelShapeGuide("t4", "?: dyPos xPos x1"));
            geometry.Guides.Add(new ModelShapeGuide("xb", "?: dz t4 x1"));
            geometry.Guides.Add(new ModelShapeGuide("t5", "?: dxPos y1 yPos"));
            geometry.Guides.Add(new ModelShapeGuide("yl", "?: dz y1 t5"));
            geometry.Guides.Add(new ModelShapeGuide("t6", "?: dyPos t yPos"));
            geometry.Guides.Add(new ModelShapeGuide("yt", "?: dz t6 t"));
            geometry.Guides.Add(new ModelShapeGuide("t7", "?: dxPos yPos y1"));
            geometry.Guides.Add(new ModelShapeGuide("yr", "?: dz y1 t7"));
            geometry.Guides.Add(new ModelShapeGuide("t8", "?: dyPos yPos b"));
            geometry.Guides.Add(new ModelShapeGuide("yb", "?: dz t8 b"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "adj2", "-2147483647", "2147483647", "xPos", "yPos"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xPos", "yPos"));
            geometry.ShapeTextRectangle.FromString("l", "t", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("xt", "yt"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("xr", "yr"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("xb", "yb"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("l", "b"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathLine("xl", "yl"));
            item.Instructions.Add(new PathLine("l", "y1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWedgeRoundRectCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -20833"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 62500"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 16667"));
            geometry.Guides.Add(new ModelShapeGuide("dxPos", "*/ w adj1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dyPos", "*/ h adj2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("xPos", "+- hc dxPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("yPos", "+- vc dyPos 0"));
            geometry.Guides.Add(new ModelShapeGuide("dq", "*/ dxPos h w"));
            geometry.Guides.Add(new ModelShapeGuide("ady", "abs dyPos"));
            geometry.Guides.Add(new ModelShapeGuide("adq", "abs dq"));
            geometry.Guides.Add(new ModelShapeGuide("dz", "+- ady 0 adq"));
            geometry.Guides.Add(new ModelShapeGuide("xg1", "?: dxPos 7 2"));
            geometry.Guides.Add(new ModelShapeGuide("xg2", "?: dxPos 10 5"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "*/ w xg1 12"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "*/ w xg2 12"));
            geometry.Guides.Add(new ModelShapeGuide("yg1", "?: dyPos 7 2"));
            geometry.Guides.Add(new ModelShapeGuide("yg2", "?: dyPos 10 5"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "*/ h yg1 12"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "*/ h yg2 12"));
            geometry.Guides.Add(new ModelShapeGuide("t1", "?: dxPos l xPos"));
            geometry.Guides.Add(new ModelShapeGuide("xl", "?: dz l t1"));
            geometry.Guides.Add(new ModelShapeGuide("t2", "?: dyPos x1 xPos"));
            geometry.Guides.Add(new ModelShapeGuide("xt", "?: dz t2 x1"));
            geometry.Guides.Add(new ModelShapeGuide("t3", "?: dxPos xPos r"));
            geometry.Guides.Add(new ModelShapeGuide("xr", "?: dz r t3"));
            geometry.Guides.Add(new ModelShapeGuide("t4", "?: dyPos xPos x1"));
            geometry.Guides.Add(new ModelShapeGuide("xb", "?: dz t4 x1"));
            geometry.Guides.Add(new ModelShapeGuide("t5", "?: dxPos y1 yPos"));
            geometry.Guides.Add(new ModelShapeGuide("yl", "?: dz y1 t5"));
            geometry.Guides.Add(new ModelShapeGuide("t6", "?: dyPos t yPos"));
            geometry.Guides.Add(new ModelShapeGuide("yt", "?: dz t6 t"));
            geometry.Guides.Add(new ModelShapeGuide("t7", "?: dxPos yPos y1"));
            geometry.Guides.Add(new ModelShapeGuide("yr", "?: dz y1 t7"));
            geometry.Guides.Add(new ModelShapeGuide("t8", "?: dyPos yPos b"));
            geometry.Guides.Add(new ModelShapeGuide("yb", "?: dz t8 b"));
            geometry.Guides.Add(new ModelShapeGuide("u1", "*/ ss adj3 100000"));
            geometry.Guides.Add(new ModelShapeGuide("u2", "+- r 0 u1"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "+- b 0 u1"));
            geometry.Guides.Add(new ModelShapeGuide("il", "*/ u1 29289 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ir", "+- r 0 il"));
            geometry.Guides.Add(new ModelShapeGuide("ib", "+- b 0 il"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj1", "-2147483647", "2147483647", "adj2", "-2147483647", "2147483647", "xPos", "yPos"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("3cd4", "hc", "t"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd2", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("cd4", "xPos", "yPos"));
            geometry.ShapeTextRectangle.FromString("il", "il", "ir", "ib");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "u1"));
            item.Instructions.Add(new PathArc("u1", "u1", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("xt", "yt"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("u2", "t"));
            item.Instructions.Add(new PathArc("u1", "u1", "3cd4", "cd4"));
            item.Instructions.Add(new PathLine("r", "y1"));
            item.Instructions.Add(new PathLine("xr", "yr"));
            item.Instructions.Add(new PathLine("r", "y2"));
            item.Instructions.Add(new PathLine("r", "v2"));
            item.Instructions.Add(new PathArc("u1", "u1", "0", "cd4"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("xb", "yb"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("u1", "b"));
            item.Instructions.Add(new PathArc("u1", "u1", "cd4", "cd4"));
            item.Instructions.Add(new PathLine("l", "y2"));
            item.Instructions.Add(new PathLine("xl", "yl"));
            item.Instructions.Add(new PathLine("l", "y1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        public static ModelShapeCustomGeometry GetCustomGeometryByPreset(ShapePreset shapeType)
        {
            Dictionary<ShapePreset, ModelShapeCustomGeometry> calculatedGeometries = ShapesPresetGeometry.calculatedGeometries;
            lock (calculatedGeometries)
            {
                return GetCustomGeometryByPresetCore(shapeType);
            }
        }

        private static ModelShapeCustomGeometry GetCustomGeometryByPresetCore(ShapePreset shapeType)
        {
            ModelShapeCustomGeometry geometry;
            Func<ModelShapeCustomGeometry> func;
            if (calculatedGeometries.TryGetValue(shapeType, out geometry))
            {
                return geometry;
            }
            if (!geometrysGenerators.TryGetValue(shapeType, out func))
            {
                return GetCustomGeometryByPresetCore(ShapePreset.Rect);
            }
            ModelShapeCustomGeometry geometry2 = func();
            calculatedGeometries.Add(shapeType, geometry2);
            return geometry2;
        }

        private static Dictionary<ShapePreset, Func<ModelShapeCustomGeometry>> InitializeGenerators() => 
            new Dictionary<ShapePreset, Func<ModelShapeCustomGeometry>> { 
                { 
                    ShapePreset.AccentBorderCallout1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentBorderCallout1)
                },
                { 
                    ShapePreset.AccentBorderCallout2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentBorderCallout2)
                },
                { 
                    ShapePreset.AccentBorderCallout3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentBorderCallout3)
                },
                { 
                    ShapePreset.AccentCallout1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentCallout1)
                },
                { 
                    ShapePreset.AccentCallout2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentCallout2)
                },
                { 
                    ShapePreset.AccentCallout3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateAccentCallout3)
                },
                { 
                    ShapePreset.ActionButtonBackPrevious,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonBackPrevious)
                },
                { 
                    ShapePreset.ActionButtonBeginning,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonBeginning)
                },
                { 
                    ShapePreset.ActionButtonBlank,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonBlank)
                },
                { 
                    ShapePreset.ActionButtonDocument,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonDocument)
                },
                { 
                    ShapePreset.ActionButtonEnd,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonEnd)
                },
                { 
                    ShapePreset.ActionButtonForwardNext,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonForwardNext)
                },
                { 
                    ShapePreset.ActionButtonHelp,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonHelp)
                },
                { 
                    ShapePreset.ActionButtonHome,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonHome)
                },
                { 
                    ShapePreset.ActionButtonInformation,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonInformation)
                },
                { 
                    ShapePreset.ActionButtonMovie,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonMovie)
                },
                { 
                    ShapePreset.ActionButtonReturn,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonReturn)
                },
                { 
                    ShapePreset.ActionButtonSound,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateActionButtonSound)
                },
                { 
                    ShapePreset.Arc,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateArc)
                },
                { 
                    ShapePreset.BentArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentArrow)
                },
                { 
                    ShapePreset.BentConnector2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentConnector2)
                },
                { 
                    ShapePreset.BentConnector3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentConnector3)
                },
                { 
                    ShapePreset.BentConnector4,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentConnector4)
                },
                { 
                    ShapePreset.BentConnector5,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentConnector5)
                },
                { 
                    ShapePreset.BentUpArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBentUpArrow)
                },
                { 
                    ShapePreset.Bevel,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBevel)
                },
                { 
                    ShapePreset.BlockArc,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBlockArc)
                },
                { 
                    ShapePreset.BorderCallout1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBorderCallout1)
                },
                { 
                    ShapePreset.BorderCallout2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBorderCallout2)
                },
                { 
                    ShapePreset.BorderCallout3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBorderCallout3)
                },
                { 
                    ShapePreset.BracePair,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBracePair)
                },
                { 
                    ShapePreset.BracketPair,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateBracketPair)
                },
                { 
                    ShapePreset.Callout1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCallout1)
                },
                { 
                    ShapePreset.Callout2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCallout2)
                },
                { 
                    ShapePreset.Callout3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCallout3)
                },
                { 
                    ShapePreset.Can,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCan)
                },
                { 
                    ShapePreset.ChartPlus,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateChartPlus)
                },
                { 
                    ShapePreset.ChartStar,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateChartStar)
                },
                { 
                    ShapePreset.ChartX,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateChartX)
                },
                { 
                    ShapePreset.Chevron,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateChevron)
                },
                { 
                    ShapePreset.Chord,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateChord)
                },
                { 
                    ShapePreset.CircularArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCircularArrow)
                },
                { 
                    ShapePreset.Cloud,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCloud)
                },
                { 
                    ShapePreset.CloudCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCloudCallout)
                },
                { 
                    ShapePreset.Corner,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCorner)
                },
                { 
                    ShapePreset.CornerTabs,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCornerTabs)
                },
                { 
                    ShapePreset.Cube,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCube)
                },
                { 
                    ShapePreset.CurvedConnector2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedConnector2)
                },
                { 
                    ShapePreset.CurvedConnector3,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedConnector3)
                },
                { 
                    ShapePreset.CurvedConnector4,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedConnector4)
                },
                { 
                    ShapePreset.CurvedConnector5,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedConnector5)
                },
                { 
                    ShapePreset.CurvedDownArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedDownArrow)
                },
                { 
                    ShapePreset.CurvedLeftArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedLeftArrow)
                },
                { 
                    ShapePreset.CurvedRightArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedRightArrow)
                },
                { 
                    ShapePreset.CurvedUpArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateCurvedUpArrow)
                },
                { 
                    ShapePreset.Decagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDecagon)
                },
                { 
                    ShapePreset.DiagStripe,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDiagStripe)
                },
                { 
                    ShapePreset.Diamond,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDiamond)
                },
                { 
                    ShapePreset.Dodecagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDodecagon)
                },
                { 
                    ShapePreset.Donut,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDonut)
                },
                { 
                    ShapePreset.DoubleWave,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDoubleWave)
                },
                { 
                    ShapePreset.DownArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDownArrow)
                },
                { 
                    ShapePreset.UpArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateUpArrow)
                },
                { 
                    ShapePreset.DownArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateDownArrowCallout)
                },
                { 
                    ShapePreset.Ellipse,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateEllipse)
                },
                { 
                    ShapePreset.EllipseRibbon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateEllipseRibbon)
                },
                { 
                    ShapePreset.EllipseRibbon2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateEllipseRibbon2)
                },
                { 
                    ShapePreset.FlowChartAlternateProcess,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartAlternateProcess)
                },
                { 
                    ShapePreset.FlowChartCollate,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartCollate)
                },
                { 
                    ShapePreset.FlowChartConnector,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartConnector)
                },
                { 
                    ShapePreset.FlowChartDecision,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartDecision)
                },
                { 
                    ShapePreset.FlowChartDelay,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartDelay)
                },
                { 
                    ShapePreset.FlowChartDisplay,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartDisplay)
                },
                { 
                    ShapePreset.FlowChartDocument,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartDocument)
                },
                { 
                    ShapePreset.FlowChartExtract,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartExtract)
                },
                { 
                    ShapePreset.FlowChartInputOutput,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartInputOutput)
                },
                { 
                    ShapePreset.FlowChartInternalStorage,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartInternalStorage)
                },
                { 
                    ShapePreset.FlowChartMagneticDisk,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartMagneticDisk)
                },
                { 
                    ShapePreset.FlowChartMagneticDrum,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartMagneticDrum)
                },
                { 
                    ShapePreset.FlowChartMagneticTape,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartMagneticTape)
                },
                { 
                    ShapePreset.FlowChartManualInput,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartManualInput)
                },
                { 
                    ShapePreset.FlowChartManualOperation,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartManualOperation)
                },
                { 
                    ShapePreset.FlowChartMerge,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartMerge)
                },
                { 
                    ShapePreset.FlowChartMultidocument,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartMultidocument)
                },
                { 
                    ShapePreset.FlowChartOfflineStorage,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartOfflineStorage)
                },
                { 
                    ShapePreset.FlowChartOffpageConnector,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartOffpageConnector)
                },
                { 
                    ShapePreset.FlowChartOnlineStorage,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartOnlineStorage)
                },
                { 
                    ShapePreset.FlowChartOr,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartOr)
                },
                { 
                    ShapePreset.FlowChartPredefinedProcess,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartPredefinedProcess)
                },
                { 
                    ShapePreset.FlowChartPreparation,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartPreparation)
                },
                { 
                    ShapePreset.FlowChartProcess,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartProcess)
                },
                { 
                    ShapePreset.FlowChartPunchedCard,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartPunchedCard)
                },
                { 
                    ShapePreset.FlowChartPunchedTape,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartPunchedTape)
                },
                { 
                    ShapePreset.FlowChartSort,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartSort)
                },
                { 
                    ShapePreset.FlowChartSummingJunction,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartSummingJunction)
                },
                { 
                    ShapePreset.FlowChartTerminator,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFlowChartTerminator)
                },
                { 
                    ShapePreset.FoldedCorner,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFoldedCorner)
                },
                { 
                    ShapePreset.Frame,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFrame)
                },
                { 
                    ShapePreset.Funnel,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateFunnel)
                },
                { 
                    ShapePreset.Gear6,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateGear6)
                },
                { 
                    ShapePreset.Gear9,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateGear9)
                },
                { 
                    ShapePreset.HalfFrame,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHalfFrame)
                },
                { 
                    ShapePreset.Heart,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHeart)
                },
                { 
                    ShapePreset.Heptagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHeptagon)
                },
                { 
                    ShapePreset.Hexagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHexagon)
                },
                { 
                    ShapePreset.HomePlate,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHomePlate)
                },
                { 
                    ShapePreset.HorizontalScroll,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateHorizontalScroll)
                },
                { 
                    ShapePreset.IrregularSeal1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateIrregularSeal1)
                },
                { 
                    ShapePreset.IrregularSeal2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateIrregularSeal2)
                },
                { 
                    ShapePreset.LeftArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftArrow)
                },
                { 
                    ShapePreset.LeftArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftArrowCallout)
                },
                { 
                    ShapePreset.LeftBrace,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftBrace)
                },
                { 
                    ShapePreset.LeftBracket,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftBracket)
                },
                { 
                    ShapePreset.LeftCircularArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftCircularArrow)
                },
                { 
                    ShapePreset.LeftRightArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftRightArrow)
                },
                { 
                    ShapePreset.LeftRightArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftRightArrowCallout)
                },
                { 
                    ShapePreset.LeftRightCircularArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftRightCircularArrow)
                },
                { 
                    ShapePreset.LeftRightRibbon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftRightRibbon)
                },
                { 
                    ShapePreset.LeftRightUpArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftRightUpArrow)
                },
                { 
                    ShapePreset.LeftUpArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLeftUpArrow)
                },
                { 
                    ShapePreset.LightningBolt,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLightningBolt)
                },
                { 
                    ShapePreset.Line,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLine)
                },
                { 
                    ShapePreset.LineInv,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateLineInv)
                },
                { 
                    ShapePreset.MathDivide,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathDivide)
                },
                { 
                    ShapePreset.MathEqual,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathEqual)
                },
                { 
                    ShapePreset.MathMinus,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathMinus)
                },
                { 
                    ShapePreset.MathMultiply,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathMultiply)
                },
                { 
                    ShapePreset.MathNotEqual,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathNotEqual)
                },
                { 
                    ShapePreset.MathPlus,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMathPlus)
                },
                { 
                    ShapePreset.Moon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateMoon)
                },
                { 
                    ShapePreset.NonIsoscelesTrapezoid,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateNonIsoscelesTrapezoid)
                },
                { 
                    ShapePreset.NoSmoking,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateNoSmoking)
                },
                { 
                    ShapePreset.NotchedRightArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateNotchedRightArrow)
                },
                { 
                    ShapePreset.Octagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateOctagon)
                },
                { 
                    ShapePreset.Parallelogram,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateParallelogram)
                },
                { 
                    ShapePreset.Pentagon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePentagon)
                },
                { 
                    ShapePreset.Pie,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePie)
                },
                { 
                    ShapePreset.PieWedge,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePieWedge)
                },
                { 
                    ShapePreset.Plaque,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePlaque)
                },
                { 
                    ShapePreset.PlaqueTabs,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePlaqueTabs)
                },
                { 
                    ShapePreset.Plus,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GeneratePlus)
                },
                { 
                    ShapePreset.QuadArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateQuadArrow)
                },
                { 
                    ShapePreset.QuadArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateQuadArrowCallout)
                },
                { 
                    ShapePreset.Rect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRect)
                },
                { 
                    ShapePreset.Ribbon,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRibbon)
                },
                { 
                    ShapePreset.Ribbon2,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRibbon2)
                },
                { 
                    ShapePreset.RightArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRightArrow)
                },
                { 
                    ShapePreset.RightArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRightArrowCallout)
                },
                { 
                    ShapePreset.RightBrace,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRightBrace)
                },
                { 
                    ShapePreset.RightBracket,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRightBracket)
                },
                { 
                    ShapePreset.Round1Rect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRound1Rect)
                },
                { 
                    ShapePreset.Round2DiagRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRound2DiagRect)
                },
                { 
                    ShapePreset.Round2SameRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRound2SameRect)
                },
                { 
                    ShapePreset.RoundRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRoundRect)
                },
                { 
                    ShapePreset.RtTriangle,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateRtTriangle)
                },
                { 
                    ShapePreset.SmileyFace,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSmileyFace)
                },
                { 
                    ShapePreset.Snip1Rect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSnip1Rect)
                },
                { 
                    ShapePreset.Snip2DiagRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSnip2DiagRect)
                },
                { 
                    ShapePreset.Snip2SameRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSnip2SameRect)
                },
                { 
                    ShapePreset.SnipRoundRect,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSnipRoundRect)
                },
                { 
                    ShapePreset.SquareTabs,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSquareTabs)
                },
                { 
                    ShapePreset.Star10,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar10)
                },
                { 
                    ShapePreset.Star12,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar12)
                },
                { 
                    ShapePreset.Star16,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar16)
                },
                { 
                    ShapePreset.Star24,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar24)
                },
                { 
                    ShapePreset.Star32,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar32)
                },
                { 
                    ShapePreset.Star4,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar4)
                },
                { 
                    ShapePreset.Star5,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar5)
                },
                { 
                    ShapePreset.Star6,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar6)
                },
                { 
                    ShapePreset.Star7,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar7)
                },
                { 
                    ShapePreset.Star8,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStar8)
                },
                { 
                    ShapePreset.StraightConnector1,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStraightConnector1)
                },
                { 
                    ShapePreset.StripedRightArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateStripedRightArrow)
                },
                { 
                    ShapePreset.Sun,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSun)
                },
                { 
                    ShapePreset.SwooshArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateSwooshArrow)
                },
                { 
                    ShapePreset.Teardrop,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateTeardrop)
                },
                { 
                    ShapePreset.Trapezoid,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateTrapezoid)
                },
                { 
                    ShapePreset.Triangle,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateTriangle)
                },
                { 
                    ShapePreset.UpArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateUpArrowCallout)
                },
                { 
                    ShapePreset.UpDownArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateUpDownArrow)
                },
                { 
                    ShapePreset.UpDownArrowCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateUpDownArrowCallout)
                },
                { 
                    ShapePreset.UturnArrow,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateUturnArrow)
                },
                { 
                    ShapePreset.VerticalScroll,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateVerticalScroll)
                },
                { 
                    ShapePreset.Wave,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateWave)
                },
                { 
                    ShapePreset.WedgeEllipseCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateWedgeEllipseCallout)
                },
                { 
                    ShapePreset.WedgeRectCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateWedgeRectCallout)
                },
                { 
                    ShapePreset.WedgeRoundRectCallout,
                    new Func<ModelShapeCustomGeometry>(ShapesPresetGeometry.GenerateWedgeRoundRectCallout)
                }
            };
    }
}

