namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public static class PresetTextWarpGenerators
    {
        private static readonly Dictionary<DrawingPresetTextWarp, ModelShapeCustomGeometry> calculatedGeometries = new Dictionary<DrawingPresetTextWarp, ModelShapeCustomGeometry>();
        private static readonly Dictionary<DrawingPresetTextWarp, Func<ModelShapeCustomGeometry>> geometrysGenerators = InitializeGenerators();

        private static ModelShapeCustomGeometry GenerateArchDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 0"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "+- 32400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("nv1", "+- 0 0 v1"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "?: nv1 v2 v1"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- adval 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- d1 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "+- 0 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: w2 d1 d2"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: v1 d2 c2"));
            geometry.Guides.Add(new ModelShapeGuide("c0", "?: w1 d1 c1"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: stAng c0 v3"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 wd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 hd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj", "0", "21599999", "", "", "", "x1", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateArchDownPour()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 0"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "+- 32400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("nv1", "+- 0 0 v1"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "?: nv1 v2 v1"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- adval 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- d1 0 21600000"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "+- 0 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: w2 d1 d2"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: v1 d2 c2"));
            geometry.Guides.Add(new ModelShapeGuide("c0", "?: w1 d1 c1"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: stAng c0 v3"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("adval2", "pin 0 adj2 99000"));
            geometry.Guides.Add(new ModelShapeGuide("ratio", "*/ adval2 1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin iwd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos ihd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 iwd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 ihd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt3", "sin iwd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("ht3", "cos ihd2 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "cat2 iwd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "sat2 ihd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy3 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "adj2", "0", "100000", "x2", "y2"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x3", "y3"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "stAng", "swAng"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stAng", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateArchUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val cd2"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "+- 32400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("end", "?: v1 v1 v2"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- end 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- 21600000 d1 0"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: w2 d1 d2"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: v1 d2 c2"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: w1 d1 c1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj", "0", "21599999", "", "", "", "x1", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateArchUpPour()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "+- 32400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("end", "?: v1 v1 v2"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- end 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- 21600000 d1 0"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: w2 d1 d2"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: v1 d2 c2"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "?: w1 d1 c1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("adval2", "pin 0 adj2 99000"));
            geometry.Guides.Add(new ModelShapeGuide("ratio", "*/ adval2 1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin iwd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos ihd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 iwd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 ihd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "adj2", "0", "100000", "x2", "y2"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateButton()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("bot", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("lef", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("top", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("rig", "+- 21600000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("c3", "?: top adval 0"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: lef 10800000 c3"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: bot rig c2"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "?: adval c1 0"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 21600000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("stAngB", "?: stAng w1 0"));
            geometry.Guides.Add(new ModelShapeGuide("td1", "*/ bot 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("td2", "*/ top 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("ntd2", "+- 0 0 td2"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 0 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("c6", "?: top ntd2 w2"));
            geometry.Guides.Add(new ModelShapeGuide("c5", "?: lef 10800000 c6"));
            geometry.Guides.Add(new ModelShapeGuide("c4", "?: bot td1 c5"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "?: adval c4 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("swAngT", "+- 0 0 v1"));
            geometry.Guides.Add(new ModelShapeGuide("stT", "?: lef stAngB stAng"));
            geometry.Guides.Add(new ModelShapeGuide("stB", "?: lef stAng stAngB"));
            geometry.Guides.Add(new ModelShapeGuide("swT", "?: lef v1 swAngT"));
            geometry.Guides.Add(new ModelShapeGuide("swB", "?: lef swAngT v1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin wd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos hd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 wd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 hd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt3", "sin wd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("ht3", "cos hd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("dx3", "cat2 wd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "sat2 hd2 ht3 wt3"));
            geometry.Guides.Add(new ModelShapeGuide("x3", "+- hc dx3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- vc dy3 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj", "0", "21599999", "", "", "", "x3", "y3"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stT", "swT"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "vc"));
            item.Instructions.Add(new PathLine("r", "vc"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stB", "swB"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateButtonPour()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("bot", "+- 5400000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("lef", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("top", "+- 16200000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("rig", "+- 21600000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("c3", "?: top adval 0"));
            geometry.Guides.Add(new ModelShapeGuide("c2", "?: lef 10800000 c3"));
            geometry.Guides.Add(new ModelShapeGuide("c1", "?: bot rig c2"));
            geometry.Guides.Add(new ModelShapeGuide("stAng", "?: adval c1 0"));
            geometry.Guides.Add(new ModelShapeGuide("w1", "+- 21600000 0 stAng"));
            geometry.Guides.Add(new ModelShapeGuide("stAngB", "?: stAng w1 0"));
            geometry.Guides.Add(new ModelShapeGuide("td1", "*/ bot 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("td2", "*/ top 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("ntd2", "+- 0 0 td2"));
            geometry.Guides.Add(new ModelShapeGuide("w2", "+- 0 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("c6", "?: top ntd2 w2"));
            geometry.Guides.Add(new ModelShapeGuide("c5", "?: lef 10800000 c6"));
            geometry.Guides.Add(new ModelShapeGuide("c4", "?: bot td1 c5"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "?: adval c4 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("swAngT", "+- 0 0 v1"));
            geometry.Guides.Add(new ModelShapeGuide("stT", "?: lef stAngB stAng"));
            geometry.Guides.Add(new ModelShapeGuide("stB", "?: lef stAng stAngB"));
            geometry.Guides.Add(new ModelShapeGuide("swT", "?: lef v1 swAngT"));
            geometry.Guides.Add(new ModelShapeGuide("swB", "?: lef swAngT v1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt6", "sin wd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("ht6", "cos hd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("dx6", "cat2 wd2 ht6 wt6"));
            geometry.Guides.Add(new ModelShapeGuide("dy6", "sat2 hd2 ht6 wt6"));
            geometry.Guides.Add(new ModelShapeGuide("x6", "+- hc dx6 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- vc dy6 0"));
            geometry.Guides.Add(new ModelShapeGuide("adval2", "pin 40000 adj2 99000"));
            geometry.Guides.Add(new ModelShapeGuide("ratio", "*/ adval2 1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin iwd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos ihd2 stT"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 iwd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 ihd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.Guides.Add(new ModelShapeGuide("wt5", "sin iwd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("ht5", "cos ihd2 stB"));
            geometry.Guides.Add(new ModelShapeGuide("dx5", "cat2 iwd2 ht5 wt5"));
            geometry.Guides.Add(new ModelShapeGuide("dy5", "sat2 ihd2 ht5 wt5"));
            geometry.Guides.Add(new ModelShapeGuide("x5", "+- hc dx5 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- vc dy5 0"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- hd2 0 ihd2"));
            geometry.Guides.Add(new ModelShapeGuide("d12", "*/ d1 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("yu", "+- vc 0 d12"));
            geometry.Guides.Add(new ModelShapeGuide("yd", "+- vc d12 0"));
            geometry.Guides.Add(new ModelShapeGuide("v1", "*/ d12 d12 1"));
            geometry.Guides.Add(new ModelShapeGuide("v2", "*/ ihd2 ihd2 1"));
            geometry.Guides.Add(new ModelShapeGuide("v3", "*/ v1 1 v2"));
            geometry.Guides.Add(new ModelShapeGuide("v4", "+- 1 0 v3"));
            geometry.Guides.Add(new ModelShapeGuide("v5", "*/ iwd2 iwd2 1"));
            geometry.Guides.Add(new ModelShapeGuide("v6", "*/ v4 v5 1"));
            geometry.Guides.Add(new ModelShapeGuide("v7", "sqrt v6"));
            geometry.Guides.Add(new ModelShapeGuide("xl", "+- hc 0 v7"));
            geometry.Guides.Add(new ModelShapeGuide("xr", "+- hc v7 0"));
            geometry.Guides.Add(new ModelShapeGuide("wtadj", "sin iwd2 adj1"));
            geometry.Guides.Add(new ModelShapeGuide("htadj", "cos ihd2 adj1"));
            geometry.Guides.Add(new ModelShapeGuide("dxadj", "cat2 iwd2 htadj wtadj"));
            geometry.Guides.Add(new ModelShapeGuide("dyadj", "sat2 ihd2 htadj wtadj"));
            geometry.Guides.Add(new ModelShapeGuide("xadj", "+- hc dxadj 0"));
            geometry.Guides.Add(new ModelShapeGuide("yadj", "+- vc dyadj 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "adj2", "0", "100000", "xadj", "yadj"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stT", "swT"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "stT", "swT"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xl", "yu"));
            item.Instructions.Add(new PathLine("xr", "yu"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("xl", "yd"));
            item.Instructions.Add(new PathLine("xr", "yd"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x5", "y5"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "stB", "swB"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x6", "y6"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "stB", "swB"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCanDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 14286"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 33333"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("ncd2", "*/ cd2 -1 1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "33333", "hc", "y0"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathArc("wd2", "dy", "cd2", "ncd2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "dy", "cd2", "ncd2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCanUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 85714"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 66667 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "+- h 0 dy1"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "66667", "100000", "hc", "y0"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "dy", "cd2", "cd2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathArc("wd2", "dy", "cd2", "cd2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCascadeDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44444"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 28570 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "+- h 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ dy2 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- t dy3 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "28570", "100000", "l", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCascadeUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44444"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 28570 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "+- h 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("dy3", "*/ dy2 1 4"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- t dy3 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "28570", "100000", "r", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "y1"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChevron()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t b y"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "l", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("r", "y"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("hc", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateChevronInverted()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 75000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 50000 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- b 0 y"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "50000", "100000", "l", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("hc", "y1"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("r", "y"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCircle()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("d0", "+- adval 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- 21600000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d3", "?: d1 d1 10799999"));
            geometry.Guides.Add(new ModelShapeGuide("d4", "?: d0 d2 d3"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "*/ d4 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 adj"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj", "0", "21599999", "", "", "", "x1", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCirclePour()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
            geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("adval", "pin 0 adj1 21599999"));
            geometry.Guides.Add(new ModelShapeGuide("d0", "+- adval 0 10800000"));
            geometry.Guides.Add(new ModelShapeGuide("d1", "+- 10800000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d2", "+- 21600000 0 adval"));
            geometry.Guides.Add(new ModelShapeGuide("d3", "?: d1 d1 10799999"));
            geometry.Guides.Add(new ModelShapeGuide("d4", "?: d0 d2 d3"));
            geometry.Guides.Add(new ModelShapeGuide("swAng", "*/ d4 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt1", "sin wd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("ht1", "cos hd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("dx1", "cat2 wd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("dy1", "sat2 hd2 ht1 wt1"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- hc dx1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- vc dy1 0"));
            geometry.Guides.Add(new ModelShapeGuide("adval2", "pin 0 adj2 99000"));
            geometry.Guides.Add(new ModelShapeGuide("ratio", "*/ adval2 1 100000"));
            geometry.Guides.Add(new ModelShapeGuide("iwd2", "*/ wd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("ihd2", "*/ hd2 ratio 1"));
            geometry.Guides.Add(new ModelShapeGuide("wt2", "sin iwd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("ht2", "cos ihd2 adval"));
            geometry.Guides.Add(new ModelShapeGuide("dx2", "cat2 iwd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "sat2 ihd2 ht2 wt2"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- hc dx2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- vc dy2 0"));
            geometry.AdjustHandles.Add(new PolarAdjustHandle("adj1", "0", "21599999", "adj2", "0", "100000", "x2", "y2"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "y1"));
            item.Instructions.Add(new PathArc("wd2", "hd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y2"));
            item.Instructions.Add(new PathArc("iwd2", "ihd2", "adval", "swAng"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurveDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 45977"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 56338"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("gd1", "*/ dy 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("gd2", "*/ dy 5 4"));
            geometry.Guides.Add(new ModelShapeGuide("gd3", "*/ dy 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("gd4", "*/ dy 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("gd5", "+- h 0 gd3"));
            geometry.Guides.Add(new ModelShapeGuide("gd6", "+- gd4 h 0"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t gd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- t gd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- t gd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- t gd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- t gd5 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- t gd6 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l wd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 wd3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "56338", "r", "y0"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "y1", "x2", "y2", "r", "y0"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y5"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "y6", "x2", "y6", "r", "y5"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateCurveUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 45977"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 56338"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("gd1", "*/ dy 3 4"));
            geometry.Guides.Add(new ModelShapeGuide("gd2", "*/ dy 5 4"));
            geometry.Guides.Add(new ModelShapeGuide("gd3", "*/ dy 3 8"));
            geometry.Guides.Add(new ModelShapeGuide("gd4", "*/ dy 1 8"));
            geometry.Guides.Add(new ModelShapeGuide("gd5", "+- h 0 gd3"));
            geometry.Guides.Add(new ModelShapeGuide("gd6", "+- gd4 h 0"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t gd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- t gd2 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- t gd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- t gd4 0"));
            geometry.Guides.Add(new ModelShapeGuide("y5", "+- t gd5 0"));
            geometry.Guides.Add(new ModelShapeGuide("y6", "+- t gd6 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l wd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 wd3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "56338", "l", "y0"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y0"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "y2", "x2", "y1", "r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y5"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x1", "y6", "x2", "y6", "r", "y5"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDeflate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 18750"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 37500"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a ss 100000"));
            geometry.Guides.Add(new ModelShapeGuide("gd0", "*/ dy 4 3"));
            geometry.Guides.Add(new ModelShapeGuide("gd1", "+- h 0 gd0"));
            geometry.Guides.Add(new ModelShapeGuide("adjY", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t gd0 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t gd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x0", "+- l wd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 wd3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "37500", "hc", "adjY"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x0", "y0", "x1", "y0", "r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x0", "y1", "x1", "y1", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDeflateBottom()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 6250 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a ss 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy2", "+- h 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("cp", "+- y1 0 dy2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "6250", "100000", "hc", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cp", "r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDeflateInflate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 35000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 5000 adj 95000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("del", "*/ h 5 100"));
            geometry.Guides.Add(new ModelShapeGuide("dh1", "*/ h 45 100"));
            geometry.Guides.Add(new ModelShapeGuide("dh2", "*/ h 55 100"));
            geometry.Guides.Add(new ModelShapeGuide("yh", "+- dy 0 del"));
            geometry.Guides.Add(new ModelShapeGuide("yl", "+- dy del 0"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- yh yh dh1"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- yl yl dh2"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "5000", "95000", "hc", "dy"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "dh1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y3", "r", "dh1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "dh2"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y4", "r", "dh2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDeflateInflateDeflate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 3000 adj 47000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("del", "*/ h 3 100"));
            geometry.Guides.Add(new ModelShapeGuide("ey1", "*/ h 30 100"));
            geometry.Guides.Add(new ModelShapeGuide("ey2", "*/ h 36 100"));
            geometry.Guides.Add(new ModelShapeGuide("ey3", "*/ h 63 100"));
            geometry.Guides.Add(new ModelShapeGuide("ey4", "*/ h 70 100"));
            geometry.Guides.Add(new ModelShapeGuide("by", "+- b 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("yh1", "+- dy 0 del"));
            geometry.Guides.Add(new ModelShapeGuide("yl1", "+- dy del 0"));
            geometry.Guides.Add(new ModelShapeGuide("yh2", "+- by 0 del"));
            geometry.Guides.Add(new ModelShapeGuide("yl2", "+- by del 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- yh1 yh1 ey1"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- yl1 yl1 ey2"));
            geometry.Guides.Add(new ModelShapeGuide("y3", "+- yh2 yh2 ey3"));
            geometry.Guides.Add(new ModelShapeGuide("y4", "+- yl2 yl2 ey4"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "3000", "47000", "hc", "dy"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ey1"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y1", "r", "ey1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ey2"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y2", "r", "ey2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ey3"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y3", "r", "ey3"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ey4"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "y4", "r", "ey4"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDeflateTop()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 93750"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("cp", "+- y1 dy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "93750", "hc", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "cp", "r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateDoubleWave1()
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
            geometry.Guides.Add(new ModelShapeGuide("of", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs of"));
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
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc of 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "12500", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y2", "x4", "y3", "x5", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x6", "y2", "x7", "y3", "x8", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x9", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x10", "y5", "x11", "y6", "x12", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x13", "y5", "x14", "y6", "x15", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFadeDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 49999"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ a w 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "49999", "", "", "", "x1", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFadeLeft()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 49999"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "49999", "l", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFadeRight()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 49999"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "49999", "r", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateFadeUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 49999"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ a w 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "0", "49999", "", "", "", "x1", "t"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateInflate()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 18750"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 20000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("gd", "*/ dy 1 3"));
            geometry.Guides.Add(new ModelShapeGuide("gd0", "+- 0 0 gd"));
            geometry.Guides.Add(new ModelShapeGuide("gd1", "+- h 0 gd0"));
            geometry.Guides.Add(new ModelShapeGuide("ty", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("by", "+- b 0 dy"));
            geometry.Guides.Add(new ModelShapeGuide("y0", "+- t gd0 0"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t gd1 0"));
            geometry.Guides.Add(new ModelShapeGuide("x0", "+- l wd3 0"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- r 0 wd3"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "20000", "l", "ty"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ty"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x0", "y0", "x1", "y0", "r", "ty"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "by"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x0", "y1", "x1", "y1", "r", "by"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateInflateBottom()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 60000 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ty", "+- t dy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "60000", "100000", "l", "ty"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ty"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "b", "r", "ty"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateInflateTop()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 40000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("ty", "+- t dy 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "50000", "l", "ty"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "ty"));
            item.Instructions.Add(new PathQuadraticBezier(FakeDocumentModel.Instance, "hc", "t", "r", "ty"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GeneratePlain()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 30000 adj 70000"));
            geometry.Guides.Add(new ModelShapeGuide("mid", "*/ a w 100000"));
            geometry.Guides.Add(new ModelShapeGuide("midDir", "+- mid 0 hc"));
            geometry.Guides.Add(new ModelShapeGuide("dl", "+- mid 0 l"));
            geometry.Guides.Add(new ModelShapeGuide("dr", "+- r 0 mid"));
            geometry.Guides.Add(new ModelShapeGuide("dl2", "*/ dl 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("dr2", "*/ dr 2 1"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "?: midDir dr2 dl2"));
            geometry.Guides.Add(new ModelShapeGuide("xr", "+- l dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("xl", "+- r 0 dx"));
            geometry.Guides.Add(new ModelShapeGuide("tlx", "?: midDir l xl"));
            geometry.Guides.Add(new ModelShapeGuide("trx", "?: midDir xr r"));
            geometry.Guides.Add(new ModelShapeGuide("blx", "?: midDir xl l"));
            geometry.Guides.Add(new ModelShapeGuide("brx", "?: midDir r xr"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj", "30000", "70000", "", "", "", "mid", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("tlx", "t"));
            item.Instructions.Add(new PathLine("trx", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("blx", "b"));
            item.Instructions.Add(new PathLine("brx", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRingInside()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 50000 adj 99000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("r", "*/ dy 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t r 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 r"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "50000", "99000", "hc", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "r", "10800000", "21599999"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathArc("wd2", "r", "10800000", "21599999"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateRingOutside()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 50000 adj 99000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("r", "*/ dy 1 2"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t r 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 r"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "50000", "99000", "hc", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathArc("wd2", "r", "10800000", "-21599999"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathArc("wd2", "r", "10800000", "-21599999"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSlantDown()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44445"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 28569 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "28569", "100000", "l", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateSlantUp()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 55555"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 71431"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "71431", "l", "y1"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateStop()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 14286 adj 50000"));
            geometry.Guides.Add(new ModelShapeGuide("dx", "*/ w 1 3"));
            geometry.Guides.Add(new ModelShapeGuide("dy", "*/ a h 100000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "+- l dx 0"));
            geometry.Guides.Add(new ModelShapeGuide("x2", "+- r 0 dx"));
            geometry.Guides.Add(new ModelShapeGuide("y1", "+- t dy 0"));
            geometry.Guides.Add(new ModelShapeGuide("y2", "+- b 0 dy"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "14286", "50000", "l", "dy"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y1"));
            item.Instructions.Add(new PathLine("x1", "t"));
            item.Instructions.Add(new PathLine("x2", "t"));
            item.Instructions.Add(new PathLine("r", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y2"));
            item.Instructions.Add(new PathLine("x1", "b"));
            item.Instructions.Add(new PathLine("x2", "b"));
            item.Instructions.Add(new PathLine("r", "y2"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTriangle()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "*/ a h 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "100000", "l", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y"));
            item.Instructions.Add(new PathLine("hc", "t"));
            item.Instructions.Add(new PathLine("r", "y"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "b"));
            item.Instructions.Add(new PathLine("r", "b"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateTriangleInverted()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(FakeDocumentModel.Instance);
            geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
            geometry.Guides.Add(new ModelShapeGuide("a", "pin 0 adj 100000"));
            geometry.Guides.Add(new ModelShapeGuide("y", "*/ a h 100000"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj", "0", "100000", "l", "y"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "t"));
            item.Instructions.Add(new PathLine("r", "t"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("l", "y"));
            item.Instructions.Add(new PathLine("hc", "b"));
            item.Instructions.Add(new PathLine("r", "y"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWave1()
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
            geometry.Guides.Add(new ModelShapeGuide("of", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs of"));
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
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc of 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "20000", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y2", "x4", "y3", "x5", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x6", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x7", "y5", "x8", "y6", "x10", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWave2()
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
            geometry.Guides.Add(new ModelShapeGuide("of", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs of"));
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
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc of 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "20000", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y3", "x4", "y2", "x5", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x6", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x7", "y6", "x8", "y5", "x10", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        private static ModelShapeCustomGeometry GenerateWave4()
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
            geometry.Guides.Add(new ModelShapeGuide("of", "*/ w a2 100000"));
            geometry.Guides.Add(new ModelShapeGuide("of2", "*/ w a2 50000"));
            geometry.Guides.Add(new ModelShapeGuide("x1", "abs of"));
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
            geometry.Guides.Add(new ModelShapeGuide("xAdj", "+- hc of 0"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("", "", "", "adj1", "0", "12500", "l", "y1"));
            geometry.AdjustHandles.Add(new XYAdjustHandle("adj2", "-10000", "10000", "", "", "", "xAdj", "b"));
            ModelShapePath item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x2", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x3", "y3", "x4", "y2", "x5", "y1"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x6", "y3", "x7", "y2", "x8", "y1"));
            geometry.Paths.Add(item);
            item = new ModelShapePath(FakeDocumentModel.Instance);
            item.Instructions.Add(new PathMove("x9", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x10", "y6", "x11", "y5", "x12", "y4"));
            item.Instructions.Add(new PathCubicBezier(FakeDocumentModel.Instance, "x13", "y6", "x14", "y5", "x15", "y4"));
            geometry.Paths.Add(item);
            return geometry;
        }

        public static ModelShapeCustomGeometry GetCustomGeometryByPreset(DrawingPresetTextWarp warpType)
        {
            Dictionary<DrawingPresetTextWarp, ModelShapeCustomGeometry> calculatedGeometries = PresetTextWarpGenerators.calculatedGeometries;
            lock (calculatedGeometries)
            {
                return GetCustomGeometryByPresetCore(warpType);
            }
        }

        private static ModelShapeCustomGeometry GetCustomGeometryByPresetCore(DrawingPresetTextWarp warpType)
        {
            ModelShapeCustomGeometry geometry;
            Func<ModelShapeCustomGeometry> func;
            if (calculatedGeometries.TryGetValue(warpType, out geometry))
            {
                return geometry;
            }
            if (!geometrysGenerators.TryGetValue(warpType, out func))
            {
                return null;
            }
            ModelShapeCustomGeometry geometry2 = func();
            calculatedGeometries.Add(warpType, geometry2);
            return geometry2;
        }

        private static Dictionary<DrawingPresetTextWarp, Func<ModelShapeCustomGeometry>> InitializeGenerators() => 
            new Dictionary<DrawingPresetTextWarp, Func<ModelShapeCustomGeometry>> { 
                { 
                    DrawingPresetTextWarp.ArchDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateArchDown)
                },
                { 
                    DrawingPresetTextWarp.ArchDownPour,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateArchDownPour)
                },
                { 
                    DrawingPresetTextWarp.ArchUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateArchUp)
                },
                { 
                    DrawingPresetTextWarp.ArchUpPour,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateArchUpPour)
                },
                { 
                    DrawingPresetTextWarp.Button,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateButton)
                },
                { 
                    DrawingPresetTextWarp.ButtonPour,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateButtonPour)
                },
                { 
                    DrawingPresetTextWarp.CanDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCanDown)
                },
                { 
                    DrawingPresetTextWarp.CanUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCanUp)
                },
                { 
                    DrawingPresetTextWarp.CascadeDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCascadeDown)
                },
                { 
                    DrawingPresetTextWarp.CascadeUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCascadeUp)
                },
                { 
                    DrawingPresetTextWarp.Chevron,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateChevron)
                },
                { 
                    DrawingPresetTextWarp.ChevronInverted,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateChevronInverted)
                },
                { 
                    DrawingPresetTextWarp.Circle,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCircle)
                },
                { 
                    DrawingPresetTextWarp.CirclePour,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCirclePour)
                },
                { 
                    DrawingPresetTextWarp.CurveDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCurveDown)
                },
                { 
                    DrawingPresetTextWarp.CurveUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateCurveUp)
                },
                { 
                    DrawingPresetTextWarp.Deflate,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDeflate)
                },
                { 
                    DrawingPresetTextWarp.DeflateBottom,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDeflateBottom)
                },
                { 
                    DrawingPresetTextWarp.DeflateInflate,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDeflateInflate)
                },
                { 
                    DrawingPresetTextWarp.InflateDeflate,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDeflateInflateDeflate)
                },
                { 
                    DrawingPresetTextWarp.DeflateTop,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDeflateTop)
                },
                { 
                    DrawingPresetTextWarp.DoubleWave1,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateDoubleWave1)
                },
                { 
                    DrawingPresetTextWarp.FadeDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateFadeDown)
                },
                { 
                    DrawingPresetTextWarp.FadeLeft,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateFadeLeft)
                },
                { 
                    DrawingPresetTextWarp.FadeRight,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateFadeRight)
                },
                { 
                    DrawingPresetTextWarp.FadeUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateFadeUp)
                },
                { 
                    DrawingPresetTextWarp.Inflate,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateInflate)
                },
                { 
                    DrawingPresetTextWarp.InflateBottom,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateInflateBottom)
                },
                { 
                    DrawingPresetTextWarp.InflateTop,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateInflateTop)
                },
                { 
                    DrawingPresetTextWarp.Plain,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GeneratePlain)
                },
                { 
                    DrawingPresetTextWarp.RingInside,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateRingInside)
                },
                { 
                    DrawingPresetTextWarp.RingOutside,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateRingOutside)
                },
                { 
                    DrawingPresetTextWarp.SlantDown,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateSlantDown)
                },
                { 
                    DrawingPresetTextWarp.SlantUp,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateSlantUp)
                },
                { 
                    DrawingPresetTextWarp.Stop,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateStop)
                },
                { 
                    DrawingPresetTextWarp.Triangle,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateTriangle)
                },
                { 
                    DrawingPresetTextWarp.TriangleInverted,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateTriangleInverted)
                },
                { 
                    DrawingPresetTextWarp.Wave1,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateWave1)
                },
                { 
                    DrawingPresetTextWarp.Wave2,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateWave2)
                },
                { 
                    DrawingPresetTextWarp.Wave4,
                    new Func<ModelShapeCustomGeometry>(PresetTextWarpGenerators.GenerateWave4)
                }
            };
    }
}

