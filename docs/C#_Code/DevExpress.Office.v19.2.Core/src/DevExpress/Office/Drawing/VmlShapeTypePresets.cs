namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public static class VmlShapeTypePresets
    {
        private static readonly Dictionary<string, VmlShapeType> generatedVmlShapeTypes = new Dictionary<string, VmlShapeType>();
        private static readonly Dictionary<string, Func<VmlShapeType>> vmlShapeTypeGenerators = InitializeGenerators();

        private static VmlShapeType GenerateAccentBorderCallout1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t50",
                Spt = 50f,
                Path = "m@0@1l@2@3nfem@2,l@2,21600nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -8280;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentBorderCallout2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t51",
                Spt = 51f,
                Path = "m@0@1l@2@3@4@5nfem@4,l@4,21600nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -10080;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -3600;
            type.AdjustValues[3] = 0xfd2;
            type.AdjustValues[4] = -1800;
            type.AdjustValues[5] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentBorderCallout3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t52",
                Spt = 52f,
                Path = "m@0@1l@2@3@4@5@6@7nfem@6,l@6,21600nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = 0x5b68;
            type.AdjustValues[1] = 0x5f50;
            type.AdjustValues[2] = 0x6270;
            type.AdjustValues[3] = 0x5460;
            type.AdjustValues[4] = 0x6270;
            type.AdjustValues[5] = 0xfd2;
            type.AdjustValues[6] = 0x5b68;
            type.AdjustValues[7] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.Formulas.Add(new VmlSingleFormula("val #6"));
            type.Formulas.Add(new VmlSingleFormula("val #7"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentBorderCallout90()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t181",
                Spt = 181f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -1800;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentCallout1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t44",
                Spt = 44f,
                Path = "m@0@1l@2@3nfem@2,l@2,21600nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -8280;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentCallout2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t45",
                Spt = 45f,
                Path = "m@0@1l@2@3@4@5nfem@4,l@4,21600nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -10080;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -3600;
            type.AdjustValues[3] = 0xfd2;
            type.AdjustValues[4] = -1800;
            type.AdjustValues[5] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentCallout3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t46",
                Spt = 46f,
                Path = "m@0@1l@2@3@4@5@6@7nfem@6,l@6,21600nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = 0x5b68;
            type.AdjustValues[1] = 0x5f50;
            type.AdjustValues[2] = 0x6270;
            type.AdjustValues[3] = 0x5460;
            type.AdjustValues[4] = 0x6270;
            type.AdjustValues[5] = 0xfd2;
            type.AdjustValues[6] = 0x5b68;
            type.AdjustValues[7] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.Formulas.Add(new VmlSingleFormula("val #6"));
            type.Formulas.Add(new VmlSingleFormula("val #7"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateAccentCallout90()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t179",
                Spt = 179f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -1800;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateActionButtonBackPrevious()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t194",
                Spt = 194f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@12@9nfl@11@4@12@10xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonBeginning()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t196",
                Spt = 196f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@12@9l@17@4@12@10xem@11@9l@16@9@16@10@11@10xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 4"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonBlank()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t189",
                Spt = 189f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0e"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonDocument()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t198",
                Spt = 198f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@12@9nfl@12@10@13@10@13@14@15@9xem@15@9nfl@15@14@13@14e"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 4"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 @11 6075"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 6075 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 4050"));
            type.Formulas.Add(new VmlSingleFormula("sum @13 @5 4050"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @13 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @15 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonEnd()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t195",
                Spt = 195f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@11@9l@16@4@11@10xem@17@9l@12@9@12@10@17@10xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonForwardNext()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t193",
                Spt = 193f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@11@9nfl@12@4@11@10xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonHelp()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t191",
                Spt = 191f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@33@27nfqy@3@9@40@27@39@4@37@29l@37@30@36@30@36@29qy@37@28@39@27@3@26@34@27xem@3@31nfqx@35@32@3@10@38@32@3@31xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 7"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 14"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 2 7"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 14"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 11 28"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 7"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 4 7"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 17 28"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 9 14"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 21 28"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 11 14"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 25 28"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @21 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @23 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @24 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @25 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @20 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @21 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @22 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @24 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @27 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @29 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @30 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @33 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @37 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @38 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @40 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonHome()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t190",
                Spt = 190f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@3@9nfl@11@4@28@4@28@10@33@10@33@4@12@4@32@26@32@24@31@24@31@25xem@31@25nfl@32@26em@28@4nfl@33@4em@29@10nfl@29@27@30@27@30@10e"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 9 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 11 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 13 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @21 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @20 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @22 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @23 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @27 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @29 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @30 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @33 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonInformation()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t192",
                Spt = 192f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@3@9nfqx@11@4@3@10@12@4@3@9xem@3@25nfqx@33@26@3@27@36@26@3@25xem@32@28nfl@32@29@34@29@34@30@32@30@32@31@37@31@37@30@35@30@35@28xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 32"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 32"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 9 32"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 13 32"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 19 32"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 11 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 13 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @23 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @24 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @20 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @21 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @22 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @27 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @29 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @30 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @33 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @37 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonMovie()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t200",
                Spt = 200f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@11@39nfl@11@44@31@44@32@43@33@43@33@47@35@47@35@45@36@45@38@46@12@46@12@41@38@41@37@42@35@42@35@41@34@40@32@40@31@39xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1455 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1905 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 2325 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 16155 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 17010 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 19335 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 19725 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 20595 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5280 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5730 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 6630 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7492 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 9067 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 9555 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 13342 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 14580 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 15592 21600"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @20 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @21 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @22 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @23 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @24 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @25 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @26 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @27 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @28 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @29 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @30 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @31 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @33 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @37 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @38 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @40 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @41 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @42 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @43 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @44 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @45 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @46 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @47 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonReturn()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t197",
                Spt = 197f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@12@21nfl@23@9@3@21@24@21@24@20qy@3@19l@25@19qx@26@20l@26@21@11@21@11@20qy@25@10l@3@10qx@22@20l@22@21xe"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 4"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @20 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @21 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @23 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateActionButtonSound()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t199",
                Spt = 199f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0em@11@21nfl@11@22@24@22@25@10@25@9@24@21xem@26@21nfl@12@20em@26@4nfl@12@4em@26@22nfl@12@23e"
            };
            type.AdjustValues[0] = 0x546;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @4 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 8100 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @3 8100"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 8100 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 1 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 5 8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 11 16"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 7 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @18 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @20 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @21 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @23 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateArc()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t19",
                Spt = 19f,
                Path = "wr-21600,,21600,43200,,,21600,21600nfewr-21600,,21600,43200,,,21600,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -5898240;
            type.AdjustValues[1] = 0;
            type.AdjustValues[2] = 0;
            type.AdjustValues[3] = 0x5460;
            type.AdjustValues[4] = 0x5460;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "0,0;21600,21600;0,21600";
            return type;
        }

        private static VmlShapeType GenerateArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t13",
                Spt = 13f,
                Path = "m@0,l@0@1,0@1,0@2@0@2@0,21600,21600,10800xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 @3 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,@1,@6,@2";
            type.ShapePath.ConnectionSites = "@0,0;0,10800;@0,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateBalloon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t17",
                Spt = 17f,
                Path = "m18000,18000qx21600,14400l21600,3600qy18000,l3600,qx,3600l,14400qy3600,18000l3600,18000@0,21600,5400,18000xe"
            };
            type.AdjustValues[0] = 0x708;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "1054,1054,20545,16945";
            type.ShapePath.ConnectionSites = "10800,0;0,9000;@0,21600;10800,18000;21600,9000";
            type.ShapePath.ConnectionAngles = "270,180,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateBentArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t91",
                Spt = 91f,
                Path = "m21600,6079l@0,0@0@1,12427@1qx,12158l,21600@4,21600@4,12158qy12427@2l@0@2@0,12158xe"
            };
            type.AdjustValues[0] = 0x3b16;
            type.AdjustValues[1] = 0xb60;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 12158 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 32768 32059"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 #1 6079"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 #0 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "12427,@1,@8,@2;0,12158,@4,21600";
            type.ShapePath.ConnectionSites = "@0,0;@0,12158;@5,21600;21600,6079";
            type.ShapePath.ConnectionAngles = "270,90,90,0";
            return type;
        }

        private static VmlShapeType GenerateBentConnector2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t33",
                Spt = 33f,
                Path = "m,l21600,r,21600e",
                Filled = false,
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateBentConnector3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t34",
                Spt = 34f,
                Path = "m,l@0,0@0,21600,21600,21600e"
            };
            type.AdjustValues[0] = 0x2a30;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateBentConnector4()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t35",
                Spt = 35f,
                Path = "m,l@0,0@0@1,21600@1,21600,21600e"
            };
            type.AdjustValues[0] = 0x2a30;
            type.AdjustValues[1] = 0x2a30;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 width"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateBentConnector5()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t36",
                Spt = 36f,
                Path = "m,l@0,0@0@1@2@1@2,21600,21600,21600e"
            };
            type.AdjustValues[0] = 0x2a30;
            type.AdjustValues[1] = 0x2a30;
            type.AdjustValues[2] = 0x2a30;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 #2"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 height"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateBentUpArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t90",
                Spt = 90f,
                Path = "m@4,l@0@2@5@2@5@12,0@12,,21600@1,21600@1@2,21600@2xe"
            };
            type.AdjustValues[0] = 0x2429;
            type.AdjustValues[1] = 0x4852;
            type.AdjustValues[2] = 0x1c20;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 #0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #2 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 0 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod 21600 @0 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod 21600 @4 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod 21600 @5 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod 21600 @7 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 @15 @16"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,@12,@1,21600;@5,@17,@1,21600";
            type.ShapePath.ConnectionSites = "@4,0;@0,@2;0,@11;@14,21600;@1,@13;21600,@2";
            type.ShapePath.ConnectionAngles = "270,180,180,90,0,0";
            return type;
        }

        private static VmlShapeType GenerateBevel()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t84",
                Spt = 84f,
                Path = "m,l,21600r21600,l21600,xem@0@0nfl@0@2@1@2@1@0xem,nfl@0@0em,21600nfl@0@2em21600,21600nfl@1@2em21600,nfl@1@0e"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @5 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@0,@1,@2";
            type.ShapePath.ConnectionSites = "0,@4;@0,@4;@3,21600;@3,@2;21600,@4;@1,@4;@3,0;@3,@0";
            return type;
        }

        private static VmlShapeType GenerateBlockArc()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t95",
                Spt = 95f,
                Path = "al10800,10800@0@0@2@14,10800,10800,10800,10800@3@15xe"
            };
            type.AdjustValues[0] = 0xb40000;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sumangle #0 0 180"));
            type.Formulas.Add(new VmlSingleFormula("sumangle #0 0 90"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sumangle #0 90 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 2 1"));
            type.Formulas.Add(new VmlSingleFormula("abs #0"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @8 0 90"));
            type.Formulas.Add(new VmlSingleFormula("if @9 @7 @5"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @10 0 360"));
            type.Formulas.Add(new VmlSingleFormula("if @10 @11 @10"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @12 0 360"));
            type.Formulas.Add(new VmlSingleFormula("if @12 @13 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @14"));
            type.Formulas.Add(new VmlSingleFormula("val 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @18 5400 0"));
            type.Formulas.Add(new VmlSingleFormula("cos @19 #0"));
            type.Formulas.Add(new VmlSingleFormula("sin @19 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @20 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @21 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("if @9 @17 @25"));
            type.Formulas.Add(new VmlSingleFormula("if @9 0 21600"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 #0"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 #0"));
            type.Formulas.Add(new VmlSingleFormula("sin #1 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @29 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @30 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("if @4 0 @31"));
            type.Formulas.Add(new VmlSingleFormula("if #0 @34 0"));
            type.Formulas.Add(new VmlSingleFormula("if @6 @35 @31"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @36"));
            type.Formulas.Add(new VmlSingleFormula("if @4 0 @33"));
            type.Formulas.Add(new VmlSingleFormula("if #0 @38 @32"));
            type.Formulas.Add(new VmlSingleFormula("if @6 @39 0"));
            type.Formulas.Add(new VmlSingleFormula("if @4 @32 21600"));
            type.Formulas.Add(new VmlSingleFormula("if @6 @41 @33"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@36,@40,@37,@42";
            type.ShapePath.ConnectionSites = "10800,@27;@22,@23;10800,@26;@24,@23";
            return type;
        }

        private static VmlShapeType GenerateBorderCallout1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t47",
                Spt = 47f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -8280;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateBorderCallout2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t48",
                Spt = 48f,
                Path = "m@0@1l@2@3@4@5nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -10080;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -3600;
            type.AdjustValues[3] = 0xfd2;
            type.AdjustValues[4] = -1800;
            type.AdjustValues[5] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateBorderCallout3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t49",
                Spt = 49f,
                Path = "m@0@1l@2@3@4@5@6@7nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = 0x5b68;
            type.AdjustValues[1] = 0x5f50;
            type.AdjustValues[2] = 0x6270;
            type.AdjustValues[3] = 0x5460;
            type.AdjustValues[4] = 0x6270;
            type.AdjustValues[5] = 0xfd2;
            type.AdjustValues[6] = 0x5b68;
            type.AdjustValues[7] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.Formulas.Add(new VmlSingleFormula("val #6"));
            type.Formulas.Add(new VmlSingleFormula("val #7"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateBorderCallout90()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t180",
                Spt = 180f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600xe"
            };
            type.AdjustValues[0] = -1800;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateBracePair()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t186",
                Spt = 186f,
                Path = "m@9,nfqx@0@0l@0@7qy0@4@0@8l@0@6qy@9,21600em@10,nfqx@5@0l@5@7qy21600@4@5@8l@5@6qy@10,21600em@9,nsqx@0@0l@0@7qy0@4@0@8l@0@6qy@9,21600l@10,21600qx@5@6l@5@8qy21600@4@5@7l@5@0qy@10,xe"
            };
            type.AdjustValues[0] = 0x708;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @13"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@13,@11,@14,@12";
            type.ShapePath.ConnectionSites = "@3,0;0,@4;@3,@2;@1,@4";
            return type;
        }

        private static VmlShapeType GenerateBracketPair()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t185",
                Spt = 185f,
                Path = "m@0,nfqx0@0l0@2qy@0,21600em@1,nfqx21600@0l21600@2qy@1,21600em@0,nsqx0@0l0@2qy@0,21600l@1,21600qx21600@2l21600@0qy@1,xe"
            };
            type.AdjustValues[0] = 0xe10;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@3,@3,@4,@5";
            type.ShapePath.ConnectionSites = "@8,0;0,@9;@8,@7;@6,@9";
            return type;
        }

        private static VmlShapeType GenerateCallout1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t41",
                Spt = 41f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -8280;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateCallout2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t42",
                Spt = 42f,
                Path = "m@0@1l@2@3@4@5nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -10080;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -3600;
            type.AdjustValues[3] = 0xfd2;
            type.AdjustValues[4] = -1800;
            type.AdjustValues[5] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateCallout3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t43",
                Spt = 43f,
                Path = "m@0@1l@2@3@4@5@6@7nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = 0x5b68;
            type.AdjustValues[1] = 0x5f50;
            type.AdjustValues[2] = 0x6270;
            type.AdjustValues[3] = 0x5460;
            type.AdjustValues[4] = 0x6270;
            type.AdjustValues[5] = 0xfd2;
            type.AdjustValues[6] = 0x5b68;
            type.AdjustValues[7] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("val #4"));
            type.Formulas.Add(new VmlSingleFormula("val #5"));
            type.Formulas.Add(new VmlSingleFormula("val #6"));
            type.Formulas.Add(new VmlSingleFormula("val #7"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateCallout90()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t178",
                Spt = 178f,
                Path = "m@0@1l@2@3nfem,l21600,r,21600l,21600nsxe"
            };
            type.AdjustValues[0] = -1800;
            type.AdjustValues[1] = 0x5eec;
            type.AdjustValues[2] = -1800;
            type.AdjustValues[3] = 0xfd2;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.ConnectionSites = "@0,@1;10800,0;10800,21600;0,10800;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateCan()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t22",
                Spt = 22f,
                Path = "m10800,qx0@1l0@2qy10800,21600,21600@2l21600@1qy10800,xem0@1qy10800@0,21600@1nfe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "0,@0,21600,@2";
            type.ShapePath.ConnectionSites = "10800,@0;10800,0;0,10800;10800,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateChevron()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t55",
                Spt = 55f,
                Path = "m@0,l,0@1,10800,,21600@0,21600,21600,10800xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,0,10800,21600;0,0,16200,21600;0,0,21600,21600";
            type.ShapePath.ConnectionSites = "@2,0;@1,10800;@2,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateCircularArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t99",
                Spt = 99f,
                Path = "al10800,10800@8@8@4@6,10800,10800,10800,10800@9@7l@30@31@17@18@24@25@15@16@32@33xe"
            };
            type.AdjustValues[0] = -11796480;
            type.AdjustValues[1] = 0;
            type.AdjustValues[2] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("val 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @2 360 0"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @2 @5"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @6"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #2 0 2700"));
            type.Formulas.Add(new VmlSingleFormula("cos @10 #1"));
            type.Formulas.Add(new VmlSingleFormula("sin @10 #1"));
            type.Formulas.Add(new VmlSingleFormula("cos 13500 #1"));
            type.Formulas.Add(new VmlSingleFormula("sin 13500 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @13 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #2 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 5400 0"));
            type.Formulas.Add(new VmlSingleFormula("cos @20 #1"));
            type.Formulas.Add(new VmlSingleFormula("sin @20 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @21 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @23 @22"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 @23 @11"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 #1"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 #1"));
            type.Formulas.Add(new VmlSingleFormula("cos #2 #1"));
            type.Formulas.Add(new VmlSingleFormula("sin #2 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @26 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @27 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @29 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 5400 0"));
            type.Formulas.Add(new VmlSingleFormula("cos @34 #0"));
            type.Formulas.Add(new VmlSingleFormula("sin @34 #0"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @37 180 0"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @37 @38"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 @39"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 @39"));
            type.Formulas.Add(new VmlSingleFormula("cos #2 @39"));
            type.Formulas.Add(new VmlSingleFormula("sin #2 @39"));
            type.Formulas.Add(new VmlSingleFormula("sum @40 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @41 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @42 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @43 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 10800 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "@44,@45;@48,@49;@46,@47;@17,@18;@24,@25;@15,@16";
            return type;
        }

        private static VmlShapeType GenerateCloudCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t106",
                Spt = 106f,
                Path = "ar,7165,4345,13110,1950,7185,1080,12690,475,11732,4835,17650,1080,12690,2910,17640,2387,9757,10107,20300,2910,17640,8235,19545,7660,12382,14412,21597,8235,19545,14280,18330,12910,11080,18695,18947,14280,18330,18690,15045,14822,5862,21597,15082,18690,15045,20895,7665,15772,2592,21105,9865,20895,7665,19140,2715,14330,,19187,6595,19140,2715,14910,1170,10992,,15357,5945,14910,1170,11250,1665,6692,650,12025,7917,11250,1665,7005,2580,1912,1972,8665,11162,7005,2580,1950,7185xear,7165,4345,13110,1080,12690,2340,13080nfear475,11732,4835,17650,2910,17640,3465,17445nfear7660,12382,14412,21597,7905,18675,8235,19545nfear7660,12382,14412,21597,14280,18330,14400,17370nfear12910,11080,18695,18947,18690,15045,17070,11475nfear15772,2592,21105,9865,20175,9015,20895,7665nfear14330,,19187,6595,19200,3345,19140,2715nfear14330,,19187,6595,14910,1170,14550,1980nfear10992,,15357,5945,11250,1665,11040,2340nfear1912,1972,8665,11162,7650,3270,7005,2580nfear1912,1972,8665,11162,1950,7185,2070,7890nfem@23@37qx@35@24@23@36@34@24@23@37xem@16@33qx@31@17@16@32@30@17@16@33xem@38@29qx@27@39@38@28@26@39@38@29xe"
            };
            type.AdjustValues[0] = 0x546;
            type.AdjustValues[1] = 0x6540;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("cosatan2 10800 @0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sinatan2 10800 @0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("mod @6 @7 0"));
            type.Formulas.Add(new VmlSingleFormula("prod 600 11 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 1 3"));
            type.Formulas.Add(new VmlSingleFormula("prod 600 3 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @12 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 @6 @8"));
            type.Formulas.Add(new VmlSingleFormula("prod @13 @7 @8"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @15 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("prod 600 8 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @11 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @18 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @20 @6 @8"));
            type.Formulas.Add(new VmlSingleFormula("prod @20 @7 @8"));
            type.Formulas.Add(new VmlSingleFormula("sum @21 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("prod 600 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 600 0"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 600"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 600 0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 600"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 @25 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @25"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 @25 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @25"));
            type.Formulas.Add(new VmlSingleFormula("sum @23 @12 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @23 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 @12 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "2977,3262,17087,17337";
            type.ShapePath.ConnectionSites = "67,10800;10800,21577;21582,10800;10800,1235;@38,@39";
            return type;
        }

        private static VmlShapeType GenerateCube()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t16",
                Spt = 16f,
                Path = "m@0,l0@0,,21600@1,21600,21600@2,21600,xem0@0nfl@1@0,21600,em@1@0nfl@1,21600e"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("mid height #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid width #0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "0,@0,@1,21600";
            type.ShapePath.ConnectionSites = "@6,0;@4,@0;0,@3;@4,21600;@1,@3;21600,@5";
            type.ShapePath.ConnectionAngles = "270,270,180,90,0,0";
            return type;
        }

        private static VmlShapeType GenerateCurvedConnector2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t37",
                Spt = 37f,
                Path = "m,c10800,,21600,10800,21600,21600e",
                Filled = false,
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateCurvedConnector3()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t38",
                Spt = 38f,
                Path = "m,c@0,0@1,5400@1,10800@1,16200@2,21600,21600,21600e",
                Filled = false,
                Formulas = new VmlSingleFormulasCollection()
            };
            type.Formulas.Add(new VmlSingleFormula("mid #0 0"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 21600"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateCurvedConnector4()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t39",
                Spt = 39f,
                Path = "m,c@0,0@1@6@1@5@1@7@3@8@2@8@4@8,21600@9,21600,21600e",
                Filled = false,
                Formulas = new VmlSingleFormulasCollection()
            };
            type.Formulas.Add(new VmlSingleFormula("mid #0 0"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 21600"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 @2"));
            type.Formulas.Add(new VmlSingleFormula("mid @2 21600"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 0"));
            type.Formulas.Add(new VmlSingleFormula("mid @5 0"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 @5"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 21600"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateCurvedConnector5()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t40",
                Spt = 40f,
                Path = "m,c@1,0@2@8@2@7@2@9@3@10@0@10@4@10@5@12@5@11@5@13@6,21600,21600,21600e",
                Filled = false,
                Formulas = new VmlSingleFormulasCollection()
            };
            type.Formulas.Add(new VmlSingleFormula("mid #0 #2"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 0"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 @0"));
            type.Formulas.Add(new VmlSingleFormula("mid #2 @0"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("mid #2 21600"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 0"));
            type.Formulas.Add(new VmlSingleFormula("mid @7 0"));
            type.Formulas.Add(new VmlSingleFormula("mid @7 #1"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("mid #1 21600"));
            type.Formulas.Add(new VmlSingleFormula("mid @11 #1"));
            type.Formulas.Add(new VmlSingleFormula("mid @11 21600"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateCurvedDownArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t105",
                Spt = 105f,
                Path = "wr,0@3@23,0@22@4,0@15,0@1@23@7,0@13@2l@14@2@8@22@12@2at,0@3@23@11@2@17@26@15,0@1@23@17@26@15@22xewr,0@3@23@4,0@17@26nfe"
            };
            type.AdjustValues[0] = 0x32a0;
            type.AdjustValues[1] = 0x4bf0;
            type.AdjustValues[2] = 0x3840;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 width #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 #1 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @9 height @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @15 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid @4 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("prod @18 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @19"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod height 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @24 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @25"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 128 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 128"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @17 @12"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @20 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @32 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height height 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @9 @9 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 0 @35"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @36"));
            type.Formulas.Add(new VmlSingleFormula("sum @37 height 0"));
            type.Formulas.Add(new VmlSingleFormula("prod width height @38"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @33 @41 height"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @42"));
            type.Formulas.Add(new VmlSingleFormula("sum @43 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @45"));
            type.Formulas.Add(new VmlSingleFormula("prod height 4390 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod height 28378 32768"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@45,@47,@46,@48";
            type.ShapePath.ConnectionSites = "@17,0;@16,@22;@12,@2;@8,@22;@14,@2";
            type.ShapePath.ConnectionAngles = "270,90,90,90,0";
            return type;
        }

        private static VmlShapeType GenerateCurvedLeftArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t103",
                Spt = 103f,
                Path = "wr@22,0@21@3,,0@21@4@22@14@21@1@21@7@2@12l@2@13,0@8@2@11at@22,0@21@3@2@10@24@16@22@14@21@1@24@16,0@14xear@22@14@21@1@21@7@24@16nfe"
            };
            type.AdjustValues[0] = 0x32a0;
            type.AdjustValues[1] = 0x4bf0;
            type.AdjustValues[2] = 0x1c20;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 width #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 #1 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid width #0"));
            type.Formulas.Add(new VmlSingleFormula("ellipse #2 height @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @9 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 @9 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @14 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid @4 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("prod @17 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @18"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @23 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 128 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 128"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @16 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @29 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height height 1"));
            type.Formulas.Add(new VmlSingleFormula("prod #2 #2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 0 @32"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @33"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 height 0"));
            type.Formulas.Add(new VmlSingleFormula("prod width height @35"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @30 @38 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 0 64"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @41"));
            type.Formulas.Add(new VmlSingleFormula("prod height 4390 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod height 28378 32768"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@43,@41,@44,@42";
            type.ShapePath.ConnectionSites = "0,@15;@2,@11;0,@8;@2,@13;@21,@16";
            type.ShapePath.ConnectionAngles = "180,180,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateCurvedRightArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t102",
                Spt = 102f,
                Path = "ar,0@23@3@22,,0@4,0@15@23@1,0@7@2@13l@2@14@22@8@2@12wa,0@23@3@2@11@26@17,0@15@23@1@26@17@22@15xear,0@23@3,0@4@26@17nfe"
            };
            type.AdjustValues[0] = 0x32a0;
            type.AdjustValues[1] = 0x4bf0;
            type.AdjustValues[2] = 0x3840;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 width #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 #1 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @9 height @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @15 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid @4 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("prod @18 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @19"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod height 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @24 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @25"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 128 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 128"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @17 @12"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @20 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @32 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height height 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @9 @9 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 0 @35"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @36"));
            type.Formulas.Add(new VmlSingleFormula("sum @37 height 0"));
            type.Formulas.Add(new VmlSingleFormula("prod width height @38"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @33 @41 height"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @42"));
            type.Formulas.Add(new VmlSingleFormula("sum @43 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @45"));
            type.Formulas.Add(new VmlSingleFormula("prod height 4390 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod height 28378 32768"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@47,@45,@48,@46";
            type.ShapePath.ConnectionSites = "0,@17;@2,@14;@22,@8;@2,@12;@22,@16";
            type.ShapePath.ConnectionAngles = "180,90,0,0,0";
            return type;
        }

        private static VmlShapeType GenerateCurvedUpArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t104",
                Spt = 104f,
                Path = "ar0@22@3@21,,0@4@21@14@22@1@21@7@21@12@2l@13@2@8,0@11@2wa0@22@3@21@10@2@16@24@14@22@1@21@16@24@14,xewr@14@22@1@21@7@21@16@24nfe"
            };
            type.AdjustValues[0] = 0x32a0;
            type.AdjustValues[1] = 0x4bf0;
            type.AdjustValues[2] = 0x1c20;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 width #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 #1 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid width #0"));
            type.Formulas.Add(new VmlSingleFormula("ellipse #2 height @4"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @9 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 @9 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 width #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @14 1 2"));
            type.Formulas.Add(new VmlSingleFormula("mid @4 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 #1 width"));
            type.Formulas.Add(new VmlSingleFormula("prod @17 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @18"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @23 @4 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 128 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 128"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @16 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @29 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height height 1"));
            type.Formulas.Add(new VmlSingleFormula("prod #2 #2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 0 @32"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @33"));
            type.Formulas.Add(new VmlSingleFormula("sum @34 height 0"));
            type.Formulas.Add(new VmlSingleFormula("prod width height @35"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 64 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @30 @38 height"));
            type.Formulas.Add(new VmlSingleFormula("sum @39 0 64"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @41"));
            type.Formulas.Add(new VmlSingleFormula("prod height 4390 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod height 28378 32768"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@41,@43,@42,@44";
            type.ShapePath.ConnectionSites = "@8,0;@11,@2;@15,0;@16,@21;@13,@2";
            type.ShapePath.ConnectionAngles = "270,270,270,90,0";
            return type;
        }

        private static VmlShapeType GenerateDiamond()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t4",
                Spt = 4f,
                Path = "m10800,l,10800,10800,21600,21600,10800xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5400,5400,16200,16200";
            return type;
        }

        private static VmlShapeType GenerateDonut()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t23",
                Spt = 23f,
                Path = "m,10800qy10800,,21600,10800,10800,21600,,10800xm@0,10800qy10800@2@1,10800,10800@0@0,10800xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateDoubleWave()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t188",
                Spt = 188f,
                Path = "m@43@0c@42@1@41@3@40@0@39@1@38@3@37@0l@30@4c@31@5@32@6@33@4@34@5@35@6@36@4xe"
            };
            type.AdjustValues[0] = 0x57c;
            type.AdjustValues[1] = 0x2a30;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 41 9"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23 9"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 1 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 2 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 4 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 5 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @8"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @13"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 4 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 5 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @21"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @22"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @23"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @24"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @19 0"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @18 @20"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @17 @21"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @16 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @15 @22"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @14 @23"));
            type.Formulas.Add(new VmlSingleFormula("if @7 21600 @24"));
            type.Formulas.Add(new VmlSingleFormula("if @7 0 @29"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @9 @28"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @10 @27"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @8 @8"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @11 @26"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @12 @25"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @13 21600"));
            type.Formulas.Add(new VmlSingleFormula("sum @36 0 @30"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("max @30 @37"));
            type.Formulas.Add(new VmlSingleFormula("min @36 @43"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @48"));
            type.Formulas.Add(new VmlSingleFormula("mid @36 @43"));
            type.Formulas.Add(new VmlSingleFormula("mid @30 @37"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@46,@48,@47,@49";
            type.ShapePath.ConnectionSites = "@40,@0;@51,10800;@33,@4;@50,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateDownArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t67",
                Spt = 67f,
                Path = "m0@0l@1@0@1,0@2,0@2@0,21600@0,10800,21600xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 @3 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@1,0,@2,@6";
            type.ShapePath.ConnectionSites = "10800,0;0,@0;10800,21600;21600,@0";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateDownArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t80",
                Spt = 80f,
                Path = "m,l21600,,21600@0@5@0@5@2@4@2,10800,21600@1@2@3@2@3@0,0@0xe"
            };
            type.AdjustValues[0] = 0x3840;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0x4650;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,0,21600,@0";
            type.ShapePath.ConnectionSites = "10800,0;0,@6;10800,21600;21600,@6";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateEllipseRibbon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t107",
                Spt = 107f,
                Path = "ar@9@38@8@37,0@27@0@26@9@13@8@4@0@25@22@25@9@38@8@37@22@26@3@27l@7@40@3,wa@9@35@8@10@3,0@21@33@9@36@8@1@21@31@20@31@9@35@8@10@20@33,,l@5@40xewr@9@36@8@1@20@31@0@32nfl@20@33ear@9@36@8@1@21@31@22@32nfl@21@33em@0@26nfl@0@32em@22@26nfl@22@32e"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0x49d4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 8"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod width 7 8"));
            type.Formulas.Add(new VmlSingleFormula("prod width 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @6"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #2"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 30573 4096"));
            type.Formulas.Add(new VmlSingleFormula("prod @11 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 #2 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 height #1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @16 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @17 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 #1 height"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @23 width @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 height @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @11 @19"));
            type.Formulas.Add(new VmlSingleFormula("sum #2 @11 @19"));
            type.Formulas.Add(new VmlSingleFormula("prod @11 2391 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @29 width @11"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 @30 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 #1 height"));
            type.Formulas.Add(new VmlSingleFormula("sum height @30 @14"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @34"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 @19 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @15 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @35 @15 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum @28 @14 @18"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @39"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 0 @18"));
            type.Formulas.Add(new VmlSingleFormula("prod @41 2 3"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @42"));
            type.Formulas.Add(new VmlSingleFormula("sum #2 0 @42"));
            type.Formulas.Add(new VmlSingleFormula("min @44 20925"));
            type.Formulas.Add(new VmlSingleFormula("prod width 3 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @46 0 4"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@1,@22,@25";
            type.ShapePath.ConnectionSites = "@6,@1;@5,@40;@6,@4;@7,@40";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateEllipseRibbon2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t108",
                Spt = 108f,
                Path = "wr@9@34@8@35,0@24@0@23@9,0@8@11@0@22@19@22@9@34@8@35@19@23@3@24l@7@36@3@4at@9@31@8@32@3@4@18@30@9@1@8@33@18@28@17@28@9@31@8@32@17@30,0@4l@5@36xear@9@1@8@33@17@28@0@29nfl@17@30ewr@9@1@8@33@18@28@19@29nfl@18@30em@0@23nfl@0@29em@19@23nfl@19@29e"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x3f48;
            type.AdjustValues[2] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 8"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod width 7 8"));
            type.Formulas.Add(new VmlSingleFormula("prod width 3 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @6"));
            type.Formulas.Add(new VmlSingleFormula("prod #2 30573 4096"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 height #2"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 @5 0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @17"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @20 width @10"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 0 @21"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 @16 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum #2 @16 @10"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 2391 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 0 @17"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @26 width @10"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 #1 @27"));
            type.Formulas.Add(new VmlSingleFormula("sum @22 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 0 @27"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #2"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @12 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @10 @16"));
            type.Formulas.Add(new VmlSingleFormula("sum @31 @10 @13"));
            type.Formulas.Add(new VmlSingleFormula("sum @32 @10 @13"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 @12 @15"));
            type.Formulas.Add(new VmlSingleFormula("sum @16 0 @15"));
            type.Formulas.Add(new VmlSingleFormula("prod @37 2 3"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 @38 0"));
            type.Formulas.Add(new VmlSingleFormula("sum #2 @38 0"));
            type.Formulas.Add(new VmlSingleFormula("max @40 675"));
            type.Formulas.Add(new VmlSingleFormula("prod width 3 8"));
            type.Formulas.Add(new VmlSingleFormula("sum @42 0 4"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@22,@19,@1";
            type.ShapePath.ConnectionSites = "@6,0;@5,@36;@6,@1;@7,@36";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateFlowChartAlternateProcess()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t176",
                Spt = 176f,
                Path = "m@0,qx0@0l0@2qy@0,21600l@1,21600qx21600@2l21600@0qy@1,xe"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@3,@3,@4,@5";
            type.ShapePath.ConnectionSites = "@8,0;0,@9;@8,@7;@6,@9";
            return type;
        }

        private static VmlShapeType GenerateFlowChartCollate()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t125",
                Spt = 125f,
                Path = "m21600,21600l,21600,21600,,,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "5400,5400,16200,16200";
            type.ShapePath.ConnectionSites = "10800,0;10800,10800;10800,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartConnector()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t120",
                Spt = 120f,
                Path = "m10800,qx,10800,10800,21600,21600,10800,10800,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateFlowChartDecision()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t110",
                Spt = 110f,
                Path = "m10800,l,10800,10800,21600,21600,10800xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5400,5400,16200,16200";
            return type;
        }

        private static VmlShapeType GenerateFlowChartDelay()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t135",
                Spt = 135f,
                Path = "m10800,qx21600,10800,10800,21600l,21600,,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,3163,18437,18437";
            return type;
        }

        private static VmlShapeType GenerateFlowChartDisplay()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t134",
                Spt = 134f,
                Path = "m17955,v862,282,1877,1410,2477,3045c21035,5357,21372,7895,21597,10827v-225,2763,-562,5300,-1165,7613c19832,20132,18817,21260,17955,21597r-14388,l,10827,3567,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.TextboxRect = "3567,0,17955,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartDocument()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t114",
                Spt = 114f,
                Path = "m,20172v945,400,1887,628,2795,913c3587,21312,4342,21370,5060,21597v2037,,2567,-227,3095,-285c8722,21197,9325,20970,9855,20800v490,-228,945,-400,1472,-740c11817,19887,12347,19660,12875,19375v567,-228,1095,-513,1700,-740c15177,18462,15782,18122,16537,17950v718,-113,1398,-398,2228,-513c19635,17437,20577,17322,21597,17322l21597,,,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,0,21600,17322";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,20400;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartExtract()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t127",
                Spt = 127f,
                Path = "m10800,l21600,21600,,21600xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5400,10800,16200,21600";
            type.ShapePath.ConnectionSites = "10800,0;5400,10800;10800,21600;16200,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartInputOutput()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t111",
                Spt = 111f,
                Path = "m4321,l21600,,17204,21600,,21600xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "4321,0,17204,21600";
            type.ShapePath.ConnectionSites = "12961,0;10800,0;2161,10800;8602,21600;10800,21600;19402,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartInternalStorage()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t113",
                Spt = 113f,
                Path = "m,l,21600r21600,l21600,xem4236,nfl4236,21600em,4236nfl21600,4236e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "4236,4236,21600,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartMagneticDisk()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t132",
                Spt = 132f,
                Path = "m10800,qx,3391l,18209qy10800,21600,21600,18209l21600,3391qy10800,xem,3391nfqy10800,6782,21600,3391e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "0,6782,21600,18209";
            type.ShapePath.ConnectionSites = "10800,6782;10800,0;0,10800;10800,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateFlowChartMagneticDrum()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t133",
                Spt = 133f,
                Path = "m21600,10800qy18019,21600l3581,21600qx,10800,3581,l18019,qx21600,10800xem18019,21600nfqx14438,10800,18019,e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "3581,0,14438,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;14438,10800;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0,0";
            return type;
        }

        private static VmlShapeType GenerateFlowChartMagneticTape()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t131",
                Spt = 131f,
                Path = "ar,,21600,21600,18685,18165,10677,21597l20990,21597r,-3432xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            return type;
        }

        private static VmlShapeType GenerateFlowChartManualInput()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t118",
                Spt = 118f,
                Path = "m,4292l21600,r,21600l,21600xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,4291,21600,21600";
            type.ShapePath.ConnectionSites = "10800,2146;0,10800;10800,21600;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartManualOperation()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t119",
                Spt = 119f,
                Path = "m,l21600,,17240,21600r-12880,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "4321,0,17204,21600";
            type.ShapePath.ConnectionSites = "10800,0;2180,10800;10800,21600;19420,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartMerge()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t128",
                Spt = 128f,
                Path = "m,l21600,,10800,21600xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5400,0,16200,10800";
            type.ShapePath.ConnectionSites = "10800,0;5400,10800;10800,21600;16200,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartMultidocument()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t115",
                Spt = 115f,
                Path = "m,20465v810,317,1620,452,2397,725c3077,21325,3790,21417,4405,21597v1620,,2202,-180,2657,-272c7580,21280,8002,21010,8455,20917v422,-135,810,-405,1327,-542c10205,20150,10657,19967,11080,19742v517,-182,970,-407,1425,-590c13087,19017,13605,18745,14255,18610v615,-180,1262,-318,1942,-408c16975,18202,17785,18022,18595,18022r,-1670l19192,16252r808,l20000,14467r722,-75l21597,14392,21597,,2972,r,1815l1532,1815r,1860l,3675,,20465xem1532,3675nfl18595,3675r,12677em2972,1815nfl20000,1815r,12652e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "0,3675,18595,18022";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,19890;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartOffpageConnector()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t177",
                Spt = 177f,
                Path = "m,l21600,r,17255l10800,21600,,17255xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,0,21600,17255";
            return type;
        }

        private static VmlShapeType GenerateFlowChartOnlineStorage()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t130",
                Spt = 130f,
                Path = "m3600,21597c2662,21202,1837,20075,1087,18440,487,16240,75,13590,,10770,75,8007,487,5412,1087,3045,1837,1465,2662,337,3600,l21597,v-937,337,-1687,1465,-2512,3045c18485,5412,18072,8007,17997,10770v75,2820,488,5470,1088,7670c19910,20075,20660,21202,21597,21597xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "3600,0,17997,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;17997,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartOr()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t124",
                Spt = 124f,
                Path = "m10800,qx,10800,10800,21600,21600,10800,10800,xem,10800nfl21600,10800em10800,nfl10800,21600e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateFlowChartPredefinedProcess()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t112",
                Spt = 112f,
                Path = "m,l,21600r21600,l21600,xem2610,nfl2610,21600em18990,nfl18990,21600e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "2610,0,18990,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartPreparation()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t117",
                Spt = 117f,
                Path = "m4353,l17214,r4386,10800l17214,21600r-12861,l,10800xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "4353,0,17214,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartProcess()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t109",
                Spt = 109f,
                Path = "m,l,21600r21600,l21600,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            return type;
        }

        private static VmlShapeType GenerateFlowChartPunchedCard()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t121",
                Spt = 121f,
                Path = "m4321,l21600,r,21600l,21600,,4338xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,4321,21600,21600";
            return type;
        }

        private static VmlShapeType GenerateFlowChartPunchedTape()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t122",
                Spt = 122f,
                Path = "m21597,19450v-225,-558,-750,-1073,-1650,-1545c18897,17605,17585,17347,16197,17260v-1500,87,-2700,345,-3787,645c11472,18377,10910,18892,10800,19450v-188,515,-750,1075,-1613,1460c8100,21210,6825,21425,5400,21597,3937,21425,2700,21210,1612,20910,675,20525,150,19965,,19450l,2147v150,558,675,1073,1612,1460c2700,3950,3937,4165,5400,4337,6825,4165,8100,3950,9187,3607v863,-387,1425,-902,1613,-1460c10910,1632,11472,1072,12410,600,13497,300,14697,85,16197,v1388,85,2700,300,3750,600c20847,1072,21372,1632,21597,2147xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,4337,21600,17260";
            type.ShapePath.ConnectionSites = "10800,2147;0,10800;10800,19450;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateFlowChartSort()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t126",
                Spt = 126f,
                Path = "m10800,l,10800,10800,21600,21600,10800xem,10800nfl21600,10800e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "5400,5400,16200,16200";
            return type;
        }

        private static VmlShapeType GenerateFlowChartSummingJunction()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t123",
                Spt = 123f,
                Path = "m10800,qx,10800,10800,21600,21600,10800,10800,xem3163,3163nfl18437,18437em3163,18437nfl18437,3163e",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateFlowChartTerminator()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t116",
                Spt = 116f,
                Path = "m3475,qx,10800,3475,21600l18125,21600qx21600,10800,18125,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "1018,3163,20582,18437";
            return type;
        }

        private static VmlShapeType GenerateFoldedCorner()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t65",
                Spt = 65f,
                Path = "m,l,21600@0,21600,21600@0,21600,xem@0,21600nfl@3@5c@7@9@11@13,21600@0e"
            };
            type.AdjustValues[0] = 0x49d4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 8481 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 @0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 1117 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 @0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 11764 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 @0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 6144 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 @0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 20480 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 @0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 6144 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @0 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "0,0,21600,@13";
            return type;
        }

        private static VmlShapeType GenerateHeart()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t74",
                Spt = 74f,
                Path = "m10860,2187c10451,1746,9529,1018,9015,730,7865,152,6685,,5415,,4175,152,2995,575,1967,1305,1150,2187,575,3222,242,4220,,5410,242,6560,575,7597l10860,21600,20995,7597v485,-1037,605,-2187,485,-3377c21115,3222,20420,2187,19632,1305,18575,575,17425,152,16275,,15005,,13735,152,12705,730v-529,288,-1451,1016,-1845,1457xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5037,2277,16557,13677";
            type.ShapePath.ConnectionSites = "10860,2187;2928,10800;10860,21600;18672,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateHexagon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t9",
                Spt = 9f,
                Path = "m@0,l,10800@0,21600@1,21600,21600,10800@1,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "1800,1800,19800,19800;3600,3600,18000,18000;6300,6300,15300,15300";
            return type;
        }

        private static VmlShapeType GenerateHomePlate()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t15",
                Spt = 15f,
                Path = "m@0,l,,,21600@0,21600,21600,10800xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,0,10800,21600;0,0,16200,21600;0,0,21600,21600";
            type.ShapePath.ConnectionSites = "@1,0;0,10800;@1,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateHorizontalScroll()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t98",
                Spt = 98f,
                Path = "m0@5qy@2@1l@0@1@0@2qy@7,,21600@2l21600@9qy@7@10l@1@10@1@11qy@2,21600,0@11xem0@5nfqy@2@6@1@5@3@4@2@5l@2@6em@1@5nfl@1@10em21600@2nfqy@7@1l@0@1em@0@2nfqy@8@3@7@2l@7@1e"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 5 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 3 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @5"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@1,@1,@7,@10";
            type.ShapePath.ConnectionSites = "@13,@1;0,@14;@13,@10;@12,@14";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateHostControl()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t201",
                Spt = 201f,
                Path = "m,l,21600r21600,l21600,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.ShadowOk = false;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.StrokeOk = false;
            type.ShapePath.FillOk = false;
            type.ShapeProtections = new VmlShapeProtections();
            type.ShapeProtections.Ext = VmlExtensionHandlingBehavior.Edit;
            type.ShapeProtections.ShapeType = true;
            type.Stroke = new VmlLineStrokeSettings(FakeDocumentModel.Instance);
            type.Stroke.JoinStyle = VmlStrokeJoinStyle.Miter;
            return type;
        }

        private static VmlShapeType GenerateIrregularSeal1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t71",
                Spt = 71f,
                Path = "m10800,5800l8352,2295,7312,6320,370,2295,4627,7617,,8615r3722,3160l135,14587r5532,-650l4762,17617,7715,15627r770,5973l10532,14935r2715,4802l14020,14457r4125,3638l16837,12942r4763,348l17607,10475,21097,8137,16702,7315,18380,4457r-4225,868l14522,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "4627,6320,16702,13937";
            type.ShapePath.ConnectionSites = "14522,0;0,8615;8485,21600;21600,13290";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateIrregularSeal2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t72",
                Spt = 72f,
                Path = "m11462,4342l9722,1887,8550,6382,4502,3625r870,4192l1172,8270r2763,3322l,12877r3330,2493l1285,17825r3520,415l4917,21600,7527,18125r1173,1587l9872,17370r1740,1472l12180,15935r2762,1435l14640,14350r4237,1282l16380,12310r1890,-1020l16985,9402,21600,6645,16380,6532,18007,3172,14525,5777,14790,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "5372,6382,14640,15935";
            type.ShapePath.ConnectionSites = "9722,1887;0,12877;11612,18842;21600,6645";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateIsocelesTriangle()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t5",
                Spt = 5f,
                Path = "m@0,l,21600r21600,xe"
            };
            type.AdjustValues[0] = 0x2a30;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,10800,10800,18000;5400,10800,16200,18000;10800,10800,21600,18000;0,7200,7200,21600;7200,7200,14400,21600;14400,7200,21600,21600";
            type.ShapePath.ConnectionSites = "@0,0;@1,10800;0,21600;10800,21600;21600,21600;@2,10800";
            return type;
        }

        private static VmlShapeType GenerateLeftArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t66",
                Spt = 66f,
                Path = "m@0,l@0@1,21600@1,21600@2@0@2@0,21600,,10800xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 #1 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@4,@1,21600,@2";
            type.ShapePath.ConnectionSites = "@0,0;0,10800;@0,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateLeftArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t77",
                Spt = 77f,
                Path = "m@0,l@0@3@2@3@2@1,,10800@2@4@2@5@0@5@0,21600,21600,21600,21600,xe"
            };
            type.AdjustValues[0] = 0x1c20;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0xe10;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@0,0,21600,21600";
            type.ShapePath.ConnectionSites = "@7,0;0,10800;@7,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateLeftBrace()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t87",
                Spt = 87f,
                Path = "m21600,qx10800@0l10800@2qy0@11,10800@3l10800@1qy21600,21600e"
            };
            type.AdjustValues[0] = 0x708;
            type.AdjustValues[1] = 0x2a30;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("min #1 @6"));
            type.Formulas.Add(new VmlSingleFormula("prod @7 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "13963,@4,21600,@5";
            type.ShapePath.ConnectionSites = "21600,0;0,10800;21600,21600";
            return type;
        }

        private static VmlShapeType GenerateLeftBracket()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t85",
                Spt = 85f,
                Path = "m21600,qx0@0l0@1qy21600,21600e"
            };
            type.AdjustValues[0] = 0x708;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "6326,@2,21600,@3";
            type.ShapePath.ConnectionSites = "21600,0;0,10800;21600,21600";
            return type;
        }

        private static VmlShapeType GenerateLeftRightArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t69",
                Spt = 69f,
                Path = "m,10800l@0,21600@0@3@2@3@2,21600,21600,10800@2,0@2@1@0@1@0,xe"
            };
            type.AdjustValues[0] = 0x10e0;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 #1 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@5,@1,@6,@3";
            type.ShapePath.ConnectionSites = "@2,0;10800,@1;@0,0;0,10800;@0,21600;10800,@3;@2,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,270,270,180,90,90,90,0";
            return type;
        }

        private static VmlShapeType GenerateLeftRightArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t81",
                Spt = 81f,
                Path = "m@0,l@0@3@2@3@2@1,,10800@2@4@2@5@0@5@0,21600@8,21600@8@5@9@5@9@4,21600,10800@9@1@9@3@8@3@8,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0xa8c;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@0,0,@8,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateLeftRightUpArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t182",
                Spt = 182f,
                Path = "m10800,l@0@2@1@2@1@6@7@6@7@5,0@8@7,21600@7@9@10@9@10,21600,21600@8@10@5@10@6@4@6@4@2@3@2xe"
            };
            type.AdjustValues[0] = 0x1950;
            type.AdjustValues[1] = 0x21c0;
            type.AdjustValues[2] = 0x181b;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 21600 @3"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 21600 @3"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 @3 21600"));
            type.Formulas.Add(new VmlSingleFormula("prod 10800 21600 @3"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 21600 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 @8"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 0 @8"));
            type.Formulas.Add(new VmlSingleFormula("prod @12 @7 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @13"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 @16 @15"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@13,@6,@14,@9;@1,@17,@4,@9";
            type.ShapePath.ConnectionSites = "10800,0;0,@8;10800,@9;21600,@8";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateLeftUpArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t89",
                Spt = 89f,
                Path = "m@4,l@0@2@5@2@5@5@2@5@2@0,0@4@2,21600@2@1@1@1@1@2,21600@2xe"
            };
            type.AdjustValues[0] = 0x2429;
            type.AdjustValues[1] = 0x4852;
            type.AdjustValues[2] = 0x181b;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 #0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #2 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 0 21600"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 @10 @11"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@12,@5,@1,@1;@5,@12,@1,@1";
            type.ShapePath.ConnectionSites = "@4,0;@0,@2;@2,@0;0,@4;@2,21600;@7,@1;@1,@7;21600,@2";
            type.ShapePath.ConnectionAngles = "270,180,270,180,90,90,0,0";
            return type;
        }

        private static VmlShapeType GenerateLightningBolt()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t73",
                Spt = 73f,
                Path = "m8472,l,3890,7602,8382,5022,9705r7200,4192l10012,14915r11588,6685l14767,12877r1810,-870l11050,6797r1810,-717xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "8757,7437,13917,14277";
            type.ShapePath.ConnectionSites = "8472,0;0,3890;5022,9705;10012,14915;21600,21600;16577,12007;12860,6080";
            type.ShapePath.ConnectionAngles = "270,270,180,180,90,0,0";
            return type;
        }

        private static VmlShapeType GenerateMoon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t184",
                Spt = 184f,
                Path = "m21600,qx,10800,21600,21600wa@0@10@6@11,21600,21600,21600,xe"
            };
            type.AdjustValues[0] = 0x2a30;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 #0 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod 21600 21600 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("sum @9 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("ellipse @13 21600 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @14"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 10800 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@12,@15,@0,@16";
            type.ShapePath.ConnectionSites = "21600,0;0,10800;21600,21600;@0,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateNoSmoking()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t57",
                Spt = 57f,
                Path = "m,10800qy10800,,21600,10800,10800,21600,,10800xar@0@0@16@16@12@14@15@13xar@0@0@16@16@13@15@14@12xe"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 @2 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 @0 1"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 1 8"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @6"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 1 8"));
            type.Formulas.Add(new VmlSingleFormula("sqrt @8"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 @9 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateNotchedRightArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t94",
                Spt = 94f,
                Path = "m@0,l@0@1,0@1@5,10800,0@2@0@2@0,21600,21600,10800xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 @3 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@5,@1,@6,@2";
            type.ShapePath.ConnectionSites = "@0,0;@5,10800;@0,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateOctagon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t10",
                Spt = 10f,
                Path = "m@0,l0@0,0@2@0,21600@1,21600,21600@2,21600@0@1,xe"
            };
            type.AdjustValues[0] = 0x18b6;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,0,21600,21600;2700,2700,18900,18900;5400,5400,16200,16200";
            type.ShapePath.ConnectionSites = "@8,0;0,@9;@8,@7;@6,@9";
            return type;
        }

        private static VmlShapeType GenerateParallelogram()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t7",
                Spt = 7f,
                Path = "m@0,l,21600@1,21600,21600,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 width"));
            type.Formulas.Add(new VmlSingleFormula("mid @1 0"));
            type.Formulas.Add(new VmlSingleFormula("prod height width #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("if @10 @8 0"));
            type.Formulas.Add(new VmlSingleFormula("if @10 @7 height"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "1800,1800,19800,19800;8100,8100,13500,13500;10800,10800,10800,10800";
            type.ShapePath.ConnectionSites = "@4,0;10800,@11;@3,10800;@5,21600;10800,@12;@2,10800";
            return type;
        }

        private static VmlShapeType GeneratePentagon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t56",
                Spt = 56f,
                Path = "m10800,l,8259,4200,21600r13200,l21600,8259xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "4200,5077,17400,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,8259;4200,21600;10800,21600;17400,21600;21600,8259";
            type.ShapePath.ConnectionAngles = "270,180,90,90,90,0";
            return type;
        }

        private static VmlShapeType GeneratePictureFrame()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t75",
                Spt = 75f,
                Path = "m@4@5l@4@11@9@11@9@5xe",
                Filled = false,
                Stroked = false,
                PreferRelative = true,
                Formulas = new VmlSingleFormulasCollection()
            };
            type.Formulas.Add(new VmlSingleFormula("if lineDrawn pixelLineWidth 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 1 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("prod @2 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 21600 pixelWidth"));
            type.Formulas.Add(new VmlSingleFormula("prod @3 21600 pixelHeight"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 0 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @7 21600 pixelWidth"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @7 21600 pixelHeight"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 21600 0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapeProtections = new VmlShapeProtections();
            type.ShapeProtections.AspectRatio = true;
            type.Stroke = new VmlLineStrokeSettings(FakeDocumentModel.Instance);
            type.Stroke.JoinStyle = VmlStrokeJoinStyle.Miter;
            return type;
        }

        private static VmlShapeType GeneratePlaque()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t21",
                Spt = 21f,
                Path = "m@0,qy0@0l0@2qx@0,21600l@1,21600qy21600@2l21600@0qx@1,xe"
            };
            type.AdjustValues[0] = 0xe10;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 7071 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@3,@3,@4,@5";
            type.ShapePath.ConnectionSites = "@8,0;0,@9;@8,@7;@6,@9";
            return type;
        }

        private static VmlShapeType GeneratePlus()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t11",
                Spt = 11f,
                Path = "m@0,l@0@0,0@0,0@2@0@2@0,21600@1,21600@1@2,21600@2,21600@0@1@0@1,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2929 10000"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,0,21600,21600;5400,5400,16200,16200;10800,10800,10800,10800";
            type.ShapePath.ConnectionSites = "@8,0;0,@9;@8,@7;@6,@9";
            return type;
        }

        private static VmlShapeType GenerateQuadArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t76",
                Spt = 76f,
                Path = "m10800,l@0@2@1@2@1@1@2@1@2@0,,10800@2@3@2@4@1@4@1@5@0@5,10800,21600@3@5@4@5@4@4@5@4@5@3,21600,10800@5@0@5@1@4@1@4@2@3@2xe"
            };
            type.AdjustValues[0] = 0x1950;
            type.AdjustValues[1] = 0x21c0;
            type.AdjustValues[2] = 0x10e0;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("prod @7 #2 @6"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @8"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.TextboxRect = "@8,@1,@9,@4;@1,@8,@4,@9";
            return type;
        }

        private static VmlShapeType GenerateQuadArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t83",
                Spt = 83f,
                Path = "m@0@0l@3@0@3@2@1@2,10800,0@4@2@5@2@5@0@8@0@8@3@9@3@9@1,21600,10800@9@4@9@5@8@5@8@8@5@8@5@9@4@9,10800,21600@1@9@3@9@3@8@0@8@0@5@2@5@2@4,,10800@2@1@2@3@0@3xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1fa4;
            type.AdjustValues[2] = 0xa8c;
            type.AdjustValues[3] = 0x24ea;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.TextboxRect = "@0,@0,@8,@8";
            return type;
        }

        private static VmlShapeType GenerateRibbon()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t53",
                Spt = 53f,
                Path = "m,l@3,qx@4@11l@4@10@5@10@5@11qy@6,l@21,0@19@15@21@16@9@16@9@17qy@8@22l@1@22qx@0@17l@0@16,0@16,2700@15xem@4@11nfqy@3@12l@1@12qx@0@13@1@10l@4@10em@5@11nfqy@6@12l@8@12qx@9@13@8@10l@5@10em@0@13nfl@0@16em@9@13nfl@9@16e"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 1 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @11 2 1"));
            type.Formulas.Add(new VmlSingleFormula("prod @11 3 1"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum @14 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 2700"));
            type.Formulas.Add(new VmlSingleFormula("sum @18 0 2700"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,@10,@9,21600";
            type.ShapePath.ConnectionSites = "@18,@10;2700,@15;@18,21600;@19,@15";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateRibbon2()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t54",
                Spt = 54f,
                Path = "m0@29l@3@29qx@4@19l@4@10@5@10@5@19qy@6@29l@28@29@26@22@28@23@9@23@9@24qy@8,l@1,qx@0@24l@0@23,0@23,2700@22xem@4@19nfqy@3@20l@1@20qx@0@21@1@10l@4@10em@5@19nfqy@6@20l@8@20qx@9@21@8@10l@5@10em@0@21nfl@0@23em@9@21nfl@9@23e"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x49d4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 675 0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 1 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @10 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod height 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 4"));
            type.Formulas.Add(new VmlSingleFormula("prod height 3 2"));
            type.Formulas.Add(new VmlSingleFormula("prod height 2 3"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @12 @15 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @13 @16 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @17 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @19"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 2700"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 0 2700"));
            type.Formulas.Add(new VmlSingleFormula("val width"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@0,0,@9,@10";
            type.ShapePath.ConnectionSites = "@25,0;2700,@22;@25,@10;@26,@22";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateRightArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t78",
                Spt = 78f,
                Path = "m,l,21600@0,21600@0@5@2@5@2@4,21600,10800@2@1@2@3@0@3@0,xe"
            };
            type.AdjustValues[0] = 0x3840;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0x4650;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,0,@0,21600";
            type.ShapePath.ConnectionSites = "@6,0;0,10800;@6,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateRightBrace()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t88",
                Spt = 88f,
                Path = "m,qx10800@0l10800@2qy21600@11,10800@3l10800@1qy,21600e"
            };
            type.AdjustValues[0] = 0x708;
            type.AdjustValues[1] = 0x2a30;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 #0 0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("min #1 @6"));
            type.Formulas.Add(new VmlSingleFormula("prod @7 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,@4,7637,@5";
            type.ShapePath.ConnectionSites = "0,0;21600,@11;0,21600";
            return type;
        }

        private static VmlShapeType GenerateRightBracket()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t86",
                Spt = 86f,
                Path = "m,qx21600@0l21600@1qy,21600e"
            };
            type.AdjustValues[0] = 0x708;
            type.Filled = false;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 9598 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "0,@2,15274,@3";
            type.ShapePath.ConnectionSites = "0,0;0,21600;21600,10800";
            return type;
        }

        private static VmlShapeType GenerateRightTriangle()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t6",
                Spt = 6f,
                Path = "m,l,21600r21600,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "1800,12600,12600,19800";
            type.ShapePath.ConnectionSites = "0,0;0,10800;0,21600;10800,21600;21600,21600;10800,10800";
            return type;
        }

        private static VmlShapeType GenerateSeal16()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t59",
                Spt = 59f,
                Path = "m21600,10800l@5@10,20777,6667@7@12,18436,3163@8@11,14932,822@6@9,10800,0@10@9,6667,822@12@11,3163,3163@11@12,822,6667@9@10,,10800@9@6,822,14932@11@8,3163,18436@12@7,6667,20777@10@5,10800,21600@6@5,14932,20777@8@7,18436,18436@7@8,20777,14932@5@6xe"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 32138 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 6393 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 27246 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 18205 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @13 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @13"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@15,@15,@14,@14";
            return type;
        }

        private static VmlShapeType GenerateSeal24()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t92",
                Spt = 92f,
                Path = "m21600,10800l@7@14,21232,8005@9@16,20153,5400@11@18,18437,3163@12@17,16200,1447@10@15,13595,368@8@13,10800,0@14@13,8005,368@16@15,5400,1447@18@17,3163,3163@17@18,1447,5400@15@16,368,8005@13@14,,10800@13@8,368,13595@15@10,1447,16200@17@12,3163,18437@18@11,5400,20153@16@9,8005,21232@14@7,10800,21600@8@7,13595,21232@10@9,16200,20153@12@11,18437,18437@11@12,20153,16200@9@10,21232,13595@7@8xe"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 32488 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 4277 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 30274 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 12540 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 25997 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 19948 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @5"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @6"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @19"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@21,@21,@20,@20";
            return type;
        }

        private static VmlShapeType GenerateSeal32()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t60",
                Spt = 60f,
                Path = "m21600,10800l@9@18,21392,8693@11@20,20777,6667@13@22,19780,4800@15@24,18436,3163@16@23,16800,1820@14@21,14932,822@12@19,12907,208@10@17,10800,0@18@17,8693,208@20@19,6667,822@22@21,4800,1820@24@23,3163,3163@23@24,1820,4800@21@22,822,6667@19@20,208,8693@17@18,,10800@17@10,208,12907@19@12,822,14932@21@14,1820,16800@23@16,3163,18436@24@15,4800,19780@22@13,6667,20777@20@11,8693,21392@18@9,10800,21600@10@9,12907,21392@12@11,14932,20777@14@13,16800,19780@16@15,18436,18436@15@16,19780,16800@13@14,20777,14932@11@12,21392,12907@9@10xe"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 32610 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 3212 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 31357 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 9512 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 28899 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 15447 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 25330 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 20788 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @3 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @5 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @6 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @8 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @5"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @6"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @8"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @25 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @25"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@27,@27,@26,@26";
            return type;
        }

        private static VmlShapeType GenerateSeal4()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t187",
                Spt = 187f,
                Path = "m21600,10800l@2@3,10800,0@3@3,,10800@3@2,10800,21600@2@2xe"
            };
            type.AdjustValues[0] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@3,@3,@2,@2";
            return type;
        }

        private static VmlShapeType GenerateSeal8()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t58",
                Spt = 58f,
                Path = "m21600,10800l@3@6,18436,3163@4@5,10800,0@6@5,3163,3163@5@6,,10800@5@4,3163,18436@6@3,10800,21600@4@3,18436,18436@3@4xe"
            };
            type.AdjustValues[0] = 0x9ea;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 30274 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 12540 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @7"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "@9,@9,@8,@8";
            return type;
        }

        private static VmlShapeType GenerateSmileyFace()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t96",
                Spt = 96f,
                Path = "m10800,qx,10800,10800,21600,21600,10800,10800,xem7340,6445qx6215,7570,7340,8695,8465,7570,7340,6445xnfem14260,6445qx13135,7570,14260,8695,15385,7570,14260,6445xnfem4960@0c8853@3,12747@3,16640@0nfe"
            };
            type.AdjustValues[0] = 0x4470;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 33030 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 4 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 1 3"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 0 @2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163";
            return type;
        }

        private static VmlShapeType GenerateStar()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t12",
                Spt = 12f,
                Path = "m10800,l8280,8259,,8259r6720,5146l4200,21600r6600,-5019l17400,21600,14880,13405,21600,8259r-8280,xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "6720,8259,14880,15628";
            type.ShapePath.ConnectionSites = "10800,0;0,8259;4200,21600;17400,21600;21600,8259";
            return type;
        }

        private static VmlShapeType GenerateStraightConnector1()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t32",
                Spt = 32f,
                Path = "m,l21600,21600e",
                Filled = false,
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.None;
            type.ShapePath.FillOk = false;
            return type;
        }

        private static VmlShapeType GenerateStripedRightArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t93",
                Spt = 93f,
                Path = "m@0,l@0@1,3375@1,3375@2@0@2@0,21600,21600,10800xem1350@1l1350@2,2700@2,2700@1xem0@1l0@2,675@2,675@1xe"
            };
            type.AdjustValues[0] = 0x3f48;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @4 @3 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "3375,@1,@6,@2";
            type.ShapePath.ConnectionSites = "@0,0;0,10800;@0,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateSun()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t183",
                Spt = 183f,
                Path = "m21600,10800l@15@14@15@18xem18436,3163l@17@12@16@13xem10800,l@14@10@18@10xem3163,3163l@12@13@13@12xem,10800l@10@18@10@14xem3163,18436l@13@16@12@17xem10800,21600l@18@15@14@15xem18436,18436l@16@17@17@16xem10800@19qx@19,10800,10800@20@20,10800,10800@19xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 30274 32768"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 12540 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @1 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @2 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23170 32768"));
            type.Formulas.Add(new VmlSingleFormula("sum @7 10800 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("prod @5 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 3 4"));
            type.Formulas.Add(new VmlSingleFormula("sum @10 791 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 791 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @11 2700 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @13"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @14"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            type.ShapePath.TextboxRect = "@9,@9,@8,@8";
            return type;
        }

        private static VmlShapeType GenerateTrapezoid()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t8",
                Spt = 8f,
                Path = "m,l@0,21600@1,21600,21600,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("mid #0 width"));
            type.Formulas.Add(new VmlSingleFormula("mid @1 0"));
            type.Formulas.Add(new VmlSingleFormula("prod height width #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("if @10 @8 0"));
            type.Formulas.Add(new VmlSingleFormula("if @10 @7 height"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.TextboxRect = "1800,1800,19800,19800;4500,4500,17100,17100;7200,7200,14400,14400";
            type.ShapePath.ConnectionSites = "@3,10800;10800,21600;@2,10800;10800,0";
            return type;
        }

        private static VmlShapeType GenerateUpArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t68",
                Spt = 68f,
                Path = "m0@0l@1@0@1,21600@2,21600@2@0,21600@0,10800,xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1518;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod #0 #1 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 @3"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@1,@4,@2,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,@0;10800,21600;21600,@0";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateUpArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t79",
                Spt = 79f,
                Path = "m0@0l@3@0@3@2@1@2,10800,0@4@2@5@2@5@0,21600@0,21600,21600,,21600xe"
            };
            type.AdjustValues[0] = 0x1c20;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0xe10;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,@0,21600,21600";
            type.ShapePath.ConnectionSites = "10800,0;0,@7;10800,21600;21600,@7";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateUpDownArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t70",
                Spt = 70f,
                Path = "m10800,l21600@0@3@0@3@2,21600@2,10800,21600,0@2@1@2@1@0,0@0xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x10e0;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 #0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 @4"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @5"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@1,@5,@3,@6";
            type.ShapePath.ConnectionSites = "10800,0;0,@0;@1,10800;0,@2;10800,21600;21600,@2;@3,10800;21600,@0";
            type.ShapePath.ConnectionAngles = "270,180,180,180,90,0,0,0";
            return type;
        }

        private static VmlShapeType GenerateUpDownArrowCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t82",
                Spt = 82f,
                Path = "m0@0l@3@0@3@2@1@2,10800,0@4@2@5@2@5@0,21600@0,21600@8@5@8@5@9@4@9,10800,21600@1@9@3@9@3@8,0@8xe"
            };
            type.AdjustValues[0] = 0x1518;
            type.AdjustValues[1] = 0x1518;
            type.AdjustValues[2] = 0xa8c;
            type.AdjustValues[3] = 0x1fa4;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("val #2"));
            type.Formulas.Add(new VmlSingleFormula("val #3"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #3"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 21600 0"));
            type.Formulas.Add(new VmlSingleFormula("prod @6 1 2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,@0,21600,@8";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;21600,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateUturnArrow()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t101",
                Spt = 101f,
                Path = "m15662,14285l21600,8310r-2970,qy9250,,,8485l,21600r6110,l6110,8310qy8907,5842l9725,5842qx12520,8310l9725,8310xe",
                ShapePath = new VmlShapePath()
            };
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "0,8310,6110,21600";
            type.ShapePath.ConnectionSites = "9250,0;3055,21600;9725,8310;15662,14285;21600,8310";
            type.ShapePath.ConnectionAngles = "270,90,90,90,0";
            return type;
        }

        private static VmlShapeType GenerateVerticalScroll()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t97",
                Spt = 97f,
                Path = "m@5,qx@1@2l@1@0@2@0qx0@7@2,21600l@9,21600qx@10@7l@10@1@11@1qx21600@2@11,xem@5,nfqx@6@2@5@1@4@3@5@2l@6@2em@5@1nfl@10@1em@2,21600nfqx@1@7l@1@0em@2@0nfqx@3@8@2@7l@1@7e"
            };
            type.AdjustValues[0] = 0xa8c;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum height 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 3 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 5 4"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 3 2"));
            type.Formulas.Add(new VmlSingleFormula("prod @1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum height 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @5"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum width 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("val height"));
            type.Formulas.Add(new VmlSingleFormula("prod height 1 2"));
            type.Formulas.Add(new VmlSingleFormula("prod width 1 2"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ExtrusionOk = false;
            type.ShapePath.TextboxRect = "@1,@1,@10,@7";
            type.ShapePath.ConnectionSites = "@14,0;@1,@13;@14,@12;@10,@13";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateWave()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t64",
                Spt = 64f,
                Path = "m@28@0c@27@1@26@3@25@0l@21@4c@22@5@23@6@24@4xe"
            };
            type.AdjustValues[0] = 0xaf9;
            type.AdjustValues[1] = 0x2a30;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 41 9"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 23 9"));
            type.Formulas.Add(new VmlSingleFormula("sum 0 0 @2"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @3"));
            type.Formulas.Add(new VmlSingleFormula("sum #1 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 2 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 4 3"));
            type.Formulas.Add(new VmlSingleFormula("prod @8 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 4 3"));
            type.Formulas.Add(new VmlSingleFormula("prod #1 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @15"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @16"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @17"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @14 0"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @13 @15"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @12 @16"));
            type.Formulas.Add(new VmlSingleFormula("if @7 21600 @17"));
            type.Formulas.Add(new VmlSingleFormula("if @7 0 @20"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @9 @19"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @10 @18"));
            type.Formulas.Add(new VmlSingleFormula("if @7 @11 21600"));
            type.Formulas.Add(new VmlSingleFormula("sum @24 0 @21"));
            type.Formulas.Add(new VmlSingleFormula("sum @4 0 @0"));
            type.Formulas.Add(new VmlSingleFormula("max @21 @25"));
            type.Formulas.Add(new VmlSingleFormula("min @24 @28"));
            type.Formulas.Add(new VmlSingleFormula("prod @0 2 1"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 @33"));
            type.Formulas.Add(new VmlSingleFormula("mid @26 @27"));
            type.Formulas.Add(new VmlSingleFormula("mid @24 @28"));
            type.Formulas.Add(new VmlSingleFormula("mid @22 @23"));
            type.Formulas.Add(new VmlSingleFormula("mid @21 @25"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "@31,@33,@32,@34";
            type.ShapePath.ConnectionSites = "@35,@0;@38,10800;@37,@4;@36,10800";
            type.ShapePath.ConnectionAngles = "270,180,90,0";
            return type;
        }

        private static VmlShapeType GenerateWedgeEllipseCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t63",
                Spt = 63f,
                Path = "wr,,21600,21600@15@16@17@18l@21@22xe"
            };
            type.AdjustValues[0] = 0x546;
            type.AdjustValues[1] = 0x6540;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("atan2 @2 @3"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @4 11 0"));
            type.Formulas.Add(new VmlSingleFormula("sumangle @4 0 11"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 @4"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 @4"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 @5"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 @5"));
            type.Formulas.Add(new VmlSingleFormula("cos 10800 @6"));
            type.Formulas.Add(new VmlSingleFormula("sin 10800 @6"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @7"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @8"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @9"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @10"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 @12"));
            type.Formulas.Add(new VmlSingleFormula("mod @2 @3 0"));
            type.Formulas.Add(new VmlSingleFormula("sum @19 0 10800"));
            type.Formulas.Add(new VmlSingleFormula("if @20 #0 @13"));
            type.Formulas.Add(new VmlSingleFormula("if @20 #1 @14"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "3163,3163,18437,18437";
            type.ShapePath.ConnectionSites = "10800,0;3163,3163;0,10800;3163,18437;10800,21600;18437,18437;21600,10800;18437,3163;@21,@22";
            return type;
        }

        private static VmlShapeType GenerateWedgeRectCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t61",
                Spt = 61f,
                Path = "m,l0@8@12@24,0@9,,21600@6,21600@15@27@7,21600,21600,21600,21600@9@18@30,21600@8,21600,0@7,0@21@33@6,xe"
            };
            type.AdjustValues[0] = 0x546;
            type.AdjustValues[1] = 0x6540;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @1 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @0 3600 12600"));
            type.Formulas.Add(new VmlSingleFormula("if @0 9000 18000"));
            type.Formulas.Add(new VmlSingleFormula("if @1 3600 12600"));
            type.Formulas.Add(new VmlSingleFormula("if @1 9000 18000"));
            type.Formulas.Add(new VmlSingleFormula("if @2 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("if #0 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @6 #0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @6 @13"));
            type.Formulas.Add(new VmlSingleFormula("if @5 @6 @14"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #0 21600"));
            type.Formulas.Add(new VmlSingleFormula("if @3 21600 @16"));
            type.Formulas.Add(new VmlSingleFormula("if @4 21600 @17"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #0 @6"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @19 @6"));
            type.Formulas.Add(new VmlSingleFormula("if #1 @6 @20"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @8 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @22 @8"));
            type.Formulas.Add(new VmlSingleFormula("if #0 @8 @23"));
            type.Formulas.Add(new VmlSingleFormula("if @2 21600 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @3 21600 @25"));
            type.Formulas.Add(new VmlSingleFormula("if @5 21600 @26"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #1 @8"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @8 @28"));
            type.Formulas.Add(new VmlSingleFormula("if @4 @8 @29"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @31 0"));
            type.Formulas.Add(new VmlSingleFormula("if #1 0 @32"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;21600,10800;@34,@35";
            return type;
        }

        private static VmlShapeType GenerateWedgeRRectCallout()
        {
            VmlShapeType type = new VmlShapeType(FakeDocumentModel.Instance) {
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Id = "_x0000_t62",
                Spt = 62f,
                Path = "m3600,qx,3600l0@8@12@24,0@9,,18000qy3600,21600l@6,21600@15@27@7,21600,18000,21600qx21600,18000l21600@9@18@30,21600@8,21600,3600qy18000,l@7,0@21@33@6,xe"
            };
            type.AdjustValues[0] = 0x546;
            type.AdjustValues[1] = 0x6540;
            type.Formulas = new VmlSingleFormulasCollection();
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 10800 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum #0 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("sum @0 @1 0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("sum 21600 0 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @0 3600 12600"));
            type.Formulas.Add(new VmlSingleFormula("if @0 9000 18000"));
            type.Formulas.Add(new VmlSingleFormula("if @1 3600 12600"));
            type.Formulas.Add(new VmlSingleFormula("if @1 9000 18000"));
            type.Formulas.Add(new VmlSingleFormula("if @2 0 #0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @10 0"));
            type.Formulas.Add(new VmlSingleFormula("if #0 0 @11"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @6 #0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @6 @13"));
            type.Formulas.Add(new VmlSingleFormula("if @5 @6 @14"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #0 21600"));
            type.Formulas.Add(new VmlSingleFormula("if @3 21600 @16"));
            type.Formulas.Add(new VmlSingleFormula("if @4 21600 @17"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #0 @6"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @19 @6"));
            type.Formulas.Add(new VmlSingleFormula("if #1 @6 @20"));
            type.Formulas.Add(new VmlSingleFormula("if @2 @8 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @22 @8"));
            type.Formulas.Add(new VmlSingleFormula("if #0 @8 @23"));
            type.Formulas.Add(new VmlSingleFormula("if @2 21600 #1"));
            type.Formulas.Add(new VmlSingleFormula("if @3 21600 @25"));
            type.Formulas.Add(new VmlSingleFormula("if @5 21600 @26"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #1 @8"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @8 @28"));
            type.Formulas.Add(new VmlSingleFormula("if @4 @8 @29"));
            type.Formulas.Add(new VmlSingleFormula("if @2 #1 0"));
            type.Formulas.Add(new VmlSingleFormula("if @3 @31 0"));
            type.Formulas.Add(new VmlSingleFormula("if #1 0 @32"));
            type.Formulas.Add(new VmlSingleFormula("val #0"));
            type.Formulas.Add(new VmlSingleFormula("val #1"));
            type.ShapePath = new VmlShapePath();
            type.ShapePath.ConnectType = VmlConnectType.Custom;
            type.ShapePath.TextboxRect = "791,791,20809,20809";
            type.ShapePath.ConnectionSites = "10800,0;0,10800;10800,21600;21600,10800;@34,@35";
            return type;
        }

        public static VmlShapeType GetVmlShapeType(string type)
        {
            Dictionary<string, VmlShapeType> generatedVmlShapeTypes = VmlShapeTypePresets.generatedVmlShapeTypes;
            lock (generatedVmlShapeTypes)
            {
                return GetVmlShapeTypeCore(type);
            }
        }

        private static VmlShapeType GetVmlShapeTypeCore(string type)
        {
            VmlShapeType type2;
            Func<VmlShapeType> func;
            if (generatedVmlShapeTypes.TryGetValue(type, out type2))
            {
                return type2;
            }
            if (!vmlShapeTypeGenerators.TryGetValue(type, out func))
            {
                return null;
            }
            VmlShapeType type3 = func();
            generatedVmlShapeTypes.Add(type, type3);
            return type3;
        }

        private static Dictionary<string, Func<VmlShapeType>> InitializeGenerators() => 
            new Dictionary<string, Func<VmlShapeType>> { 
                { 
                    "_x0000_t4",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateDiamond)
                },
                { 
                    "_x0000_t5",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateIsocelesTriangle)
                },
                { 
                    "_x0000_t6",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRightTriangle)
                },
                { 
                    "_x0000_t7",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateParallelogram)
                },
                { 
                    "_x0000_t8",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateTrapezoid)
                },
                { 
                    "_x0000_t9",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateHexagon)
                },
                { 
                    "_x0000_t10",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateOctagon)
                },
                { 
                    "_x0000_t11",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GeneratePlus)
                },
                { 
                    "_x0000_t12",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateStar)
                },
                { 
                    "_x0000_t13",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateArrow)
                },
                { 
                    "_x0000_t15",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateHomePlate)
                },
                { 
                    "_x0000_t16",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCube)
                },
                { 
                    "_x0000_t17",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBalloon)
                },
                { 
                    "_x0000_t19",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateArc)
                },
                { 
                    "_x0000_t21",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GeneratePlaque)
                },
                { 
                    "_x0000_t22",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCan)
                },
                { 
                    "_x0000_t23",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateDonut)
                },
                { 
                    "_x0000_t32",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateStraightConnector1)
                },
                { 
                    "_x0000_t33",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentConnector2)
                },
                { 
                    "_x0000_t34",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentConnector3)
                },
                { 
                    "_x0000_t35",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentConnector4)
                },
                { 
                    "_x0000_t36",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentConnector5)
                },
                { 
                    "_x0000_t37",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedConnector2)
                },
                { 
                    "_x0000_t38",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedConnector3)
                },
                { 
                    "_x0000_t39",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedConnector4)
                },
                { 
                    "_x0000_t40",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedConnector5)
                },
                { 
                    "_x0000_t41",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCallout1)
                },
                { 
                    "_x0000_t42",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCallout2)
                },
                { 
                    "_x0000_t43",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCallout3)
                },
                { 
                    "_x0000_t44",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentCallout1)
                },
                { 
                    "_x0000_t45",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentCallout2)
                },
                { 
                    "_x0000_t46",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentCallout3)
                },
                { 
                    "_x0000_t47",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBorderCallout1)
                },
                { 
                    "_x0000_t48",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBorderCallout2)
                },
                { 
                    "_x0000_t49",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBorderCallout3)
                },
                { 
                    "_x0000_t50",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentBorderCallout1)
                },
                { 
                    "_x0000_t51",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentBorderCallout2)
                },
                { 
                    "_x0000_t52",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentBorderCallout3)
                },
                { 
                    "_x0000_t53",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRibbon)
                },
                { 
                    "_x0000_t54",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRibbon2)
                },
                { 
                    "_x0000_t55",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateChevron)
                },
                { 
                    "_x0000_t56",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GeneratePentagon)
                },
                { 
                    "_x0000_t57",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateNoSmoking)
                },
                { 
                    "_x0000_t58",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSeal8)
                },
                { 
                    "_x0000_t59",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSeal16)
                },
                { 
                    "_x0000_t60",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSeal32)
                },
                { 
                    "_x0000_t61",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateWedgeRectCallout)
                },
                { 
                    "_x0000_t62",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateWedgeRRectCallout)
                },
                { 
                    "_x0000_t63",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateWedgeEllipseCallout)
                },
                { 
                    "_x0000_t64",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateWave)
                },
                { 
                    "_x0000_t65",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFoldedCorner)
                },
                { 
                    "_x0000_t66",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftArrow)
                },
                { 
                    "_x0000_t67",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateDownArrow)
                },
                { 
                    "_x0000_t68",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateUpArrow)
                },
                { 
                    "_x0000_t69",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftRightArrow)
                },
                { 
                    "_x0000_t70",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateUpDownArrow)
                },
                { 
                    "_x0000_t71",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateIrregularSeal1)
                },
                { 
                    "_x0000_t72",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateIrregularSeal2)
                },
                { 
                    "_x0000_t73",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLightningBolt)
                },
                { 
                    "_x0000_t74",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateHeart)
                },
                { 
                    "_x0000_t75",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GeneratePictureFrame)
                },
                { 
                    "_x0000_t76",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateQuadArrow)
                },
                { 
                    "_x0000_t77",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftArrowCallout)
                },
                { 
                    "_x0000_t78",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRightArrowCallout)
                },
                { 
                    "_x0000_t79",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateUpArrowCallout)
                },
                { 
                    "_x0000_t80",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateDownArrowCallout)
                },
                { 
                    "_x0000_t81",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftRightArrowCallout)
                },
                { 
                    "_x0000_t82",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateUpDownArrowCallout)
                },
                { 
                    "_x0000_t83",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateQuadArrowCallout)
                },
                { 
                    "_x0000_t84",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBevel)
                },
                { 
                    "_x0000_t85",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftBracket)
                },
                { 
                    "_x0000_t86",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRightBracket)
                },
                { 
                    "_x0000_t87",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftBrace)
                },
                { 
                    "_x0000_t88",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateRightBrace)
                },
                { 
                    "_x0000_t89",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftUpArrow)
                },
                { 
                    "_x0000_t90",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentUpArrow)
                },
                { 
                    "_x0000_t91",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBentArrow)
                },
                { 
                    "_x0000_t92",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSeal24)
                },
                { 
                    "_x0000_t93",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateStripedRightArrow)
                },
                { 
                    "_x0000_t94",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateNotchedRightArrow)
                },
                { 
                    "_x0000_t95",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBlockArc)
                },
                { 
                    "_x0000_t96",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSmileyFace)
                },
                { 
                    "_x0000_t97",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateVerticalScroll)
                },
                { 
                    "_x0000_t98",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateHorizontalScroll)
                },
                { 
                    "_x0000_t99",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCircularArrow)
                },
                { 
                    "_x0000_t101",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateUturnArrow)
                },
                { 
                    "_x0000_t102",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedRightArrow)
                },
                { 
                    "_x0000_t103",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedLeftArrow)
                },
                { 
                    "_x0000_t104",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedUpArrow)
                },
                { 
                    "_x0000_t105",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCurvedDownArrow)
                },
                { 
                    "_x0000_t106",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCloudCallout)
                },
                { 
                    "_x0000_t107",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateEllipseRibbon)
                },
                { 
                    "_x0000_t108",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateEllipseRibbon2)
                },
                { 
                    "_x0000_t109",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartProcess)
                },
                { 
                    "_x0000_t110",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartDecision)
                },
                { 
                    "_x0000_t111",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartInputOutput)
                },
                { 
                    "_x0000_t112",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartPredefinedProcess)
                },
                { 
                    "_x0000_t113",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartInternalStorage)
                },
                { 
                    "_x0000_t114",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartDocument)
                },
                { 
                    "_x0000_t115",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartMultidocument)
                },
                { 
                    "_x0000_t116",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartTerminator)
                },
                { 
                    "_x0000_t117",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartPreparation)
                },
                { 
                    "_x0000_t118",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartManualInput)
                },
                { 
                    "_x0000_t119",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartManualOperation)
                },
                { 
                    "_x0000_t120",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartConnector)
                },
                { 
                    "_x0000_t121",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartPunchedCard)
                },
                { 
                    "_x0000_t122",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartPunchedTape)
                },
                { 
                    "_x0000_t123",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartSummingJunction)
                },
                { 
                    "_x0000_t124",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartOr)
                },
                { 
                    "_x0000_t125",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartCollate)
                },
                { 
                    "_x0000_t126",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartSort)
                },
                { 
                    "_x0000_t127",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartExtract)
                },
                { 
                    "_x0000_t128",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartMerge)
                },
                { 
                    "_x0000_t130",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartOnlineStorage)
                },
                { 
                    "_x0000_t131",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartMagneticTape)
                },
                { 
                    "_x0000_t132",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartMagneticDisk)
                },
                { 
                    "_x0000_t133",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartMagneticDrum)
                },
                { 
                    "_x0000_t134",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartDisplay)
                },
                { 
                    "_x0000_t135",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartDelay)
                },
                { 
                    "_x0000_t176",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartAlternateProcess)
                },
                { 
                    "_x0000_t177",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateFlowChartOffpageConnector)
                },
                { 
                    "_x0000_t178",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateCallout90)
                },
                { 
                    "_x0000_t179",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentCallout90)
                },
                { 
                    "_x0000_t180",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBorderCallout90)
                },
                { 
                    "_x0000_t181",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateAccentBorderCallout90)
                },
                { 
                    "_x0000_t182",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateLeftRightUpArrow)
                },
                { 
                    "_x0000_t183",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSun)
                },
                { 
                    "_x0000_t184",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateMoon)
                },
                { 
                    "_x0000_t185",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBracketPair)
                },
                { 
                    "_x0000_t186",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateBracePair)
                },
                { 
                    "_x0000_t187",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateSeal4)
                },
                { 
                    "_x0000_t188",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateDoubleWave)
                },
                { 
                    "_x0000_t189",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonBlank)
                },
                { 
                    "_x0000_t190",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonHome)
                },
                { 
                    "_x0000_t191",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonHelp)
                },
                { 
                    "_x0000_t192",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonInformation)
                },
                { 
                    "_x0000_t193",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonForwardNext)
                },
                { 
                    "_x0000_t194",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonBackPrevious)
                },
                { 
                    "_x0000_t195",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonEnd)
                },
                { 
                    "_x0000_t196",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonBeginning)
                },
                { 
                    "_x0000_t197",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonReturn)
                },
                { 
                    "_x0000_t198",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonDocument)
                },
                { 
                    "_x0000_t199",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonSound)
                },
                { 
                    "_x0000_t200",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateActionButtonMovie)
                },
                { 
                    "_x0000_t201",
                    new Func<VmlShapeType>(VmlShapeTypePresets.GenerateHostControl)
                }
            };
    }
}

