namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    public static class BinaryCustomGeometryGenerator
    {
        private static readonly Dictionary<int, ModelShapeCustomGeometry> calculatedGeometries = new Dictionary<int, ModelShapeCustomGeometry>();
        private static readonly Dictionary<int, Func<ModelShapeCustomGeometry>> customGeometrysGenerators = InitializeGenerators();

        public static bool ContainsCustomGeometryGenerator(int shapeTypeCode) => 
            (shapeTypeCode != 1) ? customGeometrysGenerators.ContainsKey(shapeTypeCode) : false;

        private static ModelShapeCustomGeometry GenerateArc()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -5898240"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 21600"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "*/ adj1 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "*/ adj2 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- adj4 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R0", "+- G0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("R1", "+- G1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("R2", "?: G0 G0 R0"));
            geometry.Guides.Add(new ModelShapeGuide("R3", "?: G1 G1 R1"));
            geometry.Guides.Add(new ModelShapeGuide("R4", "+- R3 0 R2"));
            geometry.Guides.Add(new ModelShapeGuide("R5", "+- R4 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("R6", "?: R4 R4 R5"));
            geometry.Guides.Add(new ModelShapeGuide("R7", "cos G2 R2"));
            geometry.Guides.Add(new ModelShapeGuide("R8", "sin G3 R2"));
            geometry.Guides.Add(new ModelShapeGuide("R9", "+- R7 G2 0"));
            geometry.Guides.Add(new ModelShapeGuide("R10", "+- R8 G3 0"));
            geometry.Guides.Add(new ModelShapeGuide("R11", "+- G2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R12", "+- G3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R13", "+- R2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R14", "+- R6 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 21600 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T4", "T5"));
            geometry.ShapeTextRectangle.FromString("0", "0", "r", "b");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance) {
                FillMode = PathFillMode.None,
                ExtrusionOK = false
            };
            item.Instructions.Add(new PathMove("R9", "R10"));
            item.Instructions.Add(new PathArc("R11", "R12", "R13", "R14"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Stroke = false
            };
            item.Instructions.Add(new PathMove("R9", "R10"));
            item.Instructions.Add(new PathArc("R11", "R12", "R13", "R14"));
            item.Instructions.Add(new PathLine("G2", "G3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 15126"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 2912"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- 12158 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- G2 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "*/ G3 32768 32059"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "*/ G4 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- 21600 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G6 G1 6079"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "+- G7 G0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- 12158 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "+- 12427 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "+- 12158 0 G2"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 15126 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 15126 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 12158 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 3237 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 6079 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 12427 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ G1 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ G8 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ G2 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T8", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T9", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T10", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T11", "T6", "T7"));
            geometry.ShapeTextRectangle.FromString("T12", "T13", "T14", "T15");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("21600", "6079"));
            item.Instructions.Add(new PathLine("G0", "0"));
            item.Instructions.Add(new PathLine("G0", "G1"));
            item.Instructions.Add(new PathLine("12427", "G1"));
            item.Instructions.Add(new PathArc("12427", "G9", "3cd4", "-5400000"));
            item.Instructions.Add(new PathLine("0", "21600"));
            item.Instructions.Add(new PathLine("G4", "21600"));
            item.Instructions.Add(new PathLine("G4", "12158"));
            item.Instructions.Add(new PathArc("G10", "G11", "cd2", "cd4"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G0", "12158"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBentUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 9257"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 18514"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 7200"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "*/ G0 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- G3 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- 21600 G0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- G1 G2 0"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "*/ G1 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- G8 0 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "*/ 21600 G0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "*/ 21600 G4 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "*/ 21600 G5 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G13", "*/ 21600 G7 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G14", "*/ G1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G15", "+- G5 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G16", "+- G0 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G17", "*/ G2 G15 G16"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 15429 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 9257 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 7200 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 18001 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 9257 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 18514 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 15000 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 7200 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ G12 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T20", "*/ G1 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T21", "*/ 21600 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T12", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T13", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T14", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T15", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T16", "T8", "T9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T17", "T10", "T11"));
            geometry.ShapeTextRectangle.FromString("T18", "T19", "T20", "T21");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("G4", "0"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G5", "G2"));
            item.Instructions.Add(new PathLine("G5", "G12"));
            item.Instructions.Add(new PathLine("0", "G12"));
            item.Instructions.Add(new PathLine("0", "21600"));
            item.Instructions.Add(new PathLine("G1", "21600"));
            item.Instructions.Add(new PathLine("G1", "G2"));
            item.Instructions.Add(new PathLine("21600", "G2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateBlockArc()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 11796480"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 5400"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "*/ adj1 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("W0", "+- G1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("W1", "+- G1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("W2", "?: W0 W0 W1"));
            geometry.Guides.Add(new ModelShapeGuide("W3", "+- 10800000 0 W2"));
            geometry.Guides.Add(new ModelShapeGuide("W4", "+- W3 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("W5", "?: W3 W3 W4"));
            geometry.Guides.Add(new ModelShapeGuide("W6", "+- W5 0 W2"));
            geometry.Guides.Add(new ModelShapeGuide("W7", "+- W6 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("W8", "?: W6 W6 W7"));
            geometry.Guides.Add(new ModelShapeGuide("W9", "+- 0 0 W8"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- 0 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 180 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- G1 T0 T1"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 90 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- G1 T2 T3"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "*/ G4 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 90 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- G1 T4 T5"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G6 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "abs G1"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 90 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- G8 T6 T7"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "?: G9 G7 G5"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 360 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "+- G10 T8 T9"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "?: G10 G11 G10"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 0 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 360 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G13", "+- G12 T10 T11"));
            geometry.Guides.Add(new ModelShapeGuide("G14", "?: G12 G13 G12"));
            geometry.Guides.Add(new ModelShapeGuide("G15", "+- 0 0 G14"));
            geometry.Guides.Add(new ModelShapeGuide("G16", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G17", "+- 10800 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G18", "*/ G0 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G19", "+- G18 5400 0"));
            geometry.Guides.Add(new ModelShapeGuide("G20", "cos G19 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G21", "sin G19 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G22", "+- G20 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G23", "+- G21 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G24", "+- 10800 0 G20"));
            geometry.Guides.Add(new ModelShapeGuide("G25", "+- G0 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G26", "?: G9 G17 G25"));
            geometry.Guides.Add(new ModelShapeGuide("G27", "?: G9 0 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G28", "cos 10800 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G29", "sin 10800 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G30", "sin G0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G31", "+- G28 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G32", "+- G29 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G33", "+- G30 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G34", "?: G4 0 G31"));
            geometry.Guides.Add(new ModelShapeGuide("G35", "?: G1 G34 0"));
            geometry.Guides.Add(new ModelShapeGuide("G36", "?: G6 G35 G31"));
            geometry.Guides.Add(new ModelShapeGuide("G37", "+- 21600 0 G36"));
            geometry.Guides.Add(new ModelShapeGuide("G38", "?: G4 0 G33"));
            geometry.Guides.Add(new ModelShapeGuide("G39", "?: G1 G38 G32"));
            geometry.Guides.Add(new ModelShapeGuide("G40", "?: G6 G39 0"));
            geometry.Guides.Add(new ModelShapeGuide("G41", "?: G4 G32 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G42", "?: G6 G41 G33"));
            geometry.Guides.Add(new ModelShapeGuide("R7", "cos G0 W2"));
            geometry.Guides.Add(new ModelShapeGuide("R8", "sin G0 W2"));
            geometry.Guides.Add(new ModelShapeGuide("R9", "+- R7 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R10", "+- R8 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R11", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R12", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R13", "+- W2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R14", "+- W8 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R15", "cos 10800 W5"));
            geometry.Guides.Add(new ModelShapeGuide("R16", "sin 10800 W5"));
            geometry.Guides.Add(new ModelShapeGuide("R17", "+- R15 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R18", "+- R16 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R19", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R20", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R21", "+- W5 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R22", "+- W9 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 2700 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 5400 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 18900 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T20", "*/ G36 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T21", "*/ G40 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T22", "*/ G37 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T23", "*/ G42 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T12", "T13"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T14", "T15"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T16", "T17"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T18", "T19"));
            geometry.ShapeTextRectangle.FromString("T20", "T21", "T22", "T23");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("R9", "R10"));
            item.Instructions.Add(new PathArc("R11", "R12", "R13", "R14"));
            item.Instructions.Add(new PathLine("R17", "R18"));
            item.Instructions.Add(new PathArc("R19", "R20", "R21", "R22"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCircularArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val -11796480"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 5400"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "*/ adj2 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "*/ adj1 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("R0", "+- G0 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("R1", "+- G1 21600000 0"));
            geometry.Guides.Add(new ModelShapeGuide("R2", "?: G0 G0 R0"));
            geometry.Guides.Add(new ModelShapeGuide("R3", "?: G1 G1 R1"));
            geometry.Guides.Add(new ModelShapeGuide("R4", "+- R3 0 R2"));
            geometry.Guides.Add(new ModelShapeGuide("R5", "+- 21600000 0 R4"));
            geometry.Guides.Add(new ModelShapeGuide("R6", "+- 0 0 R5"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- G0 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- 0 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 360 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- G2 T0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "?: G2 G2 G5"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "+- 0 0 G6"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- 0 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "+- G8 0 2700"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "cos G10 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "sin G10 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G13", "cos 13500 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G14", "sin 13500 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G15", "+- G11 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G16", "+- G12 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G17", "+- G13 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G18", "+- G14 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G19", "*/ G8 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G20", "+- G19 5400 0"));
            geometry.Guides.Add(new ModelShapeGuide("G21", "cos G20 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G22", "sin G20 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G23", "+- G21 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G24", "+- G12 G23 G22"));
            geometry.Guides.Add(new ModelShapeGuide("G25", "+- G22 G23 G11"));
            geometry.Guides.Add(new ModelShapeGuide("G26", "cos 10800 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G27", "sin 10800 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G28", "cos G8 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G29", "sin G8 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G30", "+- G26 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G31", "+- G27 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G32", "+- G28 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G33", "+- G29 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G34", "+- G19 5400 0"));
            geometry.Guides.Add(new ModelShapeGuide("G35", "cos G34 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G36", "sin G34 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G37", "+/ G1 G0 2"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 180 60000 1"));
            geometry.Guides.Add(new ModelShapeGuide("G38", "+- G37 T2 0"));
            geometry.Guides.Add(new ModelShapeGuide("G39", "?: G2 G37 G38"));
            geometry.Guides.Add(new ModelShapeGuide("G40", "cos 10800 G39"));
            geometry.Guides.Add(new ModelShapeGuide("G41", "sin 10800 G39"));
            geometry.Guides.Add(new ModelShapeGuide("G42", "cos G8 G39"));
            geometry.Guides.Add(new ModelShapeGuide("G43", "sin G8 G39"));
            geometry.Guides.Add(new ModelShapeGuide("G44", "+- G40 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G45", "+- G41 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G46", "+- G42 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G47", "+- G43 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G48", "+- G35 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G49", "+- G36 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R7", "cos G8 G0"));
            geometry.Guides.Add(new ModelShapeGuide("R8", "sin G8 G0"));
            geometry.Guides.Add(new ModelShapeGuide("R9", "+- R7 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R10", "+- R8 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R11", "+- G8 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R12", "+- G8 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R13", "+- R2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R14", "+- R6 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R15", "cos 10800 G1"));
            geometry.Guides.Add(new ModelShapeGuide("R16", "sin 10800 G1"));
            geometry.Guides.Add(new ModelShapeGuide("R17", "+- R15 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R18", "+- R16 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("R19", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R20", "+- 10800 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R21", "+- R3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("R22", "+- R5 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 10799 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 2700 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 10799 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 5400 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 24300 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 18900 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 16200 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 13500 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ 18437 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T8", "T9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T10", "T11"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T12", "T13"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T14", "T15"));
            geometry.ShapeTextRectangle.FromString("T16", "T17", "T18", "T19");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("R9", "R10"));
            item.Instructions.Add(new PathArc("R11", "R12", "R13", "R14"));
            item.Instructions.Add(new PathLine("R17", "R18"));
            item.Instructions.Add(new PathArc("R19", "R20", "R21", "R22"));
            item.Instructions.Add(new PathLine("G30", "G31"));
            item.Instructions.Add(new PathLine("G17", "G18"));
            item.Instructions.Add(new PathLine("G24", "G25"));
            item.Instructions.Add(new PathLine("G15", "G16"));
            item.Instructions.Add(new PathLine("G32", "G33"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDonut()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 5400"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- 10800 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- 10800 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 18437 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 18437 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ 18437 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T8", "T9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T10", "T11"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T12", "T13"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T14", "T15"));
            geometry.ShapeTextRectangle.FromString("T16", "T17", "T18", "T19");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("0", "10800"));
            item.Instructions.Add(new PathArc("10800", "10800", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "0", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("G0", "10800"));
            item.Instructions.Add(new PathArc("G1", "G2", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("G1", "G2", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("G1", "G2", "0", "cd4"));
            item.Instructions.Add(new PathArc("G1", "G2", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateHeart()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 10860 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 2187 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 2928 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 10860 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 18672 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 5037 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 2277 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 16557 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 13677 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T8", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T9", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T10", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T11", "T6", "T7"));
            geometry.ShapeTextRectangle.FromString("T12", "T13", "T14", "T15");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("10860", "2187"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "10451", "1746", "9529", "1018", "9015", "730"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "7865", "152", "6685", "0", "5415", "0"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "4175", "152", "2995", "575", "1967", "1305"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "1150", "2187", "575", "3222", "242", "4220"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "0", "5410", "242", "6560", "575", "7597"));
            item.Instructions.Add(new PathLine("10860", "21600"));
            item.Instructions.Add(new PathLine("20995", "7597"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "21480", "6560", "21600", "5410", "21480", "4220"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "21115", "3222", "20420", "2187", "19632", "1305"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "18575", "575", "17425", "152", "16275", "0"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "15005", "0", "13735", "152", "12705", "730"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "12176", "1018", "11254", "1746", "10860", "2187"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftRightUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 6480"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 8640"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 6171"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- 21600 0 adj1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- 21600 0 adj2"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "*/ G0 21600 G3"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "*/ G1 21600 G3"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G2 G3 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "*/ 10800 21600 G3"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "*/ G4 21600 G3"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "+- 21600 0 G7"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "+- G5 0 G8"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "+- G6 0 G8"));
            geometry.Guides.Add(new ModelShapeGuide("G13", "*/ G12 G7 G11"));
            geometry.Guides.Add(new ModelShapeGuide("G14", "+- 21600 0 G13"));
            geometry.Guides.Add(new ModelShapeGuide("G15", "+- G0 0 10800"));
            geometry.Guides.Add(new ModelShapeGuide("G16", "+- G1 0 10800"));
            geometry.Guides.Add(new ModelShapeGuide("G17", "*/ G2 G16 G15"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 15429 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 18514 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 15429 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ G13 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ G6 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ G14 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ G9 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T8", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T9", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T10", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T11", "T6", "T7"));
            geometry.ShapeTextRectangle.FromString("T12", "T13", "T14", "T15");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("10800", "0"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G1", "G2"));
            item.Instructions.Add(new PathLine("G1", "G6"));
            item.Instructions.Add(new PathLine("G7", "G6"));
            item.Instructions.Add(new PathLine("G7", "G5"));
            item.Instructions.Add(new PathLine("0", "G8"));
            item.Instructions.Add(new PathLine("G7", "21600"));
            item.Instructions.Add(new PathLine("G7", "G9"));
            item.Instructions.Add(new PathLine("G10", "G9"));
            item.Instructions.Add(new PathLine("G10", "21600"));
            item.Instructions.Add(new PathLine("21600", "G8"));
            item.Instructions.Add(new PathLine("G10", "G5"));
            item.Instructions.Add(new PathLine("G10", "G6"));
            item.Instructions.Add(new PathLine("G4", "G6"));
            item.Instructions.Add(new PathLine("G4", "G2"));
            item.Instructions.Add(new PathLine("G3", "G2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateLeftUpArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 9257"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 18514"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 6171"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "*/ adj1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- G3 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- 21600 G0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- G1 G2 0"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "*/ G1 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- G8 0 21600"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "+- G5 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "+- G0 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "*/ G2 G10 G11"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 15429 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 9257 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 6171 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 6171 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 9257 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 15429 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 6171 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 12343 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 18514 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 18514 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 12343 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 6171 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T20", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T21", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T22", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T23", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T24", "*/ G12 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T25", "*/ G5 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T26", "*/ G1 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T27", "*/ G1 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T16", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T17", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T18", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T19", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T20", "T8", "T9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T21", "T10", "T11"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T22", "T12", "T13"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T23", "T14", "T15"));
            geometry.ShapeTextRectangle.FromString("T24", "T25", "T26", "T27");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("G4", "0"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G5", "G2"));
            item.Instructions.Add(new PathLine("G5", "G5"));
            item.Instructions.Add(new PathLine("G2", "G5"));
            item.Instructions.Add(new PathLine("G2", "G0"));
            item.Instructions.Add(new PathLine("0", "G4"));
            item.Instructions.Add(new PathLine("G2", "21600"));
            item.Instructions.Add(new PathLine("G2", "G1"));
            item.Instructions.Add(new PathLine("G1", "G1"));
            item.Instructions.Add(new PathLine("G1", "G2"));
            item.Instructions.Add(new PathLine("21600", "G2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateNoSmoking()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 2700"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "*/ G0 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- 21600 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "*/ G2 G2 1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "*/ G0 G0 1"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- G3 0 G4"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "*/ G5 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "sqrt G6"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "*/ G4 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "sqrt G8"));
            geometry.Guides.Add(new ModelShapeGuide("G10", "+- G7 G9 0"));
            geometry.Guides.Add(new ModelShapeGuide("G11", "+- G7 0 G9"));
            geometry.Guides.Add(new ModelShapeGuide("G12", "+- G10 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G13", "+- 10800 0 G10"));
            geometry.Guides.Add(new ModelShapeGuide("G14", "+- G11 10800 0"));
            geometry.Guides.Add(new ModelShapeGuide("G15", "+- 10800 0 G11"));
            geometry.Guides.Add(new ModelShapeGuide("G16", "+- 21600 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G17", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G18", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G19", "+- G16 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G20", "+- G16 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G21", "+- G12 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G22", "+- G14 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G23", "+- G15 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G24", "+- G13 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G25", "+- G19 0 G17"));
            geometry.Guides.Add(new ModelShapeGuide("G26", "+- G20 0 G18"));
            geometry.Guides.Add(new ModelShapeGuide("G27", "*/ G25 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G28", "*/ G26 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G29", "+- G17 G27 0"));
            geometry.Guides.Add(new ModelShapeGuide("G30", "+- G18 G28 0"));
            geometry.Guides.Add(new ModelShapeGuide("G31", "+- G21 0 G29"));
            geometry.Guides.Add(new ModelShapeGuide("G32", "+- G22 0 G30"));
            geometry.Guides.Add(new ModelShapeGuide("G33", "+- G23 0 G29"));
            geometry.Guides.Add(new ModelShapeGuide("G34", "+- G24 0 G30"));
            geometry.Guides.Add(new ModelShapeGuide("G35", "at2 G31 G32"));
            geometry.Guides.Add(new ModelShapeGuide("G36", "*/ G31 G33 1"));
            geometry.Guides.Add(new ModelShapeGuide("G37", "*/ G32 G34 1"));
            geometry.Guides.Add(new ModelShapeGuide("G38", "+- G36 G37 0"));
            geometry.Guides.Add(new ModelShapeGuide("G39", "*/ G31 G34 1"));
            geometry.Guides.Add(new ModelShapeGuide("G40", "*/ G33 G32 1"));
            geometry.Guides.Add(new ModelShapeGuide("G41", "+- G39 0 G40"));
            geometry.Guides.Add(new ModelShapeGuide("G42", "at2 G38 G41"));
            geometry.Guides.Add(new ModelShapeGuide("G43", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G44", "+- G0 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G45", "+- G16 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G46", "+- G16 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G47", "+- G13 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G48", "+- G15 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G49", "+- G14 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G50", "+- G12 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G51", "+- G45 0 G43"));
            geometry.Guides.Add(new ModelShapeGuide("G52", "+- G46 0 G44"));
            geometry.Guides.Add(new ModelShapeGuide("G53", "*/ G51 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G54", "*/ G52 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G55", "+- G43 G53 0"));
            geometry.Guides.Add(new ModelShapeGuide("G56", "+- G44 G54 0"));
            geometry.Guides.Add(new ModelShapeGuide("G57", "+- G47 0 G55"));
            geometry.Guides.Add(new ModelShapeGuide("G58", "+- G48 0 G56"));
            geometry.Guides.Add(new ModelShapeGuide("G59", "+- G49 0 G55"));
            geometry.Guides.Add(new ModelShapeGuide("G60", "+- G50 0 G56"));
            geometry.Guides.Add(new ModelShapeGuide("G61", "at2 G57 G58"));
            geometry.Guides.Add(new ModelShapeGuide("G62", "*/ G57 G59 1"));
            geometry.Guides.Add(new ModelShapeGuide("G63", "*/ G58 G60 1"));
            geometry.Guides.Add(new ModelShapeGuide("G64", "+- G62 G63 0"));
            geometry.Guides.Add(new ModelShapeGuide("G65", "*/ G57 G60 1"));
            geometry.Guides.Add(new ModelShapeGuide("G66", "*/ G59 G58 1"));
            geometry.Guides.Add(new ModelShapeGuide("G67", "+- G65 0 G66"));
            geometry.Guides.Add(new ModelShapeGuide("G68", "at2 G64 G67"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 18437 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 18437 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 3163 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 3163 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 18437 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T19", "*/ 18437 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T8", "T9"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T10", "T11"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T12", "T13"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T14", "T15"));
            geometry.ShapeTextRectangle.FromString("T16", "T17", "T18", "T19");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("0", "10800"));
            item.Instructions.Add(new PathArc("10800", "10800", "cd2", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "3cd4", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "0", "cd4"));
            item.Instructions.Add(new PathArc("10800", "10800", "cd4", "cd4"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("G21", "G22"));
            item.Instructions.Add(new PathArc("G27", "G28", "G35", "G42"));
            item.Instructions.Add(new PathClose());
            item.Instructions.Add(new PathMove("G47", "G48"));
            item.Instructions.Add(new PathArc("G53", "G54", "G61", "G68"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateQuadArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 6480"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 8640"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 4320"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- 21600 0 adj1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- 21600 0 adj2"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- 21600 0 adj3"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- adj1 0 10800"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "+- adj2 0 10800"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "*/ G7 adj3 G6"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- 21600 0 G8"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ G8 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ G1 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ G9 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ G4 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("5400000", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("10800000", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("16200000", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("T0", "T1", "T2", "T3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("10800", "0"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G1", "G2"));
            item.Instructions.Add(new PathLine("G1", "G1"));
            item.Instructions.Add(new PathLine("G2", "G1"));
            item.Instructions.Add(new PathLine("G2", "G0"));
            item.Instructions.Add(new PathLine("0", "10800"));
            item.Instructions.Add(new PathLine("G2", "G3"));
            item.Instructions.Add(new PathLine("G2", "G4"));
            item.Instructions.Add(new PathLine("G1", "G4"));
            item.Instructions.Add(new PathLine("G1", "G5"));
            item.Instructions.Add(new PathLine("G0", "G5"));
            item.Instructions.Add(new PathLine("10800", "21600"));
            item.Instructions.Add(new PathLine("G3", "G5"));
            item.Instructions.Add(new PathLine("G4", "G5"));
            item.Instructions.Add(new PathLine("G4", "G4"));
            item.Instructions.Add(new PathLine("G5", "G4"));
            item.Instructions.Add(new PathLine("G5", "G3"));
            item.Instructions.Add(new PathLine("21600", "10800"));
            item.Instructions.Add(new PathLine("G5", "G0"));
            item.Instructions.Add(new PathLine("G5", "G1"));
            item.Instructions.Add(new PathLine("G4", "G1"));
            item.Instructions.Add(new PathLine("G4", "G2"));
            item.Instructions.Add(new PathLine("G3", "G2"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateQuadArrowCallout()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 5400"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 8100"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj3", "val 2700"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj4", "val 9450"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- adj3 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- adj4 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- 21600 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "+- 21600 0 G3"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- G0 21600 0"));
            geometry.Guides.Add(new ModelShapeGuide("G7", "*/ G6 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("G8", "+- 21600 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G9", "+- 21600 0 G2"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ G0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ G0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ G8 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ G8 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "r", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("5400000", "hc", "b"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("10800000", "l", "vc"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("16200000", "hc", "t"));
            geometry.ShapeTextRectangle.FromString("T0", "T1", "T2", "T3");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("G0", "G0"));
            item.Instructions.Add(new PathLine("G3", "G0"));
            item.Instructions.Add(new PathLine("G3", "G2"));
            item.Instructions.Add(new PathLine("G1", "G2"));
            item.Instructions.Add(new PathLine("10800", "0"));
            item.Instructions.Add(new PathLine("G4", "G2"));
            item.Instructions.Add(new PathLine("G5", "G2"));
            item.Instructions.Add(new PathLine("G5", "G0"));
            item.Instructions.Add(new PathLine("G8", "G0"));
            item.Instructions.Add(new PathLine("G8", "G3"));
            item.Instructions.Add(new PathLine("G9", "G3"));
            item.Instructions.Add(new PathLine("G9", "G1"));
            item.Instructions.Add(new PathLine("21600", "10800"));
            item.Instructions.Add(new PathLine("G9", "G4"));
            item.Instructions.Add(new PathLine("G9", "G5"));
            item.Instructions.Add(new PathLine("G8", "G5"));
            item.Instructions.Add(new PathLine("G8", "G8"));
            item.Instructions.Add(new PathLine("G5", "G8"));
            item.Instructions.Add(new PathLine("G5", "G9"));
            item.Instructions.Add(new PathLine("G4", "G9"));
            item.Instructions.Add(new PathLine("10800", "21600"));
            item.Instructions.Add(new PathLine("G1", "G9"));
            item.Instructions.Add(new PathLine("G3", "G9"));
            item.Instructions.Add(new PathLine("G3", "G8"));
            item.Instructions.Add(new PathLine("G0", "G8"));
            item.Instructions.Add(new PathLine("G0", "G5"));
            item.Instructions.Add(new PathLine("G2", "G5"));
            item.Instructions.Add(new PathLine("G2", "G4"));
            item.Instructions.Add(new PathLine("0", "10800"));
            item.Instructions.Add(new PathLine("G2", "G1"));
            item.Instructions.Add(new PathLine("G2", "G3"));
            item.Instructions.Add(new PathLine("G0", "G3"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRectangle()
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

        private static ModelShapeCustomGeometry GenerateStripedRightArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 16200"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 5400"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- adj2 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G2", "+- 21600 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G3", "+- 10800 0 G1"));
            geometry.Guides.Add(new ModelShapeGuide("G4", "+- 21600 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("G5", "*/ G4 G3 10800"));
            geometry.Guides.Add(new ModelShapeGuide("G6", "+- 21600 0 G5"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 16200 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 16200 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 11796480 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 3375 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ G1 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ G6 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ G2 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T8", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T9", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T10", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T11", "T6", "T7"));
            geometry.ShapeTextRectangle.FromString("T12", "T13", "T14", "T15");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("G0", "0"));
            item.Instructions.Add(new PathLine("G0", "G1"));
            item.Instructions.Add(new PathLine("3375", "G1"));
            item.Instructions.Add(new PathLine("3375", "G2"));
            item.Instructions.Add(new PathLine("G0", "G2"));
            item.Instructions.Add(new PathLine("G0", "21600"));
            item.Instructions.Add(new PathLine("21600", "10800"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("1350", "G1"));
            item.Instructions.Add(new PathLine("1350", "G2"));
            item.Instructions.Add(new PathLine("2700", "G2"));
            item.Instructions.Add(new PathLine("2700", "G1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance) {
                Width = 0x5460L,
                Height = 0x5460L
            };
            item.Instructions.Add(new PathMove("0", "G1"));
            item.Instructions.Add(new PathLine("0", "G2"));
            item.Instructions.Add(new PathLine("675", "G2"));
            item.Instructions.Add(new PathLine("675", "G1"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTrapezoid()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 5400"));
            geometry.Guides.Add(new ModelShapeGuide("G0", "+- adj1 0 0"));
            geometry.Guides.Add(new ModelShapeGuide("G1", "+- 21600 0 G0"));
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 18900 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 2700 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 10800 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 10800 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 4500 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 4500 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 17100 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 17100 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("0", "T6", "T7"));
            geometry.ShapeTextRectangle.FromString("T8", "T9", "T10", "T11");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("0", "0"));
            item.Instructions.Add(new PathLine("G0", "21600"));
            item.Instructions.Add(new PathLine("G1", "21600"));
            item.Instructions.Add(new PathLine("21600", "0"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateUturnArrow()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.Guides.Add(new ModelShapeGuide("T0", "*/ 9250 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T1", "*/ 0 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T2", "*/ 3055 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T3", "*/ 21600 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T4", "*/ 9725 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T5", "*/ 8310 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T6", "*/ 15662 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T7", "*/ 14285 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T8", "*/ 21600 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T9", "*/ 8310 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T10", "*/ 17694720 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T11", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T12", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T13", "*/ 5898240 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T14", "*/ 0 60000 65536"));
            geometry.Guides.Add(new ModelShapeGuide("T15", "*/ 0 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T16", "*/ 8310 h 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T17", "*/ 6110 w 21600"));
            geometry.Guides.Add(new ModelShapeGuide("T18", "*/ 21600 h 21600"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T10", "T0", "T1"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T11", "T2", "T3"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T12", "T4", "T5"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T13", "T6", "T7"));
            geometry.ConnectionSites.Add(new ModelShapeConnection("T14", "T8", "T9"));
            geometry.ShapeTextRectangle.FromString("T15", "T16", "T17", "T18");
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("15662", "14285"));
            item.Instructions.Add(new PathLine("21600", "8310"));
            item.Instructions.Add(new PathLine("18630", "8310"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "18630", "3721", "14430", "0", "9250", "0"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "4141", "0", "0", "3799", "0", "8485"));
            item.Instructions.Add(new PathLine("0", "21600"));
            item.Instructions.Add(new PathLine("6110", "21600"));
            item.Instructions.Add(new PathLine("6110", "8310"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "6110", "6947", "7362", "5842", "8907", "5842"));
            item.Instructions.Add(new PathLine("9725", "5842"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "11269", "5842", "12520", "6947", "12520", "8310"));
            item.Instructions.Add(new PathLine("9725", "8310"));
            item.Instructions.Add(new PathClose());
            geometry.Paths.Add(item);
            return geometry;
        }

        public static ModelShapeCustomGeometry GetCustomGeometryByShapeTypeCode(int shapeTypeCode)
        {
            Dictionary<int, ModelShapeCustomGeometry> calculatedGeometries = BinaryCustomGeometryGenerator.calculatedGeometries;
            lock (calculatedGeometries)
            {
                return GetCustomGeometryByShapeTypeCodeCore(shapeTypeCode);
            }
        }

        private static ModelShapeCustomGeometry GetCustomGeometryByShapeTypeCodeCore(int shapeTypeCode)
        {
            ModelShapeCustomGeometry geometry;
            Func<ModelShapeCustomGeometry> func;
            if (calculatedGeometries.TryGetValue(shapeTypeCode, out geometry))
            {
                return geometry;
            }
            if (!customGeometrysGenerators.TryGetValue(shapeTypeCode, out func))
            {
                return GetCustomGeometryByShapeTypeCodeCore(1);
            }
            ModelShapeCustomGeometry geometry2 = func();
            calculatedGeometries.Add(shapeTypeCode, geometry2);
            return geometry2;
        }

        private static Dictionary<int, Func<ModelShapeCustomGeometry>> InitializeGenerators() => 
            new Dictionary<int, Func<ModelShapeCustomGeometry>> { 
                { 
                    1,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateRectangle)
                },
                { 
                    8,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateTrapezoid)
                },
                { 
                    0x17,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateDonut)
                },
                { 
                    0x39,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateNoSmoking)
                },
                { 
                    0x5f,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateBlockArc)
                },
                { 
                    0x4a,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateHeart)
                },
                { 
                    0x13,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateArc)
                },
                { 
                    0x4c,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateQuadArrow)
                },
                { 
                    0xb6,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateLeftRightUpArrow)
                },
                { 
                    0x5b,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateBentArrow)
                },
                { 
                    0x65,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateUturnArrow)
                },
                { 
                    0x59,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateLeftUpArrow)
                },
                { 
                    90,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateBentUpArrow)
                },
                { 
                    0x5d,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateStripedRightArrow)
                },
                { 
                    0x53,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateQuadArrowCallout)
                },
                { 
                    0x63,
                    new Func<ModelShapeCustomGeometry>(BinaryCustomGeometryGenerator.GenerateCircularArrow)
                }
            };
    }
}

